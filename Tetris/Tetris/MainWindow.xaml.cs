
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
        private DispatcherTimer gameTickTimer = new System.Windows.Threading.DispatcherTimer();

        const int TetronimoSquareSize = 20;
        private SolidColorBrush tBrush = Brushes.Red;

        private List<TetronimoPart> tetronimoParts = new List<TetronimoPart>();


        public MainWindow()
        {
            InitializeComponent();
            gameTickTimer.Tick += GameTickTimer_Tick;

        }

        private void GameTickTimer_Tick(object sender, EventArgs e)
        {
            MoveTetromino();
        }

        private void MoveTetromino()
        {

            for (int i = tetronimoParts.Count - 1; i >= 0; i--)
            {
                GameArea.Children.Remove(tetronimoParts[0].UiElement);
                double nextX = tetronimoParts[i].Position.X;
                double nextY = tetronimoParts[i].Position.Y + 1;
                tetronimoParts.RemoveAt(1);
                tetronimoParts.Add(new TetronimoPart()
                {
                    Position = new Point(nextX, nextY)
                });

            }

            DrawTetronimo();
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
    }
}
