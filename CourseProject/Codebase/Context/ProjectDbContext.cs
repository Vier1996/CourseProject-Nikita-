using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace CourseProject.Codebase.Context;

public class ProjectDbContext : DbContext // класс "контекст базы данных"
{
    private readonly MySqlConnectionStringBuilder _credits = new MySqlConnectionStringBuilder(); // собиратель строки соединения

    public DbSet<GroupModel> Groups { get; set; } // коллекция моделей "Группа"
    public DbSet<QualificationModel> Qualifications { get; set; } // коллекция моделей "Квалификация"
    public DbSet<FormedEducationModel> FormedEducations { get; set; } // коллекция моделей "Форма обучения"
    public DbSet<SpecialityModel> Specialities { get; set; } // коллекция моделей "Специализация"

    private Action _onComplete = null; // обратный вызов "выполнено"
    
    public ProjectDbContext() // конструктор класса
    {
        _credits.Server = "127.0.0.1"; // локальный айпи
        _credits.Port = 57736; // локальный порт
        _credits.UserID = "root"; // имя пользователя
        _credits.Password = "admin"; // пороль
        _credits.Database = "projectdatabase"; // название базы данных
    }

    public ProjectDbContext OnInitializationComplete(Action onComplete) // методы "Инициализация выполнена"
    {
        _onComplete = onComplete; // присваивание обратного вызова
        return this; // возвращение своего собственного экземпляра
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // метод "Настройка"
    {
        if (!optionsBuilder.IsConfigured) // проверка "наастроен ли Entity framework с MySql"
        {
            DbConnection connection = new MySqlConnection(_credits.ToString()); // создание соеденительной строки
            optionsBuilder.UseMySql(connection, ServerVersion.AutoDetect(_credits.ToString())); // указываем Entity Framework использовать MySql
        }
    }
}

public class GroupModel // модель группы
{
    public int Id { get; set; }
    public string? Faculty { get; set; }
    public string? GroupName { get; set; }
    public int Course { get; set; }
    public int CountOfStudents { get; set; }
    public int CountOfSubGroups { get; set; }


    public QualificationModel? QualificationReference { get; set; }
    public int QualificationReferenceId { get; set; }
    public FormedEducationModel? FormedEducationReference { get; set; }
    public int FormedEducationReferenceId { get; set; }

    public SpecialityModel? SpecialityReference { get; set; }
    public int SpecialityReferenceId { get; set; }

}

public class QualificationModel // модель квалификации
{
    public int Id { get; set; }
    public string? QualificationName { get; set; }
}

public class FormedEducationModel // модель формы обучения
{
    public int Id { get; set; }
    public string? FormName { get; set; }
}

public class SpecialityModel // модель специализации
{
    public int Id { get; set; }
    public string? SpecialityName { get; set; }
    public string? Profile { get; set; }
}