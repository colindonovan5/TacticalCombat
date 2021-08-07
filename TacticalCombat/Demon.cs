using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TacticalCombat
{
    public class Demon : Enemy, ICharacter
    {
        public Demon() : base()
        {

            CharacterName = "Demon";
            Range = 3;
            MaxHealth = 15;
            Health = MaxHealth;
            Damage = 1;
            BaseSpeed = 3;
            Speed = BaseSpeed;
            TileColor = Brushes.Yellow;
        }
    }
}
