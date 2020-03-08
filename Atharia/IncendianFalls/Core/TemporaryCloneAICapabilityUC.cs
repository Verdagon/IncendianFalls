using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class TemporaryCloneAICapabilityUC {
  public readonly Root root;
  public readonly int id;
  public TemporaryCloneAICapabilityUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public TemporaryCloneAICapabilityUCIncarnation incarnation { get { return root.GetTemporaryCloneAICapabilityUCIncarnation(id); } }
  public void AddObserver(ITemporaryCloneAICapabilityUCEffectObserver observer) {
    root.AddTemporaryCloneAICapabilityUCObserver(id, observer);
  }
  public void RemoveObserver(ITemporaryCloneAICapabilityUCEffectObserver observer) {
    root.RemoveTemporaryCloneAICapabilityUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectTemporaryCloneAICapabilityUCDelete(id);
  }
  public static TemporaryCloneAICapabilityUC Null = new TemporaryCloneAICapabilityUC(null, 0);
  public bool Exists() { return root != null && root.TemporaryCloneAICapabilityUCExists(id); }
  public bool NullableIs(TemporaryCloneAICapabilityUC that) {
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
  public bool Is(TemporaryCloneAICapabilityUC that) {
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
    set { root.EffectTemporaryCloneAICapabilityUCSetCharges(id, value); }
  }
}
}
