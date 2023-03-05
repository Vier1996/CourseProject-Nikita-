using CourseProject.Codebase.Context;
using Microsoft.EntityFrameworkCore;

using var db = new ProjectDbContext();

Console.WriteLine("Inserting a new data");

//FormedEducationModel a = db.FormedEducations.First();
//db.FormedEducations.Remove(a);
//db.SaveChanges();

Console.WriteLine("DONE");