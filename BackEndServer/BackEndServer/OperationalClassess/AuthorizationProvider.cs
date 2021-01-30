using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEndServer.OperationalClassess
{
    public class AuthorizationProvider
    {
        public static IEnumerable<Audience> GetAllAudiences()
        {
            var dbContext = new URDEV_SW_MOBILITYEntities();
            return dbContext.Audiences.Where(a => a.StatusCode == 0).ToList<Audience>();
        }
    }
}