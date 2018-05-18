using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThugLifeGame_v2
{
    class Cell
    {
        public int id
        {
            set;
            get;
        }

        public int moveDirection = -1;
        public int X
        {
            get;
            private set;
        }
        public int Y
        {
            get;
            private set;
        }
        public bool isBlack
        {
            get;
            set;
        }
        int whiteNeighboursCount = 0;
        int blackNeighboursCount = 0;
        public Cell[] neighbours = new Cell[8];
        public Cell[] refNeighbours = new Cell[8];
        public bool isAlive = false;

        Random rnd = new Random();

        public Cell(int Y, int X)
        {
            this.X = X;
            this.Y = Y;
            isAlive = false;
            isBlack = false;
            id = -1;
        }
        public Cell(int Y, int X, bool isAlive)
        {
            this.X = X;
            this.Y = Y;
            this.isAlive = isAlive;
            isBlack = false;
            id = -1;
        }
        public Cell(int Y, int X, bool isAlive, bool isBlack)
        {
            this.X = X;
            this.Y = Y;
            this.isAlive = isAlive;
            this.isBlack = isBlack;
            id = -1;
        }
        public Cell(int Y, int X, bool isAlive, bool isBlack, int id)
        {
            this.X = X;
            this.Y = Y;
            this.isAlive = isAlive;
            this.isBlack = isBlack;
            this.id = id; 
        }
        public Cell(int Y, int X, bool isAlive, bool isBlack, int id, int moveDirection)
        {
            this.X = X;
            this.Y = Y;
            this.isAlive = isAlive;
            this.isBlack = isBlack;
            this.id = id;
            this.moveDirection = moveDirection;
        }

        void CountNeighbours()
        {
            whiteNeighboursCount = 0;
            blackNeighboursCount = 0;
            foreach (Cell elem in neighbours)
            {
                if (elem.isAlive)
                {
                    if (elem.isBlack) blackNeighboursCount++;
                    else whiteNeighboursCount++;
                }
            }
        }

        public void MakeLifeCycle()
        {
            CountNeighbours();
            DefineState();
            if (!isBlack)
            {
                if (!isAlive & whiteNeighboursCount > 2)
                {
                    isAlive = true;
                }
                else if (isAlive & whiteNeighboursCount > 4 | isAlive & whiteNeighboursCount < 3)
                {
                    isAlive = false;
                }
            }
        }

        Cell FindAnyBlack()
        {
            foreach (Cell elem in refNeighbours) if (elem.isBlack) return elem;
            return null;
        }

        public void DefineState()
        {
            if (isAlive & blackNeighboursCount > whiteNeighboursCount) TurnIntoBlack(FindAnyBlack());
            else TurnIntoWhite();
        }

        void TurnIntoWhite()
        {
            isBlack = false;
            id = -1;
            moveDirection = -1;
        }
        void TurnIntoBlack(Cell black)
        {
            isBlack = true;
            if (black != null)
            {
                id = black.id;
                moveDirection = black.moveDirection;
            }
        }

        public void BackToDefaultState()
        {
            moveDirection = -1;
            isAlive = false;
            isBlack = false;
            id = -1;
        }

        public void BecomeAClone(Cell cell)
        {
            id = cell.id;
            moveDirection = cell.moveDirection;
            isBlack = cell.isBlack;
            isAlive = cell.isAlive;
        }
    }
}