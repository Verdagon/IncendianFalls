using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class NoImpulseAsIImpulse : IImpulse {
  public readonly NoImpulse obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public NoImpulseAsIImpulse(NoImpulse obj) {
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
    return new NoImpulseAsIImpulse(obj);
  }

         public int GetWeight() {
    return IncendianFalls.NoImpulseExtensions.GetWeightImpl(obj);
  }
  public Void Enact(Unit unit, Game game) {
    return IncendianFalls.NoImpulseExtensions.EnactImpl(obj, unit, game);
  }

}
public static class NoImpulseAsIImpulseCaster {
  public static NoImpulseAsIImpulse AsIImpulse(this NoImpulse obj) {
    return new NoImpulseAsIImpulse(obj);
  }
}

}
