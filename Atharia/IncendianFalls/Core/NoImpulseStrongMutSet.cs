using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class NoImpulseStrongMutSet {
  public readonly Root root;
  public readonly int id;
  public NoImpulseStrongMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public NoImpulseStrongMutSetIncarnation incarnation {
    get { return root.GetNoImpulseStrongMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, INoImpulseStrongMutSetEffectObserver observer) {
    broadcaster.AddNoImpulseStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, INoImpulseStrongMutSetEffectObserver observer) {
    broadcaster.RemoveNoImpulseStrongMutSetObserver(id, observer);
  }
  public void Add(NoImpulse element) {
      root.EffectNoImpulseStrongMutSetAdd(id, element.id);
  }
  public void Remove(NoImpulse element) {
      root.EffectNoImpulseStrongMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectNoImpulseStrongMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectNoImpulseStrongMutSetRemove(id, element);
    }
  }
  public bool Contains(NoImpulse element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<NoImpulse> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetNoImpulse(element);
    }
  }
  public void Destruct() {
    var elements = new List<NoImpulse>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.NoImpulseExists(element.id)) {
        violations.Add("Null constraint violated! NoImpulseStrongMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.NoImpulseExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
