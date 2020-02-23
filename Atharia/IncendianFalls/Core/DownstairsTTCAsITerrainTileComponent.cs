using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class DownstairsTTCAsITerrainTileComponent : ITerrainTileComponent {
  public readonly DownstairsTTC obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public DownstairsTTCAsITerrainTileComponent(DownstairsTTC obj) {
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
    return new DownstairsTTCAsIDestructible(obj);
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
    return new DownstairsTTCAsITerrainTileComponent(obj);
  }

         public Void Destruct() {
    return DownstairsTTCExtensions.Destruct(obj);
  }

}
public static class DownstairsTTCAsITerrainTileComponentCaster {
  public static DownstairsTTCAsITerrainTileComponent AsITerrainTileComponent(this DownstairsTTC obj) {
    return new DownstairsTTCAsITerrainTileComponent(obj);
  }
}

}
