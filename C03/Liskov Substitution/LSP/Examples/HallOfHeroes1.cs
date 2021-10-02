using LSP.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LSP.Examples.Update1
{
    public class HallOfHeroes : HallOfFame
    {
        public override void Add(Ninja ninja)
        {
            if (InternalMembers.Contains(ninja))
            {
                return;
            }
            InternalMembers.Add(ninja);
        }
    }
}
