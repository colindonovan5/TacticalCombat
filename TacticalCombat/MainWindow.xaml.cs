using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TacticalCombat
{




    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Random r = new Random();
        public static List<Character> Characters = new List<Character>();
        public static List<Enemy> Enemies = new List<Enemy>();
        public static List<Tile> Tiles = new List<Tile>();
        public static Roxas Roxas = new Roxas(); //Change Roxas class to a regular Character class then make these inheriting from it.
        public static Luna Luna = new Luna();
        public static Thorin Thorin = new Thorin();
        public static Demon Demon = new Demon();
        public static Orc Orc = new Orc();
        public static Character SelectedCharacter = Roxas;
        public static int Round = 0;
        public MainWindow()
        {
            Characters.Add(Roxas);
            Characters.Add(Luna);
            Characters.Add(Thorin);
            Enemies.Add(Demon);
            Enemies.Add(Orc);
            SelectedCharacter = Characters[r.Next(0, Characters.Count)];
            InitializeComponent();
            DefineTiles();
            UpdateAllLocations();
            
        }
        /// <summary>
        /// Update the locations of all characters in the Characters list
        /// </summary>
        void UpdateAllLocations()
        {
            foreach (Character character in Characters)
            {
                if(character == SelectedCharacter)
                {
                    character.UpdateLocation();
                }
                else
                {
                    character.UpdateXY();
                    character.UnpaintSurroundings();
                    character.CurrentTile.Button.Background = character.TileColor;
                }
            }

            foreach (Enemy enemy in Enemies)
            {
                enemy.UpdateLocation();
            }
            SelectedCharacter.UpdateLocation();

        }
        //Sets up the Tiles list. 
        void DefineTiles()
        {
            for (int y = 0; y < Container.ColumnDefinitions.Count; y++)
            {
                for (int x = 0; x < Container.RowDefinitions.Count; x++)
                {
                    Tiles.Add(new Tile(x,y));

                }
            }
            int i = 0;
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                Tiles[i].Button = button;
                i++;

            });

        }

        //Button Click - Sets selected character and moves them
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            Roxas.UpdateLocation();
            Luna.UpdateLocation();
            SelectedCharacter.UpdateLocation();
            Button button = (Button)sender;
            Tile tile = null; 
            foreach (Tile t in Tiles)
            {
                if(t.Button == button)
                {
                    tile = t;
                }
            }
            bool CharacterChanged = false;
            foreach (Character character in Characters)
            {
                if(character.CurrentTile.Button == button)
                {
                    if (character.Dead)
                    {
                        MessageBox.Show("RIP " + character.CharacterName);
                        CharacterChanged = true;
                    }
                    else
                    {
                        SelectedCharacter = character;
                        CharacterChanged = true;
                    }

                }
            }
            foreach (Enemy enemy in Enemies)
            {
                if(enemy.CurrentTile.Button == button)
                {
                    CharacterChanged = true;
                }
            }
            if(!CharacterChanged)
            {
                SelectedCharacter.Move(tile);
            }
            UpdateAllLocations();

        }

        //Movement when keys are pressed down. (More of accessability and requested feature, doesn't need to be totally polished yet)
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Key key = e.Key;
            UpdateAllLocations();
            //Movement with keyboard
            if (key == Key.Up)
            {
                SelectedCharacter.Move(Direction.Up);
                //All this stuff ensures character switch on contact with another character
                foreach (Character character in Characters)
                {
                    if(character.CurrentTile == SelectedCharacter.CurrentTile && SelectedCharacter != character)
                    {
                        SelectedCharacter.Move(Direction.Down);
                        SelectedCharacter = character;
                    }
                }

            }
            if (key == Key.Down)
            {
                SelectedCharacter.Move(Direction.Down);
                foreach (Character character in Characters)
                {
                    if (character.CurrentTile == SelectedCharacter.CurrentTile && SelectedCharacter != character)
                    {
                        SelectedCharacter.Move(Direction.Up);
                        SelectedCharacter = character;
                    }
                }
            }
            if (key == Key.Right)
            {
                SelectedCharacter.Move(Direction.Right);

                foreach (Character character in Characters)
                {
                    if (character.CurrentTile == SelectedCharacter.CurrentTile && SelectedCharacter != character)
                    {
                        SelectedCharacter.Move(Direction.Left);
                        SelectedCharacter = character;
                    }
                }
            }
            if (key == Key.Left)
            {

                SelectedCharacter.Move(Direction.Left);

                foreach (Character character in Characters)
                {
                    if (character.CurrentTile == SelectedCharacter.CurrentTile && SelectedCharacter != character)
                    {
                        SelectedCharacter.Move(Direction.Right);
                        SelectedCharacter = character;
                    }
                }
            }
            
            UpdateAllLocations();

            //Next Round
            if(key == Key.N)
            {
                NextRound();
            }


        }

        /// <summary>
        /// Advance the round and begin the enemies' turn.
        /// </summary>
        void NextRound()
        {
            Round++;
            foreach (Character character in Characters)
            {
                character.Speed = character.BaseSpeed;
                character.HasMoved = false;
                character.UsedAction = false;
            }
            foreach (Enemy enemy in Enemies)
            {
                enemy.Speed = enemy.BaseSpeed;
                enemy.HasMoved = false;
                enemy.UsedAction = false;
                enemy.Move();
                enemy.Attack();
            }
            
            UpdateAllLocations();
                
        }


        /// <summary>
        /// Right click event - attacks enemies if they are clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RightClick(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            Tile tile = null;
            foreach (Tile t in Tiles)
            {
                if (t.Button == button)
                {
                    tile = t;
                }
            }
            foreach (Enemy character in Enemies)
            {
                if(tile == character.CurrentTile)
                {
                    SelectedCharacter.Attack(character);
                }
            }
            
            UpdateAllLocations();

        }

        /// <summary>
        /// Update the locations of characters when mouse is moved
        /// </summary>
        /// <param name="sender">Window</param>
        /// <param name="e"></param>
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateAllLocations();
        }
    }
}
