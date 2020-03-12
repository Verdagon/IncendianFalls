using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class UnitUnleashBideEventAsITerrainTileEvent : ITerrainTileEvent {
  public readonly UnitUnleashBideEvent obj;
  public UnitUnleashBideEventAsITerrainTileEvent(UnitUnleashBideEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void Visit(ITerrainTileEventVisitor visitor) { visitor.Visit(this); }
}
public static class UnitUnleashBideEventAsITerrainTileEventCaster {
  public static UnitUnleashBideEventAsITerrainTileEvent AsITerrainTileEvent(this UnitUnleashBideEvent obj) {
    return new UnitUnleashBideEventAsITerrainTileEvent(obj);
  }
}

}
