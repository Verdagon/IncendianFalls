using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FloorTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public FloorTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public FloorTTCMutSetIncarnation incarnation {
    get { return root.GetFloorTTCMutSetIncarnation(id); }
  }
  public void AddObserver(IFloorTTCMutSetEffectObserver observer) {
    root.AddFloorTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(IFloorTTCMutSetEffectObserver observer) {
    root.RemoveFloorTTCMutSetObserver(id, observer);
  }
  public void Add(FloorTTC element) {
    root.EffectFloorTTCMutSetAdd(id, element.id);
  }
  public void Remove(FloorTTC element) {
    root.EffectFloorTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectFloorTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectFloorTTCMutSetRemove(id, elementId);
    }
  }
  public bool Contains(FloorTTC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<FloorTTC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetFloorTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<FloorTTC>();
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
      if (!root.FloorTTCExists(element.id)) {
        violations.Add("Null constraint violated! FloorTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.FloorTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
