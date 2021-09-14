using System;
using System.Collections.Generic;

namespace PolymorphismExmaple
{
    class Program
    {
        static void Main(string[] args)
        {
            var WhiteRook1 = new Rook("Rook", "White", "F3");
            var BlackBishop = new Bishop("Bishop", "Black", "C4");
            var BlackKing = new King("King", "Black", "B8");
            var WhiteRook2 = new Rook("Rook", "White", "B3");

            Console.WriteLine(BlackBishop);
            BlackBishop.Eat(WhiteRook2);
            Console.WriteLine(WhiteRook2);
            Console.WriteLine(BlackBishop);

            WhiteRook1.ViewPossibleMoves();
            WhiteRook1.Eat(BlackBishop);
            Console.WriteLine(WhiteRook1);


            Console.WriteLine(BlackKing);
            BlackKing.ViewPossibleMoves();
            BlackKing.Move("C7");
            Console.WriteLine(BlackKing);
        }
    }



    abstract class ChessPiece
    {
        protected string currentPosition;
        protected string color;
        protected string pieceName;
        protected int pieceValue;
        protected List<string> availableMoves = new List<string>();

        protected ChessPiece(string piecename, string piececolor, string position, int piecevalue = 0) 
        {
            pieceName = piecename;
            C = piececolor;
            CP = position;
            pieceValue = piecevalue;
            isAlive = true;
        }

        protected bool isAlive { get; set; }

        protected string CP 
        { 
            get => currentPosition;
            set
            {
                if ("ABCDEFGH".Contains(value[0]) && "12345678".Contains(value[1]) && value.Length == 2)
                {
                    currentPosition = value;
                }
                else
                {
                    Console.WriteLine("Invalid Position");
                    Console.WriteLine("Set to A1 by default");
                    var rd = new Random();
                    currentPosition = PositionName(rd.Next(0, 64));
                }
            } 
        }

        protected string C
        {
            get => color;
            set
            {
                if (value!="White" && value!="Black")
                {
                    Console.WriteLine("Invalid Color");
                    Console.WriteLine("Set to white by default");
                    color = "White";
                }
                else
                {
                    color = value;
                }
            }
        }

        public void Move(string nextPosition) 
        {
            if (isAlive == true)
            {
                PossibleMoves();
                if (availableMoves.Contains(nextPosition))
                {
                    currentPosition = nextPosition;
                }
                else
                {
                    throw new Exception();
                }
                availableMoves.Clear();
            }
            else
            {
                Console.WriteLine("You can't use this piece");
            }
        }

        public virtual void Eat(ChessPiece A)
        {
            if (isAlive == true)
            {
                PossibleMoves();
                if (availableMoves.Contains(A.currentPosition) && A.color != this.color)
                {
                    A.isAlive = false;
                    this.currentPosition = A.currentPosition;
                }
                else
                {
                    throw new Exception();
                }
                availableMoves.Clear();
            }
            else
            {
                Console.WriteLine("You can't use this piece");
            }
        }

        protected abstract void PossibleMoves();

        public void ViewPossibleMoves()
        {
            PossibleMoves();
            Console.Write("Available moves are: ");
            foreach (string item in availableMoves)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
            availableMoves.Clear();
        }


        private string PositionName(int pos)
        {
            string letters = "ABCDEFGH";
            string _positionname;
            _positionname = letters[(pos % 8)].ToString() + (pos / 8 + 1).ToString();
            return _positionname;
        } 

        public override string ToString()
        {
            if (isAlive == true)
            {
                return $"{color} {pieceName} on {currentPosition}.";
            }
            else
            {
                return "this piece has been taken";
            }
        }
    }

    sealed class Rook : ChessPiece
    {
        public Rook(string name, string color, string pos, int pval = 5) : base(name, color, pos, pval) { }

        protected override void PossibleMoves()
        {
            for (int i = 0; i < 8; i++)
            {
                string letters = "ABCDEFGH";
                availableMoves.Add(letters[i] + currentPosition[1].ToString());
                availableMoves.Add(currentPosition[0] + (i+1).ToString());
            }
            availableMoves.Remove(currentPosition);
            availableMoves.Remove(currentPosition);
        }
    }

    sealed class King : ChessPiece
    {
        public King(string name, string color, string pos, int pval = 1) : base(name, color, pos, pval) { }

        protected override void PossibleMoves()
        {
            string letters = "ABCDEFGH";
            if (Convert.ToInt32(currentPosition[1].ToString()) + 1 < 9)
            {
                availableMoves.Add(currentPosition[0] + (Convert.ToInt32(currentPosition[1].ToString()) + 1).ToString());
            }
            if (Convert.ToInt32(currentPosition[1].ToString()) - 1 > 0)
            {
                availableMoves.Add(currentPosition[0] + (Convert.ToInt32(currentPosition[1].ToString()) - 1).ToString());
            }
            if (letters.IndexOf(currentPosition[0]) + 1 < 8)
            {
                availableMoves.Add(letters[letters.IndexOf(currentPosition[0]) + 1] + currentPosition[1].ToString());
            }
            if (letters.IndexOf(currentPosition[0]) - 1 >= 0)
            {
                availableMoves.Add(letters[letters.IndexOf(currentPosition[0]) - 1] + currentPosition[1].ToString());
            }
        }
    }

    sealed class Bishop : ChessPiece
    {
        public Bishop(string name, string color, string pos, int pval = 2) : base(name, color, pos, pval) { }
        string letters = "ABCDEFGH";

        protected override void PossibleMoves()
        {
            if (color == "White")
            {
                if (Convert.ToInt32(currentPosition[1].ToString()) < 8)
                {
                    availableMoves.Add(currentPosition[0] + (Convert.ToInt32(currentPosition[1].ToString()) + 1).ToString());
                }
            }
            if (color == "Black")
            {
                if (Convert.ToInt32(currentPosition[1].ToString()) > 1)
                {
                    availableMoves.Add(currentPosition[0] + (Convert.ToInt32(currentPosition[1].ToString()) - 1).ToString());
                }
            }
        }

        public override void Eat(ChessPiece A)
        {
            if (color == "White")
            {
                if (Convert.ToInt32(currentPosition[1].ToString()) < 8 && letters.IndexOf(currentPosition[0]) + 1 < 7)
                {
                    availableMoves.Add(letters[letters.IndexOf(currentPosition[0]) + 1] + (Convert.ToInt32(currentPosition[1].ToString()) + 1).ToString());
                }
            }
            if (color == "Black")
            {
                if (Convert.ToInt32(currentPosition[1].ToString()) > 1 && letters.IndexOf(currentPosition[0]) - 1 > 0)
                {
                    availableMoves.Add(letters[letters.IndexOf(currentPosition[0]) - 1] + (Convert.ToInt32(currentPosition[1].ToString()) - 1).ToString());
                }
            }
            base.Eat(A);
        }
    }
}
