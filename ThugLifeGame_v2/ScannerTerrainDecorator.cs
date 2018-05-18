using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace ThugLifeGame_v2
{
    class ScannerTerrainDecorator : Decorator
    {
        int[,] intCells = new int[fieldSize + 2, fieldSize + 2];
        List<int[,]> patternList = new List<int[,]>();
        protected Canvas patternCanvas;
        Canvas bufferCanvas;
        int[,] patterns = new int[fieldSize + 2, fieldSize + 2];
        Random rnd = new Random();

        public ScannerTerrainDecorator(Terrain terrain, Canvas canvas, Canvas patternCanvas) : base(terrain, canvas)
        {
            this.cells = terrain.cells;
            this.terrain = terrain;
            this.patternCanvas = patternCanvas;
            bufferCanvas = canvas;
            ConvertCells();
            InitPatterns();
        }

        void ConvertCells()
        {
            for (int i = 0; i <= fieldSize + 1; i++)
            {
                for (int j = 0; j <= fieldSize + 1; j++)
                {
                    if (cells[i, j].isAlive)
                    {
                        intCells[i, j] = 1;
                    }
                }
            }
        }

        public override void MakeStep()
        {
            ClearArray(intCells);

            
            terrain.MakeStep();
            FillPatterns();

            canvas = patternCanvas;
            rectColor = new SolidColorBrush(Colors.Firebrick);
            canvas.Children.Clear();
            NewDrawCells();
            //rectColor = new SolidColorBrush(Colors.DarkCyan);
            rectColor = new SolidColorBrush(Colors.DarkCyan);
            canvas = bufferCanvas;
            //NewDrawCells();
            
        }

        public override void ResetField()
        {
            base.ResetField();
            ClearArray(patterns);
        }

        protected void NewDrawCells()
        {
            for (int i = 1; i <= fieldSize; i++)
            {
                for (int j = 1; j <= fieldSize; j++)
                {
                    if (patterns[i, j] == 1) DrawRectangle(i, j);
                }
            }
        }

        void Compare(int[,] a, int Y, int X)
        {
            int sizeY = a.GetLength(0);
            int sizeX = a.GetLength(1);

            bool equal = true;
            ConvertCells();
            int h = 0;
            int w = 0;

            for (int i = Y; i < Y + sizeY; i++)
            {
                for (int j = X; j < X + sizeX; j++)
                {
                    if (intCells[i, j] != a[h, w] & !cells[i, j].isBlack) equal = false;
                    w++;
                }
                h++;
                w = 0;
            }
            if (equal)
            {
                var direction = rnd.Next(0, 4);
                currentId++;
                for (int i = Y; i < Y + sizeY; i++)
                {
                    for (int j = X; j < X + sizeX; j++)
                    {
                        patterns[i, j] = intCells[i, j];
                        if (cells[i, j].isAlive & !cells[i, j].isBlack)
                        {
                            cells[i, j].isBlack = true;
                            cells[i, j].id = currentId;
                            cells[i, j].moveDirection = direction;
                        }
                    }
                }
            }
        }

        public void FillPatterns()
        {
            ClearArray(patterns);
            foreach (int[,] element in patternList)
            {
                for (int i = 0; i <= fieldSize - element.GetLength(0) + 2; i++)
                {
                    for (int j = 0; j <= fieldSize - element.GetLength(1) + 2; j++)
                    {
                        Compare(element, i, j);
                    }
                }
            }
        }

        void ClearArray(int[,] a)
        {
            for (int i = 0; i <= fieldSize + 1; i++)
            {
                for (int j = 0; j <= fieldSize + 1; j++)
                {
                    a[i, j] = 0;
                }
            }
        }

        void InitPatterns()
        {
            patternList.Add(new int[4, 4] { {0, 0, 0, 0},
                                            {0, 1, 1, 0},
                                            {0, 1, 1, 0},
                                            {0, 0, 0, 0} });

            patternList.Add(new int[5, 5] { {0, 0, 0, 0, 0},
                                            {0, 0, 1, 0, 0},
                                            {0, 0, 1, 0, 0},
                                            {0, 1, 1, 1, 0},
                                            {0, 0, 0, 0, 0} });

            patternList.Add(new int[5, 5] { {0, 0, 0, 0, 0},
                                            {0, 1, 1, 1, 0},
                                            {0, 0, 1, 0, 0},
                                            {0, 0, 1, 0, 0},
                                            {0, 0, 0, 0, 0} });

            patternList.Add(new int[5, 5] { {0, 0, 0, 0, 0},
                                            {0, 0, 0, 1, 0},
                                            {0, 1, 1, 1, 0},
                                            {0, 0, 0, 1, 0},
                                            {0, 0, 0, 0, 0} });

            patternList.Add(new int[5, 5] { {0, 0, 0, 0, 0},
                                            {0, 1, 0, 0, 0},
                                            {0, 1, 1, 1, 0},
                                            {0, 1, 0, 0, 0},
                                            {0, 0, 0, 0, 0} });

            patternList.Add(new int[5, 5] { {0, 0, 0, 0, 0},
                                            {0, 1, 0, 0, 0},
                                            {0, 0, 1, 1, 0},
                                            {0, 0, 1, 0, 0},
                                            {0, 0, 0, 0, 0} });

            patternList.Add(new int[5, 5] { {0, 0, 0, 0, 0},
                                            {0, 0, 1, 0, 0},
                                            {0, 1, 1, 0, 0},
                                            {0, 0, 0, 1, 0},
                                            {0, 0, 0, 0, 0} });

            patternList.Add(new int[5, 5] { {0, 0, 0, 0, 0},
                                            {0, 0, 0, 1, 0},
                                            {0, 1, 1, 0, 0},
                                            {0, 0, 1, 0, 0},
                                            {0, 0, 0, 0, 0} });

            patternList.Add(new int[5, 5] { {0, 0, 0, 0, 0},
                                            {0, 0, 1, 0, 0},
                                            {0, 0, 1, 1, 0},
                                            {0, 1, 0, 0, 0},
                                            {0, 0, 0, 0, 0} });

            patternList.Add(new int[6, 6] { {0, 0, 0, 0, 0, 0},
                                            {0, 0, 1, 0, 0, 0},
                                            {0, 0, 1, 0, 1, 0},
                                            {0, 1, 1, 1, 0, 0},
                                            {0, 0, 0, 1, 0, 0},
                                            {0, 0, 0, 0, 0, 0 } });

            patternList.Add(new int[6, 6] { {0, 0, 0, 0, 0, 0},
                                            {0, 0, 0, 1, 0, 0},
                                            {0, 1, 1, 0, 0, 0},
                                            {0, 0, 1, 1, 1, 0},
                                            {0, 0, 1, 0, 0, 0},
                                            {0, 0, 0, 0, 0, 0 } });

            patternList.Add(new int[5, 5] { {0, 0, 0, 0, 0},
                                            {0, 1, 1, 1, 0},
                                            {0, 1, 1, 1, 0},
                                            {0, 1, 1, 1, 0},
                                            {0, 0, 0, 0, 0} });

            patternList.Add(new int[5, 5] { {0, 0, 0, 0, 0},
                                            {0, 0, 1, 0, 0},
                                            {0, 1, 1, 1, 0},
                                            {0, 0, 1, 0, 0},
                                            {0, 0, 0, 0, 0} });

            patternList.Add(new int[7, 7] { {0, 0, 0, 0, 0, 0, 0},
                                            {0, 0, 0, 1, 0, 0, 0},
                                            {0, 0, 1, 0, 1, 0, 0},
                                            {0, 1, 0, 0, 0, 1, 0},
                                            {0, 0, 1, 0, 1, 0, 0},
                                            {0, 0, 0, 1, 0, 0, 0},
                                            {0, 0, 0, 0, 0, 0, 0} });

            patternList.Add(new int[6, 6] { {0, 0, 0, 0, 0, 0},
                                            {0, 0, 1, 0, 0, 0},
                                            {0, 1, 0, 1, 1, 0},
                                            {0, 1, 0, 1, 1, 0},
                                            {0, 0, 1, 0, 0, 0},
                                            {0, 0, 0, 0, 0, 0} });

            patternList.Add(new int[6, 4] { {0, 0, 0, 0},
                                            {0, 1, 0, 0},
                                            {0, 1, 1, 0},
                                            {0, 1, 1, 0},
                                            {0, 1, 0, 0},
                                            {0, 0, 0, 0} });

            patternList.Add(new int[6, 5] { {0, 0, 0, 0, 0},
                                            {0, 0, 0, 1, 0},
                                            {0, 1, 1, 1, 0},
                                            {0, 1, 1, 1, 0},
                                            {0, 0, 0, 1, 0},
                                            {0, 0, 0, 0, 0} });
            
            patternList.Add(new int[6, 6] { {0, 0, 0, 0, 0, 0},
                                            {0, 0, 0, 1, 0, 0},
                                            {0, 1, 1, 0, 1, 0},
                                            {0, 1, 1, 0, 1, 0},
                                            {0, 0, 0, 1, 0, 0},
                                            {0, 0, 0, 0, 0, 0} });

            patternList.Add(new int[6, 4] { {0, 0, 0, 0},
                                            {0, 0, 1, 0},
                                            {0, 1, 1, 0},
                                            {0, 1, 1, 0},
                                            {0, 0, 1, 0},
                                            {0, 0, 0, 0} });

            patternList.Add(new int[6, 5] { {0, 0, 0, 0, 0},
                                            {0, 1, 0, 0, 0},
                                            {0, 1, 1, 1, 0},
                                            {0, 1, 1, 1, 0},
                                            {0, 1, 0, 0, 0},
                                            {0, 0, 0, 0, 0} });

            /*
            patternList.Add(new int[5, 5] { {0, 0, 0, 0, 0},
                                            {0, 0, 1, 0, 0},
                                            {0, 0, 0, 0, 0},
                                            {0, 1, 1, 1, 0},
                                            {0, 0, 0, 0, 0} });

            patternList.Add(new int[5, 5] { {0, 0, 0, 0, 0},
                                            {0, 1, 1, 1, 0},
                                            {0, 0, 0, 0, 0},
                                            {0, 0, 1, 0, 0},
                                            {0, 0, 0, 0, 0} });

            patternList.Add(new int[5, 5] { {0, 0, 0, 0, 0},
                                            {0, 0, 0, 1, 0},
                                            {0, 1, 0, 1, 0},
                                            {0, 0, 0, 1, 0},
                                            {0, 0, 0, 0, 0} });

            patternList.Add(new int[5, 5] { {0, 0, 0, 0, 0},
                                            {0, 1, 0, 0, 0},
                                            {0, 1, 0, 1, 0},
                                            {0, 1, 0, 0, 0},
                                            {0, 0, 0, 0, 0} });
            */
        }

        public override TimeSpan GetTimeSpan()
        {
            return terrain.GetTimeSpan();
        }

        public override int GetCellsNumber()
        {
            return terrain.GetCellsNumber();
        }

        public override float GetChangePercent()
        {
            return terrain.GetChangePercent();
        }

        public override void PointCreate(int Y, int X)
        {
            terrain.PointCreate(Y, X);
        }

        public override void PointRemove(int Y, int X)
        {
            terrain.PointRemove(Y, X);
        }

        public override Canvas GetPatternCanvas()
        {
            return patternCanvas;
        }
    }
}
