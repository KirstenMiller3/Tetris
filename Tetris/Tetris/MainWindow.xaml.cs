
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Collections.Generic;


namespace Tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random rnd = new Random();

        private DispatcherTimer gameTickTimer = new System.Windows.Threading.DispatcherTimer();

        const int TetronimoSquareSize = 20;
        private SolidColorBrush tBrush = Brushes.Red;

        private char[] shapes = {'I', 'J', 'L', 'O', 'S', 'Z', 'T'};

        private Dictionary<char, SolidColorBrush> colours = new Dictionary<char, SolidColorBrush>();

        private List<TetronimoPart> tetronimoParts = new List<TetronimoPart>();


        public MainWindow()
        {
            InitializeComponent();
            gameTickTimer.Tick += GameTickTimer_Tick;


        }

        private void initialiseColourMap()
        {
            colours.Add('I', Brushes.Aqua);
            colours.Add('J', Brushes.Blue);
            colours.Add('L', Brushes.Orange);
            colours.Add('O', Brushes.Yellow);
            colours.Add('S', Brushes.Green);
            colours.Add('T', Brushes.Purple);
            colours.Add('Z', Brushes.Red);
        }

        private void GameTickTimer_Tick(object sender, EventArgs e)
        {
            MoveTetromino();
        }

        private void MoveTetromino()
        {

            for (int i = tetronimoParts.Count - 1; i >= 0; i--)
            {
                GameArea.Children.Remove(tetronimoParts[i].UiElement);
                double nextX = tetronimoParts[i].Position.X;
                double nextY = tetronimoParts[i].Position.Y + 20;
                tetronimoParts.RemoveAt(i);
                tetronimoParts.Add(new TetronimoPart()
                {
                    Position = new Point(nextX, nextY)
                });

            }

            DrawTetronimo();

            DoCollisionCheck();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            DrawGameArea();
            StartNewGame();
        }

        private void StartNewGame()
        {
            tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 20, TetronimoSquareSize * 1) });
            tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 20, TetronimoSquareSize * 2) });
            tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 20, TetronimoSquareSize * 3) });
            tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 21, TetronimoSquareSize * 3) });

            DrawTetronimo();
            gameTickTimer.Interval = TimeSpan.FromMilliseconds(40);
            gameTickTimer.IsEnabled = true;
        }

        private void DrawTetronimo()
        {
            foreach (TetronimoPart t in tetronimoParts)
            {
                if (t.UiElement == null)
                {
                    t.UiElement = new Rectangle()
                    {
                        Width = TetronimoSquareSize,
                        Height = TetronimoSquareSize,
                        Fill = (tBrush)
                    };
                    GameArea.Children.Add(t.UiElement);
                    Canvas.SetTop(t.UiElement, t.Position.Y);
                    Canvas.SetLeft(t.UiElement, t.Position.X);
                }
            }
        }

        private void DrawGameArea()
        {
            bool doneDrawingBackground = false;
            int nextX = 0, nextY = 0;
            int rowCounter = 0;

            while (!doneDrawingBackground)
            {
                Rectangle rect = new Rectangle
                {
                    Width = TetronimoSquareSize,
                    Height = TetronimoSquareSize,
                    Fill = Brushes.Black
                };
                GameArea.Children.Add(rect);
                Canvas.SetTop(rect, nextY);
                Canvas.SetLeft(rect, nextX);

                nextX += TetronimoSquareSize;
                if (nextX >= GameArea.ActualWidth)
                {
                    nextX = 0;
                    nextY += TetronimoSquareSize;
                    rowCounter++;
                }

                if (nextY >= GameArea.ActualHeight)
                    doneDrawingBackground = true;
            }
        }

        private void DoCollisionCheck()
        {
             foreach(TetronimoPart t in tetronimoParts)
             {
                if(t.Position.Y >= GameArea.ActualHeight - 30)
                {
                    tetronimoParts = new List<TetronimoPart>();
                    NextPiece();
                }
             }
        }


        private void NextPiece()
        {
            int index = rnd.Next(0, 6);
            char shape = shapes[index];

            switch(shape)
            {
                case 'I':
                    {
                        tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 18, TetronimoSquareSize * 1) });
                        tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 19, TetronimoSquareSize * 1) });
                        tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 20, TetronimoSquareSize * 1) });
                        tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 21, TetronimoSquareSize * 1) });
                        break;
                    }
                case 'J':
                    {
                        tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 20, TetronimoSquareSize * 1) });
                        tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 20, TetronimoSquareSize * 2) });
                        tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 20, TetronimoSquareSize * 3) });
                        tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 19, TetronimoSquareSize * 3) });
                        break;
                    }
                case 'L':
                    {
                        tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 20, TetronimoSquareSize * 1) });
                        tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 20, TetronimoSquareSize * 2) });
                        tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 20, TetronimoSquareSize * 3) });
                        tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 21, TetronimoSquareSize * 3) });
                        break;
                    }
                case 'O':
                    {
                        tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 20, TetronimoSquareSize * 1) });
                        tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 20, TetronimoSquareSize * 2) });
                        tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 21, TetronimoSquareSize * 1) });
                        tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 21, TetronimoSquareSize * 2) });
                        break;
                    }
                case 'S':
                    {
                        tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 19, TetronimoSquareSize * 2) });
                        tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 20, TetronimoSquareSize * 2) });
                        tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 20, TetronimoSquareSize * 1) });
                        tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 21, TetronimoSquareSize * 1) });
                        break;
                    }
                case 'Z':
                    {
                        tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 19, TetronimoSquareSize * 1) });
                        tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 20, TetronimoSquareSize * 1) });
                        tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 20, TetronimoSquareSize * 2) });
                        tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 21, TetronimoSquareSize * 2) });
                        break;
                    }
                case 'T':
                    {
                        tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 19, TetronimoSquareSize * 1) });
                        tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 20, TetronimoSquareSize * 1) });
                        tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 21, TetronimoSquareSize * 1) });
                        tetronimoParts.Add(new TetronimoPart() { Position = new Point(TetronimoSquareSize * 20, TetronimoSquareSize * 2) });
                        break;
                    }
            }
        }
    }
}
