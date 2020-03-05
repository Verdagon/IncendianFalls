using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class BaseCombatTimeUCAsICombatTimeFactorUC : ICombatTimeFactorUC {
  public readonly BaseCombatTimeUC obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public BaseCombatTimeUCAsICombatTimeFactorUC(BaseCombatTimeUC obj) {
    this.obj = obj;
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    obj.FindReachableObjects(foundIds);
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
    return new BaseCombatTimeUCAsICloneableUC(obj);
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
    return new BaseCombatTimeUCAsIDestructible(obj);
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
    return new BaseCombatTimeUCAsIUnitComponent(obj);
  }
  public bool Is(ICombatTimeFactorUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(ICombatTimeFactorUC that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public ICombatTimeFactorUC AsICombatTimeFactorUC() {
    return new BaseCombatTimeUCAsICombatTimeFactorUC(obj);
  }

         public Void Destruct() {
    return BaseCombatTimeUCExtensions.Destruct(obj);
  }
  public int GetCombatTimeAddConstant() {
    return BaseCombatTimeUCExtensions.GetCombatTimeAddConstant(obj);
  }
  public int GetCombatTimeMultiplierPercent() {
    return BaseCombatTimeUCExtensions.GetCombatTimeMultiplierPercent(obj);
  }

}
public static class BaseCombatTimeUCAsICombatTimeFactorUCCaster {
  public static BaseCombatTimeUCAsICombatTimeFactorUC AsICombatTimeFactorUC(this BaseCombatTimeUC obj) {
    return new BaseCombatTimeUCAsICombatTimeFactorUC(obj);
  }
}

}
