using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CliffTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public CliffTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public CliffTTCMutSetIncarnation incarnation {
    get { return root.GetCliffTTCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, ICliffTTCMutSetEffectObserver observer) {
    broadcaster.AddCliffTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ICliffTTCMutSetEffectObserver observer) {
    broadcaster.RemoveCliffTTCMutSetObserver(id, observer);
  }
  public void Add(CliffTTC element) {
      root.EffectCliffTTCMutSetAdd(id, element.id);
  }
  public void Remove(CliffTTC element) {
      root.EffectCliffTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectCliffTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectCliffTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(CliffTTC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<CliffTTC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetCliffTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<CliffTTC>();
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
      if (!root.CliffTTCExists(element.id)) {
        violations.Add("Null constraint violated! CliffTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.CliffTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
