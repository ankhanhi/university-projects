using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CS_Lab1_PIN_24_Khan_Anna
{
    class ResearchTeam : Team, INameAndCopy
    {
        string topic;
        TimeFrame duration;
        ArrayList members;
        ArrayList publications;
        Team team;
        public IEnumerator GetEnumerator()
        {
            return new ResearchTeamEnumerator(this);
        }

        public class ResearchTeamEnumerator : IEnumerator
        {
            ResearchTeam researchTeam;
            int index = -1;
            public ResearchTeamEnumerator(ResearchTeam researchTeamValue)
            {
                researchTeam = (ResearchTeam)researchTeamValue.DeepCopy();
            }
            public bool MoveNext()
            {
                return ++index < researchTeam.publications.Count;
            }

            public object Current
            {
                get
                {
                    return ((Paper)researchTeam.publications[index]).Author;
                }
            }
            public void Reset()
            {
                index = -1;
            }

        }

        public string Topic
        {
            get
            {
                return topic;
            }
            set
            {
                topic = value;
            }
        }
        public TimeFrame Duration
        {
            get
            {
                return duration;
            }
            set
            {
                duration = value;
            }
        }
        public ArrayList Members
        {
            get
            {
                return members;
            }
            set
            {
                members = value;
            }
        }
        public ArrayList Publications
        {
            get
            {
                return publications;
            }
            set
            {
                publications = value;
            }
        }
        public Team Team
        {
            get
            {
                return new Team(Organization, Number);
            }
            set
            {
                team.Organization = value.Organization;
                team.Number = value.Number;
            }
        }

        public ResearchTeam() : this("Ivan", "Ivanov", 1, TimeFrame.Year, new DateTime(1, 1, 1)) { }
        public ResearchTeam(string TopicValue, string OrganizationValue, int NumberValue, 
            TimeFrame DurationValue, DateTime DataPublicationValue)
        {
            Topic = TopicValue;
            Organization = OrganizationValue;
            Number = NumberValue;
            Members = new ArrayList();
            Publications = new ArrayList();
            team = new Team();
        }

        

        public Paper LastPublication
        {
            get
            {
                if (Publications.Count == 0)
                    return null;
                else
                {
                    Paper PublicationsValue = (Paper)Publications[0];
                    for (int i = 0; i < Publications.Count; i++)
                    {
                        if (DateTime.Compare(((Paper)Publications[i]).DataPublication, PublicationsValue.DataPublication) > 0)
                        {
                            PublicationsValue = (Paper)Publications[i];
                        }
                    }
                    return PublicationsValue;
                }
            }
        }

        public void AddMembers(params Person[] NewMembers)
        {
            Members.AddRange(NewMembers);
        }

        public void AddPublications(params Paper[] NewPublications)
        {
            Publications.AddRange(NewPublications);
        }

        public override string ToString()
        {
            string string_publications = "";
            foreach (Paper paper in Publications)
            {
                string_publications = string_publications + paper.ToString() + "\n";
            }
            string string_members = "";
            foreach (Person person in Members)
            {
                string_members = string_members + person.ToString() + "\n";
            }
            return ("{" + Topic + " " + Organization + " " + Number + " " + Duration + 
                "\n\nСписок публикаций:\n\n" + string_publications + "\nСписок сотрудников:\n\n" +
                string_members + "}");
        }

        public virtual string ToShortString()
        {
            return (Topic + " " + Organization + " " + Number + " " + Duration);
        }


        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                bool isEqualPublications = true;

                for (int i = 0; i < Members.Count; i++)
                {
                    if (((Paper)Publications[i]).Equals((Paper)((obj as ResearchTeam).Publications[i])) == false)
                    {
                        isEqualPublications = false;
                        break;
                    }
                }

                bool isEqualMembers = true;

                for (int i = 0; i < Members.Count; i++)
                {
                    if (((Person)Members[i]).Equals((Person)((obj as ResearchTeam).Members[i])) == false)
                    {
                        isEqualMembers = false;
                        break;
                    }                      
                }              

                return (Topic == (obj as ResearchTeam).Topic) && (Organization == (obj as ResearchTeam).Organization) 
                    && (Number == (obj as ResearchTeam).Number) && (Duration == (obj as ResearchTeam).Duration) 
                    && isEqualPublications && isEqualMembers && Team.Equals((obj as ResearchTeam).Team);
            }
        }

        public override int GetHashCode()
        {
            int hash = Topic.GetHashCode() + Organization.GetHashCode() + Number.GetHashCode() + 
                Duration.GetHashCode() + Publications.GetHashCode() + Members.GetHashCode() +
                Team.GetHashCode();

            return hash;
        }

        public static bool operator ==(ResearchTeam research1, ResearchTeam research2)
        {

            bool isEqualPublications = true;

            for (int i = 0; i < research1.Publications.Count; i++)
            {
                if (((Paper)(research1.Publications[i])).Equals((Paper)(research2.Publications[i])) == false)
                {
                    isEqualPublications = false;
                    break;
                }

            }

            bool isEqualMembers = true;

            for (int i = 0; i < research1.Members.Count; i++)
            {
                if (((Person)(research1.Members[i])).Equals((Person)(research2.Members[i])) == false)
                {
                    isEqualMembers = false;
                    break;
                }

            }
            return (research1.Topic == research2.Topic) && (research1.Organization == research2.Organization)
                && (research1.Number == research2.Number) && (research1.Duration == research2.Duration) 
                && isEqualPublications && isEqualMembers && research1.Team.Equals(research2.Team);
        }
        public static bool operator !=(ResearchTeam research1, ResearchTeam research2)
        {
            return !(research1 == research2);
        }

        public virtual new object DeepCopy()
        {
            ResearchTeam copy_example = new ResearchTeam();
            copy_example.Topic = Topic;
            copy_example.Duration = Duration;
            copy_example.Team = (Team)Team.DeepCopy();

            copy_example.Publications.Capacity = Publications.Capacity;

            for (int i = 0; i < Publications.Count; i++)
            {
                copy_example.Publications.Add(((Paper)Publications[i]).DeepCopy());
            }
            copy_example.Members.Capacity = Members.Capacity;

            for (int i = 0; i < Members.Count; i++)
            {
                copy_example.Members.Add(((Person)Members[i]).DeepCopy());
            }

            return copy_example;
        }


        public IEnumerable<Person> GetMembersMoreOnePublications()
        {
            int count_publications;
            foreach (Person person in Members)
            {
                count_publications = 0;
                foreach (Paper paper in Publications)
                {
                    if (person == paper.Author)
                    {
                        count_publications++;
                    }
                }
                if (count_publications > 1)
                {
                    yield return person;
                }
            }
        }

        public IEnumerable<Paper> GetPublicationsLastYear()
        {
            foreach (Paper paper in Publications)
            {
                if (paper.DataPublication.Year == DateTime.Today.Year)
                {
                    yield return paper;
                }
            }
        }

        public IEnumerable<Person> GetMembersNotAuthors()
        {
            bool isAuthor;
            foreach (Person person in Members)
            {
                isAuthor = false;
                foreach (Paper paper in Publications)
                {
                    if (person == paper.Author)
                    {
                        isAuthor = true;
                        break;
                    }
                }
                if (isAuthor == false) yield return person;
            }
        }
        public IEnumerable<Paper> GetPublicationsLastNYears(int n)
        {
            foreach (Paper paper in Publications)
            {
                if (paper.DataPublication.Year >= 2021 - n)
                {
                    yield return paper;
                }
            }
        }

    }

    
}
