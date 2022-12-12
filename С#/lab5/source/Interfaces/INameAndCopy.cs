using System;
using System.Collections.Generic;
using System.Text;

namespace CS_Lab5_PIN_24_Khan_Anna
{
    public interface INameAndCopy
    {
        string Name { get; set; }
        object Copy();
    }
}
