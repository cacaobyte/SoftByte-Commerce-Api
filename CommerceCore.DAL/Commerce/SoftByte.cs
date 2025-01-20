using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommerceCore.DAL.Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.DAL.Commerce
{
    public class SoftByte : AppDbContext
    {
        private string connection { get; }

        public SoftByte() { }

        public SoftByte(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public SoftByte(string connection)
        {
            this.connection = connection;
            Database.SetCommandTimeout(900000);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseNpgsql(this.connection);
        }
    }
}
