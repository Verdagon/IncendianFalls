using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class ShieldingUnitComponentAsIPreActingUnitComponent : IPreActingUnitComponent {
  public readonly ShieldingUnitComponent obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool NullableIs(IPreActingUnitComponent that) {
  if (!this.Exists() && !that.Exists()) {
    return true;
  }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
  return this.Is(that);
}
public bool Is(IPreActingUnitComponent that) {
  if (!this.Exists()) {
    throw new Exception("Called Is on a null!");
  }
  if (!that.Exists()) {
    throw new Exception("Called Is on a null!");
  }
  return root == that.root && obj.id == that.id;
}
public bool Exists() { return obj.Exists(); }
public ShieldingUnitComponentAsIPreActingUnitComponent(ShieldingUnitComponent obj) {
  this.obj = obj;
}
         public Void PreAct(Unit unit) {
    return IncendianFalls.ShieldingUnitComponentExtensions.PreActImpl(obj, unit);
  }

}
public static class ShieldingUnitComponentAsIPreActingUnitComponentCaster {
  public static ShieldingUnitComponentAsIPreActingUnitComponent AsIPreActingUnitComponent(this ShieldingUnitComponent obj) {
    return new ShieldingUnitComponentAsIPreActingUnitComponent(obj);
  }
}

}
