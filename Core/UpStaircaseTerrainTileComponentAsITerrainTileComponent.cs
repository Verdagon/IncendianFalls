using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class UpStaircaseTerrainTileComponentAsITerrainTileComponent : ITerrainTileComponent {
  public readonly UpStaircaseTerrainTileComponent obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public UpStaircaseTerrainTileComponentAsITerrainTileComponent(UpStaircaseTerrainTileComponent obj) {
    this.obj = obj;
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
  public bool NullableIs(ITerrainTileComponent that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public ITerrainTileComponent AsITerrainTileComponent() {
    return new UpStaircaseTerrainTileComponentAsITerrainTileComponent(obj);
  }

       
}
public static class UpStaircaseTerrainTileComponentAsITerrainTileComponentCaster {
  public static UpStaircaseTerrainTileComponentAsITerrainTileComponent AsITerrainTileComponent(this UpStaircaseTerrainTileComponent obj) {
    return new UpStaircaseTerrainTileComponentAsITerrainTileComponent(obj);
  }
}

}
