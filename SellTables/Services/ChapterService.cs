using SellTables.Interfaces;
using SellTables.Models;
using SellTables.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;



namespace SellTables.Services
{
    public class ChapterService : IChapterService
    {
        private ApplicationDbContext DataBaseContext;
        private IRepository<Chapter> ChapterRepository ;
       
        public ChapterService(ApplicationDbContext DataBaseContext) {
            this.DataBaseContext = DataBaseContext;
            ChapterRepository = new ChaptersRepository(DataBaseContext);
        }

       public Chapter GetChapter(int id)
        {
            return ChapterRepository.Get(id);
        }

        public void UpdateChapterPos(int oldPosition, int newPosition, int fromChapterId, int toChapterId)
        {
            Chapter oldChapter = DataBaseContext.Chapters.FirstOrDefault(p=>p.Id == fromChapterId);
            Chapter newChapter = DataBaseContext.Chapters.FirstOrDefault(p => p.Id == toChapterId);
            SwapAndUpdateChaptersPosition(oldChapter, newChapter);

        }

        private void SwapAndUpdateChaptersPosition(Chapter oldChapter, Chapter newChapter) {
            int oldchapterpos = oldChapter.Number;
            oldChapter.Number = newChapter.Number;
            newChapter.Number = oldchapterpos;

            ChapterRepository.Update(oldChapter);
            ChapterRepository.Update(newChapter);
        }

    }
}