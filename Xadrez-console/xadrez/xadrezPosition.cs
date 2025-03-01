using tabuleiro;

namespace xadrez {
    class xadrezPosition {

        public char row { get; set; }
        public int line { get; set; }   

        public xadrezPosition(char row, int line) {
            this.row = row;
            this.line = line;
        }

        public Position toPosition() {
            return new Position(8 - line, row - 'a');
        }

        override public string ToString() {
            return "" + row + line;
        }

    }
}
