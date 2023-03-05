namespace CourseProject.Codebase.MySql;

public abstract class DatabaseModel<T> where T : class
{
    private bool _logging = true;
    
    public DatabaseModel(bool logging) => _logging = logging;

    public T AddRow()
    {
        EFTransactionArgs<T> args = TryAddRow();
        LogInfo(args);
        return args.TransactionModel;
    }

    public T RemoveRow()
    {
        int index = Convert.ToInt32(Console.ReadLine());
        
        EFTransactionArgs<T> args = TryRemoveRow(index - 1);
        LogInfo(args);
        return args.TransactionModel;
    }

    public T UpdateRow()
    {
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

    protected virtual EFTransactionArgs<T> TryAddRow() => 
        new EFTransactionArgs<T>(null, EFTransactionType.NONE, EFTransactionReason.NONE, "");

    protected virtual EFTransactionArgs<T> TryRemoveRow(int index) => 
        new EFTransactionArgs<T>(null, EFTransactionType.NONE, EFTransactionReason.NONE, "");

    protected virtual EFTransactionArgs<T> TryUpdateRow(int index) => 
        new EFTransactionArgs<T>(null, EFTransactionType.NONE, EFTransactionReason.NONE, "");

    protected virtual EFTransactionArgs<T> TryDisplayInfo() => 
        new EFTransactionArgs<T>(null, EFTransactionType.NONE, EFTransactionReason.NONE, "");

    private void LogInfo(EFTransactionArgs<T> args)
    {
        if(!_logging) return;
        
        Console.WriteLine(args.Description + "\n");
    }
}