using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services.StudentServices
{
    public interface IStudentsService
    {
        IEnumerable<Student> GetStudentsWithCourses();
        IEnumerable<Student> GetStudentsWithoutCourses();
        IEnumerable<Student> GetStudentsOfCourse(int courseId);

    }
}
