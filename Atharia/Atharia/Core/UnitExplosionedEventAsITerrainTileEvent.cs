using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class UnitExplosionedEventAsITerrainTileEvent : ITerrainTileEvent {
  public readonly UnitExplosionedEvent obj;
  public UnitExplosionedEventAsITerrainTileEvent(UnitExplosionedEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitITerrainTileEvent(ITerrainTileEventVisitor visitor) { visitor.VisitITerrainTileEvent(this); }
}
public static class UnitExplosionedEventAsITerrainTileEventCaster {
  public static UnitExplosionedEventAsITerrainTileEvent AsITerrainTileEvent(this UnitExplosionedEvent obj) {
    return new UnitExplosionedEventAsITerrainTileEvent(obj);
  }
}

}
