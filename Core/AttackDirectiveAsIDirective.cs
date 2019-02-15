using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class AttackDirectiveAsIDirective : IDirective {
  public readonly AttackDirective obj;
public int id => obj.id;
public Root root => obj.root;
public void Delete() { obj.Delete(); }
public bool NullableIs(IDirective that) {
  if (!this.Exists() && !that.Exists()) {
    return true;
  }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
  return this.Is(that);
}
public bool Is(IDirective that) {
  if (!this.Exists()) {
    throw new Exception("Called Is on a null!");
  }
  if (!that.Exists()) {
    throw new Exception("Called Is on a null!");
  }
  return root == that.root && obj.id == that.id;
}
public bool Exists() { return obj.Exists(); }
public AttackDirectiveAsIDirective(AttackDirective obj) {
  this.obj = obj;
}
     
}
public static class AttackDirectiveAsIDirectiveCaster {
  public static AttackDirectiveAsIDirective AsIDirective(this AttackDirective obj) {
    return new AttackDirectiveAsIDirective(obj);
  }
}

}
