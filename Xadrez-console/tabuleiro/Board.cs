using tabuleiro;

namespace tabuleiro {
    class Board {

        public int line { get; set; }
        public int row { get; set; }
        private Part[,] parts;

        public Board(int line, int row) {
            this.line = line;
            this.row = row;
            parts = new Part[line, row];
        }

        public Part part(int line, int row) {
            return parts[line, row];
        }

    }
}
