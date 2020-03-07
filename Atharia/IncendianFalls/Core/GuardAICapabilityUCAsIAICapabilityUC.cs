using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class GuardAICapabilityUCAsIAICapabilityUC : IAICapabilityUC {
  public readonly GuardAICapabilityUC obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public GuardAICapabilityUCAsIAICapabilityUC(GuardAICapabilityUC obj) {
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
    return new GuardAICapabilityUCAsIDestructible(obj);
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
    return new GuardAICapabilityUCAsIUnitComponent(obj);
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
    return new GuardAICapabilityUCAsIAICapabilityUC(obj);
  }

         public Void Destruct() {
    return GuardAICapabilityUCExtensions.Destruct(obj);
  }
  public IImpulse ProduceImpulse(Game game, Superstate superstate, Unit unit) {
    return GuardAICapabilityUCExtensions.ProduceImpulse(obj, game, superstate, unit);
  }

}
public static class GuardAICapabilityUCAsIAICapabilityUCCaster {
  public static GuardAICapabilityUCAsIAICapabilityUC AsIAICapabilityUC(this GuardAICapabilityUC obj) {
    return new GuardAICapabilityUCAsIAICapabilityUC(obj);
  }
}

}
