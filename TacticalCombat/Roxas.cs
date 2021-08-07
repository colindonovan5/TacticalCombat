using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace TacticalCombat
{
    public class Roxas : Character, ICharacter
    {

        public Roxas() : base()
        {
            CharacterName = "Roxas";
            Range = 4;
            MaxHealth = 10;
            Health = MaxHealth;
            Damage = 1;
            BaseSpeed = 2;
            Speed = BaseSpeed;
            SpeedColor = Brushes.Orange;
            TileColor = Brushes.DarkOrange;
        }
    }
}
