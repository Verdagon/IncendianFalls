using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DefyImpulseStrongMutSet {
  public readonly Root root;
  public readonly int id;
  public DefyImpulseStrongMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public DefyImpulseStrongMutSetIncarnation incarnation {
    get { return root.GetDefyImpulseStrongMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IDefyImpulseStrongMutSetEffectObserver observer) {
    broadcaster.AddDefyImpulseStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IDefyImpulseStrongMutSetEffectObserver observer) {
    broadcaster.RemoveDefyImpulseStrongMutSetObserver(id, observer);
  }
  public void Add(DefyImpulse element) {
      root.EffectDefyImpulseStrongMutSetAdd(id, element.id);
  }
  public void Remove(DefyImpulse element) {
      root.EffectDefyImpulseStrongMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectDefyImpulseStrongMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectDefyImpulseStrongMutSetRemove(id, element);
    }
  }
  public bool Contains(DefyImpulse element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<DefyImpulse> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetDefyImpulse(element);
    }
  }
  public void Destruct() {
    var elements = new List<DefyImpulse>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.DefyImpulseExists(element.id)) {
        violations.Add("Null constraint violated! DefyImpulseStrongMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.DefyImpulseExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
