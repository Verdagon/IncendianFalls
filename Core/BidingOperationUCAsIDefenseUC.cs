using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class BidingOperationUCAsIDefenseUC : IDefenseUC {
  public readonly BidingOperationUC obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public BidingOperationUCAsIDefenseUC(BidingOperationUC obj) {
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
    return new BidingOperationUCAsIDestructible(obj);
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

         public Void Destruct() {
    return BidingOperationUCExtensions.Destruct(obj);
  }
  public int AffectIncomingDamage(int incomingDamage) {
    return BidingOperationUCExtensions.AffectIncomingDamage(obj, incomingDamage);
  }

}
public static class BidingOperationUCAsIDefenseUCCaster {
  public static BidingOperationUCAsIDefenseUC AsIDefenseUC(this BidingOperationUC obj) {
    return new BidingOperationUCAsIDefenseUC(obj);
  }
}

}
