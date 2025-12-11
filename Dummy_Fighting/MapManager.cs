using System.Drawing;

namespace Dummy_Fighting {
    internal class MapManager {
        Player player;

        int[] map = {
            0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
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

        public MapManager(Player player) {
            this.player = player;
        }

        public void printMap() {

            Console.SetCursorPosition(0,0);

            for(int i = 0; i < map.Length/mapLength; i++) { //rows
                for(int j = 0; j < mapLength; j++) {
                    writeNumCode(map[i * mapLength + j]);
                }
                Console.WriteLine();
                
            }

            Console.Write("\x1B[39;49m"); // reset colors
            
        }

        private void writeNumCode(int num) {
            /*
            0 - free
            1 - wall
            
            */
            switch (num) {
                case 0:
                    Console.Write("\x1B[30;47m "); //white bg - black fore
                    Console.Write(' ');
                break;

                case 1:
                    Console.Write("\x1B[37;40m ");
                    Console.Write(' ');
                break;

                default:
                    Console.Write("\x1B[39;49m"); // reset colors
                break;
            }
            
        }

        public void move(int dx, int dy) {
            
        }
    }
}