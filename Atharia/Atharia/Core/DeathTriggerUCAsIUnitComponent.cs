using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class DeathTriggerUCAsIUnitComponent : IUnitComponent {
  public readonly DeathTriggerUC obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public DeathTriggerUCAsIUnitComponent(DeathTriggerUC obj) {
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
    return new DeathTriggerUCAsIDestructible(obj);
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
    return new DeathTriggerUCAsIUnitComponent(obj);
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
    return new DeathTriggerUCAsIDeathPreReactor(obj);
  }

         public Void Destruct() {
    return DeathTriggerUCExtensions.Destruct(obj);
  }

}
public static class DeathTriggerUCAsIUnitComponentCaster {
  public static DeathTriggerUCAsIUnitComponent AsIUnitComponent(this DeathTriggerUC obj) {
    return new DeathTriggerUCAsIUnitComponent(obj);
  }
}

}
