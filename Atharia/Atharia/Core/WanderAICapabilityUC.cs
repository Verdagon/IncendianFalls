using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class WanderAICapabilityUC {
  public readonly Root root;
  public readonly int id;
  public WanderAICapabilityUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public WanderAICapabilityUCIncarnation incarnation { get { return root.GetWanderAICapabilityUCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IWanderAICapabilityUCEffectObserver observer) {
    broadcaster.AddWanderAICapabilityUCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IWanderAICapabilityUCEffectObserver observer) {
    broadcaster.RemoveWanderAICapabilityUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectWanderAICapabilityUCDelete(id);
  }
  public static WanderAICapabilityUC Null = new WanderAICapabilityUC(null, 0);
  public bool Exists() { return root != null && root.WanderAICapabilityUCExists(id); }
  public bool NullableIs(WanderAICapabilityUC that) {
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
  public bool Is(WanderAICapabilityUC that) {
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
