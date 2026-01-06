using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace magyarposta2.Model
{
    public class PackageEventArgs
    {
        public Package package { get; set; }
        public PackageEventArgs(Package package)
        {
            this.package = package;
        }
    }
}
