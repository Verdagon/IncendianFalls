using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class RavaArcanaLevelLinkerTTCAsIInteractableTTC : IInteractableTTC {
  public readonly RavaArcanaLevelLinkerTTC obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public RavaArcanaLevelLinkerTTCAsIInteractableTTC(RavaArcanaLevelLinkerTTC obj) {
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
    return new RavaArcanaLevelLinkerTTCAsIInteractableTTC(obj);
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
    return new RavaArcanaLevelLinkerTTCAsIDestructible(obj);
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
    return new RavaArcanaLevelLinkerTTCAsITerrainTileComponent(obj);
  }

         public string Interact(IncendianFalls.SSContext context, Game game, Superstate superstate, Unit interactingUnit, Location containingTileLocation) {
    return RavaArcanaLevelLinkerTTCExtensions.Interact(obj, context, game, superstate, interactingUnit, containingTileLocation);
  }
  public Void Destruct() {
    return RavaArcanaLevelLinkerTTCExtensions.Destruct(obj);
  }

}
public static class RavaArcanaLevelLinkerTTCAsIInteractableTTCCaster {
  public static RavaArcanaLevelLinkerTTCAsIInteractableTTC AsIInteractableTTC(this RavaArcanaLevelLinkerTTC obj) {
    return new RavaArcanaLevelLinkerTTCAsIInteractableTTC(obj);
  }
}

}
