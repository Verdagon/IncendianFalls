using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FireImpulseStrongMutSet {
  public readonly Root root;
  public readonly int id;
  public FireImpulseStrongMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public FireImpulseStrongMutSetIncarnation incarnation {
    get { return root.GetFireImpulseStrongMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IFireImpulseStrongMutSetEffectObserver observer) {
    broadcaster.AddFireImpulseStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IFireImpulseStrongMutSetEffectObserver observer) {
    broadcaster.RemoveFireImpulseStrongMutSetObserver(id, observer);
  }
  public void Add(FireImpulse element) {
      root.EffectFireImpulseStrongMutSetAdd(id, element.id);
  }
  public void Remove(FireImpulse element) {
      root.EffectFireImpulseStrongMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectFireImpulseStrongMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectFireImpulseStrongMutSetRemove(id, element);
    }
  }
  public bool Contains(FireImpulse element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<FireImpulse> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetFireImpulse(element);
    }
  }
  public void Destruct() {
    var elements = new List<FireImpulse>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.FireImpulseExists(element.id)) {
        violations.Add("Null constraint violated! FireImpulseStrongMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.FireImpulseExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
