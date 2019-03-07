using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class FireImpulse {
  public readonly Root root;
  public readonly int id;
  public FireImpulse(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public FireImpulseIncarnation incarnation { get { return root.GetFireImpulseIncarnation(id); } }
  public void AddObserver(IFireImpulseEffectObserver observer) {
    root.AddFireImpulseObserver(id, observer);
  }
  public void RemoveObserver(IFireImpulseEffectObserver observer) {
    root.RemoveFireImpulseObserver(id, observer);
  }
  public void Delete() {
    root.EffectFireImpulseDelete(id);
  }
  public static FireImpulse Null = new FireImpulse(null, 0);
  public bool Exists() { return root != null && root.FireImpulseExists(id); }
  public bool NullableIs(FireImpulse that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public void CheckForNullViolations(List<string> violations) {

    if (!root.UnitExists(targetUnit.id)) {
      violations.Add("Null constraint violated! FireImpulse#" + id + ".targetUnit");
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    if (root.UnitExists(targetUnit.id)) {
      targetUnit.FindReachableObjects(foundIds);
    }
  }
  public bool Is(FireImpulse that) {
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
  public Unit targetUnit {

    get {
      if (root == null) {
        throw new Exception("Tried to get member targetUnit of null!");
      }
      return new Unit(root, incarnation.targetUnit);
    }
                       }
}
}
