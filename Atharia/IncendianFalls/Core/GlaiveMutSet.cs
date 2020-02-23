using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class GlaiveMutSet {
  public readonly Root root;
  public readonly int id;
  public GlaiveMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public GlaiveMutSetIncarnation incarnation {
    get { return root.GetGlaiveMutSetIncarnation(id); }
  }
  public void AddObserver(IGlaiveMutSetEffectObserver observer) {
    root.AddGlaiveMutSetObserver(id, observer);
  }
  public void RemoveObserver(IGlaiveMutSetEffectObserver observer) {
    root.RemoveGlaiveMutSetObserver(id, observer);
  }
  public void Add(Glaive element) {
    root.EffectGlaiveMutSetAdd(id, element.id);
  }
  public void Remove(Glaive element) {
    root.EffectGlaiveMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectGlaiveMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectGlaiveMutSetRemove(id, elementId);
    }
  }
  public bool Contains(Glaive element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<Glaive> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetGlaive(element);
    }
  }
  public void Destruct() {
    var elements = new List<Glaive>();
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
      if (!root.GlaiveExists(element.id)) {
        violations.Add("Null constraint violated! GlaiveMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.GlaiveExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
