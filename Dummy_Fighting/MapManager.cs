using System.Drawing;

namespace Dummy_Fighting {
    internal class MapManager {
        Player player;

        int[] map = {
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
            0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 
            0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1,
            0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
            0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        };

        int mapLength = 20;
        int visibleMapLength = 5; //less than map length!!!! less than mpa height!!! ALWAYS ODD
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
            int startPrintX = 5;
            int startPrintY = 5;

            Console.SetCursorPosition(startPrintX, startPrintY);


            player.getPosition(out int pX, out int pY);

            int visibleMapLengthHalf = visibleMapLength/2;

            
            int startCol = Math.Max(0, pX+1 - visibleMapLengthHalf-1);
            if(pX + visibleMapLength-1 > mapLength){
                startCol = mapLength-visibleMapLength;
            }
            int cols = startCol + visibleMapLength;

            int startRow = Math.Max(0, pY+1 - visibleMapLengthHalf-1);
            if(pY + visibleMapLength-1 > map.Length/mapLength){
                startRow = map.Length/mapLength-visibleMapLength;
            }
            int rows = startRow + visibleMapLength;

            for(int i = startRow; i < rows; i++) { //rows
                for(int j = startCol; j < cols; j++) { //cols

                    if(XYintoMAP(j,i) > map.Length-1){return;}

                    writeNumCode(map[XYintoMAP(j,i)], (XYintoMAP(j,i)));
                }
                Console.Write($"\x1B[{pixelSize}B\x1B[{startPrintX+1}G");
                
            }
            debugText += $" visX: ({startCol}) visHalf: {visibleMapLengthHalf}";

            
            Console.Write(debugText);

            ANSI.changeColor(); // reset colors
            
        }
        string debugText;

        private int XYintoMAP(int x, int y) => y * mapLength + x;
        private int MAPintoX(int m) => m % mapLength;
        private int MAPintoY(int m) => m / mapLength;

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


            if(pX == MAPintoX(posInMap) && pY == MAPintoY(posInMap)) {
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
            if (!canMove(dx, dy)) {return;}
            player.move(dx,dy);
        }

        private bool canMove(int dx, int dy) {
            player.getPosition(out int pX, out int pY);
            int dirPos = XYintoMAP(pX+dx,pY+dy);

            if(dirPos < 0 || dirPos > map.Length-1 || pX+dx < 0 || pX+dx > mapLength-1){return false;} //map boundaries

            properties(map[dirPos], out bool passable);

            if(!passable){return false;} //not passable obj

            return true;

            
        }

        private void properties(int num, out bool passable) {
            passable = num switch {
                0 => true,
                1 => false,
                _ => false,
            };
        }
    }
}