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
            optionsBuilder.UseSqlServer<MessageDbContext>("Server=tcp:ttdatabase.database.windows.net,1433;Initial Catalog=Message;Persist Security Info=False;User ID=mitjas;Password=Knuplez1+;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            return new MessageDbContext(optionsBuilder.Options);
        }
    }
}
