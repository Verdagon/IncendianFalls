using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class NullIPostActingUC : IPostActingUC {
  public static NullIPostActingUC Null = new NullIPostActingUC();

  public Root root { get { return null; } }
  public int id { get { return 0; } }
  public void Delete() {
    throw new Exception("Can't delete a null!");
  }
  public bool Exists() { return false; }
  public bool Is(IPostActingUC that) {
    throw new Exception("Called Is on a null!");
  }
  public bool NullableIs(IPostActingUC that) {
    return !that.Exists();
  }
  public IPostActingUC AsIPostActingUC() {
    return this;
  }
         public Void PostAct(Unit unit){ throw new Exception("Called method on a null!"); }
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
