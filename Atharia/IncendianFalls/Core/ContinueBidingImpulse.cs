using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class ContinueBidingImpulse {
  public readonly Root root;
  public readonly int id;
  public ContinueBidingImpulse(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ContinueBidingImpulseIncarnation incarnation { get { return root.GetContinueBidingImpulseIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IContinueBidingImpulseEffectObserver observer) {
    broadcaster.AddContinueBidingImpulseObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IContinueBidingImpulseEffectObserver observer) {
    broadcaster.RemoveContinueBidingImpulseObserver(id, observer);
  }
  public void Delete() {
    root.EffectContinueBidingImpulseDelete(id);
  }
  public static ContinueBidingImpulse Null = new ContinueBidingImpulse(null, 0);
  public bool Exists() { return root != null && root.ContinueBidingImpulseExists(id); }
  public bool NullableIs(ContinueBidingImpulse that) {
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
  public bool Is(ContinueBidingImpulse that) {
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
