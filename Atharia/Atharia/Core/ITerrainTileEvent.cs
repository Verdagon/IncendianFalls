using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITerrainTileEvent {
  string DStr();
  int GetDeterministicHashCode();
  void VisitITerrainTileEvent(ITerrainTileEventVisitor visitor);
}

}
