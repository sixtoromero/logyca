using Logyca.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logyca.DAL
{
    public class LogycaDbContext : DbContext
    {
        public LogycaDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public LogycaDbContext()
        {
        }

        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<Code> Codes { get; set; }

    }
}
