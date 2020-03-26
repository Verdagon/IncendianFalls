using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class SorcerousUC {
  public readonly Root root;
  public readonly int id;
  public SorcerousUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public SorcerousUCIncarnation incarnation { get { return root.GetSorcerousUCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, ISorcerousUCEffectObserver observer) {
    broadcaster.AddSorcerousUCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ISorcerousUCEffectObserver observer) {
    broadcaster.RemoveSorcerousUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectSorcerousUCDelete(id);
  }
  public static SorcerousUC Null = new SorcerousUC(null, 0);
  public bool Exists() { return root != null && root.SorcerousUCExists(id); }
  public bool NullableIs(SorcerousUC that) {
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
  public bool Is(SorcerousUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public int mp {
    get { return incarnation.mp; }
    set { root.EffectSorcerousUCSetMp(id, value); }
  }
  public int maxMp {
    get { return incarnation.maxMp; }
    set { root.EffectSorcerousUCSetMaxMp(id, value); }
  }
}
}
