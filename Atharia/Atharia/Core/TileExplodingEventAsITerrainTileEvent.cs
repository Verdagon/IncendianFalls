using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class TileExplodingEventAsITerrainTileEvent : ITerrainTileEvent {
  public readonly TileExplodingEvent obj;
  public TileExplodingEventAsITerrainTileEvent(TileExplodingEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitITerrainTileEvent(ITerrainTileEventVisitor visitor) { visitor.VisitITerrainTileEvent(this); }
}
public static class TileExplodingEventAsITerrainTileEventCaster {
  public static TileExplodingEventAsITerrainTileEvent AsITerrainTileEvent(this TileExplodingEvent obj) {
    return new TileExplodingEventAsITerrainTileEvent(obj);
  }
}

}
