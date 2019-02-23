using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class DecorativeTerrainTileComponentAsITerrainTileComponent : ITerrainTileComponent {
  public readonly DecorativeTerrainTileComponent obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public DecorativeTerrainTileComponentAsITerrainTileComponent(DecorativeTerrainTileComponent obj) {
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
    return new DecorativeTerrainTileComponentAsITerrainTileComponent(obj);
  }

       
}
public static class DecorativeTerrainTileComponentAsITerrainTileComponentCaster {
  public static DecorativeTerrainTileComponentAsITerrainTileComponent AsITerrainTileComponent(this DecorativeTerrainTileComponent obj) {
    return new DecorativeTerrainTileComponentAsITerrainTileComponent(obj);
  }
}

}
