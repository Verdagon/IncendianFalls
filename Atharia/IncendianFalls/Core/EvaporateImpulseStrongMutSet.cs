using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class EvaporateImpulseStrongMutSet {
  public readonly Root root;
  public readonly int id;
  public EvaporateImpulseStrongMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public EvaporateImpulseStrongMutSetIncarnation incarnation {
    get { return root.GetEvaporateImpulseStrongMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IEvaporateImpulseStrongMutSetEffectObserver observer) {
    broadcaster.AddEvaporateImpulseStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IEvaporateImpulseStrongMutSetEffectObserver observer) {
    broadcaster.RemoveEvaporateImpulseStrongMutSetObserver(id, observer);
  }
  public void Add(EvaporateImpulse element) {
      root.EffectEvaporateImpulseStrongMutSetAdd(id, element.id);
  }
  public void Remove(EvaporateImpulse element) {
      root.EffectEvaporateImpulseStrongMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectEvaporateImpulseStrongMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectEvaporateImpulseStrongMutSetRemove(id, element);
    }
  }
  public bool Contains(EvaporateImpulse element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<EvaporateImpulse> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetEvaporateImpulse(element);
    }
  }
  public void Destruct() {
    var elements = new List<EvaporateImpulse>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.EvaporateImpulseExists(element.id)) {
        violations.Add("Null constraint violated! EvaporateImpulseStrongMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.EvaporateImpulseExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
