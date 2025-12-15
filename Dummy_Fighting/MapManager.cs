using System.Drawing;

namespace Dummy_Fighting {
    internal class MapManager {
        Player player;

        int[] map = {
            0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
            0, 0, 0, 0, 0, 0, 0, 0, 1, 0,
            0, 0, 0, 0, 0, 0, 0, 1, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        };

        int mapLength = 10;
        int visibleMapLength = 5;
        int pixelSize= 3;

        public MapManager() {
            player = new Player();
        }
        public MapManager(Player player) {
            this.player = player;
        }

        public void setPlayer(Player player) {
            this.player = player;
        }

        public void printMap() {

            Console.SetCursorPosition(0,0);

            for(int i = 0; i < map.Length/mapLength; i++) { //rows
                for(int j = 0; j < mapLength; j++) { //cols
                    writeNumCode(map[i * mapLength + j], (i*mapLength +j));
                }
                Console.Write($"\x1B[{pixelSize}B\x1B[1G");
                
            }
            Console.Write(debugText);

            ANSI.changeColor(); // reset colors
            
        }
        string debugText;

        private void writeNumCode(int num, int posInMap) {
            /*
            0 - free
            1 - wall
            
            */
            char ch = ' ';

            player.getPosition(out int pX, out int pY);
            debugText = $"Debug: playerX: {pX}, playerY: {pY}";


            

            switch (num) {
                case 0:
                    ANSI.changeColor(0,255);; //white bg - black fore
                    ch = ' ';
                break;

                case 1:
                    ANSI.changeColor(255,0);
                    ch = ' ';
                break;

                default:
                    ANSI.changeColor(); // reset colors
                    ch = ' ';
                break;
            }


            if(pX == posInMap % mapLength && pY == posInMap / mapLength) {
                ch = 'P';
            }

            resizePixels(ch);
            
        }

        private void resizePixels(char ch) {
            int col = Console.CursorLeft+1;
            for (int i = 0; i < pixelSize; i++) {
                for (int j = 0; j < pixelSize; j++) {
                    Console.Write(ch);
                }
                Console.Write($"\x1B[{col}G\x1B[1B"); //1 down, pixelSize left
            }

            Console.Write($"\x1B[{pixelSize}A\x1B[{pixelSize}C"); //    //pixelSize up, pixelSize right

            ANSI.changeColor(); // reset colors
        }

        public void move(int dx, int dy) {
            player.move(dx,dy);
        }

        private void properties(int num) {

            switch (num) {
                


                default:
                break;
            }
        }
    }
}