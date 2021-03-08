using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LightningChargingUCMutSet {
  public readonly Root root;
  public readonly int id;
  public LightningChargingUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public LightningChargingUCMutSetIncarnation incarnation {
    get { return root.GetLightningChargingUCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, ILightningChargingUCMutSetEffectObserver observer) {
    broadcaster.AddLightningChargingUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ILightningChargingUCMutSetEffectObserver observer) {
    broadcaster.RemoveLightningChargingUCMutSetObserver(id, observer);
  }
  public void Add(LightningChargingUC element) {
      root.EffectLightningChargingUCMutSetAdd(id, element.id);
  }
  public void Remove(LightningChargingUC element) {
      root.EffectLightningChargingUCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectLightningChargingUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectLightningChargingUCMutSetRemove(id, element);
    }
  }
  public bool Contains(LightningChargingUC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<LightningChargingUC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetLightningChargingUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<LightningChargingUC>();
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
      if (!root.LightningChargingUCExists(element.id)) {
        violations.Add("Null constraint violated! LightningChargingUCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.LightningChargingUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
