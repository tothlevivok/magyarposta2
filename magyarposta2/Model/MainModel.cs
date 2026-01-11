using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using magyarposta2.Persistance;

namespace magyarposta2.Model
{
    public class MainModel
    {
        private List<Package> packages { get; set; }
        public IDataAccess DataAccess;
        public event EventHandler<PackageEventArgs>? PackageAdded;

        public MainModel(IDataAccess dataAccess)
        {
            DataAccess = dataAccess;
            packages = new List<Package>();
        }

        public void AddPackage(Package package)
        {
            packages.Add(package);
            PackageAdded?.Invoke(this, new PackageEventArgs(package));
        }

        public async Task Load(string path)
        {
            List<Package> newPackages = await DataAccess.LoadAll(path);
            foreach (Package p in newPackages)
            {
                packages.Add(p);
                PackageAdded?.Invoke(this, new PackageEventArgs(p));
            }
        }

        //public Package package;

        public async Task Save(string path)
        {
            DataAccess.Save(path, packages);
        }

    }
}
