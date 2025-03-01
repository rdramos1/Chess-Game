using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tabuleiro {
    class Position {

        public int line { get; set; }
        public int row { get; set; }

        public Position(int line, int row) {
            this.line = line;
            this.row = row;
        }

        public override string ToString() {
            return line + ", " + row; 
        }

    }
}
