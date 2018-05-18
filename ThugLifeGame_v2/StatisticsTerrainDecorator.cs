using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ThugLifeGame_v2
{
    class StatisticsTerrainDecorator : Decorator
    {
        float lastTurnCellsNumber = 70;
        float cellsNumber = 0;
        float changePercent = 0;

        Stopwatch timer = new Stopwatch();
        public TimeSpan ts;

        public StatisticsTerrainDecorator(Terrain terrain, Canvas canvas) : base(terrain, canvas)
        {
            this.cells = terrain.cells;
        }

        public override void MakeStep()
        {
            cellsNumber = 0;

            timer.Reset();
            timer.Start();
            terrain.MakeStep();
            timer.Stop();
            ts = timer.Elapsed;

            for (int i = 1; i <= fieldSize; i++)
            {
                for (int j = 1; j <= fieldSize; j++)
                {
                    if (cells[i, j].isAlive) cellsNumber++;
                }
            }
            changePercent = Math.Abs((cellsNumber - lastTurnCellsNumber)) / lastTurnCellsNumber * 100;
            lastTurnCellsNumber = cellsNumber;
        }

        public override TimeSpan GetTimeSpan()
        {
            return terrain.GetTimeSpan() + ts;
        }

        public override int GetCellsNumber()
        {
            return (int)cellsNumber;
        }

        public override float GetChangePercent()
        {
            return changePercent;
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
            return terrain.GetPatternCanvas();
        }
    }
}
