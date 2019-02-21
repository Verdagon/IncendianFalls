using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class NullIUnitComponent : IUnitComponent {
  public static NullIUnitComponent Null = new NullIUnitComponent();

  public Root root { get { return null; } }
  public int id { get { return 0; } }
  public void Delete() {
    throw new Exception("Can't delete a null!");
  }
  public bool Exists() { return false; }
  public bool Is(IUnitComponent that) {
    throw new Exception("Called Is on a null!");
  }
  public bool NullableIs(IUnitComponent that) {
    return !that.Exists();
  }
       }
}
