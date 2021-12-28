using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace mato
{

    class Ai
    {
        public int appleDistanceGreed = 0;
        Coord lastRemovedCoord;
        public Boolean pause = false;
        Coord uhraus;
        Boolean reverse = false;
        int getroutes = 0;
        int globaldepth = 0;


        int maxY = 14;
        int maxX = 14;
        int prioriTizeAt = 14;
        int eaten = 0;
        public int findTailSlack = 9;
        int maxSearchDepth = 299;
        public MatoLogic mato { get; }

        Coord[] coorArr = new Coord[1];
        int tailSize = 0;
        int moves = 0;
        public Ai()
        {
            mato = new MatoLogic(maxY + 1, maxX + 1);
            this.maxSearchDepth = (maxY + 1) * (maxX + 1);
            this.tailSize = mato.wormPieces.Count;
        }

        public bool areWormPiecesEqual()
        {
            var list1 = mato.wormPieces.ToArray();
            // var list2 = this.wormPieces.ToArray();
            return false;
        }

        public void printCarr(Coord[] uloste, int raiskaus)
        {
            for (int i = 0; i < uloste.Length; i++)
            {
                Program.writeat(10 + i, raiskaus * 15, uloste[i].ToString());
            }

        }
        bool compareTwoArr(Coord[] a, Coord[] b)
        {
            return a.SequenceEqual(b);
        }

        public List<Coord> priorityIntListToCoords(List<PriorityInt> dirs, Coord c) // can be removed?
        {
            var coords = new List<Coord>();

            foreach (PriorityInt pInt in dirs)
            {
                Coord next = c.GetCoordInDir(pInt.integer);
                coords.Add(next);
            }
            return coords;

        }


        public List<int> removeDirsThatAreInTailHistoryAndReturnIntList(List<PriorityInt> dirs, Coord c, List<Coord> tailHistory)
        {
            List<int> boo = new List<int>();
            foreach (PriorityInt pInt in dirs)
            {
                var nextLocation = c.GetCoordInDir(pInt.integer);
                if (tailHistory.Contains(nextLocation))
                {
                    continue;
                }
                boo.Add(pInt.integer);
            }
            return boo;
        }


        List<PriorityInt> prioritize1(Coord head, List<Coord> wormPieces, Coord dest)
        {
            var dirs2 = new List<PriorityInt>();
            var bruh = head.GetDirectionsInsideBounds(maxY, maxX);
            foreach (int dir in bruh)
            {
                Coord c = head.GetCoordInDir(dir);
                if (wormPieces.Contains(c))
                {
                    //Console.WriteLine("fecal matter");
                    continue;
                }

                dirs2.Add(new PriorityInt(c.RectiLinearDistance(dest), dir)); // change this
            }
            if (wormPieces.Count < prioriTizeAt || head.RectiLinearDistance(dest) <= appleDistanceGreed)
            {
                dirs2.Sort();
            }
            //dirs2.Reverse();
            return dirs2;
        }

        bool canReachTailtail(LinkedList<Coord> wormPieces, Coord dest)
        {

            return false;
        }
        public void purgeSnake()
        {


        }
        public List<int> GetRoute2()
        {
            this.pause = false;
            reverse = false;
            getroutes++;
            Program.writeat(27, 1, "getroute " + getroutes + " begin  ");
            int depth = 0;
            int tailSearchCalls = 0;
            List<Coord> currentWPx = new List<Coord>(mato.wormPieces);
            List<Coord> currenTailHx = new List<Coord>(mato.tailHistoryList);
            var listOfMoves = new List<int>();
            Coord apple = mato.apple;
            int maxSearchCalls = 64900;
            int maxSearchCallsForSearch = 24900;
            int maxTailSearchCalls = 625;
            int searchCalls = 0;
            int tailsearch = 0;
            int initialDepthLimit = 20;
            int depthLimit = 33;
            int depthStep = 33;
            int deepestTail = 0;

            //if (!search(currentWPx, currenTailHx, 0) && mato.tailHistory.Count > 2)
            //search(currentWPx, currenTailHx, 0);
            // if (false)
            //if (!search(currentWPx, currenTailHx, 0) && mato.tailHistory.Count > 2)
            bool searchOk = false;

            while (depthLimit < maxSearchDepth)
            {
                Program.writeat(16, 40, "depthlimit " + depthLimit + "              ");
                searchOk = search(currentWPx, currenTailHx, 0, depthLimit) && mato.tailHistoryList.Count > 2; //??
                Console.WriteLine(searchOk);
                //Console.ReadKey();

                if (listOfMoves.Count > 0)
                {
                    searchOk = true;
                    Program.writeat(13, 50, "route from search 1             ");
                    break;
                }
                //Console.WriteLine("UREAUREAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                //Console.ReadKey();
                depthLimit += depthStep;
                searchCalls = 0;
                tailsearch = 0;

            }


            if (listOfMoves.Count == 0)
            {
                if (mato.tailHistoryList.Count > 0) listOfMoves = Gototail();

                if (listOfMoves.Count == 0)
                {
                    if (mato.tailHistoryList.Count > 1)
                    {
                        var uhraus = mato.tailHistoryList[0];
                        mato.h1 = uhraus;
                        mato.tailHistoryList.RemoveAt(0);
                        //uhraus = mato.tailHistory.Dequeue();
                        mato.tailhistorylimit--;
                    }


                    //Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("           ");
                    Program.writeat(25, 60, "gototail success " + mato.wormPieces.Count + "     ");
                    Program.writeat(13, 50, "route from gototail             ");
                    // this.pause = true;
                }

            }


            bool search(List<Coord> wormPieces, List<Coord> tailHistory, int searchDepth, int depthLimitt)
            {

                //Console.WriteLine(searchDepth + "                     ");
                if (searchDepth > depthLimitt + 1)
                {
                    // Console.WriteLine("ureaaa");
                    return false;
                }
                reverse = false;

                searchCalls++;
                if (searchCalls % 2000 == 1)
                {
                    Program.writeat(21, 0, "SEARCH WP " + wormPieces.Count + " th " + tailHistory.Count + "  mato limit  " + mato.tailhistorylimit);
                    Program.writeat(22, 0, searchCalls + " sc   -> dp " + searchDepth + "          ");
                    Program.writeat(23, 0, depthLimit + " dlll          ");

                }
                if (searchCalls > maxSearchCallsForSearch) return false;
                //Console.WriteLine(apina);
                //if (searchDepth > wormPieces.Count + tailHistory.Count + 100) { return false; }

                var head = wormPieces.Last();

                var prioritizedDirs = prioritize1(head, wormPieces, apple);
                var dirs = removeDirsThatAreInTailHistoryAndReturnIntList(prioritizedDirs, head, tailHistory);

                foreach (int dir in dirs)
                {
                    // new wormpieces -> serachin parametriksi
                    var nextLocation = head.GetCoordInDir(dir);
                    if (tailHistory.Contains(nextLocation))
                    {
                        Console.WriteLine("shouldnt be happeningxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
                        continue;
                    }
                    if (wormPieces.Contains(nextLocation))
                    {
                        Console.WriteLine("hups be happeningxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
                        continue;
                    }

                    var currentWP1 = new List<Coord>(wormPieces);
                    var currenTailH1 = new List<Coord>(tailHistory);

                    currentWP1.Add(nextLocation);




                    if (nextLocation.Equals(apple))
                    {
                        if (tailHistory.Contains(nextLocation))
                        {
                            Console.WriteLine("shouldnt be happeningxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
                            continue;
                        }
                        if (wormPieces.Contains(nextLocation))
                        {
                            Console.WriteLine("hups be happening1111111111111111111111111111111111111111111111111111");
                            continue;

                        }
                        deepestTail = 0;
                        if (mato.depth > 3 && tailHistory.Count != 0)
                        //if (false) // pakko olla >= ?
                        {
                            Program.writeat(0, 73, "tailsearch ? " + tailSearchCalls + "    ");
                            var newtailh = new List<Coord>(currenTailH1);

                            //newtailh.RemoveAt(0); // mikä funktio?
                            Coord dest = currenTailH1[0];
                            Program.writeat(20, 50, dest.ToString());

                            tailSearchCalls++;
                            //tailSearchCalls++;
                            Program.writeat(10, 9, tailSearchCalls + "              u");
                            if (tailSearchCalls > maxTailSearchCalls)
                            {
                                searchCalls = 9999999;
                                return false;
                            }
                            int maax = 33;
                            // if (true)
                            // tailsearch = 0;
                            bool ureaurea = false;
                            while (ureaurea || maax < maxSearchDepth)
                            {

                                tailsearch = 0;
                                ureaurea = canFindTail(new List<Coord>(currentWP1), newtailh, 0, dest, maax);
                                //listOfMoves.Add(dir);
                                if (ureaurea)
                                {

                                    Program.drawLines(maxY + 1, 55);
                                    Program.drawTailHistory2(newtailh, 55);
                                    Program.drawSnake2(currentWP1, 55);

                                    Console.SetCursorPosition(55, maxY + 2);
                                    listOfMoves.Add(dir);
                                    return true;
                                }
                                if (deepestTail < maax)
                                {
                                    return false;
                                }
                                maax += 33;
                            }
                            return false;

                            /*if (canFindTail(new List<Coord>(currentWP1), currenTailH1, 0, dest,maax))
                            {
                                
                            }*/

                        }
                        else
                        {
                            //Console.WriteLine("feces");
                            listOfMoves.Add(dir);
                            return true;

                        }


                    }
                    currenTailH1.Add(currentWP1[0]);
                    currentWP1.RemoveAt(0);
                    currenTailH1.RemoveAt(0);

                    if (search(currentWP1, currenTailH1, searchDepth + 1, depthLimit))
                    {
                        listOfMoves.Add(dir);
                        return true;
                    }

                }
                return false;
            }

            bool canFindTail(List<Coord> wormPieces, List<Coord> tailHistory, int searchDepth, Coord dest, int maxDepth)

            {
                if (searchDepth > maxDepth + 1)
                {
                    return false;
                }

                if (searchDepth > deepestTail)
                {
                    deepestTail = searchDepth;
                }
                tailsearch++;
                if (tailsearch % 16100 == 0)
                {
                    Program.writeat(24, 0, "TAIL WP " + wormPieces.Count + " th " + tailHistory.Count + "  mato limit  " + mato.tailhistorylimit);
                    Program.writeat(25, 0, tailsearch + " sc   -> dp " + maxDepth + "/" + searchDepth + "         ");
                }
                //Console.WriteLine(apina);
                //if (searchDepth > maxSearchDepth - wormPieces.Count - tailHistory.Count + 1)

                if (tailsearch > 15000)
                {
                    return false;
                }
                //Console.WriteLine("apina                   23222#####" + apina);
                var head = wormPieces.Last();

                var prioritizedDirs = prioritize1(head, wormPieces, dest);
                var dirs = removeDirsThatAreInTailHistoryAndReturnIntList(prioritizedDirs, head, tailHistory);


                foreach (int dir in dirs)
                {
                    // new wormpieces -> serachin parametriksi
                    var nextLocation = head.GetCoordInDir(dir);

                    if (tailHistory.Contains(nextLocation)) // shouldn't be here
                    {
                        continue;
                    }

                    if (nextLocation.Equals(dest)) // can be removed?
                    {
                        return true;
                    }

                    var currentWP1 = new List<Coord>(wormPieces);
                    var currenTailH1 = new List<Coord>(tailHistory);

                    currentWP1.Add(nextLocation);

                    if (searchDepth >= findTailSlack)
                    {
                        currenTailH1.Add(currentWP1[0]);
                        currentWP1.RemoveAt(0);
                        currenTailH1.RemoveAt(0);
                    }
                    if (canFindTail(currentWP1, currenTailH1, searchDepth + 1, dest, maxDepth))
                    {
                        return true;
                    }

                }
                //Console.WriteLine("false!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                return false;
            }

            listOfMoves.Reverse();
            //Console.WriteLine("hop");
            Program.writeat(28, 1, "getroute " + getroutes + "  end      ");
            return listOfMoves;
        }

        public List<int> Gototail()

        {
            Program.writeat(29, 20, "gototail");
            // Console.WriteLine("gototail");
            //Console.ReadKey();
            int depth = 0;
            int feces141 = 0;
            List<Coord> currentWPx = new List<Coord>(mato.wormPieces);
            List<Coord> currenTailHx = new List<Coord>(mato.tailHistoryList);
            //currenTailHx.RemoveAt(0);
            List<int> listOfMoves = new List<int>();
            Coord apple = mato.apple;
            Coord dest = currenTailHx[0];
            currenTailHx.RemoveAt(0);
            int maxcalls = 850000;
            int calls = 0;

            search3(currentWPx, currenTailHx, 0, dest);
            bool search3(List<Coord> wormPieces, List<Coord> tailHistory, int searchDepth, Coord dest)
            {
                if (calls > maxcalls) return false;
                //Console.WriteLine(apina);
                calls++;
                if (searchDepth > maxSearchDepth) { return false; }
                //if (wormPieces.Contains(dest)) return false;
                var head = wormPieces.Last();
                if (calls % 25000 == 0)
                {
                    Program.writeat(25, 60, "GOIN TAIL: " + calls + "         ");
                }
                var prioritizedDirs = prioritize1(head, wormPieces, dest);
                var dirs = removeDirsThatAreInTailHistoryAndReturnIntList(prioritizedDirs, head, tailHistory);
                foreach (int dir in dirs)
                {
                    // new wormpieces -> serachin parametriksi
                    var nextLocation = head.GetCoordInDir(dir);

                    if (tailHistory.Contains(nextLocation)) // shouldn't be here
                    {
                        //if (!tailHistory[0].Equals(nextLocation))
                        continue;

                    }
                    if (wormPieces.Contains(nextLocation))
                    {
                        Console.WriteLine("hups be happeningxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
                        continue;
                    }
                    //dest = tailHistory[0];
                    if (nextLocation.Equals(mato.apple)) // can be removed?
                    {
                        //continue;
                    }

                    var currentWP1 = new List<Coord>(wormPieces);
                    var currenTailH1 = new List<Coord>(tailHistory);

                    if (nextLocation.Equals(dest)) // can be removed?
                    {
                        currentWP1.Add(nextLocation);
                        currenTailH1.Add(currentWP1[0]);
                        currentWP1.RemoveAt(0);
                        currenTailH1.RemoveAt(0);

                        //if (wormPieces.Contains(dest) || tailHistory.Contains(dest)) continue;
                        Program.drawLines(maxY + 1, 55);
                        Program.drawTailHistory2(currenTailH1, 55);
                        Program.drawSnake2(currentWP1, 55);

                        Console.SetCursorPosition(55, maxY + 2);
                        //this.pause = true;
                        listOfMoves.Add(dir);
                        return true;
                    }
                    currentWP1.Add(nextLocation);
                    currenTailH1.Add(currentWP1[0]);
                    currentWP1.RemoveAt(0);
                    currenTailH1.RemoveAt(0);

                    if (search3(currentWP1, currenTailH1, searchDepth + 1, dest))
                    {
                        listOfMoves.Add(dir);
                        return true;
                    }

                }

                return false;
            }


            if (listOfMoves.Count > 14)
            {
                Random rnd = new Random();
                var boo = listOfMoves.TakeLast(rnd.Next(9) + 3).ToList<int>();
                return boo;
            }
            return listOfMoves;
        }

    }
}

