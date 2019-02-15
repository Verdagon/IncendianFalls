using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class Rand {
  public readonly Root root;
  public readonly int id;
  public Rand(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public RandIncarnation incarnation { get { return root.GetRandIncarnation(id); } }
  public void AddObserver(IRandEffectObserver observer) {
    root.AddRandObserver(id, observer);
  }
  public void RemoveObserver(IRandEffectObserver observer) {
    root.RemoveRandObserver(id, observer);
  }
  public void Delete() {
    root.EffectRandDelete(id);
  }
  public static Rand Null = new Rand(null, 0);
  public bool Exists() { return root != null && root.RandExists(id); }
  public bool NullableIs(Rand that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
    return this.Is(that);
  }
  public bool Is(Rand that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public int rand {
    get { return incarnation.rand; }
    set { root.EffectRandSetRand(id, value); }
  }
}
}
