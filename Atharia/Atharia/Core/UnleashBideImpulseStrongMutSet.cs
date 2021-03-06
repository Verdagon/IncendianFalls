using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UnleashBideImpulseStrongMutSet {
  public readonly Root root;
  public readonly int id;
  public UnleashBideImpulseStrongMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public UnleashBideImpulseStrongMutSetIncarnation incarnation {
    get { return root.GetUnleashBideImpulseStrongMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IUnleashBideImpulseStrongMutSetEffectObserver observer) {
    broadcaster.AddUnleashBideImpulseStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IUnleashBideImpulseStrongMutSetEffectObserver observer) {
    broadcaster.RemoveUnleashBideImpulseStrongMutSetObserver(id, observer);
  }
  public void Add(UnleashBideImpulse element) {
      root.EffectUnleashBideImpulseStrongMutSetAdd(id, element.id);
  }
  public void Remove(UnleashBideImpulse element) {
      root.EffectUnleashBideImpulseStrongMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectUnleashBideImpulseStrongMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectUnleashBideImpulseStrongMutSetRemove(id, element);
    }
  }
  public bool Contains(UnleashBideImpulse element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<UnleashBideImpulse> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetUnleashBideImpulse(element);
    }
  }
  public void Destruct() {
    var elements = new List<UnleashBideImpulse>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.UnleashBideImpulseExists(element.id)) {
        violations.Add("Null constraint violated! UnleashBideImpulseStrongMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.UnleashBideImpulseExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
