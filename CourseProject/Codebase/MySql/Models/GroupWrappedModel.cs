using CourseProject.Codebase.Context;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Codebase.MySql.Models;

public class GroupWrappedModel : DatabaseModel<GroupModel>
{
    private ProjectDbContext _dbContext;

    private QualificationWrappedModel _qualificationWrappedModel;
    private SpecialityWrappedModel _specialityWrappedModel;
    private FormedEducationWrappedModel _formedEducationWrappedModel;
    
    public GroupWrappedModel(ProjectDbContext dbContext, QualificationWrappedModel qualificationWrappedModel, 
        SpecialityWrappedModel specialityWrappedModel, FormedEducationWrappedModel formedEducationWrappedModel, bool logging = true) : base(logging)
    {
        _dbContext = dbContext;
        _qualificationWrappedModel = qualificationWrappedModel;
        _specialityWrappedModel = specialityWrappedModel;
        _formedEducationWrappedModel = formedEducationWrappedModel;
    }

    protected override EFTransactionArgs<GroupModel> TryAddRow()
    {
        GroupModel groupModel = new GroupModel();
        
        Console.WriteLine("Введите название факультета...\n");
        groupModel.Faculty = Console.ReadLine();
        
        Console.WriteLine("Введите название группы...\n");
        groupModel.GroupName = Console.ReadLine();
        
        Console.WriteLine("Введите курс...\n");
        groupModel.Course = Convert.ToInt32(Console.ReadLine());
        
        Console.WriteLine("Введите кол-во студентов...\n");
        groupModel.CountOfStudents = Convert.ToInt32(Console.ReadLine());
        
        Console.WriteLine("Введите кол-во подгрупп...\n");
        groupModel.CountOfSubGroups = Convert.ToInt32(Console.ReadLine());
        
        groupModel.QualificationReference = _qualificationWrappedModel.AddRow();
        groupModel.FormedEducationReference = _formedEducationWrappedModel.AddRow();
        groupModel.SpecialityReference = _specialityWrappedModel.AddRow();

        GroupModel existingModel = _dbContext.Groups.FirstOrDefault(it => 
            it.Faculty == groupModel.Faculty &&
            it.GroupName == groupModel.GroupName &&
            it.Course == groupModel.Course &&
            it.CountOfStudents == groupModel.CountOfStudents &&
            it.CountOfSubGroups == groupModel.CountOfSubGroups &&
            it.QualificationReference == groupModel.QualificationReference &&
            it.FormedEducationReference == groupModel.FormedEducationReference &&
            it.SpecialityReference == groupModel.SpecialityReference
            );
        
        if (existingModel == default)
        {
            _dbContext.Groups.Add(groupModel);
            _dbContext.SaveChanges();

            return new EFTransactionArgs<GroupModel>(
                groupModel,
                EFTransactionType.SUCCESSFUL,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT,
                "Добавлено!");
        }

        return new EFTransactionArgs<GroupModel>(
            existingModel,
            EFTransactionType.FAILURE,
            EFTransactionReason.CONTAINS_ENTITY_OF_THIS_ELEMENT,
            "Добавление не нуждается, данный елемент уже есть в таблице!");
    }
    protected override EFTransactionArgs<GroupModel> TryRemoveRow(int modelIndex)
    {
        int index = 0;
        GroupModel model = default;

        _dbContext.Groups.ForEachAsync(it =>
        {
            if (modelIndex == index) 
                model = it;

            index++;
        });

        if (model == default)
        {
            return new EFTransactionArgs<GroupModel>(
                null,
                EFTransactionType.FAILURE,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT, 
                $"Елемента с данным индексом:[{modelIndex+1}] не существует!");
        }

        _dbContext.Groups.Remove(model);
        _dbContext.SaveChanges();

        return new EFTransactionArgs<GroupModel>(
            model,
            EFTransactionType.SUCCESSFUL,
            EFTransactionReason.CONTAINS_ENTITY_OF_THIS_ELEMENT,
            $"Елемент с индексом:[{modelIndex+1}] удален!");
    }
    protected override EFTransactionArgs<GroupModel> TryUpdateRow(int modelIndex)
    {
        int index = 0;
        GroupModel model = default;

        _dbContext.Groups.ForEachAsync(it =>
        {
            if (modelIndex == index) 
                model = it;

            index++;
        });

        if (model == default)
        {
            return new EFTransactionArgs<GroupModel>(
                null,
                EFTransactionType.FAILURE,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT, 
                $"Елемента с данным индексом:[{modelIndex+1}] не существует!");
        }

        Console.WriteLine("Введите название факультета...\n");
        model.Faculty = Console.ReadLine();
        
        Console.WriteLine("Введите название группы...\n");
        model.GroupName = Console.ReadLine();
        
        Console.WriteLine("Введите курс...\n");
        model.Course = Convert.ToInt32(Console.ReadLine());
        
        Console.WriteLine("Введите кол-во студентов...\n");
        model.CountOfStudents = Convert.ToInt32(Console.ReadLine());
        
        Console.WriteLine("Введите кол-во подгрупп...\n");
        model.CountOfSubGroups = Convert.ToInt32(Console.ReadLine());
        
        model.QualificationReference = _qualificationWrappedModel.AddRow();
        model.FormedEducationReference = _formedEducationWrappedModel.AddRow();
        model.SpecialityReference = _specialityWrappedModel.AddRow();
        
        _dbContext.SaveChanges();

        return new EFTransactionArgs<GroupModel>(
            model,
            EFTransactionType.SUCCESSFUL,
            EFTransactionReason.CONTAINS_ENTITY_OF_THIS_ELEMENT,
            $"Елемент с индексом:[{modelIndex+1}] обновлен!");
    }
    protected override EFTransactionArgs<GroupModel> TryDisplayInfo()
    {
        string info = "";
        
        _dbContext.FormedEducations.ForEachAsync(el =>
        {
            info += $"FormName: {el.FormName}\n";
        });
        
        return new EFTransactionArgs<GroupModel>(
            null,
            EFTransactionType.SUCCESSFUL,
            EFTransactionReason.NONE, 
            $"Елементы выведены успешно!");
    }
}