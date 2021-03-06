using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CliffLandingTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public CliffLandingTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public CliffLandingTTCMutSetIncarnation incarnation {
    get { return root.GetCliffLandingTTCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, ICliffLandingTTCMutSetEffectObserver observer) {
    broadcaster.AddCliffLandingTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ICliffLandingTTCMutSetEffectObserver observer) {
    broadcaster.RemoveCliffLandingTTCMutSetObserver(id, observer);
  }
  public void Add(CliffLandingTTC element) {
      root.EffectCliffLandingTTCMutSetAdd(id, element.id);
  }
  public void Remove(CliffLandingTTC element) {
      root.EffectCliffLandingTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectCliffLandingTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectCliffLandingTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(CliffLandingTTC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<CliffLandingTTC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetCliffLandingTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<CliffLandingTTC>();
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
      if (!root.CliffLandingTTCExists(element.id)) {
        violations.Add("Null constraint violated! CliffLandingTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.CliffLandingTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
