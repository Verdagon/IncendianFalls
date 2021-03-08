using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class FireBombTTCAsIActingTTC : IActingTTC {
  public readonly FireBombTTC obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public FireBombTTCAsIActingTTC(FireBombTTC obj) {
    this.obj = obj;
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    obj.FindReachableObjects(foundIds);
  }
  public bool Is(IActingTTC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(IActingTTC that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public IActingTTC AsIActingTTC() {
    return new FireBombTTCAsIActingTTC(obj);
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
    return new FireBombTTCAsIDestructible(obj);
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
    return new FireBombTTCAsITerrainTileComponent(obj);
  }

         public Void Destruct() {
    return FireBombTTCExtensions.Destruct(obj);
  }
  public Void Act(Game game, Superstate superstate, Location containingTileLocation) {
    return FireBombTTCExtensions.Act(obj, game, superstate, containingTileLocation);
  }

}
public static class FireBombTTCAsIActingTTCCaster {
  public static FireBombTTCAsIActingTTC AsIActingTTC(this FireBombTTC obj) {
    return new FireBombTTCAsIActingTTC(obj);
  }
}

}
