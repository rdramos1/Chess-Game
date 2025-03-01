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
        }

    }
}
