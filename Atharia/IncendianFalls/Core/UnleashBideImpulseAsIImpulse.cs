using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class UnleashBideImpulseAsIImpulse : IImpulse {
  public readonly UnleashBideImpulse obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public UnleashBideImpulseAsIImpulse(UnleashBideImpulse obj) {
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
    return new UnleashBideImpulseAsIDestructible(obj);
  }
  public bool Is(IImpulse that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(IImpulse that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public IImpulse AsIImpulse() {
    return new UnleashBideImpulseAsIImpulse(obj);
  }

         public Void Destruct() {
    return UnleashBideImpulseExtensions.Destruct(obj);
  }
  public int GetWeight() {
    return UnleashBideImpulseExtensions.GetWeight(obj);
  }
  public Void Enact(Game game, Superstate superstate, Unit unit) {
    return UnleashBideImpulseExtensions.Enact(obj, game, superstate, unit);
  }

}
public static class UnleashBideImpulseAsIImpulseCaster {
  public static UnleashBideImpulseAsIImpulse AsIImpulse(this UnleashBideImpulse obj) {
    return new UnleashBideImpulseAsIImpulse(obj);
  }
}

}
