using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class StartBidingImpulse {
  public readonly Root root;
  public readonly int id;
  public StartBidingImpulse(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public StartBidingImpulseIncarnation incarnation { get { return root.GetStartBidingImpulseIncarnation(id); } }
  public void AddObserver(IStartBidingImpulseEffectObserver observer) {
    root.AddStartBidingImpulseObserver(id, observer);
  }
  public void RemoveObserver(IStartBidingImpulseEffectObserver observer) {
    root.RemoveStartBidingImpulseObserver(id, observer);
  }
  public void Delete() {
    root.EffectStartBidingImpulseDelete(id);
  }
  public static StartBidingImpulse Null = new StartBidingImpulse(null, 0);
  public bool Exists() { return root != null && root.StartBidingImpulseExists(id); }
  public bool NullableIs(StartBidingImpulse that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
    return this.Is(that);
  }
  public bool Is(StartBidingImpulse that) {
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
