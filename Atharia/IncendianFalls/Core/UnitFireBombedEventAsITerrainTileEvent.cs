using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class UnitFireBombedEventAsITerrainTileEvent : ITerrainTileEvent {
  public readonly UnitFireBombedEvent obj;
  public UnitFireBombedEventAsITerrainTileEvent(UnitFireBombedEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitITerrainTileEvent(ITerrainTileEventVisitor visitor) { visitor.VisitITerrainTileEvent(this); }
}
public static class UnitFireBombedEventAsITerrainTileEventCaster {
  public static UnitFireBombedEventAsITerrainTileEvent AsITerrainTileEvent(this UnitFireBombedEvent obj) {
    return new UnitFireBombedEventAsITerrainTileEvent(obj);
  }
}

}
