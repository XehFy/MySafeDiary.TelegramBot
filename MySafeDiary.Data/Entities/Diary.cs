using System;
using System.Collections.Generic;
using System.Text;

namespace MySafeDiary.Data.Entities
{
    public class Diary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long UserId { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}
