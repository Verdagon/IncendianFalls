using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class ChallengingUC {
  public readonly Root root;
  public readonly int id;
  public ChallengingUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ChallengingUCIncarnation incarnation { get { return root.GetChallengingUCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IChallengingUCEffectObserver observer) {
    broadcaster.AddChallengingUCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IChallengingUCEffectObserver observer) {
    broadcaster.RemoveChallengingUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectChallengingUCDelete(id);
  }
  public static ChallengingUC Null = new ChallengingUC(null, 0);
  public bool Exists() { return root != null && root.ChallengingUCExists(id); }
  public bool NullableIs(ChallengingUC that) {
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
  public bool Is(ChallengingUC that) {
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
