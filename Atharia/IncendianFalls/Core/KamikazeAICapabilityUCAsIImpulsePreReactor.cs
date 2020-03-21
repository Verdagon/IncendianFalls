using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class KamikazeAICapabilityUCAsIImpulsePreReactor : IImpulsePreReactor {
  public readonly KamikazeAICapabilityUC obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public KamikazeAICapabilityUCAsIImpulsePreReactor(KamikazeAICapabilityUC obj) {
    this.obj = obj;
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    obj.FindReachableObjects(foundIds);
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
    return new KamikazeAICapabilityUCAsIDeathPreReactor(obj);
  }
  public bool Is(IImpulsePreReactor that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(IImpulsePreReactor that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public IImpulsePreReactor AsIImpulsePreReactor() {
    return new KamikazeAICapabilityUCAsIImpulsePreReactor(obj);
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
    return new KamikazeAICapabilityUCAsIDestructible(obj);
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
    return new KamikazeAICapabilityUCAsIUnitComponent(obj);
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
    return new KamikazeAICapabilityUCAsIAICapabilityUC(obj);
  }

         public Void Destruct() {
    return KamikazeAICapabilityUCExtensions.Destruct(obj);
  }
  public Void BeforeImpulse(Game game, Superstate superstate, Unit unit, IAICapabilityUC originatingCapability, IImpulse impulse) {
    return KamikazeAICapabilityUCExtensions.BeforeImpulse(obj, game, superstate, unit, originatingCapability, impulse);
  }

}
public static class KamikazeAICapabilityUCAsIImpulsePreReactorCaster {
  public static KamikazeAICapabilityUCAsIImpulsePreReactor AsIImpulsePreReactor(this KamikazeAICapabilityUC obj) {
    return new KamikazeAICapabilityUCAsIImpulsePreReactor(obj);
  }
}

}
