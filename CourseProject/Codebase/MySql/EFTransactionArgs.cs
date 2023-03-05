namespace CourseProject.Codebase.MySql;

public class EFTransactionArgs<T>
{
    public T TransactionModel => _transactionModel;
    public EFTransactionType TransactionType => _type;
    public EFTransactionReason TransactionReason => _reason;
    public string Description => _description;
    
    private T _transactionModel;
    private EFTransactionType _type;
    private EFTransactionReason _reason;
    private string _description;

    public EFTransactionArgs(T transactionModel, EFTransactionType type, EFTransactionReason reason, string description)
    {
        _transactionModel = transactionModel;
        _type = type;
        _reason = reason;
        _description = description;
    }
}

public enum EFTransactionType
{
    NONE,
    SUCCESSFUL,
    FAILURE
}

public enum EFTransactionReason
{
    NONE,
    CONTAINS_ENTITY_OF_THIS_ELEMENT,
    NOT_CONTAINS_ENTITY_OF_THIS_ELEMENT,
}