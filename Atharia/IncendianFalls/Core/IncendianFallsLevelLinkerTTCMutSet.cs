using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class IncendianFallsLevelLinkerTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public IncendianFallsLevelLinkerTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public IncendianFallsLevelLinkerTTCMutSetIncarnation incarnation {
    get { return root.GetIncendianFallsLevelLinkerTTCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IIncendianFallsLevelLinkerTTCMutSetEffectObserver observer) {
    broadcaster.AddIncendianFallsLevelLinkerTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IIncendianFallsLevelLinkerTTCMutSetEffectObserver observer) {
    broadcaster.RemoveIncendianFallsLevelLinkerTTCMutSetObserver(id, observer);
  }
  public void Add(IncendianFallsLevelLinkerTTC element) {
      root.EffectIncendianFallsLevelLinkerTTCMutSetAdd(id, element.id);
  }
  public void Remove(IncendianFallsLevelLinkerTTC element) {
      root.EffectIncendianFallsLevelLinkerTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectIncendianFallsLevelLinkerTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectIncendianFallsLevelLinkerTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(IncendianFallsLevelLinkerTTC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<IncendianFallsLevelLinkerTTC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetIncendianFallsLevelLinkerTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<IncendianFallsLevelLinkerTTC>();
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
      if (!root.IncendianFallsLevelLinkerTTCExists(element.id)) {
        violations.Add("Null constraint violated! IncendianFallsLevelLinkerTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.IncendianFallsLevelLinkerTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
