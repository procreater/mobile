using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CordovaSPA
{
    public class Warrior
    {
        public Warrior()
        {
            Id = 1;
        }

        public Warrior(int id, string name, int experience )
        {
            Id = id;
            Name = name;
            Experience = experience;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Experience { get; private set; }
    }
}