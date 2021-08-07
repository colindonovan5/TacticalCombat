using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TacticalCombat
{
    public class Luna : Character, ICharacter
    {
        public Luna() : base()
        {
            CharacterName = "Luna";
            Range = 3;
            MaxHealth = 15;
            Health = MaxHealth;
            Damage = 1;
            BaseSpeed = 3;
            Speed = BaseSpeed;
            SpeedColor = Brushes.Aqua;
            TileColor = Brushes.MediumAquamarine;

        }
    }
}
