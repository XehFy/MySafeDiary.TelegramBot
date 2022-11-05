using System;
using System.Collections.Generic;
using System.Text;
using MySafeDiary.Data.Repositories;

namespace MySafeDiary.Domain.Abstractions
{
    public abstract class RepositoryInitializator
    {
        protected static UserRepository userRepository;
        public static DiaryRepository diaryRepository;


        public static void initUserRepository(UserRepository _userRepository)
        {
            userRepository = _userRepository;
        }
        public static void initDiaryRepository(DiaryRepository _diaryRepository)
        {
            diaryRepository = _diaryRepository;
        }
    }
}
