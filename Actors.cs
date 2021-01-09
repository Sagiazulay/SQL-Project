using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil5b
{
    class Actors
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }

        public Actors()
        {

        }

        public Actors(int iD, string name, DateTime birthday)
        {
            ID = iD;
            Name = name;
            Birthday = birthday;
        }
        public override string ToString()
        {
            return $"{Newtonsoft.Json.JsonConvert.SerializeObject(this)}";
        }
        public override bool Equals(object obj)
        {
            return this.ID == ((Actors)obj).ID;
        }

        public override int GetHashCode()
        {
            return (int)ID;
        }
        public static bool operator ==(Actors c1, Actors c2)
        {
            return c1.ID == c2.ID;
        }
        public static bool operator !=(Actors c1, Actors c2)
        {
            return !(c1 == c2);
        }
    }
}
