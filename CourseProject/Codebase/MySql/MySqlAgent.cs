using CourseProject.Codebase.Context;
using CourseProject.Codebase.MySql.Models;

namespace CourseProject.Codebase.MySql;

public class MySqlAgent
{
    public QualificationWrappedModel QualificationWrappedModel { get; }
    public SpecialityWrappedModel SpecialityWrappedModel { get; }
    public FormedEducationWrappedModel FormedEducationWrappedModel { get; }
    public GroupWrappedModel GroupWrappedModel { get; }

    public MySqlAgent(ProjectDbContext dbContext)
    {
        QualificationWrappedModel = new QualificationWrappedModel(dbContext);
        SpecialityWrappedModel = new SpecialityWrappedModel(dbContext);
        FormedEducationWrappedModel = new FormedEducationWrappedModel(dbContext);
        GroupWrappedModel = new GroupWrappedModel(dbContext, QualificationWrappedModel, SpecialityWrappedModel, FormedEducationWrappedModel); 
    }
}