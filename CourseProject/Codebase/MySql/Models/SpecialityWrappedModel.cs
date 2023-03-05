using CourseProject.Codebase.Context;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Codebase.MySql.Models;

public class SpecialityWrappedModel : DatabaseModel<SpecialityModel>
{
    private ProjectDbContext _dbContext;
    
    public SpecialityWrappedModel(ProjectDbContext dbContext, bool logging = true) : base(logging)
        => _dbContext = dbContext;
    
    protected override EFTransactionArgs<SpecialityModel> TryAddRow()
    {
        SpecialityModel specialityModel = new SpecialityModel();
        
        Console.WriteLine("Введите название специализации...\n");
        specialityModel.SpecialityName = Console.ReadLine();
        
        Console.WriteLine("Введите название профиля...\n");
        specialityModel.Profile = Console.ReadLine();
        
        SpecialityModel existingModel = _dbContext.Specialities.FirstOrDefault(it => 
            it.SpecialityName == specialityModel.SpecialityName && 
            it.Profile == specialityModel.Profile);
        
        if (existingModel == default)
        {
            _dbContext.Specialities.Add(specialityModel);
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
        SpecialityModel model = default;

        _dbContext.Specialities.ForEachAsync(it =>
        {
            if (modelIndex == index) 
                model = it;

            index++;
        });

        if (model == default)
        {
            return new EFTransactionArgs<SpecialityModel>(
                null,
                EFTransactionType.FAILURE,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT, 
                $"Елемента с данным индексом:[{modelIndex+1}] не существует!");
        }

        _dbContext.Specialities.Remove(model);
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
        SpecialityModel model = default;

        _dbContext.Specialities.ForEachAsync(it =>
        {
            if (modelIndex == index) 
                model = it;

            index++;
        });

        if (model == default)
        {
            return new EFTransactionArgs<SpecialityModel>(
                null,
                EFTransactionType.FAILURE,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT, 
                $"Елемента с данным индексом:[{modelIndex+1}] не существует!");
        }

        Console.WriteLine("Введите название специализации...\n");
        model.SpecialityName = Console.ReadLine();
        
        Console.WriteLine("Введите название профиля...\n");
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
        string info = "";
        
        _dbContext.Specialities.ForEachAsync(el =>
        {
            info += $"SpecialityName: {el.SpecialityName} || Profile: {el.Profile}\n";
        });

        return new EFTransactionArgs<SpecialityModel>(
            null,
            EFTransactionType.SUCCESSFUL,
            EFTransactionReason.NONE, 
            $"Елементы выведены успешно!");
    }
}