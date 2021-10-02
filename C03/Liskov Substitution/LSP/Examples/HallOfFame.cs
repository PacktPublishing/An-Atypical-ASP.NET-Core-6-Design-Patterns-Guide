using LSP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace LSP.Examples
{
    public class HallOfFame
    {
        protected HashSet<Ninja> InternalMembers { get; } = new HashSet<Ninja>();

        public virtual void Add(Ninja ninja)
        {
            if (InternalMembers.Contains(ninja))
            {
                return;
            }
            if (ninja.Kills >= 100)
            {
                InternalMembers.Add(ninja);
            }
        }

        public virtual IEnumerable<Ninja> Members
            => new ReadOnlyCollection<Ninja>(
                InternalMembers
                    .OrderByDescending(x => x.Kills)
                    .ToArray()
            );
    }
}
