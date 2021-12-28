using System;
using System.Collections;
using System.Collections.Generic;
using mato;

class MatoLogic
{
    int avoidtailGrowthDistance = 1;
    int initialTailHistorySize = 26;
    int tailhistoryGrowsAt = 25;
    public int tailhistorylimit = 4;
    public int growtailHistoryUntil;
    public int depth = 0;
    Random rand = new Random();
    public Queue<Coord> wormPieces { get; } // head is always the last of the que
    public Coord apple { get; private set; }
    public Coord removedTail { get; set; } // what is this?
    public Coord h1;

    //public Queue<Coord> tailHistory { get; }
    public List<Coord> tailHistoryList { get; }
    public int moves = 0;
    int maxY; // basically just height and width -1?
    int maxX;
    public int maxWormLenght; // ??
    public Coord head;
    public MatoLogic(int height, int width)

    {
        //  tailhistorylimit = 3;
        wormPieces = new Queue<Coord>();

        maxY = height - 1;
        maxX = width - 1;
        maxWormLenght = height * width;
        wormPieces = new Queue<Coord>();

        this.head = new Coord(0, 0);

        wormPieces.Enqueue(new Coord(1, 0));
        wormPieces.Enqueue(new Coord(0, 0));
        removedTail = new Coord(0, 1);
        //this.tailHistory = new Queue<Coord>();
        this.tailHistoryList = new List<Coord>();
        //tailHistory.Enqueue(new Coord(2, 0));
        createApple();
        depth = wormPieces.Count;
        h1 = new Coord(0, 2);
        //Move(1);
        //Move(1);
        //Move(1);
        //this.maxWormLenght = height * width * (MaxWormLenghtPercentage / 100); // 

        // needs initial location of the worm
    }

    public Coord getHead(int direction)
    {
        return head;
    }

    // todo createApple for specific coordinates
    public void createApple() // needs to be improved, will be terrible on large grids
                              // also make an another medthod for creating the worst case apple!!

    {
        Program.writeat(5, 80, "apple start   ");
        Coord apple;
        Coord newCoord;
        do
        {
            int y = rand.Next(maxY + 1);
            //Console.WriteLine(y);
            int x = rand.Next(maxX + 1);
            //Console.WriteLine(x);
            newCoord = new Coord(y, x);

        } while (WormContainsCoord(newCoord));
        apple = newCoord;
        this.apple = apple;
        //Console.WriteLine(apple);
        //Console.ReadKey();
        Program.writeat(5, 80, "apple stop       ");
    }

    public void createAppleBad()
    {


    }
    public int tailHistorySize()
    {
        //return tailHistory.Count;
        return -5;
    }
    public Boolean KillTail()
    {
        return false;



    }
    public Boolean addTail()
    {
        return false;



    }

    public bool tryToCreateApple()
    {
        return false;
    }

    public bool coordInsideBounds(Coord c) =>
        c.IsInsideBounds(maxY, maxX);

    public bool WormContainsCoord(Coord c) => // this method will be expanded later on
        wormPieces.Contains(c);

    public GameStatus Move(int direction) // return value should be int or enum 
                                          // also missing some functionality
    {

        //  Console.WriteLine("old: " + wormPieces.Peek());
        Coord nextCoord = head.GetCoordInDir(direction);
        //  Console.WriteLine(nextCoord + " " + coordInsideBounds(nextCoord));
        //  Console.ReadKey();
        moves++;
        head = nextCoord;
        if (!coordInsideBounds(nextCoord))
        {
            return GameStatus.HitWall;
        }
        if (WormContainsCoord(nextCoord))
        {
            return GameStatus.HitItself;
        }


        wormPieces.Enqueue(nextCoord);


        if (nextCoord.Equals(this.apple))
        {
            // if lenght maxlenght then stop;
            // worm eats the apple and grows (last bit of tail wont get removed);
            depth++;
            Program.writeat(17, 3, depth + " / " + ((maxY + 1) * (maxX + 1)));
            Program.writeat(18, 3, tailHistoryList.Count + "");
            // Program.writeat(23, 55, apple + " ATE APPLE AT");
            if (wormPieces.Count >= maxWormLenght)
            {
                return GameStatus.GameWon; // game won
            }
            Program.writeat(23, 55, wormPieces.Count + " tailhistoryshouldbe " + howMuchTailHistoryShouldBe() + "   ");
            if (tailHistoryList.Count < howMuchTailHistoryShouldBe())
            {
                if (getHeadTailHistoryDistance() > 2)
                {
                    if (!tailHistoryList.Contains(h1) && getHeadTailHistoryDistance() > avoidtailGrowthDistance)
                        tailHistoryList.Insert(0, h1);
                    // return GameStatus.AteAppleAndGrew;
                }
            }
            this.tailhistorylimit = howMuchTailHistoryShouldBe();

            createApple();
            return GameStatus.AteAppleAndGrew;
        }

        //removedTail = wormPieces.Dequeue();

        removedTail = wormPieces.Dequeue();
        tailHistoryList.Add(removedTail);
        if (moves > initialTailHistorySize)
        {
            h1 = tailHistoryList[0];
            tailHistoryList.RemoveAt(0);
        }

        //tailHistory.Enqueue(removedTail);
        //if (tailHistoryList.Count < 2) tailHistoryList.Add(removedTail);
        //h1 = tailHistory.Peek();

        //if (this.tailHistory.Count >= tailhistorylimit) tailHistory.Dequeue();
        // if (this.tailHistoryList.Count >= 10) tailHistoryList.RemoveAt(0);
        if (howMuchTailHistoryShouldBe() > tailHistoryList.Count)
        {
            // this.tailhistorylimit = howMuchTailHistoryShouldBe();

        }
        /*
        if (tailHistory.Count > 8)
        {
            h2 = tailHistory.Dequeue();
        }
        */
        //createApple();
        return GameStatus.MoveOk;

    }

    public int getHeadTailHistoryDistance()
    {
        if (tailHistoryList.Count == 0) return 0;
        return head.RectiLinearDistance(tailHistoryList[0]);
    }
    public int howMuchTailHistoryShouldBe()
    {

        int maxSize = (maxY + 1) * (maxX + 1);
        int maxL = maxSize / 5;
        int pyh = wormPieces.Count / 3;
        //int remaining = maxSize - length;
        if (wormPieces.Count < tailhistoryGrowsAt)
        {
            return pyh;
        }
        //if (maxL > maxSize - wormPieces.Count)
        {
            return maxSize - wormPieces.Count;

        }

        //return maxL;
    }
}


// the naming isn't ideal, I'd stick with public being camel cased CamelCased

// hashcode compute!! best hashing strategy for xy
// better hashing algoritmhm
// unckecked int uint? ei kai tarvi
// unmodifiable list static suuunnat buu boo