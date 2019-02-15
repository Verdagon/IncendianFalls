using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class NullIDirective : IDirective {
  public static NullIDirective Null = new NullIDirective();

  public Root root { get { return null; } }
  public int id { get { return 0; } }
  public void Delete() {
    throw new Exception("Can't delete a null!");
  }
  public bool Exists() { return false; }
  public bool Is(IDirective that) {
    throw new Exception("Called Is on a null!");
  }
  public bool NullableIs(IDirective that) {
    return !that.Exists();
  }
       }
}
