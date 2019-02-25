using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class UnitAsIDestructible : IDestructible {
  public readonly Unit obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public UnitAsIDestructible(Unit obj) {
    this.obj = obj;
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    obj.FindReachableObjects(foundIds);
  }
  public bool Is(IDestructible that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(IDestructible that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public IDestructible AsIDestructible() {
    return new UnitAsIDestructible(obj);
  }

         public Void Destruct() {
    return UnitExtensions.Destruct(obj);
  }

}
public static class UnitAsIDestructibleCaster {
  public static UnitAsIDestructible AsIDestructible(this Unit obj) {
    return new UnitAsIDestructible(obj);
  }
}

}
