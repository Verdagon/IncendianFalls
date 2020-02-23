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
  public void AddObserver(ICliffTTCMutSetEffectObserver observer) {
    root.AddCliffTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(ICliffTTCMutSetEffectObserver observer) {
    root.RemoveCliffTTCMutSetObserver(id, observer);
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
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectCliffTTCMutSetRemove(id, elementId);
    }
  }
  public bool Contains(CliffTTC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<CliffTTC> GetEnumerator() {
    foreach (var element in incarnation.set) {
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
