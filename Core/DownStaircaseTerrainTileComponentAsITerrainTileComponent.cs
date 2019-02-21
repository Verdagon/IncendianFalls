using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class DownStaircaseTerrainTileComponentAsITerrainTileComponent : ITerrainTileComponent {
  public readonly DownStaircaseTerrainTileComponent obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool NullableIs(ITerrainTileComponent that) {
  if (!this.Exists() && !that.Exists()) {
    return true;
  }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
  return this.Is(that);
}
public bool Is(ITerrainTileComponent that) {
  if (!this.Exists()) {
    throw new Exception("Called Is on a null!");
  }
  if (!that.Exists()) {
    throw new Exception("Called Is on a null!");
  }
  return root == that.root && obj.id == that.id;
}
public bool Exists() { return obj.Exists(); }
public DownStaircaseTerrainTileComponentAsITerrainTileComponent(DownStaircaseTerrainTileComponent obj) {
  this.obj = obj;
}
       
}
public static class DownStaircaseTerrainTileComponentAsITerrainTileComponentCaster {
  public static DownStaircaseTerrainTileComponentAsITerrainTileComponent AsITerrainTileComponent(this DownStaircaseTerrainTileComponent obj) {
    return new DownStaircaseTerrainTileComponentAsITerrainTileComponent(obj);
  }
}

}
