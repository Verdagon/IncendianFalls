using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FlowerTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public FlowerTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public FlowerTTCMutSetIncarnation incarnation {
    get { return root.GetFlowerTTCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IFlowerTTCMutSetEffectObserver observer) {
    broadcaster.AddFlowerTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IFlowerTTCMutSetEffectObserver observer) {
    broadcaster.RemoveFlowerTTCMutSetObserver(id, observer);
  }
  public void Add(FlowerTTC element) {
      root.EffectFlowerTTCMutSetAdd(id, element.id);
  }
  public void Remove(FlowerTTC element) {
      root.EffectFlowerTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectFlowerTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectFlowerTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(FlowerTTC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<FlowerTTC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetFlowerTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<FlowerTTC>();
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
      if (!root.FlowerTTCExists(element.id)) {
        violations.Add("Null constraint violated! FlowerTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.FlowerTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
