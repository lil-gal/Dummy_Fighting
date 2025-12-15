namespace Dummy_Fighting {
    class ANSI {
        public static void changeColor(int fore = 255, int back = 17) {
            Console.Write($"\x1B[38;5;{fore}m");
            Console.Write($"\x1B[48;5;{back}m");
        }
    }
}