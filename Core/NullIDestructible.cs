using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class NullIDestructible : IDestructible {
  public static NullIDestructible Null = new NullIDestructible();

  public Root root { get { return null; } }
  public int id { get { return 0; } }
  public void Delete() {
    throw new Exception("Can't delete a null!");
  }
  public bool Exists() { return false; }
  public bool Is(IDestructible that) {
    throw new Exception("Called Is on a null!");
  }
  public bool NullableIs(IDestructible that) {
    return !that.Exists();
  }
  public IDestructible AsIDestructible() {
    return this;
  }
       
  public Void Destruct() {
    throw new Exception("Called Destruct on a null!");
  }
             }
}
