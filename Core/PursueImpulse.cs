using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class PursueImpulse {
  public readonly Root root;
  public readonly int id;
  public PursueImpulse(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public PursueImpulseIncarnation incarnation { get { return root.GetPursueImpulseIncarnation(id); } }
  public void AddObserver(IPursueImpulseEffectObserver observer) {
    root.AddPursueImpulseObserver(id, observer);
  }
  public void RemoveObserver(IPursueImpulseEffectObserver observer) {
    root.RemovePursueImpulseObserver(id, observer);
  }
  public void Delete() {
    root.EffectPursueImpulseDelete(id);
  }
  public static PursueImpulse Null = new PursueImpulse(null, 0);
  public bool Exists() { return root != null && root.PursueImpulseExists(id); }
  public bool NullableIs(PursueImpulse that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
    return this.Is(that);
  }
  public bool Is(PursueImpulse that) {
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
  public Location stepLocation {
    get { return incarnation.stepLocation; }
  }
}
}
