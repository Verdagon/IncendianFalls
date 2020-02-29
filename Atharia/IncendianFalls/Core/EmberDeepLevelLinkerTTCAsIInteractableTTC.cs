using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class EmberDeepLevelLinkerTTCAsIInteractableTTC : IInteractableTTC {
  public readonly EmberDeepLevelLinkerTTC obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public EmberDeepLevelLinkerTTCAsIInteractableTTC(EmberDeepLevelLinkerTTC obj) {
    this.obj = obj;
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    obj.FindReachableObjects(foundIds);
  }
  public bool Is(IInteractableTTC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(IInteractableTTC that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public IInteractableTTC AsIInteractableTTC() {
    return new EmberDeepLevelLinkerTTCAsIInteractableTTC(obj);
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
    return new EmberDeepLevelLinkerTTCAsIDestructible(obj);
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
    return new EmberDeepLevelLinkerTTCAsITerrainTileComponent(obj);
  }

         public string Interact(Game game, Superstate superstate, Unit interactingUnit, Location containingTileLocation) {
    return EmberDeepLevelLinkerTTCExtensions.Interact(obj, game, superstate, interactingUnit, containingTileLocation);
  }
  public Void Destruct() {
    return EmberDeepLevelLinkerTTCExtensions.Destruct(obj);
  }

}
public static class EmberDeepLevelLinkerTTCAsIInteractableTTCCaster {
  public static EmberDeepLevelLinkerTTCAsIInteractableTTC AsIInteractableTTC(this EmberDeepLevelLinkerTTC obj) {
    return new EmberDeepLevelLinkerTTCAsIInteractableTTC(obj);
  }
}

}
