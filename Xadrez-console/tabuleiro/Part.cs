using tabuleiro;

namespace tabuleiro {
    internal class Part {

        public Position position { get; set; }
        public Color color { get; set; }
        public int moves { get; set; }
        public Board board { get; protected set; }

        public Part(Position position, Board board, Color color) {
            this.position = position;
            this.board = board;
            this.color = color;
            this.moves = 0;
        }

    }
}
