using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class AttackDirective {
  public readonly Root root;
  public readonly int id;
  public AttackDirective(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public AttackDirectiveIncarnation incarnation { get { return root.GetAttackDirectiveIncarnation(id); } }
  public void AddObserver(IAttackDirectiveEffectObserver observer) {
    root.AddAttackDirectiveObserver(id, observer);
  }
  public void RemoveObserver(IAttackDirectiveEffectObserver observer) {
    root.RemoveAttackDirectiveObserver(id, observer);
  }
  public void Delete() {
    root.EffectAttackDirectiveDelete(id);
  }
  public static AttackDirective Null = new AttackDirective(null, 0);
  public bool Exists() { return root != null && root.AttackDirectiveExists(id); }
  public bool NullableIs(AttackDirective that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
    return this.Is(that);
  }
  public bool Is(AttackDirective that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public Unit targetUnit {
    get { return new Unit(root, incarnation.targetUnit); }
  }
  public LocationMutList pathToLastSeenLocation {
    get { return new LocationMutList(root, incarnation.pathToLastSeenLocation); }
  }
}
}
