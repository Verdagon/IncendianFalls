using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class DefyingUC {
  public readonly Root root;
  public readonly int id;
  public DefyingUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public DefyingUCIncarnation incarnation { get { return root.GetDefyingUCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IDefyingUCEffectObserver observer) {
    broadcaster.AddDefyingUCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IDefyingUCEffectObserver observer) {
    broadcaster.RemoveDefyingUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectDefyingUCDelete(id);
  }
  public static DefyingUC Null = new DefyingUC(null, 0);
  public bool Exists() { return root != null && root.DefyingUCExists(id); }
  public bool NullableIs(DefyingUC that) {
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
  public bool Is(DefyingUC that) {
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
