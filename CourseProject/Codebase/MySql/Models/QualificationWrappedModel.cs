using CourseProject.Codebase.Context;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Codebase.MySql.Models;

public class QualificationWrappedModel : DatabaseModel<QualificationModel>
{
    private ProjectDbContext _dbContext;
    
    public QualificationWrappedModel(ProjectDbContext dbContext, bool logging = true) : base(logging)
        => _dbContext = dbContext;
    protected override EFTransactionArgs<QualificationModel> TryAddRow()
    {
        QualificationModel qualificationModel = new QualificationModel();
        
        Console.WriteLine("Введите название квалификации...\n");
        qualificationModel.QualificationName = Console.ReadLine();
        
        QualificationModel existingModel = _dbContext.Qualifications.FirstOrDefault(it => it.QualificationName == qualificationModel.QualificationName);
        if (existingModel == default)
        {
            _dbContext.Qualifications.Add(qualificationModel);
            _dbContext.SaveChanges();
            
            return new EFTransactionArgs<QualificationModel>(
                qualificationModel,
                EFTransactionType.SUCCESSFUL,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT,
                "Добавлено!");
        }

        return new EFTransactionArgs<QualificationModel>(
            existingModel,
            EFTransactionType.FAILURE,
            EFTransactionReason.CONTAINS_ENTITY_OF_THIS_ELEMENT,
            "Добавление не нуждается, данный елемент уже есть в таблице!");
    }
    protected override EFTransactionArgs<QualificationModel> TryRemoveRow(int modelIndex)
    {
        int index = 0;
        QualificationModel model = default;

        _dbContext.Qualifications.ForEachAsync(it =>
        {
            if (modelIndex == index) 
                model = it;

            index++;
        });

        if (model == default)
        {
            return new EFTransactionArgs<QualificationModel>(
                null,
                EFTransactionType.FAILURE,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT, 
                $"Елемента с данным индексом:[{modelIndex+1}] не существует!");
        }

        _dbContext.Qualifications.Remove(model);
        _dbContext.SaveChanges();

        return new EFTransactionArgs<QualificationModel>(
            model,
            EFTransactionType.SUCCESSFUL,
            EFTransactionReason.CONTAINS_ENTITY_OF_THIS_ELEMENT,
            $"Елемент с индексом:[{modelIndex+1}] удален!");
    }
    protected override EFTransactionArgs<QualificationModel> TryUpdateRow(int modelIndex)
    {
        int index = 0;
        QualificationModel model = default;

        _dbContext.Qualifications.ForEachAsync(it =>
        {
            if (modelIndex == index) 
                model = it;

            index++;
        });

        if (model == default)
        {
            return new EFTransactionArgs<QualificationModel>(
                null,
                EFTransactionType.FAILURE,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT, 
                $"Елемента с данным индексом:[{modelIndex+1}] не существует!");
        }

        Console.WriteLine("Введите название квалификации...\n");
        model.QualificationName = Console.ReadLine();
        
        _dbContext.SaveChanges();

        return new EFTransactionArgs<QualificationModel>(
            model,
            EFTransactionType.SUCCESSFUL,
            EFTransactionReason.CONTAINS_ENTITY_OF_THIS_ELEMENT,
            $"Елемент с индексом:[{modelIndex+1}] обновлен!");
    }
    protected override EFTransactionArgs<QualificationModel> TryDisplayInfo()
    {
        string info = "";

        _dbContext.Qualifications.ForEachAsync(el =>
        {
            info += $"QualificationName: {el.QualificationName}\n";
        });

        return new EFTransactionArgs<QualificationModel>(
            null,
            EFTransactionType.SUCCESSFUL,
            EFTransactionReason.NONE, 
            $"Елементы выведены успешно!");
    }
}