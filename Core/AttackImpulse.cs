using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class AttackImpulse {
  public readonly Root root;
  public readonly int id;
  public AttackImpulse(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public AttackImpulseIncarnation incarnation { get { return root.GetAttackImpulseIncarnation(id); } }
  public void AddObserver(IAttackImpulseEffectObserver observer) {
    root.AddAttackImpulseObserver(id, observer);
  }
  public void RemoveObserver(IAttackImpulseEffectObserver observer) {
    root.RemoveAttackImpulseObserver(id, observer);
  }
  public void Delete() {
    root.EffectAttackImpulseDelete(id);
  }
  public static AttackImpulse Null = new AttackImpulse(null, 0);
  public bool Exists() { return root != null && root.AttackImpulseExists(id); }
  public bool NullableIs(AttackImpulse that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public void CheckForNullViolations(List<string> violations) {
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
  }
  public bool Is(AttackImpulse that) {
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
