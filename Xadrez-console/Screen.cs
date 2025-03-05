using tabuleiro;
using xadrez;

namespace Xadrez_console {
    class Screen {

         public static void printmatch(ChessMatch match) {
            printboard(match.board);
            Console.WriteLine();
            printCapturedParts(match);
            Console.WriteLine();
            Console.WriteLine("Round: " + match.round);
            if(!match.finished) {
                Console.WriteLine("waiting for play: " + match.Player);
                if (match.check) {
                    Console.WriteLine("CHECK!");
                }
            }
            else {
                Console.WriteLine("CHECKMATE!");
                Console.WriteLine("Winner: " + match.Player);
            }
        }

        public static void printCapturedParts(ChessMatch match) {
            Console.WriteLine("Captured parts: ");
            Console.Write("White: ");
            SetConsoleColor(Color.White);
            printCollection(match.capturedPartsByColor(Color.White));
            Console.WriteLine();
            Console.ResetColor();
            Console.Write("Black: ");
            SetConsoleColor(Color.Black);
            printCollection(match.capturedPartsByColor(Color.Black));
            Console.WriteLine();
            Console.ResetColor();
        }

        public static void printCollection(HashSet<Part> collection) {
            Console.Write("[");
            foreach (Part x in collection) {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }

        public static void printboard(Board board) {

            for (int i = 0; i < board.line; i++) {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.row; j++) {
                    printPart(board.part(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void printboard(Board board, bool[,] possiblePositions) {

            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor changedBackground = ConsoleColor.DarkGray;

            for (int i = 0; i < board.line; i++) {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.row; j++) {
                    if(possiblePositions[i, j]) {
                        Console.BackgroundColor = changedBackground;
                    }
                    else {
                        Console.BackgroundColor = originalBackground;
                    }
                    printPart(board.part(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = originalBackground;
        }

        public static void printPart(Part part) {

            if (part == null) {
                Console.Write("- ");
            }
            else {
                if (part.color == Color.White) {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(part);
                    Console.ForegroundColor = aux;
                }
                else {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(part);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }

        public static ChessPosition readChessPosition() {
            string s = Console.ReadLine().Replace(" ",  "");
            char row = s[0];
            int line = int.Parse(s[1] + "");
            return new ChessPosition(row, line);
        }
        private static void SetConsoleColor(Color player) {
            if (player == Color.White) {
                Console.ForegroundColor = ConsoleColor.White;
            }
            else {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
        }
    }
}
