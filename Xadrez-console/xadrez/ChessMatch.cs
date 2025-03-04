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
        public ChessMatch() {
            board = new Board(8, 8);
            round = 1;
            Player = Color.White;
            finished = false;
            parts = new HashSet<Part>();
            capturedParts = new HashSet<Part>();
            putpart();
        } 

        public void movePart(Position origin, Position destiny) {
            Part p = board.RemovePart(origin);
            p.increaseMoves();
            Part capturedPart = board.RemovePart(destiny);
            board.PutPart(p, destiny);
            if (capturedPart != null) {
                capturedParts.Add(capturedPart);
            }
        } 

        public void performPlay(Position origin, Position destiny) {
            movePart(origin, destiny);
            round++;
            changePlayer();
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
            if(Player == Color.White) {
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
