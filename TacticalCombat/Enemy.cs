using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace TacticalCombat
{
    public class Enemy : ICharacter
    {
        public string CharacterName { get; set; }
        public int Range { get; set; }
        public int BaseSpeed { get; set; }
        public int Speed { get; set; }
        public int MaxHealth { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool HasMoved { get; set; }
        public bool UsedAction { get; set; }
        public Tile CurrentTile { get; set; }
        public Brush TileColor { get; set; }
        public Brush RangeColor { get; set; }
        public bool Dead { get; set; }
        public Character Target { get; set; }

        public Enemy()
        {
            Random r = new Random();
            Thread.Sleep(20);
            X = r.Next(0, 21);
            Y = r.Next(0, 21);
            HasMoved = false;
            UsedAction = false;
            RangeColor = Brushes.Yellow;
            TileColor = Brushes.Yellow;
            Dead = false;
        }


        public void Attack()
        {
            int Distance = CalculateDistance(X, Y, Target.X, Target.Y);
            if (UsedAction)
            {
                MessageBox.Show(CharacterName + " already used their action this round.");
            }
            else if (Target.Dead)
            {
                Target = null;
            }
            else
            {
                if (Distance > Range)
                {
                }
                else
                {
                    Target.Health -= Damage;

                    if (Target.Health <= 0)
                    {
                        Target.Die();
                        Target = null;
                    }
                    UsedAction = true;
                }
            }



        }

        public int GetDamage()
        {
            throw new NotImplementedException();
        }

        public int GetHealth()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public int GetRange()
        {
            throw new NotImplementedException();
        }

        public void Heal()
        {
            throw new NotImplementedException();
        }

        public void Hurt()
        {
            throw new NotImplementedException();
        }

        public void Move()
        {
            Random r = new Random();
            if(Target == null)
            {
                Target = MainWindow.Characters[r.Next(0, MainWindow.Characters.Count)];
            }
            int TargetDistance = CalculateDistance(X, Y, Target.X, Target.Y);
     
            for (int i = Speed; i > 0; i--, Speed--)
            {
                if (TargetDistance > Range)
                {
                    if (Target.CurrentTile.Y > Y)
                    {
                            Y++;                       
                    }
                    else if (Target.CurrentTile.X < X)
                    {
                            X--;                        
                    }
                    if (Target.CurrentTile.Y < Y)
                    {
                            Y--;                        
                    }
                    else if (Target.CurrentTile.X > X)
                    {
                            X++;
                    }

                    UpdateLocation();
                    if (Target.CurrentTile == CurrentTile)
                    {
                        X++;
                    }
                    UpdateLocation();
                }
            }
            
        }

        public void PaintSurroundings()
        {
            CurrentTile.Button.Background = TileColor;
        }

        public void UnpaintSurroundings()
        {
            CurrentTile.Button.Background = Brushes.White;
        }

        public void UpdateLocation()
        {
            if (CurrentTile != null)
            {
                UnpaintSurroundings();

                CurrentTile.Button.Content = "";
            }
            foreach (Tile tile in MainWindow.Tiles)
            {
                if (X == tile.X && Y == tile.Y)
                {
                    this.CurrentTile = tile;
                    break;
                }
            }
            if (Health > 0)
            {
                CurrentTile.Button.Content = Health.ToString();
            }
            PaintSurroundings();
        }

        int CalculateDistance(int FirstX, int FirstY, int SecondX, int SecondY)
        {
            int Distance = 0;
            //Checks and sets the distance from the tile the character is currently on to the tile clicked
            int xDistance = SecondX - FirstX;
            int yDistance = SecondY - FirstY;
            int Dis = xDistance * xDistance + yDistance * yDistance;
            Distance = (int)Math.Sqrt(Dis);
            return Distance;
        }

        public void Die()
        {
            Health = 0;
            Damage = 0;
            Range = 0;
            BaseSpeed = 0;
            Speed = 0;
            Dead = true;
            MessageBox.Show("i ded");
        }
    }
}
