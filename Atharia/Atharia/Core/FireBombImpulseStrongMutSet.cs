using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FireBombImpulseStrongMutSet {
  public readonly Root root;
  public readonly int id;
  public FireBombImpulseStrongMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public FireBombImpulseStrongMutSetIncarnation incarnation {
    get { return root.GetFireBombImpulseStrongMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IFireBombImpulseStrongMutSetEffectObserver observer) {
    broadcaster.AddFireBombImpulseStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IFireBombImpulseStrongMutSetEffectObserver observer) {
    broadcaster.RemoveFireBombImpulseStrongMutSetObserver(id, observer);
  }
  public void Add(FireBombImpulse element) {
      root.EffectFireBombImpulseStrongMutSetAdd(id, element.id);
  }
  public void Remove(FireBombImpulse element) {
      root.EffectFireBombImpulseStrongMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectFireBombImpulseStrongMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectFireBombImpulseStrongMutSetRemove(id, element);
    }
  }
  public bool Contains(FireBombImpulse element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<FireBombImpulse> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetFireBombImpulse(element);
    }
  }
  public void Destruct() {
    var elements = new List<FireBombImpulse>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.FireBombImpulseExists(element.id)) {
        violations.Add("Null constraint violated! FireBombImpulseStrongMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.FireBombImpulseExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
