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
    public class Character : ICharacter
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
        public bool UsedAction { get; set; }
        public bool HasMoved { get; set; }
        public bool TilesPainted { get; set; }
        public Tile CurrentTile { get; set; }
        public Brush SpeedColor { get; set; }
        public Brush TileColor { get; set; }
        public Brush RangeColor { get; set; }
        public bool Dead { get; set; }

        public Character()
        {
            Random r = new Random();
            Thread.Sleep(20);
            X = r.Next(0, 21);
            Y = r.Next(0, 21);
            TilesPainted = false;
            HasMoved = false;
            UsedAction = false;
            RangeColor = Brushes.OrangeRed;
            Dead = false;
        }

        /// <summary>
        /// Attack another character
        /// </summary>
        /// <param name="Target">The character that is being attacked</param>
        public void Attack(ICharacter Target)
        {
            int Distance = CalculateDistance(X, Y, Target.X, Target.Y);
            if (UsedAction)
            {
                MessageBox.Show(CharacterName + " already used their action this round.");
            }else if (Target.Dead)
            {
                MessageBox.Show("You can't attack a dead person.");
            }
            else
            {
                if (Distance > Range)
                {
                    MessageBox.Show("Your range isnt that big you rock");
                }
                else
                {
                    Target.Health -= Damage;

                    if (Target.Health <= 0)
                    {
                        Target.Die();

                    }
                    UsedAction = true;
                }
            }



        }

        public int GetDamage()
        {
            return Damage;
        }

        public int GetHealth()
        {
            return Health;
        }

        public string GetName()
        {
            return CharacterName;
        }

        public int GetRange()
        {
            return Range;
        }

        public void Heal()
        {
            throw new NotImplementedException();
        }

        public void Hurt()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update the Character's location without painting the tiles around them
        /// </summary>
        public void UpdateXY()
        {
            if (CurrentTile != null)
            {
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
            if(Health > 0)
            {
                CurrentTile.Button.Content = Health.ToString();

            }
        }

        /// <summary>
        /// Updates the x and y coordinates of the character and paints their surroundings
        /// </summary>
        public void UpdateLocation()
        {
            UnpaintSurroundings();
            if (CurrentTile != null)
            {
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
            if(Health > 0)
            {
                CurrentTile.Button.Content = Health.ToString();
            }
            PaintSurroundings();
        }

        /// <summary>
        /// Used for movement involving the keyboard
        /// </summary>
        /// <param name="dir">Direction of the movement</param>
        public void Move(Direction dir)
        {
            UnpaintSurroundings();
            switch (dir)
            {
                case Direction.None:
                    break;
                case Direction.Up:
                    if (Y > 0 && Speed > 0)
                    {
                        Speed--;
                        Y--;
                    }
                    else
                    {
                        MessageBox.Show("Too far boi");
                    }
                    break;
                case Direction.Down:
                    if (Y < 20 && Speed > 0)
                    {
                        Speed--;
                        Y++;
                    }
                    else
                    {
                        MessageBox.Show("Too far boi");

                    }
                    break;
                case Direction.Left:
                    if (X > 0 && Speed > 0)
                    {
                        Speed--;
                        X--;
                    }
                    else
                    {
                        MessageBox.Show("Too far boi");
                    }
                    break;
                case Direction.Right:
                    if (X < 20 && Speed > 0)
                    {
                        Speed--;
                        X++;
                    }
                    else
                    {
                        MessageBox.Show("Too far boi");

                    }
                    break;
                default:
                    break;
            }
            UpdateLocation();
        }

        /// <summary>
        /// Used for click movement
        /// </summary>
        /// <param name="tile">The tile the character is being moved to</param>
        public void Move(Tile tile)
        {
            int Distance = CalculateDistance(X, Y, tile.X, tile.Y);

            if (Distance > Speed)
            {
                MessageBox.Show("You can't move that far. You can only move " + Speed + " tiles at a time.");
            }
            else
            {
                UnpaintSurroundings();
                Speed -= Distance;
                X = tile.X;
                Y = tile.Y;
                PaintSurroundings();

            }
            UpdateLocation();
        }
        /// <summary>
        /// Move in a direction a certain distance
        /// </summary>
        public void Move(Direction dir, int distance)
        {
            UnpaintSurroundings();
            switch (dir)
            {
                case Direction.None:
                    break;
                case Direction.Up:
                    if (Y > 0)
                    {
                        if (distance <= Speed)
                        {
                            Y -= distance;
                        }
                        else
                        {
                            MessageBox.Show("You can't move that far.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Too far boi");
                    }
                    break;
                case Direction.Down:
                    if (Y < 20)
                    {
                        if (distance <= Speed)
                        {
                            Y += distance;
                        }
                        else
                        {
                            MessageBox.Show("You can't move that far.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Too far boi");

                    }
                    break;
                case Direction.Left:
                    if (X > 0)
                    {
                        if (distance <= Speed)
                        {
                            X -= distance;
                        }
                        else
                        {
                            MessageBox.Show("You can't move that far.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Too far boi");
                    }
                    break;
                case Direction.Right:
                    if (X < 20)
                    {
                        if (distance <= Speed)
                        {
                            X += distance;
                        }
                        else
                        {
                            MessageBox.Show("You can't move that far.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Too far boi");

                    }
                    break;
                default:
                    break;
            }
            UpdateLocation();
        }

        /// <summary>
        /// Paints the distance the character can move around them
        /// </summary>
        public void PaintSurroundings()
        {
            if (!TilesPainted)
            {
                foreach (Tile tile in MainWindow.Tiles)
                {

                    int Distance = CalculateDistance(X, Y, tile.X, tile.Y);
                    

                    if(Distance <= Range)
                    {
                        tile.Painted = true;
                        tile.Button.Background = RangeColor;
                    }
                    if (Distance <= Speed)
                    {
                        tile.Painted = true;
                        tile.Button.Background = SpeedColor;
                    }
                    foreach (Character character in MainWindow.Characters)
                    {
                        //Putting this first ensures that the range is painted first and is then painted over by movement and is key
                        if (Distance <= Range)
                        {
                            if (character.CurrentTile == tile)
                            {
                                tile.Button.Background = character.TileColor;

                                break;
                            }
                            else
                            {
                                tile.Painted = true;
                                tile.Button.Background = RangeColor;
                            }
                        }
                        if (Distance <= Speed)
                        {
                            if (character.CurrentTile == tile)
                            {
                                tile.Button.Background = character.TileColor;

                                break;
                            }
                            else
                            {
                                tile.Painted = true;
                                tile.Button.Background = SpeedColor;
                            }
                        }

                    }
                    foreach (Enemy enemy in MainWindow.Enemies)
                    {
                        if (Distance <= Range)
                        {
                            if (enemy.CurrentTile == tile)
                            {
                                tile.Button.Background = enemy.TileColor;

                                break;
                            }
                        }
                            if (Distance <= Speed)
                        {
                            if (enemy.CurrentTile == tile)
                            {
                                tile.Button.Background = enemy.TileColor;

                                break;
                            }
                        }
                    }

                }
                CurrentTile.Button.Background = TileColor;
                TilesPainted = true;
            }
        }

        /// <summary>
        /// Unpaints the distance the character can move around them
        /// </summary>
        public void UnpaintSurroundings()
        {
            if (TilesPainted)
            {
                foreach (Tile tile in MainWindow.Tiles)
                {
                    if (tile.X - CurrentTile.X > Speed)
                    {

                    }
                    else if (CurrentTile.X - tile.X > Speed)
                    {
                    }
                    else if (tile.Y - CurrentTile.Y > Speed)
                    {
                    }
                    else if (CurrentTile.Y - tile.Y > Speed)
                    {
                    }
                    else
                    {
                        tile.Painted = false;
                        tile.Button.Background = Brushes.White;
                    }

                    if (tile.X - CurrentTile.X > Range)
                    {

                    }
                    else if (CurrentTile.X - tile.X > Range)
                    {
                    }
                    else if (tile.Y - CurrentTile.Y > Range)
                    {
                    }
                    else if (CurrentTile.Y - tile.Y > Range)
                    {
                    }
                    else
                    {
                        tile.Painted = false;
                        tile.Button.Background = Brushes.White;
                    }
                }
                TilesPainted = false;
            }
        }

        /// <summary>
        /// Calculate the distance between four coordinates
        /// </summary>
        /// <param name="FirstX">X coordinate of first object</param>
        /// <param name="FirstY">Y coordinate of first object</param>
        /// <param name="SecondX">X coordinate of second object</param>
        /// <param name="SecondY">Y coordinate of second object</param>
        /// <returns></returns>
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

        /// <summary>
        /// Kill the character
        /// </summary>
        public void Die()
        {
            UnpaintSurroundings();
            Speed = 0;
            BaseSpeed = 0;
            Range = 0;
            MaxHealth = 0;
            Health = 0;
            CurrentTile.Button.Content = "X";
            Dead = true;
            UpdateLocation();
        }
    }
}
