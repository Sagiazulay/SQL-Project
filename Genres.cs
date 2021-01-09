using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil5b
{
    class Genres
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Genres()
        {

        }

        public Genres(int iD, string name)
        {
            ID = iD;
            Name = name;
        }
        public override string ToString()
        {
            return $"{Newtonsoft.Json.JsonConvert.SerializeObject(this)}";
        }
        public override bool Equals(object obj)
        {
            return this.ID == ((Genres)obj).ID;
        }

        public override int GetHashCode()
        {
            return (int)ID;
        }
        public static bool operator ==(Genres c1, Genres c2)
        {
            return c1.ID == c2.ID;
        }
        public static bool operator !=(Genres c1, Genres c2)
        {
            return !(c1 == c2);
        }
    }
}
