using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class IPreActingUnitComponentAsIUnitComponent : IUnitComponent {
  public readonly IPreActingUnitComponent obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool NullableIs(IUnitComponent that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public bool Is(IUnitComponent that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool Exists() { return obj.Exists(); }
  public IPreActingUnitComponentAsIUnitComponent(IPreActingUnitComponent obj) {
    this.obj = obj;
  }
       
}
public static class IPreActingUnitComponentAsIUnitComponentCaster {
  public static IPreActingUnitComponentAsIUnitComponent AsIUnitComponent(this IPreActingUnitComponent obj) {
    return new IPreActingUnitComponentAsIUnitComponent(obj);
  }
}

}
