using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class NullIImpulse : IImpulse {
  public static NullIImpulse Null = new NullIImpulse();

  public Root root { get { return null; } }
  public int id { get { return 0; } }
  public void Delete() {
    throw new Exception("Can't delete a null!");
  }
  public bool Exists() { return false; }
  public bool Is(IImpulse that) {
    throw new Exception("Called Is on a null!");
  }
  public bool NullableIs(IImpulse that) {
    return !that.Exists();
  }
  public IImpulse AsIImpulse() {
    return this;
  }
         public int GetWeight(){ throw new Exception("Called method on a null!"); }
  public Void Enact(Unit unit, Game game){ throw new Exception("Called method on a null!"); }
}
}
