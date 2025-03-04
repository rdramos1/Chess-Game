using tabuleiro;
using xadrez;

namespace xadrez {
    class ChessMatch {

        public Board board { get; private set; }
        private int round;
        private Color Player;
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
