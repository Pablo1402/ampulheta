using Ampulheta.Domain.Entities;
using Ampulheta.Domain.Enuns;
using Ampulheta.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ampulheta.Repository.Context
{
    public static class Initializer
    {
        public static void Execute(this AmpulhetaContext context)
        {
            if (!context.UserTypes.ToList().Any())
            {
                context.UserTypes.Add(new UserType() { Name = UserTypeEnum.ADMIN.ToString() });
                context.UserTypes.Add(new UserType() { Name = UserTypeEnum.USER.ToString() });
                context.SaveChanges();
            }

            if (!context.Users.ToList().Any())
            {
                var userMaster = new User()
                {
                    Email = "barretopablo1991@gmail.com",
                    Login = "admin",
                    Password = Encript.Sha256("admin"),
                    Name = "user admin",
                    UserTypeId = UserTypeEnum.ADMIN.GetHashCode()
                };

                context.Users.Add(userMaster);
                context.SaveChanges();
            }

        }
    }
}
