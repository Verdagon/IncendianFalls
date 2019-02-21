using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class ShieldingUnitComponentAsIDefenseUnitComponent : IDefenseUnitComponent {
  public readonly ShieldingUnitComponent obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool NullableIs(IDefenseUnitComponent that) {
  if (!this.Exists() && !that.Exists()) {
    return true;
  }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
  return this.Is(that);
}
public bool Is(IDefenseUnitComponent that) {
  if (!this.Exists()) {
    throw new Exception("Called Is on a null!");
  }
  if (!that.Exists()) {
    throw new Exception("Called Is on a null!");
  }
  return root == that.root && obj.id == that.id;
}
public bool Exists() { return obj.Exists(); }
public ShieldingUnitComponentAsIDefenseUnitComponent(ShieldingUnitComponent obj) {
  this.obj = obj;
}
         public int AffectIncomingDamage(int incomingDamage) {
    return IncendianFalls.ShieldingUnitComponentExtensions.AffectIncomingDamageImpl(obj, incomingDamage);
  }

}
public static class ShieldingUnitComponentAsIDefenseUnitComponentCaster {
  public static ShieldingUnitComponentAsIDefenseUnitComponent AsIDefenseUnitComponent(this ShieldingUnitComponent obj) {
    return new ShieldingUnitComponentAsIDefenseUnitComponent(obj);
  }
}

}
