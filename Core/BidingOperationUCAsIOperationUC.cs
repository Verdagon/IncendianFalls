using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class BidingOperationUCAsIOperationUC : IOperationUC {
  public readonly BidingOperationUC obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public BidingOperationUCAsIOperationUC(BidingOperationUC obj) {
    this.obj = obj;
  }
  public bool Is(IOperationUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(IOperationUC that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public IOperationUC AsIOperationUC() {
    return new BidingOperationUCAsIOperationUC(obj);
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
    return new BidingOperationUCAsIUnitComponent(obj);
  }
  public bool Is(IDefenseUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(IDefenseUC that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public IDefenseUC AsIDefenseUC() {
    return new BidingOperationUCAsIDefenseUC(obj);
  }

         public Void OnImpulse(Unit unit, Game game, IImpulse impulse) {
    return IncendianFalls.BidingOperationUCExtensions.OnImpulseImpl(obj, unit, game, impulse);
  }

}
public static class BidingOperationUCAsIOperationUCCaster {
  public static BidingOperationUCAsIOperationUC AsIOperationUC(this BidingOperationUC obj) {
    return new BidingOperationUCAsIOperationUC(obj);
  }
}

}
