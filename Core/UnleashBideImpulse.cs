using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class UnleashBideImpulse {
  public readonly Root root;
  public readonly int id;
  public UnleashBideImpulse(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public UnleashBideImpulseIncarnation incarnation { get { return root.GetUnleashBideImpulseIncarnation(id); } }
  public void AddObserver(IUnleashBideImpulseEffectObserver observer) {
    root.AddUnleashBideImpulseObserver(id, observer);
  }
  public void RemoveObserver(IUnleashBideImpulseEffectObserver observer) {
    root.RemoveUnleashBideImpulseObserver(id, observer);
  }
  public void Delete() {
    root.EffectUnleashBideImpulseDelete(id);
  }
  public static UnleashBideImpulse Null = new UnleashBideImpulse(null, 0);
  public bool Exists() { return root != null && root.UnleashBideImpulseExists(id); }
  public bool NullableIs(UnleashBideImpulse that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
    return this.Is(that);
  }
  public bool Is(UnleashBideImpulse that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public int weight {
    get { return incarnation.weight; }
  }
}
}
