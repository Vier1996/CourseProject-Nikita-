namespace CourseProject.Codebase.MySql;

public class EFTransactionArgs<T> // класс аргумент транзакции
{
    public T TransactionModel => _transactionModel; // экземпляр выполняемой модели
    public EFTransactionType TransactionType => _type; // экземпляр типа транзакции
    public EFTransactionReason TransactionReason => _reason; // экземпляр типа причины
    public string Description => _description; // эземпляр описания
    
    private T _transactionModel; // приватный экземпляр выполняемой модели
    private EFTransactionType _type; // приватный экземпляр типа транзакции
    private EFTransactionReason _reason; // приватный экземпляр типа причины
    private string _description; // приватный экземпляр описания

    public EFTransactionArgs(T transactionModel, EFTransactionType type, EFTransactionReason reason, string description) //конструктор класса
    {
        _transactionModel = transactionModel; // присвоение выполняемой модели
        _type = type; // присвоение типа транзакции
        _reason = reason; // присвоение типа причины
        _description = description; // присвоение описания
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