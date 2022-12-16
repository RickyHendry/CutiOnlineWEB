using Microsoft.EntityFrameworkCore;
using CutiOnlineWEB.Context;
using CutiOnlineWEB.Models;
using CutiOnlineWEB.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CutiOnlineWEB.Repositories.Data
{
    public class CrudRepository : ICrudRepository
    {
        MyContext myContext;

        public CrudRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public int Delete(Crud crud)
        {
            myContext.Cruds.Remove(crud);
            var result = myContext.SaveChanges();
            return result;
        }

        public List<Crud> Get()
        {
            var data = myContext.Cruds.Include(x => x.RCrud).ToList();
            return data;
        }

        public Crud Get(int id, Crud crud)
        {
            var data = myContext.Cruds.Include(x => x.RCrud).Where(x => x.Id.Equals(id)).FirstOrDefault();
            return data;
        }

        public int Post(Crud crud)
        {
            myContext.Cruds.Add(crud);
            var result = myContext.SaveChanges();
            return result;

        }

        public int Put(int id, Crud crud)
        {
            var data = myContext.Cruds.Find(id, crud);
            data.Name = crud.Name;
            data.RcrudId = crud.RcrudId;
            //myContext.Provinces.Update(data);
            myContext.Cruds.Update(data);
            var result = myContext.SaveChanges();
            return result;
        }
    }
}
