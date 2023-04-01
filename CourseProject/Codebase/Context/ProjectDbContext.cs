using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace CourseProject.Codebase.Context;

public class ProjectDbContext : DbContext
{
    private readonly MySqlConnectionStringBuilder _credits = new MySqlConnectionStringBuilder();

    public DbSet<GroupModel> Groups { get; set; }
    public DbSet<QualificationModel> Qualifications { get; set; }
    public DbSet<FormedEducationModel> FormedEducations { get; set; }
    public DbSet<SpecialityModel> Specialities { get; set; }

    private Action _onComplete = null;
    
    public ProjectDbContext()
    {
        _credits.Server = "127.0.0.1";
        _credits.Port = 3306;
        _credits.UserID = "root";
        _credits.Password = "viprudolf";
        _credits.Database = "projectdatabase";
    }

    public ProjectDbContext OnInitializationComplete(Action onComplete)
    {
        _onComplete = onComplete;
        return this;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            DbConnection connection = new MySqlConnection(_credits.ToString());
            optionsBuilder.UseMySql(connection, ServerVersion.AutoDetect(_credits.ToString()));
        }
    }
}

public class GroupModel
{
    [Key] public int Id { get; set; }
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

public class QualificationModel
{
    [Key]public int Id { get; set; }
    public string? QualificationName { get; set; }
}

public class FormedEducationModel
{
    [Key]public int Id { get; set; }
    public string? FormName { get; set; }
}

public class SpecialityModel
{
    [Key]public int Id { get; set; }
    public string? SpecialityName { get; set; }
    public string? Profile { get; set; }
}