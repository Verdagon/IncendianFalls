using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class PursueImpulseAsIImpulse : IImpulse {
  public readonly PursueImpulse obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public PursueImpulseAsIImpulse(PursueImpulse obj) {
    this.obj = obj;
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
    return new PursueImpulseAsIImpulse(obj);
  }

         public int GetWeight() {
    return IncendianFalls.PursueImpulseExtensions.GetWeightImpl(obj);
  }
  public Void Enact(Unit unit, Game game) {
    return IncendianFalls.PursueImpulseExtensions.EnactImpl(obj, unit, game);
  }

}
public static class PursueImpulseAsIImpulseCaster {
  public static PursueImpulseAsIImpulse AsIImpulse(this PursueImpulse obj) {
    return new PursueImpulseAsIImpulse(obj);
  }
}

}
