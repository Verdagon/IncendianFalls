using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class AttackImpulseAsIImpulse : IImpulse {
  public readonly AttackImpulse obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public AttackImpulseAsIImpulse(AttackImpulse obj) {
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
    return new AttackImpulseAsIDestructible(obj);
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
    return new AttackImpulseAsIImpulse(obj);
  }

         public Void Destruct() {
    return AttackImpulseExtensions.Destruct(obj);
  }
  public int GetWeight() {
    return AttackImpulseExtensions.GetWeight(obj);
  }
  public bool Enact(Game game, Superstate superstate, Unit unit) {
    return AttackImpulseExtensions.Enact(obj, game, superstate, unit);
  }

}
public static class AttackImpulseAsIImpulseCaster {
  public static AttackImpulseAsIImpulse AsIImpulse(this AttackImpulse obj) {
    return new AttackImpulseAsIImpulse(obj);
  }
}

}
