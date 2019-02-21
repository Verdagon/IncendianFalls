using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class ShieldingUnitComponentAsIPostActingUnitComponent : IPostActingUnitComponent {
  public readonly ShieldingUnitComponent obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool NullableIs(IPostActingUnitComponent that) {
  if (!this.Exists() && !that.Exists()) {
    return true;
  }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
  return this.Is(that);
}
public bool Is(IPostActingUnitComponent that) {
  if (!this.Exists()) {
    throw new Exception("Called Is on a null!");
  }
  if (!that.Exists()) {
    throw new Exception("Called Is on a null!");
  }
  return root == that.root && obj.id == that.id;
}
public bool Exists() { return obj.Exists(); }
public ShieldingUnitComponentAsIPostActingUnitComponent(ShieldingUnitComponent obj) {
  this.obj = obj;
}
         public Void PostAct(Unit unit) {
    return IncendianFalls.ShieldingUnitComponentExtensions.PostActImpl(obj, unit);
  }

}
public static class ShieldingUnitComponentAsIPostActingUnitComponentCaster {
  public static ShieldingUnitComponentAsIPostActingUnitComponent AsIPostActingUnitComponent(this ShieldingUnitComponent obj) {
    return new ShieldingUnitComponentAsIPostActingUnitComponent(obj);
  }
}

}
