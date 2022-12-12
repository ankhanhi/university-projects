using System;
using System.Collections.Generic;
using System.Text;

namespace CS_Lab1_PIN_24_Khan_Anna
{
    class Team : INameAndCopy
    {

        string name;
        protected string organization;
        protected int number;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }


        public Team() : this("Ivan", 0){}
        public Team(string OrganizationValue, int NumberValue)
        {
            organization = OrganizationValue;
            number = NumberValue;
        }
        public string Organization
        {
            get
            {
                return organization;
            }
            set
            {
                organization = value;
            }
        }
        public int Number
        {
            get
            {
                return number;
            }
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Номер регистрации должен быть больше нуля");
                }
                else
                {
                    number = value;
                }         
            }
        }

        public override string ToString()
        {
            return (Organization + " " + Number);
        }

        public static bool operator ==(Team team1, Team team2)
        {
            return (team1.Organization == team2.Organization) && (team1.Number == team2.Number);
        }
        public static bool operator !=(Team team1, Team team2)
        {
            return !(team1 == team2);
        }
        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Team research = (Team)obj;
                return (Organization == research.Organization) && (Number == research.Number);
            }
        }
        public override int GetHashCode()
        {
            return (Organization.GetHashCode() + Number.GetHashCode());
        }


        public object DeepCopy()
        {
            Team copy_example = new Team(Organization, Number);

            return copy_example;
        }
    }
}
