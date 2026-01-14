using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using magyarposta2.Model;
using magyarposta2.ViewModels;

namespace magyarposta2.Persistance
{
    public class FileDataAccess : IDataAccess
    {
        public async Task<List<Package>> LoadAll(string path)
        {
            List<Package> packages = new List<Package>();
            using (StreamReader reader = new StreamReader(path))
            {
                try
                {
                    while (!reader.EndOfStream)
                    {
                        string line = await reader.ReadLineAsync();
                        string[] parts = line.Split(';');
                        Package package = new Package(
                            int.Parse(parts[0]),
                            parts[1],
                            DateOnly.Parse(parts[2]),
                            parts[3],
                            parts[4],
                            parts[5],
                            int.Parse(parts[6]),
                            int.Parse(parts[7])
                        );
                        packages.Add(package);
                    }
                }
                catch (Exception e)
                {

                }
            }
            return packages;
        }

        public async Task Save(string path, List<Package> packages)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                try
                {
                    foreach (Package package in packages)
                    {
                        string line = package.ToString();
                        await writer.WriteLineAsync(line);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        public async Task SaveOnDeleted(string path, List<Package> packages)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                try
                {
                    foreach (Package package in packages)
                    {
                        if (package.Status == "Torolve")
                        {
                            
                            string line = package.ToString();
                            await writer.WriteLineAsync(line);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        public async Task SaveOnlyOnArrived(string path, List<Package> packages)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                try
                {
                    foreach (Package package in packages)
                    {
                        if (package.Status == "Kiszallitva")
                        {
                            string line = package.ToString();
                            await writer.WriteLineAsync(line);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        public async Task SaveOnlyOnProcessing(string path, List<Package> packages)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                try
                {
                    foreach (Package package in packages)
                    {
                        if (package.Status == "Feldolgozas alatt")
                        {
                            string line = package.ToString();
                            await writer.WriteLineAsync(line);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        public async Task SaveOnlyOnArrive(string path, List<Package> packages)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                try
                {
                    foreach (Package package in packages)
                    {
                        if (package.Status == "Kiszallitas alatt")
                        {
                            string line = package.ToString();
                            await writer.WriteLineAsync(line);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
