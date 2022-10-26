using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services.CoursesService
{
    public interface ICoursesService
    {
        IEnumerable<Course> GetCoursesByCategory(Category category);
        IEnumerable<Course> GetCoursesWithoutChapters();
        IEnumerable<Course> GetCoursesOfAnStudent(Student student);

    }
}
