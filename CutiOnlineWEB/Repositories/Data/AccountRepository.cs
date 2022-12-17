using Microsoft.EntityFrameworkCore;
using CutiOnlineWEB.Context;
using CutiOnlineWEB.Handler;
using CutiOnlineWEB.Models;
using CutiOnlineWEB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CutiOnlineWEB.Repositories.Data
{
    public class AccountRepository
    {
        MyContext myContext;


        public AccountRepository(MyContext myContext)
        {
            this.myContext = myContext;

        }
        public ResponseLogin Login(Login login)
        {
            var data = myContext.UserRoles
                .Include(x => x.Role)
                .Include(x => x.User)
                .FirstOrDefault(x => x.User.Id.Equals(login.Email));
            var Verify = Hashing.ValidatePassword(login.Password, data.User.Password);
            if (Verify)
            {
                if (data.Role.Id == 1)
                {
                    var data1 = myContext.UserRoles
                                .Include(x => x.Role)
                                .Include(x => x.User)
                                .Include(x => x.User.Admin)
                                .FirstOrDefault(x => x.User.Email.Equals(login.Email));
                    var verify = Hashing.ValidatePassword(login.Password, data.User.Password);
                    if (Verify)
                        if (data1 != null)
                        {
                            var respon = new ResponseLogin()
                            {
                                Id = data1.User.Id,
                                IdRole = data1.Role.Id,
                                Name = data1.User.Admin.FullName,
                                Role = data1.Role.Name
                            };
                            return respon;
                        }
                }
                else if (data.Role.Id == 2)
                {
                    var data1 = myContext.UserRoles
                               .Include(x => x.Role)
                               .Include(x => x.User)
                               .Include(x => x.User.Staff)
                               .FirstOrDefault(x => x.User.Email.Equals(login.Email));
                    var verify = Hashing.ValidatePassword(login.Password, data.User.Password);
                    if (Verify)
                        if (data1 != null)
                        {
                            var respon = new ResponseLogin()
                            {
                                Id = data1.User.Id_Staff,
                                IdRole = data1.Role.Id,
                                Name = data1.User.Staff.NameSt,
                                Role = data1.Role.Name
                            };
                            return respon;
                        }
                }
            }
            return null;

        }
    }
}