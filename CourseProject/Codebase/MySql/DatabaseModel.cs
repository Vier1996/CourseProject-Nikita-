using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Codebase.MySql;

public abstract class DatabaseModel<T> where T : class // абстрактный класс модели базы данных
{
    protected DbSet<T> _container = null; // колекция моделей
    private bool _logging = true; // чекбокс логирования
    
    public DatabaseModel(DbSet<T> container, bool logging) // конструктор класса
    {
        _container = container; // присваение коллекции
        _logging = logging; // присвоения статуса логирования
    }

    public T AddRow() // метод добавление записи
    {
        EFTransactionArgs<T> args = TryAddRow(); // вызов метода добавления
        LogInfo(args); // логирование информации 
        return args.TransactionModel; // возврат обрабатываемой модели
    }

    public T RemoveRow() // методы удаления записи
    {
        Console.WriteLine("Введите индекс строки..."); // лог
        int index = Convert.ToInt32(Console.ReadLine()); // ожидание ввода индекса записи
        
        EFTransactionArgs<T> args = TryRemoveRow(index - 1); // вызов метода удаления
        LogInfo(args); // логирование информации
        return args.TransactionModel; // возврат обрабатываемой модели
    }

    public T UpdateRow()
    {
        Console.WriteLine("Введите индекс строки..."); // лог
        int index = Convert.ToInt32(Console.ReadLine()); // ожидание ввода индекса записи

        EFTransactionArgs<T> args = TryUpdateRow(index - 1); // вызов метода обновления записи
        LogInfo(args); // логирование информации
        return args.TransactionModel; // возврат обрабатываемой модели
    }

    public T DisplayInto()
    {
        EFTransactionArgs<T> args = TryDisplayInfo(); // вызов метод показа информации
        LogInfo(args); // логирование информации
        return args.TransactionModel; // возврат обрабатываемой модели
    }

    public T Find(int referenceIndex) => FindById(referenceIndex); // метод поиска по индексу

    protected virtual EFTransactionArgs<T> TryAddRow() => // метод довабления
        new EFTransactionArgs<T>(null, EFTransactionType.NONE, EFTransactionReason.NONE, "");

    protected virtual EFTransactionArgs<T> TryRemoveRow(int index) => // метод удаления
        new EFTransactionArgs<T>(null, EFTransactionType.NONE, EFTransactionReason.NONE, "");

    protected virtual EFTransactionArgs<T> TryUpdateRow(int index) => // метод обновления
        new EFTransactionArgs<T>(null, EFTransactionType.NONE, EFTransactionReason.NONE, "");

    protected virtual EFTransactionArgs<T> TryDisplayInfo() => // метод вывода информации
        new EFTransactionArgs<T>(null, EFTransactionType.NONE, EFTransactionReason.NONE, "");

    protected T Find(T model) // поиск модели по экземпляру
    {
        PropertyInfo info = model.GetType().GetProperties().Where(prp => !prp.Name.Equals("Id")).GetEnumerator().Current; // поиск информации сво-в по id
        
        foreach (var md in _container) // проходим по существующим записям
        {
            foreach (var properties in md.GetType().GetProperties().Where(prp => !prp.Name.Equals("Id"))) // проходим по записям с вытягивая сво-ва
            {
                if (properties == info) // проверяем идентичность сво-в
                    return md; // позвращаем найденный экземпляр
            }
        }

        return default; // возвращаем значение по-умолчанию
    }
    
    protected T FindByIndex(int modelIndex) // метод поиска по id записи
    {
        int index = 0; // указываем стартовый индекс

        foreach (var model in _container) // проходим по записям
        {
            if (modelIndex == index) // сравниваем индекс
                return model; // возвращаем экземпляр модели

            index++; // инкрементируем индекс
        }

        return default; // возвращаем значение по-умолчанию
    }
    
    private T FindById(int id) // метод поиска по идентефикатору
    {
        foreach (var model in _container)// проходим по записям
        {
            PropertyInfo idInfo = model.GetType().GetProperties()[0]; // вытягиваем через рефлексию значение идентефикатора
            int currentId = (int)idInfo.GetValue(model); // переводим идентефикатор в int
            
            if (currentId == id) // сравниваем id
                return model; // возвращаем экземпляр модели
        }

        return default; // возвращаем значение по-умолчанию
    }
    
    private void LogInfo(EFTransactionArgs<T> args)
    {
        if(!_logging) return;
        
        Console.WriteLine(args.Description + "\n");
    }
}