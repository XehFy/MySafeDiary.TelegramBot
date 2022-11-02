using System;
using System.Collections.Generic;
using System.Text;

namespace MySafeDiary.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Diary> Diaries { get; set; }
    }
}
