using System;
using System.Collections;
using System.Collections.Generic;
namespace mato
{
    class Program
    {
        static int origRow = Console.CursorTop;
        static int origCol = Console.CursorLeft;


        public static void draw(int y, int x, ConsoleColor color)
        {
            char c = '█';
            Console.ForegroundColor = color;
            Console.SetCursorPosition(origCol + x, origRow + y);
            Console.Write(c);

        }
        public static void draw3(int y, int x, ConsoleColor color, char c)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = color;
            Console.SetCursorPosition(origCol + x, origRow + y);
            Console.Write(c);

        }


        public static void drawTailHistory(MatoLogic mmm, int offset)
        {
            Console.BackgroundColor = ConsoleColor.White;
            int i = 0;
            foreach (Coord c in mmm.tailHistoryList)
            {
                i++;
                writeat(c.y, c.x + offset, i % 9 + "");

            }
            Console.BackgroundColor = ConsoleColor.Black;
            draw(mmm.h1.y, mmm.h1.x + offset, ConsoleColor.Black);

        }
        public static void drawTailHistory2(List<Coord> coords, int offset)
        {
            Console.BackgroundColor = ConsoleColor.White;
            int i = 0;
            foreach (Coord c in coords)
            {
                i++;
                writeat(c.y, c.x + offset, i % 9 + "");

            }
            Console.BackgroundColor = ConsoleColor.Black;
            //draw(mmm.h1.y, mmm.h1.x + offset, ConsoleColor.Black);

        }
        public static void fuckup(List<Coord> mmm, int offset)
        {
            foreach (Coord c in mmm)
            {
                draw(c.y, c.x + offset, ConsoleColor.DarkYellow);

            }
            writeat(29, 29, mmm.Count + "");

            //Console.ReadKey();


        }

        public static void writeat(int y, int x, string text)
        {
            //Console.ForegroundColor = color;
            Console.SetCursorPosition(origCol + x, origRow + y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(text);
        }

        public static void writelistt(int y, int x, List<int> lista)
        {

            //Console.ForegroundColor = color;
            Console.SetCursorPosition(origCol + x, origRow + y);
            lista.ForEach(Console.Write);
            Console.Write("xx");

        }


        public static void drawSnake(MatoLogic ml, int xoffset)
        {

            var vp = ml.wormPieces.ToArray();
            foreach (Coord c in vp)
            {
                //  Console.WriteLine(c);
            }
            // Console.ReadKey();
            //Console.Clear();
            foreach (Coord c in vp)
            {
                draw(c.y, c.x + xoffset, ConsoleColor.Green);
            }
            draw(ml.head.y, ml.head.x + xoffset, ConsoleColor.Magenta);
            draw(ml.wormPieces.Peek().y, ml.wormPieces.Peek().x + xoffset, ConsoleColor.Yellow);
            draw(ml.apple.y, ml.apple.x + xoffset, ConsoleColor.Red);

        }

        public static void drawSnake2(List<Coord> vamma, int xoffset)
        {

            //var vp = ml.wormPieces.ToArray();
            foreach (Coord c in vamma)
            {
                //  Console.WriteLine(c);
            }
            // Console.ReadKey();
            //Console.Clear();
            foreach (Coord c in vamma)
            {
                draw(c.y, c.x + xoffset, ConsoleColor.Green);
            }

            draw3(vamma[vamma.Count - 1].y, vamma[vamma.Count - 1].x + xoffset, ConsoleColor.Yellow, 'X');
            draw3(vamma[0].y, vamma[0].x + xoffset, ConsoleColor.Yellow, 'o');
            if (vamma.Count > 1)
            {
                draw3(vamma[vamma.Count - 2].y, vamma[vamma.Count - 2].x + xoffset, ConsoleColor.Yellow, 'x');

            }
            if (vamma.Count > 2)
            {
                draw3(vamma[vamma.Count - 3].y, vamma[vamma.Count - 3].x + xoffset, ConsoleColor.Yellow, '*');

            }
            //draw(ml.head.y, ml.head.x + xoffset, ConsoleColor.Magenta);
            // draw(ml.wormPieces.Peek().y, ml.wormPieces.Peek().x + xoffset, ConsoleColor.Yellow);
            // draw(ml.apple.y, ml.apple.x + xoffset, ConsoleColor.Red);

        }
        public static void drawLines(int lines, int xoffset)
        {
            for (int i = 0; i < lines; i++)
            {
                Console.SetCursorPosition(xoffset, i);
                Console.Write("                  ");
            }
        }
        public static bool twoIntListsEqual(List<int> a, List<int> b)
        {
            if (b.Count == 0 || a.Count == 0) return false;
            if (a.Count != b.Count) return false;

            for (int i = 0; i < a.Count; i++)
            {
                if (a[i] != b[i]) return false;
            }
            return true;
        }
        static void Main(string[] args)
        {
            int routesWithouteat = 0;
            List<int> feces = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var feces2 = new List<int>(feces);
            List<int> previouslist = new List<int> { -555, 555 };
            Console.Clear();
            //int[] matka = { 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 0, 0 };
            int apina = 0;
            Console.Clear();
            //MatoLogic mato = new MatoLogic(10, 26);
            Ai aijai = new Ai();
            // 8 ,5
            var mato = aijai.mato;
            drawSnake(mato, 0);
            var end = false;
            Coord lastHead = mato.head;
            int aftereat = 0;
            do
            {
                Program.writeat(29, 20, "              ");
                //Console.ReadKey();
                //writeat(24, 4, "                       "); // reset tyhjä suuntataulukko
                List<int> matka = new List<int>();
                // writeat(24, 4, "                                 ");
                Console.ForegroundColor = ConsoleColor.Blue;
                //writelistt(12, 50, matka);
                //Console.ReadKey();
                if (routesWithouteat > 30)
                {
                    Console.WriteLine("gay"); Console.WriteLine("gay");
                    Console.WriteLine("gay");
                    Console.WriteLine("gay");
                    Console.WriteLine("gay");
                    Console.WriteLine("gay");
                    aijai.appleDistanceGreed = 1;

                }
                writeat(15, 60, aijai.findTailSlack + "  ");
            aaah:
                for (int i = 0; i < 30; i++)
                {
                    if (mato.tailHistoryList.Count == 1) break;
                    matka = aijai.GetRoute2();
                    if (matka.Count == 0)
                    {
                        // writeat(24, 30, "tyhjä suuntataulukko " + i + "  ");
                        if (aijai.findTailSlack > 0)
                        {
                            aijai.findTailSlack--;
                            writeat(4, 70, "rapist1" + i + "  ");
                            goto aaah;
                        }
                    }
                    else
                    {
                        if (twoIntListsEqual(matka, previouslist))
                        {
                            if (mato.tailHistoryList.Count > 1) mato.tailHistoryList.RemoveAt(0);


                        }
                        previouslist = matka;
                        break;
                    }
                }

                routesWithouteat++;
                //writeat(26, 66, mato.howMuchTailHistoryShouldBe() + "");
                if (matka.Count == 0)
                {

                    end = true;
                }

                aijai.findTailSlack = 9;

                previouslist = matka;
                var gayrape = false;
                aftereat = 0;
                if (aijai.pause)
                {
                    Console.ReadKey();
                }


                for (int i = 0; i < matka.Count; i++)
                {
                    writeat(12, 44, i + "/" + matka.Count + "    ");
                    if (aijai.pause) Console.ReadKey();
                    if (gayrape)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.WriteLine("FATAL ERROR !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                        Console.WriteLine("FATAL ERROR !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                        Console.WriteLine("FATAL ERROR !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                        Console.WriteLine("FATAL ERROR !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                        Console.WriteLine("FATAL ERROR !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    }
                    int suunta = matka[i];
                    //  Console.ForegroundColor = ConsoleColor.Yellow;
                    //  Console.SetCursorPosition(4, 25);
                    //  Console.WriteLine(suunta);
                    if (end)
                    {
                        break;
                    }
                    // writeat(22, 5 + apina, suunta + "");
                    apina++;
                    var result = mato.Move(suunta);
                    aftereat++;
                    switch (result)
                    {

                        case GameStatus.MoveOk:
                            var a = mato.removedTail;
                            draw(mato.removedTail.y, mato.removedTail.x, ConsoleColor.Black);
                            draw(mato.head.y, mato.head.x, ConsoleColor.Blue);
                            draw(lastHead.y, lastHead.x, ConsoleColor.Green);
                            draw(mato.apple.y, mato.apple.x, ConsoleColor.Red);
                            lastHead = mato.head;
                            //Console.WriteLine(mato.tailHistory.Count);

                            //drawTailHistory(mato, 0);
                            //  Console.WriteLine(mato.head.x);
                            break;

                        case GameStatus.AteAppleAndGrew:
                            aftereat = 0;
                            draw(mato.head.y, mato.head.x, ConsoleColor.Green);
                            draw(mato.apple.y, mato.apple.x, ConsoleColor.Red);
                            matka = new List<int>();
                            i = 999999999;

                            /*
                            Console.WriteLine("APPLEAPPLEAPPLE");
                            Console.WriteLine(matka.Count);
                            Console.WriteLine(i);
                            Console.WriteLine("APPLEAPPLEAPPLE");
                            Console.ReadKey();
                            */
                            //matka = new List<int>();
                            //drawTailHistory(mato, 0);
                            //gayrape = true;
                            aijai.appleDistanceGreed = 0;
                            routesWithouteat = 0;
                            break;

                        case GameStatus.HitWall:


                        case GameStatus.HitItself:

                            writeat(22, 33, suunta + "           HITISELF CAN UNDERSTAND WHY BOO" + mato.head);
                            writeat(23, 33, aftereat + " after eat");
                            Console.ReadKey();
                            drawSnake(mato, 20);
                            drawTailHistory(mato, 20);

                            writeat(24, 33, suunta + " maato head" + mato.head);
                            writeat(25, 33, mato.wormPieces.Peek().ToString() + " LAST TAIL");
                            writeat(26, 33, mato.apple + " APPLE");
                            draw(mato.head.y, mato.head.x, ConsoleColor.Blue);
                            draw(mato.apple.y, mato.apple.x, ConsoleColor.Red);
                            end = true;
                            Console.ReadKey();
                            draw(lastHead.y, lastHead.x + 20, ConsoleColor.Magenta);
                            Console.ReadKey();
                            //Console.WriteLine("rapeeeeeeeee");
                            writelistt(29, 4, matka);
                            Console.ReadKey();
                            goto raiskaus;
                            break;
                        case GameStatus.GameWon:
                            writeat(22, 4, result.ToString());
                            end = true;
                            Console.ReadKey();
                            break;

                    }
                    drawSnake(mato, 35);
                    drawTailHistory(mato, 35);
                    writeat(22, 33, suunta + "");
                    if (end)
                    {

                    }
                    System.Threading.Thread.Sleep(1);
                }

            } while (!end);
        raiskaus:
            Console.ReadKey();
            Console.WriteLine("end");
            Console.ReadKey();

            Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            Console.ReadKey();
        }
    }
}

