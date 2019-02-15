using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class NullIDetail : IDetail {
  public static NullIDetail Null = new NullIDetail();

  public Root root { get { return null; } }
  public int id { get { return 0; } }
  public void Delete() {
    throw new Exception("Can't delete a null!");
  }
  public bool Exists() { return false; }
  public bool Is(IDetail that) {
    throw new Exception("Called Is on a null!");
  }
  public bool NullableIs(IDetail that) {
    return !that.Exists();
  }
         public int AffectIncomingDamage(int incomingDamage){ throw new Exception("Called method on a null!"); }
  public Void PreAct(Unit unit){ throw new Exception("Called method on a null!"); }
  public Void PostAct(Unit unit){ throw new Exception("Called method on a null!"); }
}
}
