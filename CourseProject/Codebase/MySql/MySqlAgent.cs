using CourseProject.Codebase.Context;
using CourseProject.Codebase.MySql.Models;

namespace CourseProject.Codebase.MySql;

public class MySqlAgent // класс описывающий агента sql
{
    public QualificationWrappedModel QualificationWrappedModel { get; } // экземпляр обертки модели "квалификация"
    public SpecialityWrappedModel SpecialityWrappedModel { get; } // экземпляр обертки модели "специализация"
    public FormedEducationWrappedModel FormedEducationWrappedModel { get; } // экземпляр обертки модели "форма обучения"
    public GroupWrappedModel GroupWrappedModel { get; } // экземпляр обертки модели "группа"

    public MySqlAgent(ProjectDbContext dbContext) // конструктор класса
    {
        QualificationWrappedModel = new QualificationWrappedModel(dbContext); // создаем экземпляр класса обертки "квалификация"
        SpecialityWrappedModel = new SpecialityWrappedModel(dbContext); // создаем экземпляр класса обертки "специализация"
        FormedEducationWrappedModel = new FormedEducationWrappedModel(dbContext); // создаем экземпляр класса обертки "форма обучения"
        GroupWrappedModel = new GroupWrappedModel(dbContext, QualificationWrappedModel, SpecialityWrappedModel, 
            FormedEducationWrappedModel); // создаем экземпляр класса обертки "группа"
    }
}