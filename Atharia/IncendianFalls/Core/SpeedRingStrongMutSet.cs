using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SpeedRingStrongMutSet {
  public readonly Root root;
  public readonly int id;
  public SpeedRingStrongMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public SpeedRingStrongMutSetIncarnation incarnation {
    get { return root.GetSpeedRingStrongMutSetIncarnation(id); }
  }
  public void AddObserver(ISpeedRingStrongMutSetEffectObserver observer) {
    root.AddSpeedRingStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(ISpeedRingStrongMutSetEffectObserver observer) {
    root.RemoveSpeedRingStrongMutSetObserver(id, observer);
  }
  public void Add(SpeedRing element) {
    root.EffectSpeedRingStrongMutSetAdd(id, element.id);
  }
  public void Remove(SpeedRing element) {
    root.EffectSpeedRingStrongMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectSpeedRingStrongMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectSpeedRingStrongMutSetRemove(id, elementId);
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
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.SpeedRingExists(element.id)) {
        violations.Add("Null constraint violated! SpeedRingStrongMutSet#" + id + "." + element.id);
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
