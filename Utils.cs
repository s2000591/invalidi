using System;
using System.Collections;
using System.Collections.Generic;

namespace mato
{
    enum GameStatus
    {
        HitWall = -2,
        HitItself = -1,
        GameWon = 0,
        MoveOk = 1,
        AteAppleAndGrew = 2
    }
    enum Directions
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3

    }
    readonly struct Coord
    {
        public Coord(int y, int x)
        {
            this.y = y;
            this.x = x;
        }
        private static Coord[] directionOffsets = {
            new Coord(-1,0),
            new Coord(0,1),
            new Coord(1,0),
            new Coord(0,-1) };
        /*      
          new Coord(-1,0),
        new Coord(0,1),
        new Coord(1,0),
        new Coord(0,-1) };
                    */
        public readonly int y { get; }
        public readonly int x { get; }
        public Coord Sum(Coord c) =>
            new Coord(this.y + c.y, this.x + c.x);

        public Coord GetCoordInDir(int direction) =>
            Sum(directionOffsets[direction]);

        public bool IsInsideBounds(int maxY, int maxX) =>
            this.y >= 0 && this.y <= maxY &&
            this.x >= 0 && this.x <= maxX;

        public int RectiLinearDistance(Coord c) =>
            Math.Abs(this.y - c.y) + Math.Abs(this.x - c.x);

        public List<Coord> GetAllAdjacentInsideBounds(int maxY, int maxX)
        {
            var validCoords = new List<Coord>();
            foreach (Coord c1 in directionOffsets)
            {
                Coord c2 = ((this + c1));
                if (c2.IsInsideBounds(maxY, maxX))
                {
                    validCoords.Add(c2);
                }
            }
            return validCoords;
        }

        public List<int> GetDirectionsInsideBounds(int maxY, int maxX)
        {
            var dirs = new List<int>();

            for (int i = 0; i < directionOffsets.Length; i++)
            {

                Coord c2 = ((this + directionOffsets[i]));
                if (c2.IsInsideBounds(maxY, maxX))
                {
                    dirs.Add(i);
                }
            }
            return dirs;
        }

        public override bool Equals(object obj) =>
            obj is Coord c
            && c.y == this.y
            && c.x == this.x;

        public override int GetHashCode() =>
            HashCode.Combine(this.y, this.x); // hope this works
        public static Coord operator +(Coord a, Coord b) =>
            new Coord(a.y + b.y, a.x + b.x); // hope this works

        public override string ToString()
        {
            return "y: " + this.y + " x: " + this.x;
        }
    }
    readonly struct Priority : IComparable<Priority>
    {

        public Priority(int priority, Coord coord)
        {
            this.coord = coord;
            this.priority = priority;
        }
        public readonly Coord coord { get; }
        public readonly int priority { get; }

        public int CompareTo(Priority other) =>
            this.priority - other.priority; // oikein päin?
    }

    //readonly struct
    public class PriorityInt : IComparable<PriorityInt>
    {

        public PriorityInt(int priority, int integer)
        {
            this.integer = integer;
            this.priority = priority;
        }
        public int integer { get; }
        //public readonly
        public int priority { get; }

        public int CompareTo(PriorityInt other) =>
            this.priority - other.priority; // oikein päin?
    }

}
