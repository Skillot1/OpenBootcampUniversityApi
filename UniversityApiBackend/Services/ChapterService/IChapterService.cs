using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services.ChapterService
{
    public interface IChapterService
    {
        IEnumerable<Chapter> getChaptersOfCourse(int courseId);
    }
}
