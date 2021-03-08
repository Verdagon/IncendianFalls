using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MarkerTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public MarkerTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public MarkerTTCMutSetIncarnation incarnation {
    get { return root.GetMarkerTTCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IMarkerTTCMutSetEffectObserver observer) {
    broadcaster.AddMarkerTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IMarkerTTCMutSetEffectObserver observer) {
    broadcaster.RemoveMarkerTTCMutSetObserver(id, observer);
  }
  public void Add(MarkerTTC element) {
      root.EffectMarkerTTCMutSetAdd(id, element.id);
  }
  public void Remove(MarkerTTC element) {
      root.EffectMarkerTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectMarkerTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectMarkerTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(MarkerTTC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<MarkerTTC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetMarkerTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<MarkerTTC>();
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
      if (!root.MarkerTTCExists(element.id)) {
        violations.Add("Null constraint violated! MarkerTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.MarkerTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
