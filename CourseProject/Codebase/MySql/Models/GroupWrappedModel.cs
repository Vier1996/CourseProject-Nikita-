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
        SpecialityWrappedModel specialityWrappedModel, FormedEducationWrappedModel formedEducationWrappedModel, bool logging = true) 
        : base(dbContext.Groups, logging)
    {
        _dbContext = dbContext;
        _qualificationWrappedModel = qualificationWrappedModel;
        _specialityWrappedModel = specialityWrappedModel;
        _formedEducationWrappedModel = formedEducationWrappedModel;
    }

    protected override EFTransactionArgs<GroupModel> TryAddRow()
    {
        GroupModel groupModel = new GroupModel();
        
        Console.WriteLine("Введите название факультета...");
        groupModel.Faculty = Console.ReadLine();
        
        Console.WriteLine("Введите название группы...");
        groupModel.GroupName = Console.ReadLine();
        
        Console.WriteLine("Введите курс...");
        groupModel.Course = Convert.ToInt32(Console.ReadLine());
        
        Console.WriteLine("Введите кол-во студентов...");
        groupModel.CountOfStudents = Convert.ToInt32(Console.ReadLine());
        
        Console.WriteLine("Введите кол-во подгрупп...");
        groupModel.CountOfSubGroups = Convert.ToInt32(Console.ReadLine());
        
        groupModel.QualificationReference = _qualificationWrappedModel.AddRow();
        groupModel.FormedEducationReference = _formedEducationWrappedModel.AddRow();
        groupModel.SpecialityReference = _specialityWrappedModel.AddRow();
        
        groupModel.QualificationReferenceId = groupModel.QualificationReference.Id;
        groupModel.FormedEducationReferenceId = groupModel.FormedEducationReference.Id;
        groupModel.SpecialityReferenceId = groupModel.SpecialityReference.Id;
        
        GroupModel existingModel = Find(groupModel);
        if (existingModel == default)
        {
            _container.Add(groupModel);
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
        GroupModel model = FindByIndex(modelIndex);
        
        if (model == default)
        {
            return new EFTransactionArgs<GroupModel>(
                null,
                EFTransactionType.FAILURE,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT, 
                $"Елемента с данным индексом:[{modelIndex+1}] не существует!");
        }

        _container.Remove(model);
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
        GroupModel model = FindByIndex(modelIndex);
        
        if (model == default)
        {
            return new EFTransactionArgs<GroupModel>(
                null,
                EFTransactionType.FAILURE,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT, 
                $"Елемента с данным индексом:[{modelIndex+1}] не существует!");
        }

        Console.WriteLine("Введите название факультета...");
        model.Faculty = Console.ReadLine();
        
        Console.WriteLine("Введите название группы...");
        model.GroupName = Console.ReadLine();
        
        Console.WriteLine("Введите курс...");
        model.Course = Convert.ToInt32(Console.ReadLine());
        
        Console.WriteLine("Введите кол-во студентов...");
        model.CountOfStudents = Convert.ToInt32(Console.ReadLine());
        
        Console.WriteLine("Введите кол-во подгрупп...");
        model.CountOfSubGroups = Convert.ToInt32(Console.ReadLine());
        
        model.QualificationReference = _qualificationWrappedModel.AddRow();
        model.FormedEducationReference = _formedEducationWrappedModel.AddRow();
        model.SpecialityReference = _specialityWrappedModel.AddRow();

        model.QualificationReferenceId = model.QualificationReference.Id;
        model.FormedEducationReferenceId = model.FormedEducationReference.Id;
        model.SpecialityReferenceId = model.SpecialityReference.Id;
        
        _dbContext.SaveChanges();

        return new EFTransactionArgs<GroupModel>(
            model,
            EFTransactionType.SUCCESSFUL,
            EFTransactionReason.CONTAINS_ENTITY_OF_THIS_ELEMENT,
            $"Елемент с индексом:[{modelIndex+1}] обновлен!");
    }
    protected override EFTransactionArgs<GroupModel> TryDisplayInfo()
    {
        int index = 0;
        string info = "";

        List<GroupModel> groupModels = _container.OrderBy(fe => fe.Id).ToList();
        foreach (GroupModel model in groupModels)
        {
            QualificationModel qualificationModel = _qualificationWrappedModel.Find(model.QualificationReferenceId);
            FormedEducationModel formedEducationModel = _formedEducationWrappedModel.Find(model.FormedEducationReferenceId);
            SpecialityModel specialityModel = _specialityWrappedModel.Find(model.SpecialityReferenceId);
            
            info += $"Index: {++index} | " +
                    $"Faculty: {model.Faculty} || " +
                    $"GroupName: {model.GroupName} || " +
                    $"Course: {model.Course} || " +
                    $"CountOfStudents: {model.CountOfStudents} || " +
                    $"CountOfSubGroups: {model.CountOfSubGroups} || " +
                    $"Qualification: {qualificationModel.QualificationName} || " +
                    $"FormedEducation: {formedEducationModel.FormName} || " +
                    $"Speciality: {specialityModel.SpecialityName} || " +
                    $"SpecialityProfile: {specialityModel.Profile}. \n ";
        }

        if (index != 0) 
            Console.WriteLine(info);

        return new EFTransactionArgs<GroupModel>(
            null,
            EFTransactionType.SUCCESSFUL,
            EFTransactionReason.NONE, 
            index != 0 ? $"Елементы выведены успешно!" : "Тут пусто :(");
    }
}