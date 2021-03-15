using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class BequeathUCAsIDeathPreReactor : IDeathPreReactor {
  public readonly BequeathUC obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public BequeathUCAsIDeathPreReactor(BequeathUC obj) {
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
    return new BequeathUCAsIDestructible(obj);
  }
  public bool Is(IUnitComponent that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(IUnitComponent that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public IUnitComponent AsIUnitComponent() {
    return new BequeathUCAsIUnitComponent(obj);
  }
  public bool Is(IDeathPreReactor that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(IDeathPreReactor that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public IDeathPreReactor AsIDeathPreReactor() {
    return new BequeathUCAsIDeathPreReactor(obj);
  }

         public Void Destruct() {
    return BequeathUCExtensions.Destruct(obj);
  }
  public Void BeforeDeath(IncendianFalls.SSContext context, Game game, Superstate superstate, Unit unit) {
    return BequeathUCExtensions.BeforeDeath(obj, context, game, superstate, unit);
  }

}
public static class BequeathUCAsIDeathPreReactorCaster {
  public static BequeathUCAsIDeathPreReactor AsIDeathPreReactor(this BequeathUC obj) {
    return new BequeathUCAsIDeathPreReactor(obj);
  }
}

}
