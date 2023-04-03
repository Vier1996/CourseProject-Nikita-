using CourseProject.Codebase.Context;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Codebase.MySql.Models;

public class GroupWrappedModel : DatabaseModel<GroupModel> // класс обертка для модели "группа"
{
    private ProjectDbContext _dbContext; // объявляем экземпляр контекста базы данных

    private QualificationWrappedModel _qualificationWrappedModel; // объявляем экземпляр обертки квалификации
    private SpecialityWrappedModel _specialityWrappedModel; // объявляем экземпляр обертки специализация
    private FormedEducationWrappedModel _formedEducationWrappedModel; // объявляем экземпляр обертки формы обучения
    
    public GroupWrappedModel(ProjectDbContext dbContext, QualificationWrappedModel qualificationWrappedModel, 
        SpecialityWrappedModel specialityWrappedModel, FormedEducationWrappedModel formedEducationWrappedModel, bool logging = true) 
        : base(dbContext.Groups, logging) // конструктор класса
    {
        _dbContext = dbContext; // присваиваем контекст базы данных
        _qualificationWrappedModel = qualificationWrappedModel; // присваиваем обертку квалификации
        _specialityWrappedModel = specialityWrappedModel; // присваиваем обертку специализации
        _formedEducationWrappedModel = formedEducationWrappedModel; // присваиваем обертку формы обучения
    }

    protected override EFTransactionArgs<GroupModel> TryAddRow() // переопределенный метод добавления записи
    {
        GroupModel groupModel = new GroupModel(); // создаем экземпляр модели "Группа"
        
        Console.WriteLine("Введите название факультета...");  // лог
        groupModel.Faculty = Console.ReadLine(); // ожидание ввода пользователя
        
        Console.WriteLine("Введите название группы...");  // лог
        groupModel.GroupName = Console.ReadLine(); // ожидание ввода пользователя
        
        Console.WriteLine("Введите курс...");  // лог
        groupModel.Course = Convert.ToInt32(Console.ReadLine()); // ожидание ввода пользователя
        
        Console.WriteLine("Введите кол-во студентов...");  // лог
        groupModel.CountOfStudents = Convert.ToInt32(Console.ReadLine()); // ожидание ввода пользователя
        
        Console.WriteLine("Введите кол-во подгрупп..."); // лог
        groupModel.CountOfSubGroups = Convert.ToInt32(Console.ReadLine()); // ожидание ввода пользователя
        
        groupModel.QualificationReference = _qualificationWrappedModel.AddRow(); // присваиваем модель квалификации 
        groupModel.FormedEducationReference = _formedEducationWrappedModel.AddRow(); // присваиваем модель формы обучения 
        groupModel.SpecialityReference = _specialityWrappedModel.AddRow(); // присваиваем модель специализации 
        
        groupModel.QualificationReferenceId = groupModel.QualificationReference.Id; // присваиваем id квалификации 
        groupModel.FormedEducationReferenceId = groupModel.FormedEducationReference.Id; // присваиваем id формы обучения 
        groupModel.SpecialityReferenceId = groupModel.SpecialityReference.Id; // присваиваем id специализации 
        
        GroupModel existingModel = Find(groupModel); // проводим поиск существующей модели
        if (existingModel == default) // если модели не существует
        {
            _container.Add(groupModel); // добавляем запись в базу данных
            _dbContext.SaveChanges(); // сохраняем изменеия

            return new EFTransactionArgs<GroupModel>( // формируем и возвращаем аргументы транзакции
                groupModel,
                EFTransactionType.SUCCESSFUL,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT,
                "Добавлено!");
        }

        return new EFTransactionArgs<GroupModel>( // формируем и возвращаем аргументы транзакции
            existingModel,
            EFTransactionType.FAILURE,
            EFTransactionReason.CONTAINS_ENTITY_OF_THIS_ELEMENT,
            "Добавление не нуждается, данный елемент уже есть в таблице!");
    }
    protected override EFTransactionArgs<GroupModel> TryRemoveRow(int modelIndex) // переопределенный метод удаления записи
    {
        int index = 0; // инициализируем стартовыф индекс
        GroupModel model = FindByIndex(modelIndex); // производим поиск по id
        
        if (model == default) // если модели не существует
        {
            return new EFTransactionArgs<GroupModel>( // формируем и возвращаем аргументы транзакции
                null,
                EFTransactionType.FAILURE,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT, 
                $"Елемента с данным индексом:[{modelIndex+1}] не существует!");
        }

        _container.Remove(model); // удаляем запись из базы данных
        _dbContext.SaveChanges(); // сохраняем изменения

        return new EFTransactionArgs<GroupModel>( // формируем и возвращаем аргументы транзакции
            model,
            EFTransactionType.SUCCESSFUL,
            EFTransactionReason.CONTAINS_ENTITY_OF_THIS_ELEMENT,
            $"Елемент с индексом:[{modelIndex+1}] удален!");
    }
    protected override EFTransactionArgs<GroupModel> TryUpdateRow(int modelIndex) // переопределенный метод обновления записи
    {
        int index = 0; // инициализация стартового индекса
        GroupModel model = FindByIndex(modelIndex); // поиск модели по id
        
        if (model == default) // если модели не существует
        {
            return new EFTransactionArgs<GroupModel>( // формируем и возвращаем аргументы транзакции
                null,
                EFTransactionType.FAILURE,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT, 
                $"Елемента с данным индексом:[{modelIndex+1}] не существует!");
        }

        Console.WriteLine("Введите название факультета..."); // лог
        model.Faculty = Console.ReadLine(); // ожидание ввода пользователя
        
        Console.WriteLine("Введите название группы...");  // лог
        model.GroupName = Console.ReadLine(); // ожидание ввода пользователя
        
        Console.WriteLine("Введите курс...");  // лог
        model.Course = Convert.ToInt32(Console.ReadLine()); // ожидание ввода пользователя
        
        Console.WriteLine("Введите кол-во студентов...");  // лог
        model.CountOfStudents = Convert.ToInt32(Console.ReadLine()); // ожидание ввода пользователя
        
        Console.WriteLine("Введите кол-во подгрупп...");  // лог
        model.CountOfSubGroups = Convert.ToInt32(Console.ReadLine()); // ожидание ввода пользователя
        
        model.QualificationReference = _qualificationWrappedModel.AddRow(); // присваиваем модель квалификации 
        model.FormedEducationReference = _formedEducationWrappedModel.AddRow(); // присваиваем модель формы обучения 
        model.SpecialityReference = _specialityWrappedModel.AddRow(); // присваиваем модель специализации 

        model.QualificationReferenceId = model.QualificationReference.Id; // присваиваем id квалификации 
        model.FormedEducationReferenceId = model.FormedEducationReference.Id; // присваиваем id формы обучения 
        model.SpecialityReferenceId = model.SpecialityReference.Id; // присваиваем id специализации 
        
        _dbContext.SaveChanges(); // сохраняем изменения

        return new EFTransactionArgs<GroupModel>( // формируем и возвращаем аргументы транзакции
            model,
            EFTransactionType.SUCCESSFUL,
            EFTransactionReason.CONTAINS_ENTITY_OF_THIS_ELEMENT,
            $"Елемент с индексом:[{modelIndex+1}] обновлен!");
    }
    protected override EFTransactionArgs<GroupModel> TryDisplayInfo() // переопределенный метод вывода информации
    {
        int index = 0; // инициализация стартового индекса
        string info = ""; // инциализация информации

        List<GroupModel> groupModels = _container.OrderBy(fe => fe.Id).ToList(); // поиск моделей по id
        foreach (GroupModel model in groupModels) // проходим по моделям
        {
            QualificationModel qualificationModel = _qualificationWrappedModel.Find(model.QualificationReferenceId); // ищем необходимую модель квалификации
            FormedEducationModel formedEducationModel = _formedEducationWrappedModel.Find(model.FormedEducationReferenceId); // ищем необходимую модель формы обучения
            SpecialityModel specialityModel = _specialityWrappedModel.Find(model.SpecialityReferenceId); // ищем необходимую модель специализации
            
            info += $"Index: {++index} | " + // формируем информацию
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

        if (index != 0) // делаем проверку индекса
            Console.WriteLine(info); // выводим информацию

        return new EFTransactionArgs<GroupModel>( // формируем и возвращаем аргументы транзакции
            null,
            EFTransactionType.SUCCESSFUL,
            EFTransactionReason.NONE, 
            index != 0 ? $"Елементы выведены успешно!" : "Тут пусто :(");
    }
}