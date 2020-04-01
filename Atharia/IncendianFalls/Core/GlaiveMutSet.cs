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
  public void AddObserver(EffectBroadcaster broadcaster, IGlaiveMutSetEffectObserver observer) {
    broadcaster.AddGlaiveMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IGlaiveMutSetEffectObserver observer) {
    broadcaster.RemoveGlaiveMutSetObserver(id, observer);
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
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectGlaiveMutSetRemove(id, element);
    }
  }
  public bool Contains(Glaive element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<Glaive> GetEnumerator() {
    foreach (var element in incarnation.elements) {
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
