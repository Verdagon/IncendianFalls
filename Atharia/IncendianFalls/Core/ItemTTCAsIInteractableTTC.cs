using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class ItemTTCAsIInteractableTTC : IInteractableTTC {
  public readonly ItemTTC obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public ItemTTCAsIInteractableTTC(ItemTTC obj) {
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
    return new ItemTTCAsIDestructible(obj);
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
    return new ItemTTCAsITerrainTileComponent(obj);
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
    return new ItemTTCAsIInteractableTTC(obj);
  }

         public Void Destruct() {
    return ItemTTCExtensions.Destruct(obj);
  }
  public string Interact(Game game, Superstate superstate, Unit interactingUnit, Location containingTileLocation) {
    return ItemTTCExtensions.Interact(obj, game, superstate, interactingUnit, containingTileLocation);
  }

}
public static class ItemTTCAsIInteractableTTCCaster {
  public static ItemTTCAsIInteractableTTC AsIInteractableTTC(this ItemTTC obj) {
    return new ItemTTCAsIInteractableTTC(obj);
  }
}

}
