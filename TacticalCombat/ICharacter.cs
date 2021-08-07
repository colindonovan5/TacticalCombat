using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace TacticalCombat
{
    public interface ICharacter
    {
        string CharacterName { get; set; }
        int Range { get; set;  }
        int BaseSpeed { get; set; }
        int Speed { get; set; }
        int MaxHealth { get; set; }
        int Health { get; set; }
        int Damage { get; set; }
        int X { get; set; }
        int Y { get; set; }
        bool HasMoved { get; set; }
        bool UsedAction { get; set; }
        Tile CurrentTile { get; set; }
        Brush TileColor { get; set; }
        Brush RangeColor { get; set; }
        bool Dead { get; set; }

        int GetHealth();
        int GetDamage();
        int GetRange();
        string GetName();
        void Hurt();
        void UpdateLocation();
        void Heal();
        void PaintSurroundings();
        void UnpaintSurroundings();
        void Die();
    }
}
