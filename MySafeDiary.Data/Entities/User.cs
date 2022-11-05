using System;
using System.Collections.Generic;
using System.Text;

namespace MySafeDiary.Data.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsNoteing { get; set; }
        public bool IsPasswording { get; set; }
        public bool IsEmailing { get; set; }

        public ICollection<Diary> Diaries { get; set; }
    }
}
