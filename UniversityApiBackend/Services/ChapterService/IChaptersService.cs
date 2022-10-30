using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services.ChapterService
{
    public interface IChaptersService
    {
        IEnumerable<Chapter> getChaptersOfCourse(int courseId);
    }
}
