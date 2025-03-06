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
        public Part vulnerableEnPassant { get; private set; }

        public ChessMatch() {
            board = new Board(8, 8);
            round = 1;
            Player = Color.White;
            finished = false;
            check = false;
            vulnerableEnPassant = null;
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

            // #specialmove castling kingside rook
            if (p is King && destiny.row == origin.row + 2) {
                Position originT = new Position(origin.line, origin.row + 3);
                Position destinyT = new Position(origin.line, origin.row + 1);
                Part T = board.RemovePart(originT);
                T.increaseMoves();
                board.PutPart(T, destinyT);
            }
            // #specialmove castling queenside rook
            if (p is King && destiny.row == origin.row - 2) {
                Position originT = new Position(origin.line, origin.row - 4);
                Position destinyT = new Position(origin.line, origin.row - 1);
                Part T = board.RemovePart(originT);
                T.increaseMoves();
                board.PutPart(T, destinyT);
            }

            // #specialmove en passant
            if (p is Pawn) {
                if (origin.row != destiny.row && capturedPart == null) {
                    Position posP;
                    if (p.color == Color.White) {
                        posP = new Position(destiny.line + 1, destiny.row);
                    }
                    else {
                        posP = new Position(destiny.line - 1, destiny.row);
                    }
                    capturedPart = board.RemovePart(posP);
                    capturedParts.Add(capturedPart);
                }
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

            // #specialmove castling kingside rook
            if (p is King && destiny.row == origin.row + 2) {
                Position originT = new Position(origin.line, origin.row + 3);
                Position destinyT = new Position(origin.line, origin.row + 1);
                Part T = board.RemovePart(destinyT);
                T.decreaseMoves();
                board.PutPart(T, originT);
            }
            // #specialmove castling queenside rook
            if (p is King && destiny.row == origin.row - 2) {
                Position originT = new Position(origin.line, origin.row - 4);
                Position destinyT = new Position(origin.line, origin.row - 1);
                Part T = board.RemovePart(destinyT);
                T.decreaseMoves();
                board.PutPart(T, originT);
            }
            // #specialmove en passant
            if (p is Pawn) {
                if (origin.row != destiny.row && capturedPart == vulnerableEnPassant) {
                    Part pawn = board.RemovePart(destiny);
                    Position posP;
                    if (p.color == Color.White) {
                        posP = new Position(3, destiny.row);
                    }
                    else {
                        posP = new Position(4, destiny.row);
                    }
                    board.PutPart(pawn, posP);
                }
            }

        }

        public void performPlay(Position origin, Position destiny) {
            Part capturedPart = movePart(origin, destiny);

            if (inCheck(Player)) {
                undoPlay(origin, destiny, capturedPart);
                throw new BoardException("You can't put yourself in check!");
            }

            Part p = board.part(destiny);

            // #specialmove promotion
            if (p is Pawn) {
                if ((p.color == Color.White && destiny.line == 0) || (p.color == Color.Black && destiny.line == 7)) {
                    p = board.RemovePart(destiny);
                    parts.Remove(p);
                    Part queen = new Queen(board, p.color);
                    board.PutPart(queen, destiny);
                    parts.Add(queen);
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

                // #specialmove en passant

                if (p is Pawn && (destiny.line == origin.line - 2 || destiny.line == origin.line + 2)) {
                    vulnerableEnPassant = p;
                }
                else {
                    vulnerableEnPassant = null;
                }
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
            putNewPart('a', 1, new Tower(board, Color.White));
            putNewPart('b', 1, new Horse(board, Color.White));
            putNewPart('c', 1, new Bishop(board, Color.White));
            putNewPart('d', 1, new Queen(board, Color.White));
            putNewPart('e', 1, new King(board, Color.White, this));
            putNewPart('f', 1, new Bishop(board, Color.White));
            putNewPart('g', 1, new Horse(board, Color.White));
            putNewPart('h', 1, new Tower(board, Color.White));
            putNewPart('a', 2, new Pawn(board, Color.White, this));
            putNewPart('b', 2, new Pawn(board, Color.White, this));
            putNewPart('c', 2, new Pawn(board, Color.White, this));
            putNewPart('d', 2, new Pawn(board, Color.White, this));
            putNewPart('e', 2, new Pawn(board, Color.White, this));
            putNewPart('f', 2, new Pawn(board, Color.White, this));
            putNewPart('g', 2, new Pawn(board, Color.White, this));
            putNewPart('h', 2, new Pawn(board, Color.White, this));

            //pretas
            putNewPart('a', 8, new Tower(board, Color.Black));
            putNewPart('b', 8, new Horse(board, Color.Black));
            putNewPart('c', 8, new Bishop(board, Color.Black));
            putNewPart('d', 8, new Queen(board, Color.Black));
            putNewPart('e', 8, new King(board, Color.Black, this));
            putNewPart('f', 8, new Bishop(board, Color.Black));
            putNewPart('g', 8, new Horse(board, Color.Black));
            putNewPart('h', 8, new Tower(board, Color.Black));
            putNewPart('a', 7, new Pawn(board, Color.Black, this));
            putNewPart('b', 7, new Pawn(board, Color.Black, this));
            putNewPart('c', 7, new Pawn(board, Color.Black, this));
            putNewPart('d', 7, new Pawn(board, Color.Black, this));
            putNewPart('e', 7, new Pawn(board, Color.Black, this));
            putNewPart('f', 7, new Pawn(board, Color.Black, this));
            putNewPart('g', 7, new Pawn(board, Color.Black, this));
            putNewPart('h', 7, new Pawn(board, Color.Black, this));
            
        }

    }
}
