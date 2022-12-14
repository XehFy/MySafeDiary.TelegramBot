using Microsoft.EntityFrameworkCore;
using MySafeDiary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafeDiary.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>
    {
        public void CreateUser(User user)
        {
            Create(user);
        }
        public void UpdateUser(User user)
        {
            Update(user);
        }
        public void DeleteUser(User user)
        {
            Delete(user);
        }

        public void AddDiary(Diary diary, User user)
        {
            var userTo = BotContext.Users
                .Include(u => u.Diaries).First(u => u.Id == user.Id);
            userTo.Diaries.Add(diary);
        }

        public async Task<User> GetUserByIdAsync(long userId)
        {
            return await FindByCondition(user => user.Id.Equals(userId)).FirstOrDefaultAsync();
        }
    }
}
