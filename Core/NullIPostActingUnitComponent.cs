using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class NullIPostActingUnitComponent : IPostActingUnitComponent {
  public static NullIPostActingUnitComponent Null = new NullIPostActingUnitComponent();

  public Root root { get { return null; } }
  public int id { get { return 0; } }
  public void Delete() {
    throw new Exception("Can't delete a null!");
  }
  public bool Exists() { return false; }
  public bool Is(IPostActingUnitComponent that) {
    throw new Exception("Called Is on a null!");
  }
  public bool NullableIs(IPostActingUnitComponent that) {
    return !that.Exists();
  }
         public Void PostAct(Unit unit){ throw new Exception("Called method on a null!"); }
}
}
