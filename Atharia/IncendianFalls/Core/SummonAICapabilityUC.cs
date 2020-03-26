using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class SummonAICapabilityUC {
  public readonly Root root;
  public readonly int id;
  public SummonAICapabilityUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public SummonAICapabilityUCIncarnation incarnation { get { return root.GetSummonAICapabilityUCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, ISummonAICapabilityUCEffectObserver observer) {
    broadcaster.AddSummonAICapabilityUCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ISummonAICapabilityUCEffectObserver observer) {
    broadcaster.RemoveSummonAICapabilityUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectSummonAICapabilityUCDelete(id);
  }
  public static SummonAICapabilityUC Null = new SummonAICapabilityUC(null, 0);
  public bool Exists() { return root != null && root.SummonAICapabilityUCExists(id); }
  public bool NullableIs(SummonAICapabilityUC that) {
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
  public bool Is(SummonAICapabilityUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public string blueprintName {
    get { return incarnation.blueprintName; }
  }
  public int charges {
    get { return incarnation.charges; }
    set { root.EffectSummonAICapabilityUCSetCharges(id, value); }
  }
}
}
