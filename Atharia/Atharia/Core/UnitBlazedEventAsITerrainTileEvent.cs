using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class UnitBlazedEventAsITerrainTileEvent : ITerrainTileEvent {
  public readonly UnitBlazedEvent obj;
  public UnitBlazedEventAsITerrainTileEvent(UnitBlazedEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitITerrainTileEvent(ITerrainTileEventVisitor visitor) { visitor.VisitITerrainTileEvent(this); }
}
public static class UnitBlazedEventAsITerrainTileEventCaster {
  public static UnitBlazedEventAsITerrainTileEvent AsITerrainTileEvent(this UnitBlazedEvent obj) {
    return new UnitBlazedEventAsITerrainTileEvent(obj);
  }
}

}
