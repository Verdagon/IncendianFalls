using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class DoomedUCAsIPreActingUC : IPreActingUC {
  public readonly DoomedUC obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public DoomedUCAsIPreActingUC(DoomedUC obj) {
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
    return new DoomedUCAsIDestructible(obj);
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
    return new DoomedUCAsIUnitComponent(obj);
  }
  public bool Is(IPreActingUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(IPreActingUC that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public IPreActingUC AsIPreActingUC() {
    return new DoomedUCAsIPreActingUC(obj);
  }

         public Void Destruct() {
    return DoomedUCExtensions.Destruct(obj);
  }
  public bool PreAct(Game game, Superstate superstate, Unit unit) {
    return DoomedUCExtensions.PreAct(obj, game, superstate, unit);
  }

}
public static class DoomedUCAsIPreActingUCCaster {
  public static DoomedUCAsIPreActingUC AsIPreActingUC(this DoomedUC obj) {
    return new DoomedUCAsIPreActingUC(obj);
  }
}

}
