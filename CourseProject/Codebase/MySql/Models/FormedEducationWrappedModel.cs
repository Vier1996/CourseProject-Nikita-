using CourseProject.Codebase.Context;

namespace CourseProject.Codebase.MySql.Models;

public class FormedEducationWrappedModel : DatabaseModel<FormedEducationModel>
{
    private ProjectDbContext _dbContext;
    
    public FormedEducationWrappedModel(ProjectDbContext dbContext, bool logging = true) : base(dbContext.FormedEducations, logging)
        => _dbContext = dbContext;
    
    protected override EFTransactionArgs<FormedEducationModel> TryAddRow()
    {
        FormedEducationModel formedEducationModel = new FormedEducationModel();
        
        Console.WriteLine("Введите название формы обучения...");
        formedEducationModel.FormName = Console.ReadLine();

        FormedEducationModel existingModel = Find(formedEducationModel);
        
        if (existingModel == default)
        {
            _container.Add(formedEducationModel);
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
        FormedEducationModel model = FindByIndex(modelIndex);
        
        if (model == default)
        {
            return new EFTransactionArgs<FormedEducationModel>(
                null,
                EFTransactionType.FAILURE,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT, 
                $"Елемента с данным индексом:[{modelIndex+1}] не существует!");
        }

        _container.Remove(model);
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
        FormedEducationModel model = FindByIndex(modelIndex);
        
        if (model == default)
        {
            return new EFTransactionArgs<FormedEducationModel>(
                null,
                EFTransactionType.FAILURE,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT, 
                $"Елемента с данным индексом:[{modelIndex+1}] не существует!");
        }

        Console.WriteLine("Введите название формы обучения...");
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
        int index = 0;
        string info = "";

        foreach (FormedEducationModel model in _container.OrderBy(fe => fe.Id))
        {
            info += $"Index: {++index} | FormName: {model.FormName}.\n";
        }

        if (index != 0) 
            Console.WriteLine(info);

        return new EFTransactionArgs<FormedEducationModel>(
            null,
            EFTransactionType.SUCCESSFUL,
            EFTransactionReason.NONE, 
            index != 0 ? $"Елементы выведены успешно!" : "Тут пусто :(");
    }
}