using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class EvolvifyAICapabilityUCAsIAICapabilityUC : IAICapabilityUC {
  public readonly EvolvifyAICapabilityUC obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public EvolvifyAICapabilityUCAsIAICapabilityUC(EvolvifyAICapabilityUC obj) {
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
    return new EvolvifyAICapabilityUCAsIDestructible(obj);
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
    return new EvolvifyAICapabilityUCAsIUnitComponent(obj);
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
    return new EvolvifyAICapabilityUCAsIAICapabilityUC(obj);
  }

         public Void Destruct() {
    return EvolvifyAICapabilityUCExtensions.Destruct(obj);
  }
  public IImpulse ProduceImpulse(Game game, Superstate superstate, Unit unit) {
    return EvolvifyAICapabilityUCExtensions.ProduceImpulse(obj, game, superstate, unit);
  }

}
public static class EvolvifyAICapabilityUCAsIAICapabilityUCCaster {
  public static EvolvifyAICapabilityUCAsIAICapabilityUC AsIAICapabilityUC(this EvolvifyAICapabilityUC obj) {
    return new EvolvifyAICapabilityUCAsIAICapabilityUC(obj);
  }
}

}
