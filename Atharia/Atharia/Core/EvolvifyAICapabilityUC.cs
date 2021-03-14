using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class EvolvifyAICapabilityUC {
  public readonly Root root;
  public readonly int id;
  public EvolvifyAICapabilityUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public EvolvifyAICapabilityUCIncarnation incarnation { get { return root.GetEvolvifyAICapabilityUCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IEvolvifyAICapabilityUCEffectObserver observer) {
    broadcaster.AddEvolvifyAICapabilityUCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IEvolvifyAICapabilityUCEffectObserver observer) {
    broadcaster.RemoveEvolvifyAICapabilityUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectEvolvifyAICapabilityUCDelete(id);
  }
  public static EvolvifyAICapabilityUC Null = new EvolvifyAICapabilityUC(null, 0);
  public bool Exists() { return root != null && root.EvolvifyAICapabilityUCExists(id); }
  public bool NullableIs(EvolvifyAICapabilityUC that) {
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
  public bool Is(EvolvifyAICapabilityUC that) {
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
