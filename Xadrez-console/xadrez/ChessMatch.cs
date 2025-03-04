using tabuleiro;
using xadrez;

namespace xadrez {
    class ChessMatch {

        public Board board { get; private set; }
        public int round { get; private set; }
        public Color Player { get; private set; }
        public bool finished { get; set; }

        public ChessMatch() {
            board = new Board(8, 8);
            round = 1;
            Player = Color.White;
            finished = false;
            putpart();
        } 

        public void movePart(Position origin, Position destiny) {
            Part p = board.RemovePart(origin);
            p.increaseMoves();
            Part capturedPart = board.RemovePart(destiny);
            board.PutPart(p, destiny);
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

        private void putpart () {
            board.PutPart(new Tower(board, Color.White), new ChessPosition('c', 1).toPosition());
            board.PutPart(new Tower(board, Color.White), new ChessPosition('e', 1).toPosition());
            board.PutPart(new Tower(board, Color.White), new ChessPosition('c', 2).toPosition());
            board.PutPart(new Tower(board, Color.White), new ChessPosition('d', 2).toPosition());
            board.PutPart(new Tower(board, Color.White), new ChessPosition('e', 2).toPosition());

            board.PutPart(new King(board, Color.White), new ChessPosition('d', 1).toPosition());

            //adversario
           board.PutPart(new Tower(board, Color.Black), new ChessPosition('c', 8).toPosition());
            board.PutPart(new Tower(board, Color.Black), new ChessPosition('e', 8).toPosition());
            board.PutPart(new Tower(board, Color.Black), new ChessPosition('c', 7).toPosition());
            board.PutPart(new Tower(board, Color.Black), new ChessPosition('d', 7).toPosition());
            board.PutPart(new Tower(board, Color.Black), new ChessPosition('e', 7).toPosition());

            board.PutPart(new King(board, Color.Black), new ChessPosition('d', 8).toPosition()); 
        }

    }
}
