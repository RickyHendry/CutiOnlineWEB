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
        public int Register(Register register)
        {
            try
            {
                Staff Staff = new Staff()
                {
                    Name = register.Username,
                    Email = register.Email
                };
                myContext.Staffs.Add(Staff);

                var resultStaff = myContext.SaveChanges();
                if (resultStaff > 0)
                {
                    int id = myContext.Staffs.SingleOrDefault(x => x.Email.Equals(register.Email)).Id_Staff;
                    User user = new User()
                    {
                        Id = id,
                        Password = Hashing.HashPassword(register.Password)
                    };
                    myContext.Users.Add(user);
                    var resultUser = myContext.SaveChanges();
                    if (resultUser > 0)
                    {
                        UserRole userRole = new UserRole()
                        {
                            UserId = id,
                            RoleId = register.RoleId

                        };
                        myContext.UserRoles.Add(userRole);
                        var resultUserRole = myContext.SaveChanges();
                        if (resultUserRole > 0)
                        {
                            return resultUserRole;
                        }
                        myContext.Users.Remove(user);
                        myContext.SaveChanges();
                        myContext.Staffs.Remove(Staff);
                        myContext.SaveChanges();
                        return 0;
                    }
                    myContext.Staffs.Remove(Staff);
                    myContext.SaveChanges();
                    return 0;
                }
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            return 0;

        }
        //public ResponseLogin Login(Login login)
        //{
        //    var data = myContext.UserRoles
        //        .Include(x => x.Role)
        //        .Include(x => x.User)
        //        .Include(x => x.User.Staff)
        //        .FirstOrDefault(x => x.User.Id.Equals(login.Email));
        //    var verify = Hashing.ValidatePassword(login.Password, data.User.Password);
        //    if (verify)
        //    {
        //        if (data.Role.Id == 1)
        //        {
        //            var data1 = myContext.UserRoles
        //                        .Include(x => x.Role)
        //                        .Include(x => x.User)
        //                        .Include(x => x.User.Admin)
        //                        .FirstOrDefault(x => x.User.Name.Equals(login.Email));
        //            var Verify = Hashing.ValidatePassword(login.Password, data.User.Password);
        //            if (verify)
        //                if (data1 != null)
        //                {
        //                    var respon = new ResponseLogin()
        //                    {
        //                        Id = data1.User.Id,
        //                        IdRole = data1.Role.Id,
        //                        Name = data1.User.Admin.Name,
        //                        Role = data1.Role.Name
        //                    };
        //                    return respon;
        //                }
        //        }
        //        else if (data.Role.Id == 2)
        //        {
        //            var data1 = myContext.UserRoles
        //                       .Include(x => x.Role)
        //                       .Include(x => x.User)
        //                       .Include(x => x.User.Staff)
        //                       .FirstOrDefault(x => x.User.Email.Equals(login.Email));
        //            var Verify = Hashing.ValidatePassword(login.Password, data.User.Password);
        //            if (verify)
        //                if (data1 != null)
        //                {
        //                    var respon = new ResponseLogin()
        //                    {
        //                        Id = data1.User.StaffId,
        //                        IdRole = data1.Role.Id,
        //                        Name = data1.User.Staff.Name,
        //                        Role = data1.Role.Name
        //                    };
        //                    return respon;
        //                }
        //        }
        //    }
        //    return null;

        //}
        public ResponseLogin Login(Login login)
        {
            var data = myContext.UserRoles
                .Include(x => x.Role)
                .Include(x => x.User)
                .Include(x => x.User.Staff)
                .FirstOrDefault(x => x.User.Staff.Email.Equals(login.Email));
            var verify = Hashing.ValidatePassword(login.Password, data.User.Password);

            if (verify)
            {
                var response = new ResponseLogin()
                {
                    Id = data.User.Staff.Id_Staff,
                    Name = data.User.Staff.Name,
                    Email = data.User.Staff.Email,
                    Role = data.Role.Name
                };
                return response;
            }
            //else if (verify)
            //{
            //    var data1 = myContext.UserRoles
            //        .Include(x => x.Role)
            //        .Include(x => x.User)
            //        .Include(x => x.User.Admin)
            //        .FirstOrDefault(x => x.User.Admin.Email.Equals(login.Email)
            //        );
                
            //}
            return null;
        }

    }
}