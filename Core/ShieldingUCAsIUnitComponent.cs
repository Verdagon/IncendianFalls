using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class ShieldingUCAsIUnitComponent : IUnitComponent {
  public readonly ShieldingUC obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public ShieldingUCAsIUnitComponent(ShieldingUC obj) {
    this.obj = obj;
  }
  public bool Is(IPostActingUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(IPostActingUC that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public IPostActingUC AsIPostActingUC() {
    return new ShieldingUCAsIPostActingUC(obj);
  }
  public bool Is(IPreActingUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(IPreActingUC that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public IPreActingUC AsIPreActingUC() {
    return new ShieldingUCAsIPreActingUC(obj);
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
  public bool NullableIs(IUnitComponent that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public IUnitComponent AsIUnitComponent() {
    return new ShieldingUCAsIUnitComponent(obj);
  }
  public bool Is(IDefenseUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(IDefenseUC that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public IDefenseUC AsIDefenseUC() {
    return new ShieldingUCAsIDefenseUC(obj);
  }

       
}
public static class ShieldingUCAsIUnitComponentCaster {
  public static ShieldingUCAsIUnitComponent AsIUnitComponent(this ShieldingUC obj) {
    return new ShieldingUCAsIUnitComponent(obj);
  }
}

}
