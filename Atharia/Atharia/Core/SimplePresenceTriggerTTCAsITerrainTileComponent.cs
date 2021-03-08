using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class SimplePresenceTriggerTTCAsITerrainTileComponent : ITerrainTileComponent {
  public readonly SimplePresenceTriggerTTC obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public SimplePresenceTriggerTTCAsITerrainTileComponent(SimplePresenceTriggerTTC obj) {
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
    return new SimplePresenceTriggerTTCAsIDestructible(obj);
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
    return new SimplePresenceTriggerTTCAsITerrainTileComponent(obj);
  }
  public bool Is(IPresenceTriggerTTC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(IPresenceTriggerTTC that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public IPresenceTriggerTTC AsIPresenceTriggerTTC() {
    return new SimplePresenceTriggerTTCAsIPresenceTriggerTTC(obj);
  }

         public Void Destruct() {
    return SimplePresenceTriggerTTCExtensions.Destruct(obj);
  }

}
public static class SimplePresenceTriggerTTCAsITerrainTileComponentCaster {
  public static SimplePresenceTriggerTTCAsITerrainTileComponent AsITerrainTileComponent(this SimplePresenceTriggerTTC obj) {
    return new SimplePresenceTriggerTTCAsITerrainTileComponent(obj);
  }
}

}
