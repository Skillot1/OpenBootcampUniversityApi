using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public class Services
    {
        //Entiendo que sería mejor crear una interfaz para cada modelo con los métodos requeridos y luego hacer clases que hereden de ella, pero al ser un popurrí, lo hago directamente aquí

        private readonly UniversityDBContext _context;

        public Services(UniversityDBContext context)
        {
            _context = context;
        }

        //Buscar usuarios por email

       public  User FindUserByEmail(string email)
        {
            return _context.Users.Where(u => u.Email == email).FirstOrDefault();
        }

        //Buscar alumnos mayores de edad

        public List<Student> FindAdultStudents()
        {
            return _context.Students.Where(student=> (DateTime.Now.Year- student.DateOfBirth.Year) >=18).ToList();
        }

        //Buscar alumnos que tengan al menos un curso

        public List<Student> FindStudentsWithCourses()
        {
            return _context.Students.Where(student => student.Courses.Any()).ToList();
        }

        //Buscar cursos de un nivel determinado que al menos tengan un alumno inscrito

        public List<Course> FindCoursesByLevelWithStudents(Level level)
        {
            return _context.Courses.Where(course => course.Level == level && course.Students.Any()).ToList();
        }

        //Buscar cursos de un nivel determinado que sean de una categoría determinada
        public List<Course> FindCoursesByLevelAndCategory(Level level, Category category)
        {
            return _context.Courses.Where(course => course.Level == level && course.Categories.Any(cat => cat ==  category)).ToList();
        }


        //Buscar cursos sin alumnos

        public List<Course> FindCourseslWithoutStudents()
        {
            return _context.Courses.Where(course => !course.Students.Any()).ToList();
        }


    }
}
