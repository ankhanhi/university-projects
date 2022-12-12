using System;
using System.Collections.Generic;
using System.Text;

namespace CS_Lab4_PIN_24_Khan_Anna
{
    public class ResearchTeamsChangedEventArgs<TKey> : EventArgs
    {
        public string CollectionName { get; set; }
        public Revision TypeEvent { get; set; }
        public string PropertyName { get; set;}
        public int NumberChanged { get; set; }


        public ResearchTeamsChangedEventArgs(string collectionName, Revision causeEvant, string propertyName, int numberDeleted)
        {
            CollectionName = collectionName;
            TypeEvent = causeEvant;
            PropertyName = propertyName;
            NumberChanged = numberDeleted;
        }

        public override string ToString()
        {
            string result = "";
            result = CollectionName + " " + TypeEvent.ToString() + " " + PropertyName + " " + NumberChanged.ToString() + "\n";
            return result;
        }
    }
}
