using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FireTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public FireTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public FireTTCMutSetIncarnation incarnation {
    get { return root.GetFireTTCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IFireTTCMutSetEffectObserver observer) {
    broadcaster.AddFireTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IFireTTCMutSetEffectObserver observer) {
    broadcaster.RemoveFireTTCMutSetObserver(id, observer);
  }
  public void Add(FireTTC element) {
      root.EffectFireTTCMutSetAdd(id, element.id);
  }
  public void Remove(FireTTC element) {
      root.EffectFireTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectFireTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectFireTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(FireTTC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<FireTTC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetFireTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<FireTTC>();
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
      if (!root.FireTTCExists(element.id)) {
        violations.Add("Null constraint violated! FireTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.FireTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
