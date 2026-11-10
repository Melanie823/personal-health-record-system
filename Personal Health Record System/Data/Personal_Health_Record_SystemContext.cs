using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Personal_Health_Record_System.Models;

namespace Personal_Health_Record_System.Data
{
    public class Personal_Health_Record_SystemContext : DbContext
    {
        public Personal_Health_Record_SystemContext (DbContextOptions<Personal_Health_Record_SystemContext> options)
            : base(options)
        {
        }

        public DbSet<Personal_Health_Record_System.Models.Health> Health { get; set; } = default!;
        public DbSet<Personal_Health_Record_System.Models.personal> personal { get; set; } = default!;
    }
}
