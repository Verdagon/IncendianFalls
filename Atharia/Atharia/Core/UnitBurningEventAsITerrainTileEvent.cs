using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class UnitBurningEventAsITerrainTileEvent : ITerrainTileEvent {
  public readonly UnitBurningEvent obj;
  public UnitBurningEventAsITerrainTileEvent(UnitBurningEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitITerrainTileEvent(ITerrainTileEventVisitor visitor) { visitor.VisitITerrainTileEvent(this); }
}
public static class UnitBurningEventAsITerrainTileEventCaster {
  public static UnitBurningEventAsITerrainTileEvent AsITerrainTileEvent(this UnitBurningEvent obj) {
    return new UnitBurningEventAsITerrainTileEvent(obj);
  }
}

}
