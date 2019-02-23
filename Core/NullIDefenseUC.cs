using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class NullIDefenseUC : IDefenseUC {
  public static NullIDefenseUC Null = new NullIDefenseUC();

  public Root root { get { return null; } }
  public int id { get { return 0; } }
  public void Delete() {
    throw new Exception("Can't delete a null!");
  }
  public bool Exists() { return false; }
  public bool Is(IDefenseUC that) {
    throw new Exception("Called Is on a null!");
  }
  public bool NullableIs(IDefenseUC that) {
    return !that.Exists();
  }
  public IDefenseUC AsIDefenseUC() {
    return this;
  }
         public int AffectIncomingDamage(int incomingDamage){ throw new Exception("Called method on a null!"); }
  public bool Is(IUnitComponent that) {
    throw new Exception("Called Is on a null!");
  }
  public bool NullableIs(IUnitComponent that) {
    return !that.Exists();
  }
  public IUnitComponent AsIUnitComponent() {
    return NullIUnitComponent.Null;
  }
}
}
