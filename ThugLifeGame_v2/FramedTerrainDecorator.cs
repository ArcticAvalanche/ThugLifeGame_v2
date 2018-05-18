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
    class FramedTerrainDecorator : Decorator
    {
        Canvas patternCanvas, bufferCanvas;
        CheckBox drawLines;

        public FramedTerrainDecorator(Terrain terrain, Canvas canvas, CheckBox drawLines) : base(terrain, canvas)
        {
            this.drawLines = drawLines;
            this.cells = terrain.cells;
            bufferCanvas = canvas;
            patternCanvas = terrain.GetPatternCanvas();
            DrawLines();
        }

        public override void SetDrawLines(CheckBox drawLines)
        {
            this.drawLines = drawLines;
            DrawLines();
        }

        public override void MakeStep()
        {
            terrain.MakeStep();
            DrawLines();
            canvas = patternCanvas;
            DrawLines();
            canvas = bufferCanvas;
        }

        public void DrawLines()
        {
            if (drawLines.IsChecked == true)
            {
                for (int i = 0; i <= fieldSize; i++)
                {
                    Line verticalLine = new Line
                    {
                        Stroke = lineColor,
                        StrokeThickness = lineSize,
                        StrokeStartLineCap = PenLineCap.Flat,
                        StrokeEndLineCap = PenLineCap.Flat,
                        X1 = cellSize * i,
                        Y1 = 0,
                        X2 = cellSize * i,
                        Y2 = canvas.Height
                    };
                    canvas.Children.Add(verticalLine);

                    Line horizontalLine = new Line
                    {
                        Stroke = lineColor,
                        StrokeThickness = lineSize,
                        StrokeStartLineCap = PenLineCap.Flat,
                        StrokeEndLineCap = PenLineCap.Flat,
                        X1 = 0,
                        Y1 = cellSize * i,
                        X2 = canvas.Width,
                        Y2 = cellSize * i
                    };
                    canvas.Children.Add(horizontalLine);
                }
            }
            else
            {
                canvas.Children.Clear();
                DrawCells();
            }
        }

        public override void PointCreate(int Y, int X)
        {
            terrain.PointCreate(Y, X);
            DrawLines();
        }

        public override void PointRemove(int Y, int X)
        {
            terrain.PointRemove(Y, X);
            DrawLines();
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
    }
}
