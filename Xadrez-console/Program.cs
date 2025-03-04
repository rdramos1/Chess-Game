using System;
using tabuleiro;
using xadrez;

namespace Xadrez_console {
    class Program {
        static void Main(string[] args) {
            ChessMatch match = new ChessMatch();

            while (!match.finished) {
                try {
                    Console.Clear();
                    Screen.printboard(match.board);
                    Console.WriteLine();

                    Console.WriteLine("Round: " + match.round);
                    Console.Write("waiting for play: ");
                    SetConsoleColor(match.Player);
                    Console.WriteLine(match.Player);
                    Console.ResetColor();

                    Console.WriteLine();
                    Console.Write("Origin: ");
                    Position origin = Screen.readChessPosition().toPosition();
                    match.validateOriginPosition(origin);

                    bool[,] possiblePositions = match.board.part(origin).possibleMovements();

                    Console.Clear();
                    Screen.printboard(match.board, possiblePositions);

                    Console.WriteLine("");
                    Console.Write("Destiny: ");
                    Position destiny = Screen.readChessPosition().toPosition();
                    match.validateDestinyPosition(origin, destiny);

                    match.performPlay(origin, destiny);

                } catch (BoardException e) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ResetColor();
                    Console.ReadLine();
                }
            }
        }

        static void SetConsoleColor(Color player) {
            if (player == Color.White) {
                Console.ForegroundColor = ConsoleColor.White;
            }
            else {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
        }
    }
}
