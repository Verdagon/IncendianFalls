using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class StoneTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public StoneTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public StoneTTCMutSetIncarnation incarnation {
    get { return root.GetStoneTTCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IStoneTTCMutSetEffectObserver observer) {
    broadcaster.AddStoneTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IStoneTTCMutSetEffectObserver observer) {
    broadcaster.RemoveStoneTTCMutSetObserver(id, observer);
  }
  public void Add(StoneTTC element) {
      root.EffectStoneTTCMutSetAdd(id, element.id);
  }
  public void Remove(StoneTTC element) {
      root.EffectStoneTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectStoneTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectStoneTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(StoneTTC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<StoneTTC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetStoneTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<StoneTTC>();
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
      if (!root.StoneTTCExists(element.id)) {
        violations.Add("Null constraint violated! StoneTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.StoneTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
