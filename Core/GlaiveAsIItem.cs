using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class GlaiveAsIItem : IItem {
  public readonly Glaive obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public GlaiveAsIItem(Glaive obj) {
    this.obj = obj;
  }
  public bool Is(IItem that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(IItem that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public IItem AsIItem() {
    return new GlaiveAsIItem(obj);
  }

         public int AffectIncomingDamage(int incomingDamage) {
    return IncendianFalls.GlaiveExtensions.AffectIncomingDamageImpl(obj, incomingDamage);
  }
  public int AffectOutgoingDamage(int outgoingDamage) {
    return IncendianFalls.GlaiveExtensions.AffectOutgoingDamageImpl(obj, outgoingDamage);
  }

}
public static class GlaiveAsIItemCaster {
  public static GlaiveAsIItem AsIItem(this Glaive obj) {
    return new GlaiveAsIItem(obj);
  }
}

}
