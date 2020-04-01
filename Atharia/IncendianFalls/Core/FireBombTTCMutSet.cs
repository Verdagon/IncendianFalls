using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FireBombTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public FireBombTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public FireBombTTCMutSetIncarnation incarnation {
    get { return root.GetFireBombTTCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IFireBombTTCMutSetEffectObserver observer) {
    broadcaster.AddFireBombTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IFireBombTTCMutSetEffectObserver observer) {
    broadcaster.RemoveFireBombTTCMutSetObserver(id, observer);
  }
  public void Add(FireBombTTC element) {
      root.EffectFireBombTTCMutSetAdd(id, element.id);
  }
  public void Remove(FireBombTTC element) {
      root.EffectFireBombTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectFireBombTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectFireBombTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(FireBombTTC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<FireBombTTC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetFireBombTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<FireBombTTC>();
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
      if (!root.FireBombTTCExists(element.id)) {
        violations.Add("Null constraint violated! FireBombTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.FireBombTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
