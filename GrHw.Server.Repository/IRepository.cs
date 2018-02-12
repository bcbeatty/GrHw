using System;
using System.Collections.Generic;
using GrHw.Server.Domain;

namespace GrHw.Server.Repository
{
    public interface IRepository<T>  where T:class
    {
        T Save(T item);
        List<T> GetList();
    }
}
