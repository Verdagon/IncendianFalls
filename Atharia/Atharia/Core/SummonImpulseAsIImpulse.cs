using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class SummonImpulseAsIImpulse : IImpulse {
  public readonly SummonImpulse obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public SummonImpulseAsIImpulse(SummonImpulse obj) {
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
    return new SummonImpulseAsIDestructible(obj);
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
    return new SummonImpulseAsIImpulse(obj);
  }

         public Void Destruct() {
    return SummonImpulseExtensions.Destruct(obj);
  }
  public int GetWeight() {
    return SummonImpulseExtensions.GetWeight(obj);
  }
  public Void Enact(Game game, Superstate superstate, Unit unit) {
    return SummonImpulseExtensions.Enact(obj, game, superstate, unit);
  }

}
public static class SummonImpulseAsIImpulseCaster {
  public static SummonImpulseAsIImpulse AsIImpulse(this SummonImpulse obj) {
    return new SummonImpulseAsIImpulse(obj);
  }
}

}
