using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class BideImpulse {
  public readonly Root root;
  public readonly int id;
  public BideImpulse(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public BideImpulseIncarnation incarnation { get { return root.GetBideImpulseIncarnation(id); } }
  public void AddObserver(IBideImpulseEffectObserver observer) {
    root.AddBideImpulseObserver(id, observer);
  }
  public void RemoveObserver(IBideImpulseEffectObserver observer) {
    root.RemoveBideImpulseObserver(id, observer);
  }
  public void Delete() {
    root.EffectBideImpulseDelete(id);
  }
  public static BideImpulse Null = new BideImpulse(null, 0);
  public bool Exists() { return root != null && root.BideImpulseExists(id); }
  public bool NullableIs(BideImpulse that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
    return this.Is(that);
  }
  public bool Is(BideImpulse that) {
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
