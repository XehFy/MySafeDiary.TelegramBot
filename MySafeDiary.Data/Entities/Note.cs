using System;
using System.Collections.Generic;
using System.Text;

namespace MySafeDiary.Data.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Text { get; set; } 
    }
}
