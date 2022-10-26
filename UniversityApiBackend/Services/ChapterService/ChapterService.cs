using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services.ChapterService
{
    public class ChapterService : IChapterService
    {
        private readonly UniversityDBContext _context;

        public ChapterService(UniversityDBContext _context)
        {
            this._context = _context;
        }

        public IEnumerable<Chapter> getChaptersOfCourse(int courseId)
        {
           return _context.Chapters.Where(chapter => chapter.Course.Id == courseId);
        }

    }

}
