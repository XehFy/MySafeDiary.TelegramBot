using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MySafeDiary.Data.Entities;
using System.Configuration;

namespace MySafeDiary.Data
{
    public class BotContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Diary> Diaries { get; set; }
        public DbSet<Note> Notes { get; set; }

        public BotContext(DbContextOptions<BotContext> options) : base(options) { }
    }
}
