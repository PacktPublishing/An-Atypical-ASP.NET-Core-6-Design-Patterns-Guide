using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NinjaISP
{
    public class Barrel : IAttackable
    {
        public string Name => nameof(Barrel);
        public Vector2 Position { get; set; }
    }
}
