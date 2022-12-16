using CutiOnlineWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CutiOnlineWEB.Repositories.Interface
{
    interface ICrudRepository
    {   
        List<Crud> Get();

        Crud Get( int id ,Crud crud);

        
        int Post(Crud crud);

      
        int Put(int id, Crud crud);

      
        int Delete(Crud crud);
    }
}
