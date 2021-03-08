using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class LightningChargingUCAsIImpulsePreReactor : IImpulsePreReactor {
  public readonly LightningChargingUC obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public LightningChargingUCAsIImpulsePreReactor(LightningChargingUC obj) {
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
    return new LightningChargingUCAsIDestructible(obj);
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
    return new LightningChargingUCAsIUnitComponent(obj);
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
    return new LightningChargingUCAsIImpulsePreReactor(obj);
  }

         public Void Destruct() {
    return LightningChargingUCExtensions.Destruct(obj);
  }
  public Void BeforeImpulse(Game game, Superstate superstate, Unit unit, IAICapabilityUC originatingCapability, IImpulse impulse) {
    return LightningChargingUCExtensions.BeforeImpulse(obj, game, superstate, unit, originatingCapability, impulse);
  }

}
public static class LightningChargingUCAsIImpulsePreReactorCaster {
  public static LightningChargingUCAsIImpulsePreReactor AsIImpulsePreReactor(this LightningChargingUC obj) {
    return new LightningChargingUCAsIImpulsePreReactor(obj);
  }
}

}
