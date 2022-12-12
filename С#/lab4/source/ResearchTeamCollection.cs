using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CS_Lab4_PIN_24_Khan_Anna
{
    public delegate TKey KeySelector<TKey>(ResearchTeam rt);
    public delegate void ResearchTeamsChangedHandler<TKey>(object source, ResearchTeamsChangedEventArgs<TKey> args);
    public class ResearchTeamCollection<TKey>
    {
        public string CollectionName { get; set; }

        Dictionary<TKey, ResearchTeam> collection = new Dictionary<TKey, ResearchTeam>();
        KeySelector<TKey> keySelector;

        public event ResearchTeamsChangedHandler<TKey> ResearchTeamsChanged;

        void ControlPropertyChanging(object source, PropertyChangedEventArgs args)
        {
            ResearchTeamsChanged.Invoke(this, new ResearchTeamsChangedEventArgs<TKey>(CollectionName, 
                Revision.Property, args.PropertyName, (source as ResearchTeam).Number));
        }
        public ResearchTeamCollection(KeySelector<TKey> keySelector)
        {
            this.keySelector = keySelector;
        }

        public bool Replace(ResearchTeam rtold, ResearchTeam rtnew)
        {
            if (!collection.Values.Contains(rtold))
                return false;

            TKey tempKey = default(TKey);
            foreach (var pair in collection)
                if (pair.Value == rtold)
                {
                    pair.Value.PropertyChanged -= ControlPropertyChanging;
                    tempKey = pair.Key;
                    rtnew.PropertyChanged += ControlPropertyChanging;
                }

            collection[tempKey] = rtnew;
            ResearchTeamsChanged.Invoke(this, new ResearchTeamsChangedEventArgs<TKey>(CollectionName, 
                Revision.Replace, "", rtold.Number));

            return true;
        }


        public bool Remove(ResearchTeam rt)
        {
            if (!collection.Values.Contains(rt))
                return false;

            foreach (var pair in collection)
                if (pair.Value == rt)
                {
                    pair.Value.PropertyChanged -= ControlPropertyChanging;
                    collection.Remove(pair.Key);
                    ResearchTeamsChanged.Invoke(this, new ResearchTeamsChangedEventArgs<TKey>(CollectionName, 
                        Revision.Remove, "", rt.Number));
                    break;
                }

            return true;
        }


        public IEnumerable<KeyValuePair<TKey, ResearchTeam>> TimeFrameGroup(TimeFrame value)
        {
            return collection.Where(iterator => iterator.Value.Duration == value);
        }

        public IEnumerable<IGrouping<TimeFrame, KeyValuePair<TKey, ResearchTeam>>>
            KeyValuePairsSortDuration
        {
            get
            {
                return collection.GroupBy(iterator => iterator.Value.Duration);
            }
        }

        public void Add(params ResearchTeam[] newTeams)
        {
            foreach (var team in newTeams)
            {
                team.PropertyChanged += ControlPropertyChanging;
                collection.Add(keySelector(team), team);

                ResearchTeamsChanged.Invoke(this, new ResearchTeamsChangedEventArgs<TKey>(CollectionName, 
                    Revision.Property, "AddResearchTeams", team.Number));
            }
        }


        public override string ToString()
        {
            string resultString = "";
            foreach (KeyValuePair<TKey, ResearchTeam> researchTeam in collection)
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
                resultString += ("***\n'" + researchTeam.Value.Topic + "' " + researchTeam.Value.Organization
                    + " " + researchTeam.Value.Number + " " + researchTeam.Value.Duration.ToString() +
                    "\n\nСписок публикаций:\n\n" + string_publications + "\nСписок сотрудников:\n\n" +
                    string_members + "\n");
            }
            return resultString;

        }

        public virtual string ToShortString()
        {
            string resultString = "";
            foreach (KeyValuePair<TKey, ResearchTeam> researchTeam in collection)
            {
                resultString += researchTeam.Value.ToShortString();
            }
            return resultString;

        }

        public Paper LastPublicationCollection
        {
            get
            {
                if (collection.Count == 0)
                    return null;
                else
                {
                    List<Paper> papers = new List<Paper>();
                    foreach (var researchTeam in collection)
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
