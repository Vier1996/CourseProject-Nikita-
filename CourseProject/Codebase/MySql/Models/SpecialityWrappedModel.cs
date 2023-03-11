using CourseProject.Codebase.Context;

namespace CourseProject.Codebase.MySql.Models;

public class SpecialityWrappedModel : DatabaseModel<SpecialityModel>
{
    private ProjectDbContext _dbContext;
    
    public SpecialityWrappedModel(ProjectDbContext dbContext, bool logging = true) : base(dbContext.Specialities, logging) => 
        _dbContext = dbContext;

    protected override EFTransactionArgs<SpecialityModel> TryAddRow()
    {
        SpecialityModel specialityModel = new SpecialityModel();
        
        Console.WriteLine("Введите название специализации...");
        specialityModel.SpecialityName = Console.ReadLine();
        
        Console.WriteLine("Введите название профиля...");
        specialityModel.Profile = Console.ReadLine();

        SpecialityModel existingModel = Find(specialityModel);
        
        if (existingModel == default)
        {
            _container.Add(specialityModel);
            _dbContext.SaveChanges();

            return new EFTransactionArgs<SpecialityModel>(
                specialityModel,
                EFTransactionType.SUCCESSFUL,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT,
                "Добавлено!");
        }

        return new EFTransactionArgs<SpecialityModel>(
            existingModel,
            EFTransactionType.FAILURE,
            EFTransactionReason.CONTAINS_ENTITY_OF_THIS_ELEMENT,
            "Добавление не нуждается, данный елемент уже есть в таблице!");
    }
    
    protected override EFTransactionArgs<SpecialityModel> TryRemoveRow(int modelIndex)
    {
        int index = 0;
        SpecialityModel model = FindByIndex(modelIndex);

        if (model == default)
        {
            return new EFTransactionArgs<SpecialityModel>(
                null,
                EFTransactionType.FAILURE,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT, 
                $"Елемента с данным индексом:[{modelIndex+1}] не существует!");
        }

        _container.Remove(model);
        _dbContext.SaveChanges();

        return new EFTransactionArgs<SpecialityModel>(
            model,
            EFTransactionType.SUCCESSFUL,
            EFTransactionReason.CONTAINS_ENTITY_OF_THIS_ELEMENT,
            $"Елемент с индексом:[{modelIndex+1}] удален!");
    }
    
    protected override EFTransactionArgs<SpecialityModel> TryUpdateRow(int modelIndex)
    {
        int index = 0;
        SpecialityModel model = FindByIndex(modelIndex);

        if (model == default)
        {
            return new EFTransactionArgs<SpecialityModel>(
                null,
                EFTransactionType.FAILURE,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT, 
                $"Елемента с данным индексом:[{modelIndex+1}] не существует!");
        }

        Console.WriteLine("Введите название специализации...");
        model.SpecialityName = Console.ReadLine();
        
        Console.WriteLine("Введите название профиля...");
        model.Profile = Console.ReadLine();
        
        _dbContext.SaveChanges();

        return new EFTransactionArgs<SpecialityModel>(
            model,
            EFTransactionType.SUCCESSFUL,
            EFTransactionReason.CONTAINS_ENTITY_OF_THIS_ELEMENT,
            $"Елемент с индексом:[{modelIndex+1}] обновлен!");
    }
    
    protected override EFTransactionArgs<SpecialityModel> TryDisplayInfo()
    {
        int index = 0;
        string info = "";

        foreach (SpecialityModel model in _container.OrderBy(fe => fe.Id))
        {
            info += $"Index: {++index} | SpecialityName: {model.SpecialityName} || Profile: {model.Profile}.\n";
        }

        if (index != 0) 
            Console.WriteLine(info);

        return new EFTransactionArgs<SpecialityModel>(
            null,
            EFTransactionType.SUCCESSFUL,
            EFTransactionReason.NONE, 
            index != 0 ? $"Елементы выведены успешно!" : "Тут пусто :(");
    }
}