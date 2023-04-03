using CourseProject.Codebase.Context;

namespace CourseProject.Codebase.MySql.Models;

public class FormedEducationWrappedModel : DatabaseModel<FormedEducationModel>  // класс обертка для модели "форма обучения"
{
    private ProjectDbContext _dbContext; // объявляем экземпляр контекста базы данных
    
    public FormedEducationWrappedModel(ProjectDbContext dbContext, bool logging = true) : base(dbContext.FormedEducations, logging) // конструктор класса
        => _dbContext = dbContext;
    
    protected override EFTransactionArgs<FormedEducationModel> TryAddRow() // переопределенный метод добавления записи
    {
        FormedEducationModel formedEducationModel = new FormedEducationModel(); // создаем экземпляр модели "форма обучения"
        
        Console.WriteLine("Введите название формы обучения..."); // лог
        formedEducationModel.FormName = Console.ReadLine(); // ожидаем ввода от пользователя

        FormedEducationModel existingModel = Find(formedEducationModel); // проводим поиск существующей модели
        
        if (existingModel == default) // если модель не существует
        {
            _container.Add(formedEducationModel); // добавляем модель
            _dbContext.SaveChanges(); // сохраняем изменения

            return new EFTransactionArgs<FormedEducationModel>( // формируем и возвращаем аргументы транзакции
                formedEducationModel,
                EFTransactionType.SUCCESSFUL,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT,
                "Добавлено!");
        }

        return new EFTransactionArgs<FormedEducationModel>( // формируем и возвращаем аргументы транзакции
            existingModel,
            EFTransactionType.FAILURE,
            EFTransactionReason.CONTAINS_ENTITY_OF_THIS_ELEMENT,
            "Добавление не нуждается, данный елемент уже есть в таблице!");
    }
    
    protected override EFTransactionArgs<FormedEducationModel> TryRemoveRow(int modelIndex)  // переопределенный метод удаления записи
    {
        int index = 0; // инициализируем стартовый индекс
        FormedEducationModel model = FindByIndex(modelIndex); // проводим поиск по индексу
        
        if (model == default) // если модель не существует
        {
            return new EFTransactionArgs<FormedEducationModel>( // формируем и возвращаем аргументы транзакции
                null,
                EFTransactionType.FAILURE,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT, 
                $"Елемента с данным индексом:[{modelIndex+1}] не существует!");
        }

        _container.Remove(model); // удаляем запись
        _dbContext.SaveChanges(); // сохраняем изменения

        return new EFTransactionArgs<FormedEducationModel>( // формируем и возвращаем аргументы транзакции
            model,
            EFTransactionType.SUCCESSFUL,
            EFTransactionReason.CONTAINS_ENTITY_OF_THIS_ELEMENT,
            $"Елемент с индексом:[{modelIndex+1}] удален!");
    }
    
    protected override EFTransactionArgs<FormedEducationModel> TryUpdateRow(int modelIndex)  // переопределенный метод обновления записи
    {
        int index = 0; // инициализируем стартовый индекс
        FormedEducationModel model = FindByIndex(modelIndex); // проводим поиск по индексу
        
        if (model == default) // если модель не существует
        {
            return new EFTransactionArgs<FormedEducationModel>( // формируем и возвращаем аргументы транзакции
                null,
                EFTransactionType.FAILURE,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT, 
                $"Елемента с данным индексом:[{modelIndex+1}] не существует!");
        }

        Console.WriteLine("Введите название формы обучения..."); // лог
        model.FormName = Console.ReadLine(); // ожидаем ввода от пользователя
        
        _dbContext.SaveChanges(); // сохраняем изменения

        return new EFTransactionArgs<FormedEducationModel>( // формируем и возвращаем аргументы транзакции
            model,
            EFTransactionType.SUCCESSFUL,
            EFTransactionReason.CONTAINS_ENTITY_OF_THIS_ELEMENT,
            $"Елемент с индексом:[{modelIndex+1}] обновлен!");
    }
    
    protected override EFTransactionArgs<FormedEducationModel> TryDisplayInfo()  // переопределенный метод вывода информации
    {
        int index = 0; // инициализируем стартовый индекс
        string info = ""; // инициализируем информацию

        foreach (FormedEducationModel model in _container.OrderBy(fe => fe.Id)) // проходим по элементам с нужным id 
        {
            info += $"Index: {++index} | FormName: {model.FormName}.\n"; // формируем информацию
        }

        if (index != 0) // делаем проверку индекса
            Console.WriteLine(info); // выводим информациюы

        return new EFTransactionArgs<FormedEducationModel>( // формируем и возвращаем аргументы транзакции
            null,
            EFTransactionType.SUCCESSFUL,
            EFTransactionReason.NONE, 
            index != 0 ? $"Елементы выведены успешно!" : "Тут пусто :(");
    }
}