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
  public void AddObserver(EffectBroadcaster broadcaster, IWaterTTCMutSetEffectObserver observer) {
    broadcaster.AddWaterTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IWaterTTCMutSetEffectObserver observer) {
    broadcaster.RemoveWaterTTCMutSetObserver(id, observer);
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
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectWaterTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(WaterTTC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<WaterTTC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
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
