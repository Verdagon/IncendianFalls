using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LotusTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public LotusTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public LotusTTCMutSetIncarnation incarnation {
    get { return root.GetLotusTTCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, ILotusTTCMutSetEffectObserver observer) {
    broadcaster.AddLotusTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ILotusTTCMutSetEffectObserver observer) {
    broadcaster.RemoveLotusTTCMutSetObserver(id, observer);
  }
  public void Add(LotusTTC element) {
      root.EffectLotusTTCMutSetAdd(id, element.id);
  }
  public void Remove(LotusTTC element) {
      root.EffectLotusTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectLotusTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectLotusTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(LotusTTC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<LotusTTC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetLotusTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<LotusTTC>();
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
      if (!root.LotusTTCExists(element.id)) {
        violations.Add("Null constraint violated! LotusTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.LotusTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
