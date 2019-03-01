using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class MoveImpulseAsIImpulse : IImpulse {
  public readonly MoveImpulse obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public MoveImpulseAsIImpulse(MoveImpulse obj) {
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
    return new MoveImpulseAsIDestructible(obj);
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
    return new MoveImpulseAsIImpulse(obj);
  }

         public Void Destruct() {
    return MoveImpulseExtensions.Destruct(obj);
  }
  public int GetWeight() {
    return MoveImpulseExtensions.GetWeight(obj);
  }
  public bool Enact(Game game, LiveUnitByLocationMap liveUnitByLocationMap, Unit unit) {
    return MoveImpulseExtensions.Enact(obj, game, liveUnitByLocationMap, unit);
  }

}
public static class MoveImpulseAsIImpulseCaster {
  public static MoveImpulseAsIImpulse AsIImpulse(this MoveImpulse obj) {
    return new MoveImpulseAsIImpulse(obj);
  }
}

}
