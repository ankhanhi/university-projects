using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_Lab3_PIN_24_Khan_Anna
{
    public class ResearchTeamCollection<TKey>
    {
        private Dictionary<TKey, ResearchTeam> dictionary = new Dictionary<TKey, ResearchTeam>();
        private KeySelector<TKey> keySelector;

        public IEnumerable<KeyValuePair<TKey, ResearchTeam>> TimeFrameGroup(TimeFrame value)
        {
            return dictionary.Where(iterator => iterator.Value.Duration == value);
        }

        public IEnumerable<IGrouping<TimeFrame, KeyValuePair<TKey, ResearchTeam>>> 
            KeyValuePairsSortDuration
        {
            get
            {
                return dictionary.GroupBy(iterator => iterator.Value.Duration);
            }
        }

        public ResearchTeamCollection(KeySelector<TKey> _keySelector)
        {
            keySelector = _keySelector;
            dictionary = new Dictionary<TKey, ResearchTeam>();
        }

        public void AddDefaults()
        {
            var rt = new ResearchTeam("Topic1", 132, TimeFrame.Long, new DateTime(1999, 2, 3), new Team("Team1", "GameCompany", 7));
            var rt1 = new ResearchTeam("Topic2", 117, TimeFrame.TwoYears, new DateTime(2003, 1, 1), 
                new Team("Team2", "TheGreatLibrary", 7));
            var rt2 = new ResearchTeam("Topic3", 211, TimeFrame.Long, new DateTime(2012, 1, 1), 
                new Team("Team3", "Medical University", 7));

            rt.AddPublications(
                new Paper("Semiconductors", new Person("Maria", "Volkova", DateTime.Now), new DateTime(2021, 6, 13)),
                new Paper("CS the best", new Person("Diana", "Jons", DateTime.Now), new DateTime(2021, 1, 10)),
                new Paper("Northern lights", new Person("Philip", "Pullman", DateTime.Now), new DateTime(1991, 1, 3))
                );
            rt1.AddPublications(
                new Paper("Strazha!", new Person("Terry", "Pratchet", DateTime.Now), new DateTime(2021, 5, 13)),
                new Paper("Shnyr", new Person("Dmitry", "Emetc", DateTime.Now), new DateTime(2021, 1, 10))
                );
            rt2.AddPublications(
                new Paper("Hotel", new Person("Artur", "Heily", DateTime.Now), new DateTime(2021, 5, 13)),
                new Paper("1984", new Person("George", "Orwell", DateTime.Now), new DateTime(1908, 1, 10))
                );

            dictionary.Add(keySelector(rt), rt);
            dictionary.Add(keySelector(rt1), rt1);
            dictionary.Add(keySelector(rt2), rt2);
        }

        public void AddResearchTeams(params ResearchTeam[] researchTeams)
        {
            foreach (var rt in researchTeams)
            {
                dictionary.Add(keySelector(rt), rt);
            }
        }

        public override string ToString()
        {
            string resultString = "";
            foreach(KeyValuePair<TKey, ResearchTeam> researchTeam in dictionary)
            {
                string string_publications = "";
                foreach (Paper paper in researchTeam.Value.Publications)
                {
                    string_publications = string_publications + paper.ToString() + "\n";
                }
                string string_members = "";
                foreach (Person person in researchTeam.Value.Members)
                {
                    string_members = string_members + person.ToString() + "\n";
                }
                resultString+= ("***\n'" + researchTeam.Value.Topic + "' " + researchTeam.Value.Organization 
                    + " " + researchTeam.Value.Number + " " + researchTeam.Value.Duration.ToString() +
                    "\n\nСписок публикаций:\n\n" + string_publications + "\nСписок сотрудников:\n\n" +
                    string_members + "\n");
            }
            return resultString;
            
        }

        public virtual string ToShortString()
        {
            string resultString = "";
            foreach (KeyValuePair<TKey, ResearchTeam> researchTeam in dictionary)
            {
                resultString += researchTeam.Value.ToShortString();
            }
            return resultString;
                
        }

        public Paper LastPublicationCollection
        {
            get
            {
                if (dictionary.Count == 0)
                    return null;
                else
                {
                    List<Paper> papers = new List<Paper>();
                    foreach (var researchTeam in dictionary)
                    {
                        papers.Add(researchTeam.Value.LastPublication);
                    }
                    Paper PublicationsValue = papers[0];
                    for (int i = 0; i < papers.Count; i++)
                    {
                        if (DateTime.Compare((papers[i]).DataPublication, PublicationsValue.DataPublication) > 0)
                        {
                            PublicationsValue = papers[i];
                        }
                    }
                    return PublicationsValue;
                }
                
            }

        }

        


    }
}
