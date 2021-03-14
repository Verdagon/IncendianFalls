using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class ExplosionRod {
  public readonly Root root;
  public readonly int id;
  public ExplosionRod(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ExplosionRodIncarnation incarnation { get { return root.GetExplosionRodIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IExplosionRodEffectObserver observer) {
    broadcaster.AddExplosionRodObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IExplosionRodEffectObserver observer) {
    broadcaster.RemoveExplosionRodObserver(id, observer);
  }
  public void Delete() {
    root.EffectExplosionRodDelete(id);
  }
  public static ExplosionRod Null = new ExplosionRod(null, 0);
  public bool Exists() { return root != null && root.ExplosionRodExists(id); }
  public bool NullableIs(ExplosionRod that) {
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
  public bool Is(ExplosionRod that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
       }
}
