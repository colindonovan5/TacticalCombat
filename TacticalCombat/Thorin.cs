using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TacticalCombat
{
    public class Thorin : Character
    {
        public Thorin() : base()
        {
            CharacterName = "Thorin";
            Range = 2;
            MaxHealth = 12;
            Health = MaxHealth;
            Damage = 2;
            BaseSpeed = 3;
            Speed = BaseSpeed;
            SpeedColor = Brushes.LightSalmon;
            TileColor = Brushes.Salmon;
        }
    }
}
