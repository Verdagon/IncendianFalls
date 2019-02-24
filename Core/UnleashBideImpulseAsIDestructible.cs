using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class UnleashBideImpulseAsIDestructible : IDestructible {
  public readonly UnleashBideImpulse obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public UnleashBideImpulseAsIDestructible(UnleashBideImpulse obj) {
    this.obj = obj;
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

}
public static class UnleashBideImpulseAsIDestructibleCaster {
  public static UnleashBideImpulseAsIDestructible AsIDestructible(this UnleashBideImpulse obj) {
    return new UnleashBideImpulseAsIDestructible(obj);
  }
}

}
