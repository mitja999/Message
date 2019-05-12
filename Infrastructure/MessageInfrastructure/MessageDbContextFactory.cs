using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageInfrastructure
{

    public class ApplicationContextDbFactory : IDesignTimeDbContextFactory<MessageDbContext>
    {
        MessageDbContext IDesignTimeDbContextFactory<MessageDbContext>.CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MessageDbContext>();
            optionsBuilder.UseSqlServer<MessageDbContext>("Server = (localdb)\\mssqllocaldb; Database = Ineor; Trusted_Connection = True; MultipleActiveResultSets = true");

            return new MessageDbContext(optionsBuilder.Options);
        }
    }
}
