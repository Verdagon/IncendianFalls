using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domino {
  public struct SymbolId {
    public readonly string name;
    public readonly int qualityPercent;

    public SymbolId(string name, int qualityPercent) {
      this.name = name;
      this.qualityPercent = qualityPercent;
    }
  }
}
