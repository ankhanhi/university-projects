using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CS_Lab3_PIN_24_Khan_Anna
{
    public class ResearchTeamEnumerator : ResearchTeam, IEnumerator
    {
        ResearchTeam researchTeam;

        int index = -1;     
        public ResearchTeamEnumerator(ResearchTeam researchTeamValue)
        {
            researchTeam = (ResearchTeam)researchTeamValue.DeepCopy();
        }
        public bool MoveNext()
        {
            return ++index < researchTeam.Publications.Count;
        }

        public object Current
        {
            get
            {
                return ((Paper)researchTeam.Publications[index]).Author;
            }
        }
        public void Reset()
        {
            index = -1;
        }

    }
}
