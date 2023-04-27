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
        private bool VerticalPlacementMode = false;
        private TagData.SquareTypes ShipType = TagData.SquareTypes.None;
        private List<Label> UsedTiles = new List<Label>();
        private bool DemoMode = false;
        public MainWindow()
        {
            GameBoard _game = new GameBoard();
            InitializeComponent();
            First_Square.Tag = new TagData(-1, -1);
            make_board();
        }
        private void make_board()
        {
            for (int y = 0; y <= 9; y++)
            {
                for (int x = 0; x <= 9; x++)
                {
                    // Game Tiles
                    Label NewLabel = new Label();
                    NewLabel.Name = $"NewSquare{x}_{y}";
                    NewLabel.HorizontalAlignment = HorizontalAlignment.Center;
                    NewLabel.VerticalAlignment = VerticalAlignment.Center;
                    NewLabel.Height = Grid1.Height / 10;
                    NewLabel.Width = Grid1.Width / 10;
                    NewLabel.BorderBrush = Brushes.Transparent;
                    NewLabel.Content = "Sea";
                    NewLabel.Background = Brushes.RoyalBlue;
                    NewLabel.Tag = new TagData(x, y);
                    Grid1.Children.Add(NewLabel);
                    Grid.SetColumn(NewLabel, x);
                    Grid.SetRow(NewLabel, y);
                    NewLabel.MouseRightButtonDown += Square_Clicked;
                    NewLabel.MouseLeftButtonDown += NewShipPlace;
                    NewLabel.AllowDrop = true;
                }
            }
        }
        private void Square_Clicked(object sender, MouseButtonEventArgs e)
        {
            Label Lab = sender as Label;
            TagData T = Lab.Tag as TagData;
            MessageBox.Show($"{T.Coordinates}, {T.ID}, {T.Length}");
        }

        #region Ship Placement
        private void SelectShipType(object sender, MouseButtonEventArgs e)
        {
            Label lbl = sender as Label;
            if (lbl != null)
            {
                lbl.Background = Brushes.LightSkyBlue;
                if (lbl.Name == "CarrierPlace")
                {
                    ShipType = TagData.SquareTypes.Carrier;
                }
                else if (lbl.Name == "BattleshipPlace")
                {
                    ShipType = TagData.SquareTypes.BattleShip;
                }
                else if (lbl.Name == "CruiserPlace")
                {
                    ShipType = TagData.SquareTypes.Cruiser;
                }
                else if (lbl.Name == "PatrolBoat")
                {
                    ShipType = TagData.SquareTypes.Patrol;
                }
                foreach (Label L in ShipGrid.Children)
                {
                    if (L != lbl)
                    {
                        L.Background = Brushes.LightGray;
                    }
                }
            }
        }

        private void NewShipPlace(object sender, MouseButtonEventArgs e)
        {
            Label lab = sender as Label; // the tile the ship was placed on
            TagData tag = lab.Tag as TagData; // TagData associated to the first tile
            if (DemoMode)
            {
                RemoveShip(lab);
                return;
            }
            string[] C = tag.Coordinates.Split('_');
            int[] Coords = new int[]
            {
                int.Parse(C[0]), int.Parse(C[1])
            };
            int NewLength = (int)ShipType;
            if (Coords[0] + NewLength > 10 && !VerticalPlacementMode) // prevents pieces off the board
            {
                return;
            }
            else if (Coords[1] + NewLength > 10 && VerticalPlacementMode)
            {
                return;
            }
            // AHH
            int count = 0;
            List<Label> NewShipTiles = new List<Label>();
            foreach (Label i in Grid1.Children)
            {
                TagData T = i.Tag as TagData;
                string[] CurrentCoords = T.Coordinates.Split('_');
                int x = int.Parse(CurrentCoords[0]);
                int y = int.Parse(CurrentCoords[1]);
                if (tag.ID != "Sea0#0")
                {
                    continue;
                }
                if (count >= NewLength)
                {
                    break;
                }
                if (T.Coordinates == tag.Coordinates && !VerticalPlacementMode || count > 0 && !VerticalPlacementMode)
                {
                    if (UsedTiles.Contains(i))
                    {
                        return;
                    }
                    NewShipTiles.Add(i);
                    count++;
                }
                if (T.Coordinates == tag.Coordinates && VerticalPlacementMode || x == Coords[0] && y >= Coords[1] && VerticalPlacementMode)
                {
                    if (UsedTiles.Contains(i))
                    {
                        return;
                    }
                    NewShipTiles.Add(i);
                    count++;
                }
            }
            if (Place(NewShipTiles))
            {
                UsedTiles.AddRange(NewShipTiles);
            }
            
        }
        private bool Place(List<Label> TilesToPlace)
        {
            Random RGBGray = new Random();
            int GreyScale = RGBGray.Next(150, 240);
            for (int i = 0; i < TilesToPlace.Count(); i++) 
            {
                TagData NewTag = TilesToPlace[i].Tag as TagData;
                if (UsedTiles.Contains(TilesToPlace[i]))
                {
                    //EraseShip(TilesToPlace);
                    return false;
                }
                else
                {
                    NewTag.ID = TagData.ChangeID(ShipType);
                    NewTag.Length = (int)ShipType;
                    TilesToPlace[i].Background = new SolidColorBrush(Color.FromRgb((byte)GreyScale, (byte)GreyScale, (byte)GreyScale));
                    TilesToPlace[i].Content = ShipType.ToString();
                }
            }
            return true;
        }
        private void RemoveShip(Label ClickedShip)
        {
            TagData RemoveTag = ClickedShip.Tag as TagData;
            List<Label> DeleteList = new List<Label>();
            string RemoveID = RemoveTag.ID;
            foreach (Label L in UsedTiles)
            {
                TagData T = L.Tag as TagData;
                if (T.ID == RemoveID)
                {
                    T.ID = TagData.ChangeID(TagData.SquareTypes.Sea);
                    T.Length = (int)TagData.SquareTypes.Sea;
                    L.Background = Brushes.RoyalBlue;
                    L.Content = "Sea";
                    DeleteList.Add(L);
                }
            }
            foreach (Label L in DeleteList)
            {
                UsedTiles.Remove(L);
            }
        }
        
        #endregion

        private void OrientationButton_Click(object sender, RoutedEventArgs e)
        {
            if (!VerticalPlacementMode)
            {
                VerticalPlacementMode = true;
                OrientationButton.Content = "Vertical";
                int count = 0;
                foreach (Label i in OrientGrid.Children)
                {
                    Grid.SetColumn(i, 0);
                    Grid.SetRow(i, count);
                    count++;
                }
            }
            else
            {
                VerticalPlacementMode = false;
                OrientationButton.Content = "Horizontal";
                int count = 0;
                foreach (Label i in OrientGrid.Children)
                {
                    Grid.SetColumn(i, count);
                    Grid.SetRow(i, 0);
                    count++;
                }
            }
        }

        private void DeleteMode(object sender, KeyEventArgs e)
        {
            if (KeydownElement.BorderBrush == Brushes.Red && e.Key == Key.X)
            {
                DemoMode = false;
                KeydownElement.BorderBrush = Brushes.Transparent;
                KeydownElement.Content = "";
            }
            else if (e.Key == Key.X)
            {
                DemoMode = true;
                KeydownElement.BorderBrush = Brushes.Red;
                KeydownElement.Content = "Remove Mode";
            }
        }
    }
}
