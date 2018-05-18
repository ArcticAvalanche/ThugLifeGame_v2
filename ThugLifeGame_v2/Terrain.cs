using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ThugLifeGame_v2
{
    class Terrain
    {
        public static int currentId = -1;

        public const int fieldSize = 25;
        public const int startCellsNumber = 80;
        public int cellSize;

        public Cell[,] cells = new Cell[fieldSize + 2, fieldSize + 2];
        protected Random random = new Random();
        protected Canvas canvas;

        public Terrain(Canvas canvas)
        {
            cellSize = (int)canvas.Width / fieldSize;
            this.canvas = canvas;

            for (int i = 0; i <= fieldSize + 1; i++)
            {
                for (int j = 0; j <= fieldSize + 1; j++)
                {
                    cells[i, j] = new Cell(i, j);
                }
            }

            ResetField();
        }

        protected void SpreadCells()
        {
            int k = 0;
            while (k != startCellsNumber)
            {
                int X = random.Next(1, fieldSize + 1);
                int Y = random.Next(1, fieldSize + 1);

                if (!cells[Y, X].isAlive)
                {
                    cells[Y, X].isAlive = true;
                    k++;
                }
            }
        }

        public void SendNeighbours()
        {
            for (int i = 1; i <= fieldSize; i++)
            {
                for (int j = 1; j <= fieldSize; j++)
                {
                    
                    cells[i, j].neighbours[0] = new Cell(i - 1, j - 1, cells[i - 1, j - 1].isAlive, cells[i - 1, j - 1].isBlack, cells[i - 1, j - 1].id);
                    cells[i, j].neighbours[1] = new Cell(i - 1, j, cells[i - 1, j].isAlive, cells[i - 1, j].isBlack, cells[i - 1, j].id);
                    cells[i, j].neighbours[2] = new Cell(i - 1, j + 1, cells[i - 1, j + 1].isAlive, cells[i - 1, j + 1].isBlack, cells[i - 1, j + 1].id);
                    cells[i, j].neighbours[3] = new Cell(i, j - 1, cells[i, j - 1].isAlive, cells[i, j - 1].isBlack, cells[i, j - 1].id);
                    cells[i, j].neighbours[4] = new Cell(i, j + 1, cells[i, j + 1].isAlive, cells[i, j + 1].isBlack, cells[i, j + 1].id);
                    cells[i, j].neighbours[5] = new Cell(i + 1, j - 1, cells[i + 1, j - 1].isAlive, cells[i + 1, j - 1].isBlack, cells[i + 1, j - 1].id);
                    cells[i, j].neighbours[6] = new Cell(i + 1, j, cells[i + 1, j].isAlive, cells[i + 1, j].isBlack, cells[i + 1, j].id);
                    cells[i, j].neighbours[7] = new Cell(i + 1, j + 1, cells[i + 1, j + 1].isAlive, cells[i + 1, j + 1].isBlack, cells[i + 1, j + 1].id);
                    
                    cells[i, j].refNeighbours[0] = cells[i - 1, j - 1];
                    cells[i, j].refNeighbours[1] = cells[i - 1, j];
                    cells[i, j].refNeighbours[2] = cells[i - 1, j + 1];
                    cells[i, j].refNeighbours[3] = cells[i, j - 1];
                    cells[i, j].refNeighbours[4] = cells[i, j + 1];
                    cells[i, j].refNeighbours[5] = cells[i + 1, j - 1];
                    cells[i, j].refNeighbours[6] = cells[i + 1, j];
                    cells[i, j].refNeighbours[7] = cells[i + 1, j + 1];
                    
                }
            }
        }

        public virtual void MakeStep()
        {
            SendNeighbours();

            //Вверх и влево
            for (int i = 1; i <= fieldSize; i++)
            {
                for (int j = 1; j <= fieldSize; j++)
                {
                    //Если двигается вверх
                    if (cells[i, j].isBlack & cells[i, j].moveDirection == 0)
                    {
                        if (i - 1 == 0) SetNewDirection(cells[i, j].id);
                        else
                        {
                            //Если впереди пустая клетка
                            if (!cells[i - 1, j].isAlive)
                            {
                                cells[i - 1, j].BecomeAClone(cells[i, j]);
                                cells[i, j].BackToDefaultState();
                            }
                            //Если впереди черная
                            else if (cells[i - 1, j].isBlack)
                            {
                                var bufferId = cells[i - 1, j].id;
                                foreach (var elem in cells)
                                {
                                    if (elem.id == bufferId) elem.id = cells[i, j].id;
                                }
                                SetNewDirection(cells[i, j].id);
                            }
                            //Если впереди белая
                            else
                            {
                                cells[i - 1, j].DefineState();
                            }
                        }
                    }

                    //Если двигается влево
                    if (cells[i, j].isBlack & cells[i, j].moveDirection == 1)
                    {
                        if (j - 1 == 0) SetNewDirection(cells[i, j].id);
                        else
                        {
                            //Если впереди пустая клетка
                            if (!cells[i, j - 1].isAlive)
                            {
                                cells[i, j - 1].BecomeAClone(cells[i, j]);
                                cells[i, j].BackToDefaultState();
                            }
                            //Если впереди черная
                            else if (cells[i, j - 1].isBlack)
                            {
                                var bufferId = cells[i, j - 1].id;
                                foreach (var elem in cells)
                                {
                                    if (elem.id == bufferId) elem.id = cells[i, j].id;
                                }
                                SetNewDirection(cells[i, j].id);
                            }
                            //Если впереди белая
                            else
                            {
                                cells[i, j - 1].DefineState();
                            }

                        }
                    }
                }
            }

            //Вниз и вправо
            for (int i = fieldSize; i >= 1; i--)
            {
                for (int j = fieldSize; j >= 1; j--)
                {
                    //Если двигается вниз
                    if (cells[i, j].isBlack & cells[i, j].moveDirection == 2)
                    {
                        if (i == fieldSize) SetNewDirection(cells[i, j].id);
                        else
                        {
                            //Если впереди пустая клетка
                            if (!cells[i + 1, j].isAlive)
                            {
                                cells[i + 1, j].BecomeAClone(cells[i, j]);
                                cells[i, j].BackToDefaultState();
                            }
                            //Если впереди черная
                            else if (cells[i + 1, j].isBlack)
                            {
                                var bufferId = cells[i + 1, j].id;
                                foreach (var elem in cells)
                                {
                                    if (elem.id == bufferId) elem.id = cells[i, j].id;
                                }
                                SetNewDirection(cells[i, j].id);
                            }
                            //Если впереди белая
                            else
                            {
                                cells[i + 1, j].DefineState();
                            }
                        }
                    }

                    //Если двигается вправо
                    if (cells[i, j].isBlack & cells[i, j].moveDirection == 3)
                    {
                        if (j == fieldSize) SetNewDirection(cells[i, j].id);
                        else
                        {
                            //Если впереди пустая клетка
                            if (!cells[i, j + 1].isAlive)
                            {
                                cells[i, j + 1].BecomeAClone(cells[i, j]);
                                cells[i, j].BackToDefaultState();
                            }
                            //Если впереди черная
                            else if (cells[i, j + 1].isBlack)
                            {
                                var bufferId = cells[i, j + 1].id;
                                foreach (var elem in cells)
                                {
                                    if (elem.id == bufferId) elem.id = cells[i, j].id;
                                }
                                SetNewDirection(cells[i, j].id);
                            }
                            //Если впереди белая
                            else
                            {
                                cells[i, j + 1].DefineState();
                            }
                        }
                    }
                }
            }

            //Белые клетки
            for (int i = 1; i <= fieldSize; i++)
            {
                for (int j = 1; j <= fieldSize; j++)
                {
                    cells[i, j].MakeLifeCycle();
                }
            }

            DrawCells();
        }

        void SetNewDirection(int id)
        {
            var direction = random.Next(0, 4);
            foreach (Cell elem in cells)
            {
                if (elem.isBlack & elem.id == id)
                {
                    while (direction == elem.moveDirection)
                    {
                        direction = random.Next(0, 4);
                    }
                    elem.moveDirection = direction;
                }
            }
        }
        
        public virtual void ResetField()
        {
            for (int i = 1; i <= fieldSize; i++)
            {
                for (int j = 1; j <= fieldSize; j++)
                {
                    cells[i, j].isAlive = false;
                    cells[i, j].isBlack = false;
                    cells[i, j].id = -1;
                }
            }
            SpreadCells();
            SendNeighbours();
            DrawCells();
        }

        public virtual void PointCreate(int Y, int X)
        {
            int y = Y / cellSize;
            int x = X / cellSize;
            cells[y + 1, x + 1].isAlive = true;
            SendNeighbours();
            DrawCells();
        }

        public virtual void PointRemove(int Y, int X)
        {
            int y = Y / cellSize;
            int x = X / cellSize;
            cells[y + 1, x + 1].isAlive = false;
            SendNeighbours();
            DrawCells();
        }

        public virtual TimeSpan GetTimeSpan()
        {
            return TimeSpan.Zero;
        }
        public virtual int GetCellsNumber()
        {
            return 0;
        }
        public virtual float GetChangePercent()
        {
            return 0;
        }
        public virtual Canvas GetPatternCanvas()
        {
            return null;
        }

        //---Визуализация---------------------------------------------------------

        protected SolidColorBrush lineColor = new SolidColorBrush(Colors.DarkGray);
        protected SolidColorBrush rectColor = new SolidColorBrush(Colors.DarkCyan);
        protected SolidColorBrush blackRectColor = new SolidColorBrush(Colors.DarkSlateBlue);
        protected SolidColorBrush nullColor = new SolidColorBrush(Colors.White);
        protected const int lineSize = 1;

        public virtual void SetDrawLines(CheckBox drawLines) { }

        protected void DrawRectangle(int Y, int X)
        {
            SolidColorBrush color;
            color = rectColor;

            Rectangle rect = new Rectangle
            {
                Fill = color,
                Height = cellSize,
                Width = cellSize,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                Stroke = color
            };
            Canvas.SetLeft(rect, (X - 1) * cellSize);
            Canvas.SetTop(rect, (Y - 1) * cellSize);
            canvas.Children.Add(rect);
        }
        protected void DrawRectangle(int Y, int X, SolidColorBrush brush)
        {
            SolidColorBrush color;
            color = brush;

            Rectangle rect = new Rectangle
            {
                Fill = color,
                Height = cellSize,
                Width = cellSize,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                Stroke = color
            };
            Canvas.SetLeft(rect, (X - 1) * cellSize);
            Canvas.SetTop(rect, (Y - 1) * cellSize);
            canvas.Children.Add(rect);
        }

        protected virtual void DrawCells()
        {
            canvas.Children.Clear();
            for (int i = 1; i <= fieldSize; i++)
            {
                for (int j = 1; j <= fieldSize; j++)
                {
                    if (cells[i, j].isAlive)
                    {
                        if (cells[i, j].isBlack) DrawRectangle(i, j, blackRectColor);
                        else DrawRectangle(i, j);
                    }
                }
            }
        }
    }
}
