using CourseProject.Codebase.Context;
using CourseProject.Codebase.MySql.Models;

namespace CourseProject.Codebase.MySql;

public class MySqlAgent
{
    public static MySqlAgent Instance
    {
        get
        {
            if (_instance == null) 
                _instance = new MySqlAgent();
            return _instance;
        }
    }
    
    public QualificationWrappedModel QualificationWrappedModel => _qualificationWrappedModel;
    public SpecialityWrappedModel SpecialityWrappedModel => _specialityWrappedModel;
    public FormedEducationWrappedModel FormedEducationWrappedModel => _formedEducationWrappedModel;
    public GroupWrappedModel GroupWrappedModel => _groupWrappedModel;
    
    private QualificationWrappedModel _qualificationWrappedModel;
    private SpecialityWrappedModel _specialityWrappedModel;
    private FormedEducationWrappedModel _formedEducationWrappedModel;
    private GroupWrappedModel _groupWrappedModel;
    
    private static MySqlAgent _instance;

    public void Configure(ProjectDbContext dbContext)
    {
        _qualificationWrappedModel = new QualificationWrappedModel(dbContext);
        _specialityWrappedModel = new SpecialityWrappedModel(dbContext);
        _formedEducationWrappedModel = new FormedEducationWrappedModel(dbContext);
        _groupWrappedModel = new GroupWrappedModel(dbContext, _qualificationWrappedModel, _specialityWrappedModel, _formedEducationWrappedModel); 
    }
}