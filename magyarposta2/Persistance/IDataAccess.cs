using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using magyarposta2.Model;

namespace magyarposta2.Persistance
{
    public interface IDataAccess
    {
        Task Save(string path, Package package);
        Task<List<Package>> LoadAll(string path);
    }
}
