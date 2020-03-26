using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class EmberDeepLevelLinkerTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public EmberDeepLevelLinkerTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public EmberDeepLevelLinkerTTCMutSetIncarnation incarnation {
    get { return root.GetEmberDeepLevelLinkerTTCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IEmberDeepLevelLinkerTTCMutSetEffectObserver observer) {
    broadcaster.AddEmberDeepLevelLinkerTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IEmberDeepLevelLinkerTTCMutSetEffectObserver observer) {
    broadcaster.RemoveEmberDeepLevelLinkerTTCMutSetObserver(id, observer);
  }
  public void Add(EmberDeepLevelLinkerTTC element) {
      root.EffectEmberDeepLevelLinkerTTCMutSetAdd(id, element.id);
  }
  public void Remove(EmberDeepLevelLinkerTTC element) {
      root.EffectEmberDeepLevelLinkerTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectEmberDeepLevelLinkerTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectEmberDeepLevelLinkerTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(EmberDeepLevelLinkerTTC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<EmberDeepLevelLinkerTTC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetEmberDeepLevelLinkerTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<EmberDeepLevelLinkerTTC>();
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
      if (!root.EmberDeepLevelLinkerTTCExists(element.id)) {
        violations.Add("Null constraint violated! EmberDeepLevelLinkerTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.EmberDeepLevelLinkerTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
