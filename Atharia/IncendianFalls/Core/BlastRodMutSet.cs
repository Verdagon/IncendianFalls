using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BlastRodMutSet {
  public readonly Root root;
  public readonly int id;
  public BlastRodMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public BlastRodMutSetIncarnation incarnation {
    get { return root.GetBlastRodMutSetIncarnation(id); }
  }
  public void AddObserver(IBlastRodMutSetEffectObserver observer) {
    root.AddBlastRodMutSetObserver(id, observer);
  }
  public void RemoveObserver(IBlastRodMutSetEffectObserver observer) {
    root.RemoveBlastRodMutSetObserver(id, observer);
  }
  public void Add(BlastRod element) {
    root.EffectBlastRodMutSetAdd(id, element.id);
  }
  public void Remove(BlastRod element) {
    root.EffectBlastRodMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectBlastRodMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectBlastRodMutSetRemove(id, elementId);
    }
  }
  public bool Contains(BlastRod element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<BlastRod> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetBlastRod(element);
    }
  }
  public void Destruct() {
    var elements = new List<BlastRod>();
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
      if (!root.BlastRodExists(element.id)) {
        violations.Add("Null constraint violated! BlastRodMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.BlastRodExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}