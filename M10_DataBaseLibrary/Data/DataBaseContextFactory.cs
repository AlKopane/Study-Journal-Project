using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyJournal.DataBase.Data
{
    public class DataBaseContextFactory : IDesignTimeDbContextFactory<DataBaseContext>
    {

        public DataBaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataBaseContext>();
            optionsBuilder.UseSqlServer();

            return new DataBaseContext(optionsBuilder.Options);
        }
    }
}
