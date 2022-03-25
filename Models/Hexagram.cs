using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I_Ching.Models
{
    public class Hexagram 
    {
        public Trigram LowerTrigram { get; private set; }
        public Trigram UpperTrigram { get; private set; }

        public bool HasChangable
        {
            get => LowerTrigram.CanChange || UpperTrigram.CanChange;
        }

        public Hexagram(Trigram lower, Trigram upper)
        {
            LowerTrigram = lower;
            UpperTrigram = upper;
        }
        public static Hexagram GetRandom()
        {
            Random random = new Random();
            return new Hexagram(Trigram.GetRandom(), Trigram.GetRandom());
        }
        public void Change()
        {
            LowerTrigram = Trigram.Change(LowerTrigram);
            UpperTrigram = Trigram.Change(UpperTrigram);
        }

    }
}
