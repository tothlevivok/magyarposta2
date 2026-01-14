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
        public event EventHandler<PackageEventArgs>? PackageDeleted;

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
            packages.Clear();
            List<Package> newPackages = await DataAccess.LoadAll(path);
            foreach (Package p in newPackages)
            {
                packages.Add(p);
                PackageAdded?.Invoke(this, new PackageEventArgs(p));
            }
        }


        public void DeletePackage(Package package)
        {
            packages.Remove(package);
            PackageDeleted?.Invoke(this, new PackageEventArgs(package));
        }

        public async Task Save(string path)
        {
            await DataAccess.Save(path, packages);
        }

        public async Task SaveOnDeleted(string path)
        {
            await DataAccess.SaveOnDeleted(path, packages);
        }

        public async Task SaveOnlyOnArrived(string path)
        {
            await DataAccess.SaveOnlyOnArrived(path, packages);
        }

        public async Task SaveOnlyOnProcessing(string path)
        {
            await DataAccess.SaveOnlyOnProcessing(path, packages);
        }

        public async Task SaveOnlyOnArrive(string path)
        {
            await DataAccess.SaveOnlyOnArrive(path, packages);
        }
    }
}
