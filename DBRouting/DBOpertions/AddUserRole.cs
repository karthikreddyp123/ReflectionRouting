using DBRouting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBRouting.DBOpertions
{
    public class AddUserRole
    {
        private static readonly DBRouteEntities DbContextEntities = new DBRouteEntities();
        public static void AssignRole(UserRole userRole)
        {
            try
            {
                DbContextEntities.User_Roles.Add(new User_Roles
                {
                    UserID = userRole.Id,
                    RoleID = userRole.Role
                });
                DbContextEntities.SaveChanges();
            }
            catch (Exception)
            {

            }
        }
    }
}