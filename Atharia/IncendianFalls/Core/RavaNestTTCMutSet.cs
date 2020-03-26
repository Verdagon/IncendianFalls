using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class RavaNestTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public RavaNestTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public RavaNestTTCMutSetIncarnation incarnation {
    get { return root.GetRavaNestTTCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IRavaNestTTCMutSetEffectObserver observer) {
    broadcaster.AddRavaNestTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IRavaNestTTCMutSetEffectObserver observer) {
    broadcaster.RemoveRavaNestTTCMutSetObserver(id, observer);
  }
  public void Add(RavaNestTTC element) {
      root.EffectRavaNestTTCMutSetAdd(id, element.id);
  }
  public void Remove(RavaNestTTC element) {
      root.EffectRavaNestTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectRavaNestTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectRavaNestTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(RavaNestTTC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<RavaNestTTC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetRavaNestTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<RavaNestTTC>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
    foreach (var element in elements) {
      element.Destruct();
    }
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.RavaNestTTCExists(element.id)) {
        violations.Add("Null constraint violated! RavaNestTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.RavaNestTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
