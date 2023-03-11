using CourseProject.Codebase.Context;

namespace CourseProject.Codebase.MySql.Models;

public class QualificationWrappedModel : DatabaseModel<QualificationModel>
{
    private ProjectDbContext _dbContext;
    
    public QualificationWrappedModel(ProjectDbContext dbContext, bool logging = true) : base(dbContext.Qualifications, logging)
        => _dbContext = dbContext;
    
    protected override EFTransactionArgs<QualificationModel> TryAddRow()
    {
        QualificationModel qualificationModel = new QualificationModel();
        
        Console.WriteLine("Введите название квалификации...");
        qualificationModel.QualificationName = Console.ReadLine();

        QualificationModel existingModel = Find(qualificationModel);
        
        if (existingModel == default)
        {
            _container.Add(qualificationModel);
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
        QualificationModel model = FindByIndex(modelIndex);
        
        if (model == default)
        {
            return new EFTransactionArgs<QualificationModel>(
                null,
                EFTransactionType.FAILURE,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT, 
                $"Елемента с данным индексом:[{modelIndex+1}] не существует!");
        }

        _container.Remove(model);
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
        QualificationModel model = FindByIndex(modelIndex);

        if (model == default)
        {
            return new EFTransactionArgs<QualificationModel>(
                null,
                EFTransactionType.FAILURE,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT, 
                $"Елемента с данным индексом:[{modelIndex+1}] не существует!");
        }

        Console.WriteLine("Введите название квалификации...");
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
        int index = 0;
        string info = "";

        foreach (QualificationModel model in _container.OrderBy(fe => fe.Id))
        {
            info += $"Index: {++index} | QualificationName: {model.QualificationName}.\n";
        }

        if (index != 0) 
            Console.WriteLine(info);

        return new EFTransactionArgs<QualificationModel>(
            null,
            EFTransactionType.SUCCESSFUL,
            EFTransactionReason.NONE, 
            index != 0 ? $"Елементы выведены успешно!" : "Тут пусто :(");
    }
}