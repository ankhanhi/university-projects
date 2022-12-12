using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace CS_Lab5_PIN_24_Khan_Anna
{
    public delegate KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue>(int j);
    [Serializable]
    public class ResearchTeam : Team, INameAndCopy, INotifyPropertyChanged, IEnumerable
    {

        string topic;
        TimeFrame duration;
        List<Person> members = new List<Person>();
        List<Paper> publications = new List<Paper>();
        Team team = new Team();
        public event PropertyChangedEventHandler PropertyChanged;

        public ResearchTeam DeepCopy<ReserchTeam>()
        {
            try
            {
                var ms = new MemoryStream();
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Position = 0;

                return (ResearchTeam)formatter.Deserialize(ms);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResearchTeam();
            }
            
        }
        public bool Save(string filename)
        {
            try
            {
                using (var fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite))
                {
                    var bf = new BinaryFormatter();
                    bf.Serialize(fs, this);
                }
                return true;
            }
            catch
            {
                Console.WriteLine("Ошибка сериализации");
                return false;
            }
            
        }
        public bool Load(string filename)
        {
            try
            {
                using (var fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite))
                {
                    if (fs.CanRead)
                    {
                        var bf = new BinaryFormatter();
                        var rt = bf.Deserialize(fs) as ResearchTeam;
                        topic = rt.topic;
                        duration = rt.duration;
                        team = (Team)rt.team.Copy();
                        Name = rt.Name;
                        Members.AddRange(rt.Members);
                        Publications.Clear();
                        Publications.AddRange(rt.Publications);
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool AddFromConsole()
        {
            bool running = true;
            string text;
            Paper paper = new Paper();
            char[] separator = { ' ', ',' };

            while (running)
            {
                Console.WriteLine("Input: Publication's title, Author's name, Author's surname, Publication's date(dd.mm.yyyy)." +
                    "\nSeparators: ' ', ','");
                text = Console.ReadLine();
                string[] data = text.Split(separator);
                paper.Title = data[0];
                paper.Author.Name = data[1];
                paper.Author.Surname = data[2];
                string[] date1 = data[3].Split(".");
                List<int> date1Int = new List<int>();

                foreach (var value in date1)
                {
                    try { 
                        date1Int.Add(int.Parse(value));
                    }
                    catch
                    {
                        Console.WriteLine($"Attempted conversion of {value} failed. Try again");
                    }
                }

                if (date1Int.Count == 3)
                {
                    paper.DataPublication = new DateTime(date1Int[2], date1Int[1], date1Int[0]);
                    AddPublications(paper);
                    break;
                }
                else Console.WriteLine("Incorrect data. Try again");
            }
            
            return true;
        }
        public ResearchTeam Copy()
        {
            ResearchTeam copy_example = new ResearchTeam();
            copy_example.topic = topic;
            copy_example.duration = duration;
            copy_example.team = (Team)team.Copy();
            copy_example.Name = Name;

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
        public static bool Save(string filename, ResearchTeam obj)
        {
            try
            {
                var formatter = new BinaryFormatter();
                using (var fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, obj);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public static bool Load(string filename, ResearchTeam obj)
        {
            try
            {
                var formatter = new BinaryFormatter();
                using (var fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    var rt = formatter.Deserialize(fs) as ResearchTeam;
                    obj.topic = rt.topic;
                    obj.duration = rt.duration;
                    obj.team = (Team)rt.team.Copy();
                    obj.Name = rt.Name;
                    obj.Members.AddRange(rt.Members);
                    obj.Publications.Clear();
                    obj.Publications.AddRange(rt.Publications);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
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
                if (topic != value)
                {
                    topic = value;
                    OnPropertyChanged("Topic");
                }
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
                if (duration != value)
                {
                    OnPropertyChanged("Duration");
                }
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
                if (members.Equals(value) == false)
                {
                    members = value;
                    OnPropertyChanged("Members");
                }
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
                if (publications.Equals(value) == false)
                    if (PropertyChanged != null)
                    {
                        publications = value;
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Publications"));
                    }
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

        public ResearchTeam() : this("NameResearch", 1, "DefaultTeamName", TimeFrame.TwoYears, new DateTime(1, 1, 1), new Team()) { }
        public ResearchTeam(string TopicValue, int NumberValue, string TeamName, TimeFrame DurationValue, DateTime DataPublicationValue,
            Team team)
        {
            Topic = TopicValue;
            Duration = DurationValue;
            Number = NumberValue;
            Members = new List<Person>();
            Publications = new List<Paper>();
            Team = team;
            Name = TeamName;
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
                Duration.GetHashCode() + Publications.GetHashCode() +
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
