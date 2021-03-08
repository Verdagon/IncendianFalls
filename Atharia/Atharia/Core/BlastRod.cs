using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class BlastRod {
  public readonly Root root;
  public readonly int id;
  public BlastRod(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public BlastRodIncarnation incarnation { get { return root.GetBlastRodIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IBlastRodEffectObserver observer) {
    broadcaster.AddBlastRodObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IBlastRodEffectObserver observer) {
    broadcaster.RemoveBlastRodObserver(id, observer);
  }
  public void Delete() {
    root.EffectBlastRodDelete(id);
  }
  public static BlastRod Null = new BlastRod(null, 0);
  public bool Exists() { return root != null && root.BlastRodExists(id); }
  public bool NullableIs(BlastRod that) {
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
  public bool Is(BlastRod that) {
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
