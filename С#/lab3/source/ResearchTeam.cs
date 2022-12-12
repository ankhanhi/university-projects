using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CS_Lab3_PIN_24_Khan_Anna
{
    public delegate TKey KeySelector<TKey>(ResearchTeam rt);
    public class ResearchTeam : Team, INameAndCopy
    {
        

        string topic;
        TimeFrame duration;
        List<Person> members;
        List<Paper> publications;
        Team team = new Team();

        public IEnumerator GetEnumerator()
        {
            return new ResearchTeamEnumerator(this);
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
        public List<Person> Members
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
        public List<Paper> Publications
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
                return new Team(Name, Organization, Number);
            }
            set
            {
                team.Name = value.Name;
                team.Organization = value.Organization;
                team.Number = value.Number;
            }
        }

        public ResearchTeam() : this("NameResearch", 1, TimeFrame.TwoYears, new DateTime(1, 1, 1), new Team()) { }
        public ResearchTeam(string TopicValue, int NumberValue, TimeFrame DurationValue, DateTime DataPublicationValue, 
            Team team)
        {
            Topic = TopicValue;
            Duration = DurationValue;
            Number = NumberValue;
            Members = new List<Person>();
            Publications = new List<Paper>();
            Team = team;
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
            return ("\n***\n'" + Topic + "' " + Organization + " " + Number + " " + Duration.ToString() +
                "\n\nСписок публикаций:\n\n" + string_publications + "\nСписок сотрудников:\n\n" +
                string_members + "\n");
        }
        public virtual string ToShortString()
        {
            return ("'" + Topic + "' " + Organization + " " + Number + " " + Duration.ToString());
        }
        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                
                return (Topic == (obj as ResearchTeam).Topic) && (Organization == (obj as ResearchTeam).Organization)
                    && (Number == (obj as ResearchTeam).Number) && (Duration == (obj as ResearchTeam).Duration)
                    && Members.Equals(Members) && Publications.Equals(Publications) 
                    && Team.Equals((obj as ResearchTeam).Team);
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
            return (research1.Topic == research2.Topic) && (research1.Organization == research2.Organization)
                && (research1.Number == research2.Number) && (research1.Duration == research2.Duration)
                && research1.Members.Equals(research2.Members) && research1.Publications.Equals(research2.Publications) 
                && research1.Team.Equals(research2.Team);
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
                copy_example.Publications.Add((Paper)(Publications[i]).DeepCopy());
            }
            copy_example.Members.Capacity = Members.Capacity;

            for (int i = 0; i < Members.Count; i++)
            {
                copy_example.Members.Add(Members[i].DeepCopy());
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

        public void SortPublicationsByData()
        {
            Publications.Sort(new DataPublicationComparer());
        }

        public void SortPublicationsByTitle()
        {
            Publications.Sort(new TitleComparer());
        }
        public void SortPublicationsBySurnameAuthor()
        {
            Publications.Sort(new SurnameAuthorComparer());
        }



    }


}
