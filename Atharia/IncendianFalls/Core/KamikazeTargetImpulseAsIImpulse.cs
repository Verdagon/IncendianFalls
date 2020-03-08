using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class KamikazeTargetImpulseAsIImpulse : IImpulse {
  public readonly KamikazeTargetImpulse obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public KamikazeTargetImpulseAsIImpulse(KamikazeTargetImpulse obj) {
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
    return new KamikazeTargetImpulseAsIDestructible(obj);
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
    return new KamikazeTargetImpulseAsIImpulse(obj);
  }

         public Void Destruct() {
    return KamikazeTargetImpulseExtensions.Destruct(obj);
  }
  public int GetWeight() {
    return KamikazeTargetImpulseExtensions.GetWeight(obj);
  }
  public bool Enact(Game game, Superstate superstate, Unit unit) {
    return KamikazeTargetImpulseExtensions.Enact(obj, game, superstate, unit);
  }

}
public static class KamikazeTargetImpulseAsIImpulseCaster {
  public static KamikazeTargetImpulseAsIImpulse AsIImpulse(this KamikazeTargetImpulse obj) {
    return new KamikazeTargetImpulseAsIImpulse(obj);
  }
}

}
