using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class MireImpulse {
  public readonly Root root;
  public readonly int id;
  public MireImpulse(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public MireImpulseIncarnation incarnation { get { return root.GetMireImpulseIncarnation(id); } }
  public void AddObserver(IMireImpulseEffectObserver observer) {
    root.AddMireImpulseObserver(id, observer);
  }
  public void RemoveObserver(IMireImpulseEffectObserver observer) {
    root.RemoveMireImpulseObserver(id, observer);
  }
  public void Delete() {
    root.EffectMireImpulseDelete(id);
  }
  public static MireImpulse Null = new MireImpulse(null, 0);
  public bool Exists() { return root != null && root.MireImpulseExists(id); }
  public bool NullableIs(MireImpulse that) {
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
      violations.Add("Null constraint violated! MireImpulse#" + id + ".targetUnit");
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
  public bool Is(MireImpulse that) {
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
