using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;

namespace InventoryManagementSystem.DAL
{
    public class StoreConfiguration : DbConfiguration
    {
        public StoreConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
            DbInterception.Add(new StoreInterceptorTransientErrors());
            DbInterception.Add(new StoreInterceptorLogging());
        }
    }
}