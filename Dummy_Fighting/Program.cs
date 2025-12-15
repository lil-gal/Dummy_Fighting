using System;
using System.IO;
using System.Text.RegularExpressions;
namespace Dummy_Fighting {
    internal class Program {

        



        public static int ww = Console.WindowWidth;
        public static int wh = Console.WindowHeight;
        public static readonly Regex AnsiRegex = new Regex(@"\x1B\[[0-9;]*m", RegexOptions.Compiled);
        static MapManager map = new MapManager();

        static void Main() {
            ANSI.changeColor();
            Console.Clear();
            Console.CursorVisible = false;

            Player player = new Player();
            map.setPlayer(player);
            
            Npc enemy = new Npc();

            
            while(true){
                reCheckWindowProps();
                if (Console.KeyAvailable) {
                    CheckInput(Console.ReadKey(true).Key);
                }

                //map.printMap();
                battling(new List<Dummy> {player}, new List<Dummy> {enemy});


                
            }
            
        }


        static void battling(List<Dummy> leftSide, List<Dummy> rightSide) {
            printBattle(leftSide, rightSide);

            //order left side, then right side:
            foreach(Dummy dum in leftSide) {
                dum.applyEffects();
                dum.takeTurn(leftSide, rightSide);
                
                printBattle(leftSide, rightSide);
            }

            foreach(Dummy dum in rightSide) {
                dum.applyEffects();
                dum.takeTurn(rightSide, leftSide);
                
                printBattle(leftSide, rightSide);
            }
            

            printBattle(leftSide, rightSide);
        }

        

        static void printBattle(List<Dummy> leftSide, List<Dummy> rightSide) {
            int gap = 3; // lines between each character
            int curHeight = 0; // current height im writing at
            int fromRight = 35;
            ANSI.changeColor();

            //Console.SetCursorPosition(0,0);  // basically done by now, leaving it here as a backup?

            



            foreach(Dummy leftCharacter in leftSide) {
                Console.SetCursorPosition(0,curHeight);
                Console.Write(leftCharacter);


                //set new height for the next character
                curHeight = leftCharacter.ToString().Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None).Length + gap;
            }

            curHeight = 0;

            foreach(Dummy rightCharacter in rightSide) {

                foreach (string line in rightCharacter.ToStringArr()) {
                    Console.SetCursorPosition(ww-fromRight,curHeight);
                    Console.Write(line);
                    curHeight++;
                }


                //set new height for the next character
                curHeight = rightCharacter.ToString().Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None).Length + gap;
            }





            
        }

        public static void reCheckWindowProps() {
            if(ww != Console.WindowWidth || wh != Console.WindowHeight){
                ANSI.changeColor();
                Console.Clear();
                ww = Console.WindowWidth;
                wh = Console.WindowHeight;
            }
        }



        static ConsoleKey[] moveKeys = [ConsoleKey.W,ConsoleKey.S,ConsoleKey.A,ConsoleKey.D,
        ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow];
        static void CheckInput(ConsoleKey key) {
            for (int i = 0; i < moveKeys.Length; i++) {
                if(moveKeys[i] == key){
                    if(i % 4 == 0){ map.move(0, -1); break;}
                    if(i % 4 == 1){ map.move(0, 1); break;}
                    if(i % 4 == 2){ map.move(-1, 0); break;}
                    if(i % 4 == 3){ map.move(1, 0); break;}
                }
            }

        }

        

        

        public static string StripAnsi(string input)
        {
            return AnsiRegex.Replace(input, "");
        }
        
    }
}