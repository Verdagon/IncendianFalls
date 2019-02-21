using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class NullIDefenseUnitComponent : IDefenseUnitComponent {
  public static NullIDefenseUnitComponent Null = new NullIDefenseUnitComponent();

  public Root root { get { return null; } }
  public int id { get { return 0; } }
  public void Delete() {
    throw new Exception("Can't delete a null!");
  }
  public bool Exists() { return false; }
  public bool Is(IDefenseUnitComponent that) {
    throw new Exception("Called Is on a null!");
  }
  public bool NullableIs(IDefenseUnitComponent that) {
    return !that.Exists();
  }
         public int AffectIncomingDamage(int incomingDamage){ throw new Exception("Called method on a null!"); }
}
}
