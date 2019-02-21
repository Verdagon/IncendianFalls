using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class NullIPreActingUnitComponent : IPreActingUnitComponent {
  public static NullIPreActingUnitComponent Null = new NullIPreActingUnitComponent();

  public Root root { get { return null; } }
  public int id { get { return 0; } }
  public void Delete() {
    throw new Exception("Can't delete a null!");
  }
  public bool Exists() { return false; }
  public bool Is(IPreActingUnitComponent that) {
    throw new Exception("Called Is on a null!");
  }
  public bool NullableIs(IPreActingUnitComponent that) {
    return !that.Exists();
  }
         public Void PreAct(Unit unit){ throw new Exception("Called method on a null!"); }
}
}
