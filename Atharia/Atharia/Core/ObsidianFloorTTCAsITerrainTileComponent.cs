using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class ObsidianFloorTTCAsITerrainTileComponent : ITerrainTileComponent {
  public readonly ObsidianFloorTTC obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public ObsidianFloorTTCAsITerrainTileComponent(ObsidianFloorTTC obj) {
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
    return new ObsidianFloorTTCAsIDestructible(obj);
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
    return new ObsidianFloorTTCAsITerrainTileComponent(obj);
  }

         public Void Destruct() {
    return ObsidianFloorTTCExtensions.Destruct(obj);
  }

}
public static class ObsidianFloorTTCAsITerrainTileComponentCaster {
  public static ObsidianFloorTTCAsITerrainTileComponent AsITerrainTileComponent(this ObsidianFloorTTC obj) {
    return new ObsidianFloorTTCAsITerrainTileComponent(obj);
  }
}

}
