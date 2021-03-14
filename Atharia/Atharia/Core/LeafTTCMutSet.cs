using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LeafTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public LeafTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public LeafTTCMutSetIncarnation incarnation {
    get { return root.GetLeafTTCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, ILeafTTCMutSetEffectObserver observer) {
    broadcaster.AddLeafTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ILeafTTCMutSetEffectObserver observer) {
    broadcaster.RemoveLeafTTCMutSetObserver(id, observer);
  }
  public void Add(LeafTTC element) {
      root.EffectLeafTTCMutSetAdd(id, element.id);
  }
  public void Remove(LeafTTC element) {
      root.EffectLeafTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectLeafTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectLeafTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(LeafTTC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<LeafTTC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetLeafTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<LeafTTC>();
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
      if (!root.LeafTTCExists(element.id)) {
        violations.Add("Null constraint violated! LeafTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.LeafTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
