using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class WaterTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public WaterTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public WaterTTCMutSetIncarnation incarnation {
    get { return root.GetWaterTTCMutSetIncarnation(id); }
  }
  public void AddObserver(IWaterTTCMutSetEffectObserver observer) {
    root.AddWaterTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(IWaterTTCMutSetEffectObserver observer) {
    root.RemoveWaterTTCMutSetObserver(id, observer);
  }
  public void Add(WaterTTC element) {
    root.EffectWaterTTCMutSetAdd(id, element.id);
  }
  public void Remove(WaterTTC element) {
    root.EffectWaterTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectWaterTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectWaterTTCMutSetRemove(id, elementId);
    }
  }
  public bool Contains(WaterTTC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<WaterTTC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetWaterTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<WaterTTC>();
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
      if (!root.WaterTTCExists(element.id)) {
        violations.Add("Null constraint violated! WaterTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.WaterTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}