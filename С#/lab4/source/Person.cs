using System;
using System.Diagnostics;

namespace CS_Lab4_PIN_24_Khan_Anna
{
    public class Person
    {
        string name;
        string surname;
        DateTime birthday;

        public Person(string Name, string Surname, DateTime DataBirth)
        {
            name = Name;
            surname = Surname;
            birthday = DataBirth;
        }
        public Person() : this("Ivan", "Ivanov", DateTime.MinValue) { }

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
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
            }
        }
        public DateTime Birthday
        {
            get
            {
                return birthday;
            }
            set
            {
                birthday = value;
            }
        }
        public int Year
        {
            get
            {
                return Birthday.Year;
            }
            set
            {
                Birthday = new DateTime(value, Birthday.Month, Birthday.Day);
            }
        }
        public override string ToString()
        {
            return Name + " " + Surname;
        }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Person person = (Person)obj;
                return (Name == person.Name) && (Surname == person.Surname)
                    && (DateTime.Compare(Birthday, person.Birthday) == 0);
            }
        }

        public override int GetHashCode()
        {
            return (Name.GetHashCode() + Surname.GetHashCode() + Birthday.GetHashCode());
        }
        public static bool operator ==(Person person1, Person person2)
        {
            return (person1.Name == person2.Name) && (person1.Surname == person2.Surname) &&
                (DateTime.Compare(person1.Birthday, person2.Birthday) == 0);
        }
        public static bool operator !=(Person person1, Person person2)
        {
            return !(person1 == person2);
        }

        public virtual Person DeepCopy()
        {
            Person copy_example = new Person(Name, Surname, Birthday);

            return copy_example;
        }


    }
}
