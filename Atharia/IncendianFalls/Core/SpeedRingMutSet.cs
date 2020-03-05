using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SpeedRingMutSet {
  public readonly Root root;
  public readonly int id;
  public SpeedRingMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public SpeedRingMutSetIncarnation incarnation {
    get { return root.GetSpeedRingMutSetIncarnation(id); }
  }
  public void AddObserver(ISpeedRingMutSetEffectObserver observer) {
    root.AddSpeedRingMutSetObserver(id, observer);
  }
  public void RemoveObserver(ISpeedRingMutSetEffectObserver observer) {
    root.RemoveSpeedRingMutSetObserver(id, observer);
  }
  public void Add(SpeedRing element) {
    root.EffectSpeedRingMutSetAdd(id, element.id);
  }
  public void Remove(SpeedRing element) {
    root.EffectSpeedRingMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectSpeedRingMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectSpeedRingMutSetRemove(id, elementId);
    }
  }
  public bool Contains(SpeedRing element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<SpeedRing> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetSpeedRing(element);
    }
  }
  public void Destruct() {
    var elements = new List<SpeedRing>();
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
      if (!root.SpeedRingExists(element.id)) {
        violations.Add("Null constraint violated! SpeedRingMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.SpeedRingExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
