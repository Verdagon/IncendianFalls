using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class SummonAICapabilityUCAsIAICapabilityUC : IAICapabilityUC {
  public readonly SummonAICapabilityUC obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public SummonAICapabilityUCAsIAICapabilityUC(SummonAICapabilityUC obj) {
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
    return new SummonAICapabilityUCAsIDestructible(obj);
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
    return new SummonAICapabilityUCAsIUnitComponent(obj);
  }
  public bool Is(IAICapabilityUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(IAICapabilityUC that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public IAICapabilityUC AsIAICapabilityUC() {
    return new SummonAICapabilityUCAsIAICapabilityUC(obj);
  }

         public Void Destruct() {
    return SummonAICapabilityUCExtensions.Destruct(obj);
  }
  public IImpulse ProduceImpulse(Game game, Superstate superstate, Unit unit) {
    return SummonAICapabilityUCExtensions.ProduceImpulse(obj, game, superstate, unit);
  }

}
public static class SummonAICapabilityUCAsIAICapabilityUCCaster {
  public static SummonAICapabilityUCAsIAICapabilityUC AsIAICapabilityUC(this SummonAICapabilityUC obj) {
    return new SummonAICapabilityUCAsIAICapabilityUC(obj);
  }
}

}
