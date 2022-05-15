using HomeWork22.AuthApp;
using HomeWork22.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWork22.DataContext
{
    public class RecordDbContext : IdentityDbContext<User>
    {
        public RecordDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Record> Records { get; set; }
    }
}
