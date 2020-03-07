using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class DefyImpulseAsIImpulse : IImpulse {
  public readonly DefyImpulse obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public DefyImpulseAsIImpulse(DefyImpulse obj) {
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
    return new DefyImpulseAsIDestructible(obj);
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
    return new DefyImpulseAsIImpulse(obj);
  }

         public Void Destruct() {
    return DefyImpulseExtensions.Destruct(obj);
  }
  public int GetWeight() {
    return DefyImpulseExtensions.GetWeight(obj);
  }
  public bool Enact(Game game, Superstate superstate, Unit unit) {
    return DefyImpulseExtensions.Enact(obj, game, superstate, unit);
  }

}
public static class DefyImpulseAsIImpulseCaster {
  public static DefyImpulseAsIImpulse AsIImpulse(this DefyImpulse obj) {
    return new DefyImpulseAsIImpulse(obj);
  }
}

}
