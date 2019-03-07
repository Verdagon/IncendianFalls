using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class FireImpulseAsIImpulse : IImpulse {
  public readonly FireImpulse obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public FireImpulseAsIImpulse(FireImpulse obj) {
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
    return new FireImpulseAsIDestructible(obj);
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
    return new FireImpulseAsIImpulse(obj);
  }

         public Void Destruct() {
    return FireImpulseExtensions.Destruct(obj);
  }
  public int GetWeight() {
    return FireImpulseExtensions.GetWeight(obj);
  }
  public bool Enact(Game game, Superstate superstate, Unit unit) {
    return FireImpulseExtensions.Enact(obj, game, superstate, unit);
  }

}
public static class FireImpulseAsIImpulseCaster {
  public static FireImpulseAsIImpulse AsIImpulse(this FireImpulse obj) {
    return new FireImpulseAsIImpulse(obj);
  }
}

}
