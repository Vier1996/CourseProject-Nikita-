using CourseProject.Codebase.Context;

namespace CourseProject.Codebase.MySql.Models;

public class QualificationWrappedModel : DatabaseModel<QualificationModel> // класс обертка для модели "квалификация"
{
    private ProjectDbContext _dbContext; // объявляем экземпляр контекста базы данных
    
    public QualificationWrappedModel(ProjectDbContext dbContext, bool logging = true) : base(dbContext.Qualifications, logging) // конструктор класса
        => _dbContext = dbContext;
    
    protected override EFTransactionArgs<QualificationModel> TryAddRow() // переопределенный метод добавления
    {
        QualificationModel qualificationModel = new QualificationModel(); // создаем экземпляр модели "квалификации"
        
        Console.WriteLine("Введите название квалификации..."); // лог
        qualificationModel.QualificationName = Console.ReadLine(); // оидаем ввода от пользователя

        QualificationModel existingModel = Find(qualificationModel); // проводим поиск модели
        
        if (existingModel == default) // если модель не существует
        {
            _container.Add(qualificationModel); // добавляем модель
            _dbContext.SaveChanges(); // сохраяем в базу данных
            
            return new EFTransactionArgs<QualificationModel>( // формируем и возвращаем аргументы транзакции
                qualificationModel,
                EFTransactionType.SUCCESSFUL,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT,
                "Добавлено!");
        }

        return new EFTransactionArgs<QualificationModel>( // формируем и возвращаем аргументы транзакции
            existingModel,
            EFTransactionType.FAILURE,
            EFTransactionReason.CONTAINS_ENTITY_OF_THIS_ELEMENT,
            "Добавление не нуждается, данный елемент уже есть в таблице!");
    }
    
    protected override EFTransactionArgs<QualificationModel> TryRemoveRow(int modelIndex) // переопределенный метод удаления
    {
        int index = 0; // инициализируем стартовый индекс
        QualificationModel model = FindByIndex(modelIndex); // проводим поиск по индексу
        
        if (model == default) // если модель не существует
        {
            return new EFTransactionArgs<QualificationModel>( // формируем и возвращаем аргументы транзакции
                null,
                EFTransactionType.FAILURE,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT, 
                $"Елемента с данным индексом:[{modelIndex+1}] не существует!");
        }

        _container.Remove(model); // удаляем модель из списка
        _dbContext.SaveChanges(); // сохраняем базу данных

        return new EFTransactionArgs<QualificationModel>( // формируем и возвращаем аргументы транзакции
            model,
            EFTransactionType.SUCCESSFUL,
            EFTransactionReason.CONTAINS_ENTITY_OF_THIS_ELEMENT,
            $"Елемент с индексом:[{modelIndex+1}] удален!");
    }
    
    protected override EFTransactionArgs<QualificationModel> TryUpdateRow(int modelIndex) // переопределенный метод обновления
    {
        int index = 0; // инициализируем стартовый индекс
        QualificationModel model = FindByIndex(modelIndex); // проводим посик по индексу

        if (model == default) // если модель не существует
        {
            return new EFTransactionArgs<QualificationModel>( // формируем и возвращаем аргументы транзакции
                null,
                EFTransactionType.FAILURE,
                EFTransactionReason.NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT, 
                $"Елемента с данным индексом:[{modelIndex+1}] не существует!");
        }
        
        Console.WriteLine("Введите название квалификации..."); // лог
        model.QualificationName = Console.ReadLine(); // ожидаем ввода от пользователя
        
        _dbContext.SaveChanges(); // сохраняем изменения

        return new EFTransactionArgs<QualificationModel>( // формируем и возвращаем аргументы транзакции
            model,
            EFTransactionType.SUCCESSFUL,
            EFTransactionReason.CONTAINS_ENTITY_OF_THIS_ELEMENT,
            $"Елемент с индексом:[{modelIndex+1}] обновлен!");
    }
    
    protected override EFTransactionArgs<QualificationModel> TryDisplayInfo() // переопределенный метод вывода информации
    {
        int index = 0; // инициализируем стартовый индекс
        string info = ""; // иницилизируем информацию

        foreach (QualificationModel model in _container.OrderBy(fe => fe.Id)) // проходим по элементам с нужным id 
        {
            info += $"Index: {++index} | QualificationName: {model.QualificationName}.\n"; // формируем информацию
        }

        if (index != 0) // проводим проверку индекса
            Console.WriteLine(info); // выводим информацию

        return new EFTransactionArgs<QualificationModel>( // формируем и возвращаем аргументы транзакции
            null,
            EFTransactionType.SUCCESSFUL,
            EFTransactionReason.NONE, 
            index != 0 ? $"Елементы выведены успешно!" : "Тут пусто :(");
    }
}