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

    public ProjectDbContext()
    {
        _credits.Server = "127.0.0.1";
        _credits.Port = 3306;
        _credits.UserID = "root";
        _credits.Password = "viprudolf";
        _credits.Database = "projectdatabase";
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
    public int Id { get; set; }
    public string? Faculty { get; set; }
    public string? GroupName { get; set; }
    public int Course { get; set; }
    public int CountOfStudents { get; set; }
    public int CountOfSubGroups { get; set; }

    public virtual QualificationModel QualificationReference { get; set; }
    public virtual FormedEducationModel FormedEducationReference { get; set; }
    public virtual SpecialityModel SpecialityReference { get; set; }
}

public class QualificationModel
{
    public int Id { get; set; }
    public string? QualificationName { get; set; }
}

public class FormedEducationModel
{
    public int Id { get; set; }
    public string? FormName { get; set; }
}

public class SpecialityModel
{
    public int Id { get; set; }
    public string? SpecialityName { get; set; }
    public string? Profile { get; set; }
}