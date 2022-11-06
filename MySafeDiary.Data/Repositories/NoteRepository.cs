using Microsoft.EntityFrameworkCore;
using MySafeDiary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySafeDiary.Data.Repositories
{
    public class NoteRepository : RepositoryBase<Note>
    {
        public void AddNote(Note note, User user)
        {
            Create(note);
        }
    }
}
