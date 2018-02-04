using System.Collections.Generic;

namespace GrHw.Framework
{
    public interface IReport<T> where T : class
    {
        string GetReport(IList<T> list, char delimeter);
    }
}