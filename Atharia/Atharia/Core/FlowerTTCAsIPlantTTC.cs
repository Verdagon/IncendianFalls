using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class FlowerTTCAsIPlantTTC : IPlantTTC {
  public readonly FlowerTTC obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public FlowerTTCAsIPlantTTC(FlowerTTC obj) {
    this.obj = obj;
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    obj.FindReachableObjects(foundIds);
  }
  public bool Is(IDestructible that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(IDestructible that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public IDestructible AsIDestructible() {
    return new FlowerTTCAsIDestructible(obj);
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
    return new FlowerTTCAsITerrainTileComponent(obj);
  }
  public bool Is(IPlantTTC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(IPlantTTC that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public IPlantTTC AsIPlantTTC() {
    return new FlowerTTCAsIPlantTTC(obj);
  }

         public Void Destruct() {
    return FlowerTTCExtensions.Destruct(obj);
  }

}
public static class FlowerTTCAsIPlantTTCCaster {
  public static FlowerTTCAsIPlantTTC AsIPlantTTC(this FlowerTTC obj) {
    return new FlowerTTCAsIPlantTTC(obj);
  }
}

}
