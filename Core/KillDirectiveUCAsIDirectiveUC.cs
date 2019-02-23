using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class KillDirectiveUCAsIDirectiveUC : IDirectiveUC {
  public readonly KillDirectiveUC obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public KillDirectiveUCAsIDirectiveUC(KillDirectiveUC obj) {
    this.obj = obj;
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
    return new KillDirectiveUCAsIUnitComponent(obj);
  }
  public bool Is(IDirectiveUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(IDirectiveUC that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public IDirectiveUC AsIDirectiveUC() {
    return new KillDirectiveUCAsIDirectiveUC(obj);
  }

       
}
public static class KillDirectiveUCAsIDirectiveUCCaster {
  public static KillDirectiveUCAsIDirectiveUC AsIDirectiveUC(this KillDirectiveUC obj) {
    return new KillDirectiveUCAsIDirectiveUC(obj);
  }
}

}
