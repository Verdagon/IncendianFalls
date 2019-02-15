using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class DefendingDetailAsIDetail : IDetail {
  public readonly DefendingDetail obj;
public int id => obj.id;
public Root root => obj.root;
public void Delete() { obj.Delete(); }
public bool NullableIs(IDetail that) {
  if (!this.Exists() && !that.Exists()) {
    return true;
  }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
  return this.Is(that);
}
public bool Is(IDetail that) {
  if (!this.Exists()) {
    throw new Exception("Called Is on a null!");
  }
  if (!that.Exists()) {
    throw new Exception("Called Is on a null!");
  }
  return root == that.root && obj.id == that.id;
}
public bool Exists() { return obj.Exists(); }
public DefendingDetailAsIDetail(DefendingDetail obj) {
  this.obj = obj;
}
       public int AffectIncomingDamage(int incomingDamage) {
    return IncendianFalls.DefendingDetailExtensions.AffectIncomingDamageImpl(obj, incomingDamage);
  }
  public Void PreAct(Unit unit) {
    return IncendianFalls.DefendingDetailExtensions.PreActImpl(obj, unit);
  }
  public Void PostAct(Unit unit) {
    return IncendianFalls.DefendingDetailExtensions.PostActImpl(obj, unit);
  }

}
public static class DefendingDetailAsIDetailCaster {
  public static DefendingDetailAsIDetail AsIDetail(this DefendingDetail obj) {
    return new DefendingDetailAsIDetail(obj);
  }
}

}
