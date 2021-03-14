using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class OnFireTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public OnFireTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public OnFireTTCMutSetIncarnation incarnation {
    get { return root.GetOnFireTTCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IOnFireTTCMutSetEffectObserver observer) {
    broadcaster.AddOnFireTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IOnFireTTCMutSetEffectObserver observer) {
    broadcaster.RemoveOnFireTTCMutSetObserver(id, observer);
  }
  public void Add(OnFireTTC element) {
      root.EffectOnFireTTCMutSetAdd(id, element.id);
  }
  public void Remove(OnFireTTC element) {
      root.EffectOnFireTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectOnFireTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectOnFireTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(OnFireTTC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<OnFireTTC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetOnFireTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<OnFireTTC>();
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
      if (!root.OnFireTTCExists(element.id)) {
        violations.Add("Null constraint violated! OnFireTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.OnFireTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
