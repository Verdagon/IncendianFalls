using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class EvolvifyImpulseStrongMutSet {
  public readonly Root root;
  public readonly int id;
  public EvolvifyImpulseStrongMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public EvolvifyImpulseStrongMutSetIncarnation incarnation {
    get { return root.GetEvolvifyImpulseStrongMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IEvolvifyImpulseStrongMutSetEffectObserver observer) {
    broadcaster.AddEvolvifyImpulseStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IEvolvifyImpulseStrongMutSetEffectObserver observer) {
    broadcaster.RemoveEvolvifyImpulseStrongMutSetObserver(id, observer);
  }
  public void Add(EvolvifyImpulse element) {
      root.EffectEvolvifyImpulseStrongMutSetAdd(id, element.id);
  }
  public void Remove(EvolvifyImpulse element) {
      root.EffectEvolvifyImpulseStrongMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectEvolvifyImpulseStrongMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectEvolvifyImpulseStrongMutSetRemove(id, element);
    }
  }
  public bool Contains(EvolvifyImpulse element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<EvolvifyImpulse> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetEvolvifyImpulse(element);
    }
  }
  public void Destruct() {
    var elements = new List<EvolvifyImpulse>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.EvolvifyImpulseExists(element.id)) {
        violations.Add("Null constraint violated! EvolvifyImpulseStrongMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.EvolvifyImpulseExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
