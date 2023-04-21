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

namespace BattleShip
{   
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point Start_Pos;
        public MainWindow()
        {
            GameBoard _game = new GameBoard();
            InitializeComponent();
            First_Square.Tag = new TagData(-1, -1);
            make_board();
        }
        private void make_board()
        {
            double L_Coord = First_Square.Margin.Left;
            double R_Coord = First_Square.Margin.Right;
            double T_Coord = First_Square.Margin.Top;
            for (int y = 0; y <= 9; y++)
            {
                double NewTop = T_Coord + (30 * y);
                for (int x = 0; x <= 9; x++)
                {
                    Label NewLabel = new Label();
                    NewLabel.Name = $"NewSquare{x}_{y}";
                    NewLabel.Width = 48;
                    NewLabel.Height = 48;
                    NewLabel.Content = "Sea";
                    NewLabel.Background = Brushes.RoyalBlue;
                    NewLabel.Tag = new TagData(x, y);
                    double NewLeft = L_Coord + (48 * x);
                    double NewRight = R_Coord - (48 * x);
                    Grid1.Children.Add(NewLabel);
                    NewLabel.Margin = new Thickness(NewLeft, NewTop, R_Coord - (48 * x), 342 - NewTop);
                    NewLabel.Width = 48;
                    NewLabel.Height = 48;
                    NewLabel.MouseDown += Square_Clicked;
                    NewLabel.Drop += label2_Drop;
                    NewLabel.AllowDrop = true;
                }
            }
            
        }
        private void Change_name(string name)
        {
            
        }

        private void Square_Clicked(object sender, MouseButtonEventArgs e)
        {
            Label Lab = sender as Label;
            TagData T = Lab.Tag as TagData;
            MessageBox.Show($"{T.Coordinates}, {T.ID}");
        }

        #region Chat GPT is my dad!
        private void Ship2_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Label lbl = sender as Label;
            if (lbl != null)
            {
                DragDrop.DoDragDrop(lbl, lbl.Content, DragDropEffects.Copy);
            }
        }

        private void label2_Drop(object sender, DragEventArgs e)
        {
            Random ID = new Random();
            Label lab = sender as Label;
            TagData tag = lab.Tag as TagData;
            string[] coords = tag.Coordinates.Split('_');
            int x = int.Parse(coords[0]);
            int y = int.Parse(coords[1]);
            lab.Content = "Ship";
            tag.ChangeID(TagData.SquareTypes.Cruiser);
            string NewID = tag.ID;
            MessageBox.Show(tag.ID);
            foreach (Label i in Grid1.Children)
            {
                TagData T = i.Tag as TagData;
                if (T.Coordinates == $"{x+1}_{y}" || T.Coordinates == $"{x+2}_{y}")
                {
                    if (T.ID != "Sea0#0")
                    {
                        i.Content = "INTERSEPT";
                        break;
                    }
                    i.Content = "Ship";
                    T.ID = NewID;
                }
            }
            
        }
        private void EraseShip(TagData Tag)
        {
            foreach (Label i in Grid1.Children)
            {
                TagData T = i.Tag as TagData;
                if (T.Coordinates == Tag.Coordinates)
                {
                    T.ChangeID(TagData.SquareTypes.Sea);
                }
            }
        }
        #endregion
    }
}
