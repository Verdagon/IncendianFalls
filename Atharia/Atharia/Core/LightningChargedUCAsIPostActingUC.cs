using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class LightningChargedUCAsIPostActingUC : IPostActingUC {
  public readonly LightningChargedUC obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public LightningChargedUCAsIPostActingUC(LightningChargedUC obj) {
    this.obj = obj;
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    obj.FindReachableObjects(foundIds);
  }
  public bool Is(IPostActingUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(IPostActingUC that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public IPostActingUC AsIPostActingUC() {
    return new LightningChargedUCAsIPostActingUC(obj);
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
    return new LightningChargedUCAsIDestructible(obj);
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
    return new LightningChargedUCAsIUnitComponent(obj);
  }
  public bool Is(IMovementTimeFactorUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(IMovementTimeFactorUC that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public IMovementTimeFactorUC AsIMovementTimeFactorUC() {
    return new LightningChargedUCAsIMovementTimeFactorUC(obj);
  }

         public Void Destruct() {
    return LightningChargedUCExtensions.Destruct(obj);
  }
  public Void PostAct(Game game, Superstate superstate, Unit unit) {
    return LightningChargedUCExtensions.PostAct(obj, game, superstate, unit);
  }

}
public static class LightningChargedUCAsIPostActingUCCaster {
  public static LightningChargedUCAsIPostActingUC AsIPostActingUC(this LightningChargedUC obj) {
    return new LightningChargedUCAsIPostActingUC(obj);
  }
}

}
