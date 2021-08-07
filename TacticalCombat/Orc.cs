using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TacticalCombat
{
    public class Orc : Enemy, ICharacter
    {
        public Orc()
        {
            CharacterName = "Orc";
            Range = 4;
            MaxHealth = 21;
            Health = MaxHealth;
            Damage = 1;
            BaseSpeed = 3;
            Speed = BaseSpeed;
            TileColor = Brushes.Yellow;
        }
    }
}
