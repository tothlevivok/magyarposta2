using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace magyarposta2.Model
{
    public class Package
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly SentDate { get; set; }
        public string SentFrom { get; set; }
        public string Destination { get; set; }
        public string Status { get; set; }
        public int Price { get; set; }
        public int DaysToArrive { get; set; }

        public Package(int id, string name, DateOnly sentDate, string sentFrom, string destination, string status, int price, int daysToArrive)
        {
            Id = id;
            Name = name;
            SentDate = sentDate;
            SentFrom = sentFrom;
            Destination = destination;
            Status = status;
            Price = price;
            DaysToArrive = daysToArrive;
        }

        public override string ToString()
        {
            return $"{Id};{Name};{SentDate};{SentFrom};{Destination};{Status};{Price};{DaysToArrive}";
        }
    }
}
