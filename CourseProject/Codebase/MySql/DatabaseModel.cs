using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Codebase.MySql;

public abstract class DatabaseModel<T> where T : class
{
    protected DbSet<T> _container = null;
    private bool _logging = true;
    
    public DatabaseModel(DbSet<T> container, bool logging)
    {
        _container = container;
        _logging = logging;
    }

    public T AddRow()
    {
        EFTransactionArgs<T> args = TryAddRow();
        LogInfo(args);
        return args.TransactionModel;
    }

    public T RemoveRow()
    {
        Console.WriteLine("Введите индекс строки...");
        int index = Convert.ToInt32(Console.ReadLine());
        
        EFTransactionArgs<T> args = TryRemoveRow(index - 1);
        LogInfo(args);
        return args.TransactionModel;
    }

    public T UpdateRow()
    {
        Console.WriteLine("Введите индекс строки...");
        int index = Convert.ToInt32(Console.ReadLine());

        EFTransactionArgs<T> args = TryUpdateRow(index - 1);
        LogInfo(args);
        return args.TransactionModel;
    }

    public T DisplayInto()
    {
        EFTransactionArgs<T> args = TryDisplayInfo();
        LogInfo(args);
        return args.TransactionModel;
    }

    public T Find(int referenceIndex) => FindById(referenceIndex);

    protected virtual EFTransactionArgs<T> TryAddRow() => 
        new EFTransactionArgs<T>(null, EFTransactionType.NONE, EFTransactionReason.NONE, "");

    protected virtual EFTransactionArgs<T> TryRemoveRow(int index) => 
        new EFTransactionArgs<T>(null, EFTransactionType.NONE, EFTransactionReason.NONE, "");

    protected virtual EFTransactionArgs<T> TryUpdateRow(int index) => 
        new EFTransactionArgs<T>(null, EFTransactionType.NONE, EFTransactionReason.NONE, "");

    protected virtual EFTransactionArgs<T> TryDisplayInfo() => 
        new EFTransactionArgs<T>(null, EFTransactionType.NONE, EFTransactionReason.NONE, "");

    protected T Find(T model)
    {
        PropertyInfo info = model.GetType().GetProperties().Where(prp => !prp.Name.Equals("Id")).GetEnumerator().Current;
        
        foreach (var md in _container)
        {
            foreach (var properties in md.GetType().GetProperties().Where(prp => !prp.Name.Equals("Id")))
            {
                if (properties == info)
                    return md;
            }
        }

        return default;
    }
    
    protected T FindByIndex(int modelIndex)
    {
        int index = 0;

        foreach (var model in _container)
        {
            if (modelIndex == index)
                return model;

            index++;
        }

        return default;
    }
    
    private T FindById(int id)
    {
        foreach (var model in _container)
        {
            PropertyInfo idInfo = model.GetType().GetProperties()[0];
            int currentId = (int)idInfo.GetValue(model);
            
            if (currentId == id)
                return model;
        }

        return default;
    }
    
    private void LogInfo(EFTransactionArgs<T> args)
    {
        if(!_logging) return;
        
        Console.WriteLine(args.Description + "\n");
    }
}