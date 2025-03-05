using tabuleiro;
using xadrez;

namespace xadrez {
    class ChessMatch {

        public Board board { get; private set; }
        public int round { get; private set; }
        public Color Player { get; private set; }
        public bool finished { get; set; }
        private HashSet<Part> parts;
        private HashSet<Part> capturedParts;
        public bool check { get; private set; }

        public ChessMatch() {
            board = new Board(8, 8);
            round = 1;
            Player = Color.White;
            finished = false;
            check = false;
            parts = new HashSet<Part>();
            capturedParts = new HashSet<Part>();
            putpart();
        }

        public Part movePart(Position origin, Position destiny) {
            Part p = board.RemovePart(origin);
            p.increaseMoves();
            Part capturedPart = board.RemovePart(destiny);
            board.PutPart(p, destiny);
            if (capturedPart != null) {
                capturedParts.Add(capturedPart);
            }
            return capturedPart;
        }

        public void undoPlay(Position origin, Position destiny, Part capturedPart) {
            Part p = board.RemovePart(destiny);
            p.decreaseMoves();
            if (capturedPart != null) {
                board.PutPart(capturedPart, destiny);
                capturedParts.Remove(capturedPart);
            }
            board.PutPart(p, origin);
        }

        public void performPlay(Position origin, Position destiny) {
            Part capturedPart = movePart(origin, destiny);

            if (inCheck(Player)) {
                undoPlay(origin, destiny, capturedPart);
                throw new BoardException("You can't put yourself in check!");
            }

            if (inCheck(adversary(Player))) {
                check = true;
            }
            else {
                check = false;
            }

            if (checkMate(adversary(Player))) {
                finished = true;
            }
            else {
                round++;
                changePlayer();
            }

        }

        public void validateOriginPosition(Position pos) {
            if (board.part(pos) == null) {
                throw new BoardException("There is no part in the chosen position!");
            }
            if (Player != board.part(pos).color) {
                throw new BoardException("The chosen part is not yours!");
            }
            if (!board.part(pos).existPossibleMovements()) {
                throw new BoardException("There are no possible movements for the chosen part!");
            }
        }

        public void validateDestinyPosition(Position origin, Position destiny) {
            if (!board.part(origin).canMoveTo(destiny)) {
                throw new BoardException("Invalid destiny position!");
            }
        }

        private void changePlayer() {
            if (Player == Color.White) {
                Player = Color.Black;
            }
            else {
                Player = Color.White;
            }
        }

        public HashSet<Part> capturedPartsByColor(Color color) {
            HashSet<Part> aux = new HashSet<Part>();
            foreach (Part x in capturedParts) {
                if (x.color == color) {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Part> partsInGame(Color color) {
            HashSet<Part> aux = new HashSet<Part>();
            foreach (Part x in parts) {
                if (x.color == color) {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(capturedPartsByColor(color));
            return aux;
        }

        private Color adversary(Color color) {
            if (color == Color.White) {
                return Color.Black;
            }
            else {
                return Color.White;
            }
        }

        private Part king(Color color) {
            foreach (Part x in partsInGame(color)) {
                if (x is King) {
                    return x;
                }
            }
            return null;
        }

        public bool inCheck(Color color) {
            Part R = king(color);
            if (R == null) {
                throw new BoardException("There is no king of color " + color + " on the board!");
            }
            foreach (Part x in partsInGame(adversary(color))) {
                bool[,] mat = x.possibleMovements();
                if (mat[R.position.line, R.position.row]) {
                    return true;
                }
            }
            return false;
        }

        public bool checkMate(Color color) {
            if (!inCheck(color)) {
                return false;
            }
            foreach (Part x in partsInGame(color)) {
                bool[,] mat = x.possibleMovements();
                for (int i = 0; i < board.line; i++) {
                    for (int j = 0; j < board.row; j++) {
                        if (mat[i, j]) {
                            Position origin = x.position;
                            Position destiny = new Position(i, j);
                            Part capturedPart = movePart(origin, destiny);
                            bool checkTest = inCheck(color);
                            undoPlay(origin, destiny, capturedPart);
                            if (!checkTest) {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void putNewPart(char column, int line, Part part) {
            board.PutPart(part, new ChessPosition(column, line).toPosition());
            parts.Add(part);
        }

        private void putpart() {

            //brancas
            putNewPart('c', 1, new Tower(board, Color.White));
            putNewPart('e', 1, new Tower(board, Color.White));
            putNewPart('c', 2, new Tower(board, Color.White));
            putNewPart('d', 2, new Tower(board, Color.White));
            putNewPart('e', 2, new Tower(board, Color.White));
            putNewPart('d', 1, new King(board, Color.White));

            //Pretas
            putNewPart('c', 8, new Tower(board, Color.Black));
            putNewPart('e', 8, new Tower(board, Color.Black));
            putNewPart('c', 7, new Tower(board, Color.Black));
            putNewPart('d', 7, new Tower(board, Color.Black));
            putNewPart('e', 7, new Tower(board, Color.Black));
            putNewPart('d', 8, new King(board, Color.Black));
        }

    }
}
