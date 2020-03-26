using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class WarperTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public WarperTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public WarperTTCMutSetIncarnation incarnation {
    get { return root.GetWarperTTCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IWarperTTCMutSetEffectObserver observer) {
    broadcaster.AddWarperTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IWarperTTCMutSetEffectObserver observer) {
    broadcaster.RemoveWarperTTCMutSetObserver(id, observer);
  }
  public void Add(WarperTTC element) {
      root.EffectWarperTTCMutSetAdd(id, element.id);
  }
  public void Remove(WarperTTC element) {
      root.EffectWarperTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectWarperTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectWarperTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(WarperTTC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<WarperTTC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetWarperTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<WarperTTC>();
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
      if (!root.WarperTTCExists(element.id)) {
        violations.Add("Null constraint violated! WarperTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.WarperTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
