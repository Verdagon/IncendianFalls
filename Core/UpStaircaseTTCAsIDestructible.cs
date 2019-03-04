using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class UpStaircaseTTCAsIDestructible : IDestructible {
  public readonly UpStaircaseTTC obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public UpStaircaseTTCAsIDestructible(UpStaircaseTTC obj) {
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
    return new UpStaircaseTTCAsIDestructible(obj);
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
    return new UpStaircaseTTCAsITerrainTileComponent(obj);
  }

         public Void Destruct() {
    return UpStaircaseTTCExtensions.Destruct(obj);
  }

}
public static class UpStaircaseTTCAsIDestructibleCaster {
  public static UpStaircaseTTCAsIDestructible AsIDestructible(this UpStaircaseTTC obj) {
    return new UpStaircaseTTCAsIDestructible(obj);
  }
}

}
