using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NinjaISP
{
    public class Barel : IAttackable
    {
        public string Name => nameof(Barel);
        public Vector2 Position { get; set; }
    }
}
