using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class StartBidingImpulseAsIImpulse : IImpulse {
  public readonly StartBidingImpulse obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public StartBidingImpulseAsIImpulse(StartBidingImpulse obj) {
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
    return new StartBidingImpulseAsIDestructible(obj);
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
    return new StartBidingImpulseAsIImpulse(obj);
  }

         public Void Destruct() {
    return StartBidingImpulseExtensions.Destruct(obj);
  }
  public int GetWeight() {
    return StartBidingImpulseExtensions.GetWeight(obj);
  }
  public Void Enact(Unit unit, Game game) {
    return StartBidingImpulseExtensions.Enact(obj, unit, game);
  }

}
public static class StartBidingImpulseAsIImpulseCaster {
  public static StartBidingImpulseAsIImpulse AsIImpulse(this StartBidingImpulse obj) {
    return new StartBidingImpulseAsIImpulse(obj);
  }
}

}
