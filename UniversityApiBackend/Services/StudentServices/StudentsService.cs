using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services.StudentServices
{
    public class StudentsService : IStudentsService
    {

        private readonly UniversityDBContext _context;
        public StudentsService(UniversityDBContext _context)
        {
            this._context = _context;
        }


        public IEnumerable<Student> GetStudentsWithCourses()
        {
            return _context.Students.Where(s=>s.Courses != new List<Course>());
        }
        
        //Obtener todos los alumnos que no tienen cursos asociados
        public IEnumerable<Student> GetStudentsWithoutCourses()
        {
            return _context.Students.Where(s => s.Courses == new List<Course>());
        }

        //Obtener alumnos de un Curso concreto
        public IEnumerable<Student> GetStudentsOfCourse(int courseId)
        {
            return _context.Students.Where(s => s.Courses.Any(c=> c.Id==courseId));
        }
    }
}
