using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services.CoursesService
{
    public class CoursesService :ICoursesService
    {
        private readonly UniversityDBContext _context;
        public CoursesService(UniversityDBContext _context)
        {
            this._context = _context;
        }

        //Obtener todos los Cursos de una categoría concreta
        public IEnumerable<Course> GetCoursesByCategory(Category category)
        {
           return  _context.Courses.Where(c=> c.Categories.Contains(category));
        }

        //Obtener Cursos sin temarios
        public IEnumerable<Course> GetCoursesWithoutChapters()
        {
            return _context.Courses.Where(c => c.Chapter != new Chapter());
        }

        //Obtener los Cursos de un Alumno
        public IEnumerable<Course> GetCoursesOfAnStudent(Student student)
        {
            return _context.Courses.Where(c => c.Students.Contains(student));
        }


    }
}
