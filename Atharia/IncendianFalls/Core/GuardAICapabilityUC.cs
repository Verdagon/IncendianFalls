using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class GuardAICapabilityUC {
  public readonly Root root;
  public readonly int id;
  public GuardAICapabilityUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public GuardAICapabilityUCIncarnation incarnation { get { return root.GetGuardAICapabilityUCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IGuardAICapabilityUCEffectObserver observer) {
    broadcaster.AddGuardAICapabilityUCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IGuardAICapabilityUCEffectObserver observer) {
    broadcaster.RemoveGuardAICapabilityUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectGuardAICapabilityUCDelete(id);
  }
  public static GuardAICapabilityUC Null = new GuardAICapabilityUC(null, 0);
  public bool Exists() { return root != null && root.GuardAICapabilityUCExists(id); }
  public bool NullableIs(GuardAICapabilityUC that) {
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
  public bool Is(GuardAICapabilityUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public Location guardCenterLocation {
    get { return incarnation.guardCenterLocation; }
  }
  public int guardRadius {
    get { return incarnation.guardRadius; }
  }
}
}
