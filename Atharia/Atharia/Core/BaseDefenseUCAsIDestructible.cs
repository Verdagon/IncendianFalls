using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class BaseDefenseUCAsIDestructible : IDestructible {
  public readonly BaseDefenseUC obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public BaseDefenseUCAsIDestructible(BaseDefenseUC obj) {
    this.obj = obj;
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    obj.FindReachableObjects(foundIds);
  }
  public bool Is(IDefenseFactorUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(IDefenseFactorUC that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public IDefenseFactorUC AsIDefenseFactorUC() {
    return new BaseDefenseUCAsIDefenseFactorUC(obj);
  }
  public bool Is(ICloneableUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(ICloneableUC that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public ICloneableUC AsICloneableUC() {
    return new BaseDefenseUCAsICloneableUC(obj);
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
    return new BaseDefenseUCAsIDestructible(obj);
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
    return new BaseDefenseUCAsIUnitComponent(obj);
  }

         public Void Destruct() {
    return BaseDefenseUCExtensions.Destruct(obj);
  }
  public int GetIncomingDamageAddConstant() {
    return BaseDefenseUCExtensions.GetIncomingDamageAddConstant(obj);
  }
  public int GetIncomingDamageMultiplierPercent() {
    return BaseDefenseUCExtensions.GetIncomingDamageMultiplierPercent(obj);
  }

}
public static class BaseDefenseUCAsIDestructibleCaster {
  public static BaseDefenseUCAsIDestructible AsIDestructible(this BaseDefenseUC obj) {
    return new BaseDefenseUCAsIDestructible(obj);
  }
}

}
