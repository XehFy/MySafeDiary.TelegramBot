using Microsoft.EntityFrameworkCore;
using MySafeDiary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MySafeDiary.Data.Repositories
{
    public class DiaryRepository : RepositoryBase<Diary>
    {
        public void CreateDiary(Diary diary)
        {
            Create(diary);
        }
        public void UpdateDiary(Diary diary)
        {
            Update(diary);
        }
        public void DeleteDiary(Diary diary)
        {
            Delete(diary);
        }
        public void AddDiary(Diary diary, User user)
        {
            var userTo = BotContext.Users
                .Include(u => u.Diaries).First(u => u.Id == user.Id);
            userTo.Diaries.Add(diary);
        }

        public async Task<Diary> GetDiaryByUserIdAsync(long userId)
        {
            return await FindByCondition(diary => diary.UserId.Equals(userId)).FirstOrDefaultAsync();
        }
    }
}
