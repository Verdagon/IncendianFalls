using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class NullIFeature : IFeature {
  public static NullIFeature Null = new NullIFeature();

  public Root root { get { return null; } }
  public int id { get { return 0; } }
  public void Delete() {
    throw new Exception("Can't delete a null!");
  }
  public bool Exists() { return false; }
  public bool Is(IFeature that) {
    throw new Exception("Called Is on a null!");
  }
  public bool NullableIs(IFeature that) {
    return !that.Exists();
  }
       }
}
