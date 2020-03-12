using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class NullITerrainTileEvent : ITerrainTileEvent {
  public static NullITerrainTileEvent Null = new NullITerrainTileEvent();
  public string DStr() { return "null"; }
  public int GetDeterministicHashCode() { return 0; }
  public void Visit(ITerrainTileEventVisitor visitor) { throw new Exception("Called method on a null!"); }
}

}
