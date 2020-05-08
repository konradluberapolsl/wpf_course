using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace players_mvvm.Model
{
    class Player
    {
        private string name = "";
        public string Name { get { return name; } set { name = value; } }
        private string surname = "";
        public string Surname { get { return surname; } set { surname = value; } }
        private double weight = 0;
        public double Weight { get { return weight; } set { weight = value; } }
        private double age = 0;
        public double Age { get { return age; } set { age = value; } }

        public Player()
        {

        }
        public Player(string name, string surname, double weight, double age)
        {
            this.age = age;
            this.name = name;
            this.surname = surname;
            this.weight = weight;
        }
        public override string ToString()
        {
            return name + " " + surname + " " + weight.ToString("0") + " " + age.ToString("0");
        }
        public bool Equals(Player other)
        {
            if (other == null)
                return false;
            if ((this.name == other.Name) && (this.surname == other.Surname) && (this.age == other.Age) && (this.weight == other.Weight))
            {
                return true;
            }
            return false;
        }
        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            Player personObj = obj as Player;
            if (personObj == null)
                return false;
            else
                return Equals(personObj);
        }

    }
}
