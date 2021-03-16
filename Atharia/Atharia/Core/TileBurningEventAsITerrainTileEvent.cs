using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class TileBurningEventAsITerrainTileEvent : ITerrainTileEvent {
  public readonly TileBurningEvent obj;
  public TileBurningEventAsITerrainTileEvent(TileBurningEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitITerrainTileEvent(ITerrainTileEventVisitor visitor) { visitor.VisitITerrainTileEvent(this); }
}
public static class TileBurningEventAsITerrainTileEventCaster {
  public static TileBurningEventAsITerrainTileEvent AsITerrainTileEvent(this TileBurningEvent obj) {
    return new TileBurningEventAsITerrainTileEvent(obj);
  }
}

}
