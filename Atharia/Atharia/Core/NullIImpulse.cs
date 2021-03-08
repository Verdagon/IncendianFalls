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
  public void FindReachableObjects(SortedSet<int> foundIds) { }
  public bool NullableIs(IImpulse that) {
    return !that.Exists();
  }
  public IImpulse AsIImpulse() {
    return this;
  }
         public bool Is(IDestructible that) {
    throw new Exception("Called Is on a null!");
  }
  public bool NullableIs(IDestructible that) {
    return !that.Exists();
  }
  public IDestructible AsIDestructible() {
    return NullIDestructible.Null;
  }

  public int GetWeight() {
    throw new Exception("Called GetWeight on a null!");
  }
             
  public Void Enact(Game game, Superstate superstate, Unit unit) {
    throw new Exception("Called Enact on a null!");
  }
             
  public Void Destruct() {
    throw new Exception("Called Destruct on a null!");
  }
             }
}
