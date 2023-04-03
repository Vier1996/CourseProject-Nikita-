using CourseProject.Codebase.Context;

namespace CourseProject.Codebase.MySql.Models;

public class SpecialityWrappedModel : DatabaseModel<SpecialityModel> // класс обертка для модели "специализация"
{
    private ProjectDbContext _dbContext; // объявляем экземпляр контекста базы данных
    
    public SpecialityWrappedModel(ProjectDbContext dbContext, bool logging = true) : base(dbContext.Specialities, logging) => // конструктор класса
        _dbContext = dbContext;

    protected override EFTransactionArgs<SpecialityModel> TryAddRow() // переопределенный метод добавления
    {
        SpecialityModel specialityModel = new SpecialityModel(); // создаем экземпляр модели "специализация"
        
        Console.WriteLine("Введите название специализации..."); // лог
        specialityModel.SpecialityName = Console.ReadLine(); // ждем ввода от пользователя
        
        Console.WriteLine("Введите название профиля..."); // лог
        specialityModel.Profile = Console.ReadLine(); // ждем ввода от пользователя

        SpecialityModel existingModel = Find(specialityModel); // проводим поиск существующей модели
        
        if (existingModel == default) // если модель не существует 
        {
            _container.Add(specialityModel); // добавляем модель
            _dbContext.SaveChanges(); // сохраяем в базе данных

            return new EFTransactionArgs<SpecialityModel>( // формируем и возвращаем аргументы транзакции
                specialityModel,
                EFTransactionType.SUCCESSFUL,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT,
                "Добавлено!");
        }

        return new EFTransactionArgs<SpecialityModel>( // формируем и возвращаем аргументы транзакции
            existingModel,
            EFTransactionType.FAILURE,
            EFTransactionReason.CONTAINS_ENTITY_OF_THIS_ELEMENT,
            "Добавление не нуждается, данный елемент уже есть в таблице!");
    }
    
    protected override EFTransactionArgs<SpecialityModel> TryRemoveRow(int modelIndex) // метод удаления записи
    {
        int index = 0; // инициализируем стартовый индекс
        SpecialityModel model = FindByIndex(modelIndex); // проводим поиск по id

        if (model == default) // если модель не существует 
        {
            return new EFTransactionArgs<SpecialityModel>( // формируем и возвращаем аргументы транзакции
                null,
                EFTransactionType.FAILURE,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT, 
                $"Елемента с данным индексом:[{modelIndex+1}] не существует!");
        }

        _container.Remove(model); // удаляем запись из базы данных
        _dbContext.SaveChanges(); // сохраняем изменения

        return new EFTransactionArgs<SpecialityModel>( // формируем и возвращаем аргументы транзакции
            model,
            EFTransactionType.SUCCESSFUL,
            EFTransactionReason.CONTAINS_ENTITY_OF_THIS_ELEMENT,
            $"Елемент с индексом:[{modelIndex+1}] удален!");
    }
    
    protected override EFTransactionArgs<SpecialityModel> TryUpdateRow(int modelIndex) // метод обновления записи
    {
        int index = 0; // инициализируем стартовый индекс
        SpecialityModel model = FindByIndex(modelIndex); // проводим поиск по id

        if (model == default) // если модель не существует 
        {
            return new EFTransactionArgs<SpecialityModel>( // формируем и возвращаем аргументы транзакции
                null,
                EFTransactionType.FAILURE,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT, 
                $"Елемента с данным индексом:[{modelIndex+1}] не существует!");
        }

        Console.WriteLine("Введите название специализации..."); // лог
        model.SpecialityName = Console.ReadLine(); // ожидаем ввода от пользователя
        
        Console.WriteLine("Введите название профиля..."); // лог
        model.Profile = Console.ReadLine(); // ожидаем ввода от пользоватея
        
        _dbContext.SaveChanges(); // сохраняем изменения

        return new EFTransactionArgs<SpecialityModel>( // формируем и возвращаем аргументы транзакции
            model,
            EFTransactionType.SUCCESSFUL,
            EFTransactionReason.CONTAINS_ENTITY_OF_THIS_ELEMENT,
            $"Елемент с индексом:[{modelIndex+1}] обновлен!");
    }
    
    protected override EFTransactionArgs<SpecialityModel> TryDisplayInfo() // методы вывода информации
    {
        int index = 0; // инициализируем стартовый индекс
        string info = ""; // инициализируем информация

        foreach (SpecialityModel model in _container.OrderBy(fe => fe.Id)) // проходим по моделям с id
        {
            info += $"Index: {++index} | SpecialityName: {model.SpecialityName} || Profile: {model.Profile}.\n"; // формируем лог
        }

        if (index != 0) // проверям индекс на корректность
            Console.WriteLine(info); // выводим информацию

        return new EFTransactionArgs<SpecialityModel>( // формируем и возвращаем аргументы транзакции
            null,
            EFTransactionType.SUCCESSFUL,
            EFTransactionReason.NONE, 
            index != 0 ? $"Елементы выведены успешно!" : "Тут пусто :(");
    }
}