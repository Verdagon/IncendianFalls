using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class TemporaryCloneImpulseAsIDestructible : IDestructible {
  public readonly TemporaryCloneImpulse obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public TemporaryCloneImpulseAsIDestructible(TemporaryCloneImpulse obj) {
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
    return new TemporaryCloneImpulseAsIDestructible(obj);
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
    return new TemporaryCloneImpulseAsIImpulse(obj);
  }

         public Void Destruct() {
    return TemporaryCloneImpulseExtensions.Destruct(obj);
  }

}
public static class TemporaryCloneImpulseAsIDestructibleCaster {
  public static TemporaryCloneImpulseAsIDestructible AsIDestructible(this TemporaryCloneImpulse obj) {
    return new TemporaryCloneImpulseAsIDestructible(obj);
  }
}

}