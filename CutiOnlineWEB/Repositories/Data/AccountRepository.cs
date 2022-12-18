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
                Employee employee = new Employee()
                {
                    Name = register.Name,
                    Email = register.Email
                };
                myContext.Employees.Add(employee);

                var resultEmployee = myContext.SaveChanges();
                if (resultEmployee > 0)
                {
                    int id = myContext.Employees.SingleOrDefault(x => x.Email.Equals(register.Email)).Id;
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
                        myContext.Employees.Remove(employee);
                        myContext.SaveChanges();
                        return 0;
                    }
                    myContext.Employees.Remove(employee);
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
        public ResponseLogin Login(Login login)
        {
            var data = myContext.UserRoles
                .Include(x => x.Role)
                .Include(x => x.User)
                .Include(x => x.User.Employee)
                .FirstOrDefault(x => x.User.Employee.Email.Equals(login.Email));
            var verify = Hashing.ValidatePassword(login.Password, data.User.Password);

            if (verify)
            {
                var response = new ResponseLogin()
                {
                    Id = data.User.Employee.Id,
                    FullName = data.User.Employee.Name,
                    Email = data.User.Employee.Email,
                    Role = data.Role.Name
                };
                return response;
            }
            return null;
        }

        //public ResponseLogin Login(Login login)
        //{
        //    var data = myContext.UserRoles
        //        .Include(x => x.Role)
        //        .Include(x => x.User)
        //        .FirstOrDefault(x => x.User.Employee.Equals(login.Email));
        //    var Verify = Hashing.ValidatePassword(login.Password, data.User.Password);
        //    if (Verify)
        //    {
        //        if (data.Role.Id == 1)
        //        {
        //            var data1 = myContext.UserRoles
        //                        .Include(x => x.Role)
        //                        .Include(x => x.User)
        //                        .Include(x => x.User.Admin)
        //                        .FirstOrDefault(x => x.User.Admin.Equals(login.Email));
        //            var verify = Hashing.ValidatePassword(login.Password, data.User.Password);
        //            if (Verify)
        //                if (data1 != null)
        //                {
        //                    var respon = new ResponseLogin()
        //                    {
        //                        Id = data1.User.Id,
        //                        IdRole = data1.Role.Id,
        //                        FullName = data1.User.Admin.Email,
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
        //                       .Include(x => x.User.Employee)
        //                       .FirstOrDefault(x => x.User.Employee.Equals(login.Email));
        //            var verify = Hashing.ValidatePassword(login.Password, data.User.Password);
        //            if (Verify)
        //                if (data1 != null)
        //                {
        //                    var respon = new ResponseLogin()
        //                    {
        //                        Id = data1.User.Id,
        //                        IdRole = data1.Role.Id,
        //                        FullName = data1.User.Employee.Name,
        //                        Role = data1.Role.Name
        //                    };
        //                    return respon;
        //                }
        //        }
        //    }
        //    return null;

        //}
    }
}