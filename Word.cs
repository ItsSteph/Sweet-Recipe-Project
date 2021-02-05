using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    public class Word
    {
        public string Name;
        public int Amount;

        public Word(string newName, int newAmount)
        {
            Name = newName;
            Amount = newAmount;
        }

        public Word() { }

        public void SetAmount(int NewAmount)
        {
            Amount = NewAmount;

        }
        public void SetName(string NewName)
        {
            Name = NewName;
        }

    }
}