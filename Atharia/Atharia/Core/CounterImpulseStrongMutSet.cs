using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CounterImpulseStrongMutSet {
  public readonly Root root;
  public readonly int id;
  public CounterImpulseStrongMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public CounterImpulseStrongMutSetIncarnation incarnation {
    get { return root.GetCounterImpulseStrongMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, ICounterImpulseStrongMutSetEffectObserver observer) {
    broadcaster.AddCounterImpulseStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ICounterImpulseStrongMutSetEffectObserver observer) {
    broadcaster.RemoveCounterImpulseStrongMutSetObserver(id, observer);
  }
  public void Add(CounterImpulse element) {
      root.EffectCounterImpulseStrongMutSetAdd(id, element.id);
  }
  public void Remove(CounterImpulse element) {
      root.EffectCounterImpulseStrongMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectCounterImpulseStrongMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectCounterImpulseStrongMutSetRemove(id, element);
    }
  }
  public bool Contains(CounterImpulse element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<CounterImpulse> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetCounterImpulse(element);
    }
  }
  public void Destruct() {
    var elements = new List<CounterImpulse>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.CounterImpulseExists(element.id)) {
        violations.Add("Null constraint violated! CounterImpulseStrongMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.CounterImpulseExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
