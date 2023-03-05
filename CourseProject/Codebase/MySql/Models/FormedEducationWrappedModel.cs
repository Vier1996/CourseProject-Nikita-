using CourseProject.Codebase.Context;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Codebase.MySql.Models;

public class FormedEducationWrappedModel : DatabaseModel<FormedEducationModel>
{
    private ProjectDbContext _dbContext;
    
    public FormedEducationWrappedModel(ProjectDbContext dbContext, bool logging = true) : base(logging)
        => _dbContext = dbContext;
    
    protected override EFTransactionArgs<FormedEducationModel> TryAddRow()
    {
        FormedEducationModel formedEducationModel = new FormedEducationModel();
        
        Console.WriteLine("Введите название формы обучения...\n");
        formedEducationModel.FormName = Console.ReadLine();

        FormedEducationModel existingModel = _dbContext.FormedEducations.FirstOrDefault(it => it.FormName == formedEducationModel.FormName);
        
        if (existingModel == default)
        {
            _dbContext.FormedEducations.Add(formedEducationModel);
            _dbContext.SaveChanges();

            return new EFTransactionArgs<FormedEducationModel>(
                formedEducationModel,
                EFTransactionType.SUCCESSFUL,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT,
                "Добавлено!");
        }

        return new EFTransactionArgs<FormedEducationModel>(
            existingModel,
            EFTransactionType.FAILURE,
            EFTransactionReason.CONTAINS_ENTITY_OF_THIS_ELEMENT,
            "Добавление не нуждается, данный елемент уже есть в таблице!");
    }
    protected override EFTransactionArgs<FormedEducationModel> TryRemoveRow(int modelIndex)
    {
        int index = 0;
        FormedEducationModel model = default;

        _dbContext.FormedEducations.ForEachAsync(it =>
        {
            if (modelIndex == index) 
                model = it;

            index++;
        });

        if (model == default)
        {
            return new EFTransactionArgs<FormedEducationModel>(
                null,
                EFTransactionType.FAILURE,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT, 
                $"Елемента с данным индексом:[{modelIndex+1}] не существует!");
        }

        _dbContext.FormedEducations.Remove(model);
        _dbContext.SaveChanges();

        return new EFTransactionArgs<FormedEducationModel>(
            model,
            EFTransactionType.SUCCESSFUL,
            EFTransactionReason.CONTAINS_ENTITY_OF_THIS_ELEMENT,
            $"Елемент с индексом:[{modelIndex+1}] удален!");
    }
    protected override EFTransactionArgs<FormedEducationModel> TryUpdateRow(int modelIndex)
    {
        int index = 0;
        FormedEducationModel model = default;

        _dbContext.FormedEducations.ForEachAsync(it =>
        {
            if (modelIndex == index) 
                model = it;

            index++;
        });

        if (model == default)
        {
            return new EFTransactionArgs<FormedEducationModel>(
                null,
                EFTransactionType.FAILURE,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT, 
                $"Елемента с данным индексом:[{modelIndex+1}] не существует!");
        }

        Console.WriteLine("Введите название формы обучения...\n");
        model.FormName = Console.ReadLine();
        
        _dbContext.SaveChanges();

        return new EFTransactionArgs<FormedEducationModel>(
            model,
            EFTransactionType.SUCCESSFUL,
            EFTransactionReason.CONTAINS_ENTITY_OF_THIS_ELEMENT,
            $"Елемент с индексом:[{modelIndex+1}] обновлен!");
    }
    protected override EFTransactionArgs<FormedEducationModel> TryDisplayInfo()
    {
        string info = "";

        _dbContext.FormedEducations.ForEachAsync(el =>
        {
            info += $"FormName: {el.FormName}\n";
        });

        return new EFTransactionArgs<FormedEducationModel>(
            null,
            EFTransactionType.SUCCESSFUL,
            EFTransactionReason.NONE, 
            $"Елементы выведены успешно!");
    }
}