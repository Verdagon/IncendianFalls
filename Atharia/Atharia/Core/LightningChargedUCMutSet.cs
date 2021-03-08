using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LightningChargedUCMutSet {
  public readonly Root root;
  public readonly int id;
  public LightningChargedUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public LightningChargedUCMutSetIncarnation incarnation {
    get { return root.GetLightningChargedUCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, ILightningChargedUCMutSetEffectObserver observer) {
    broadcaster.AddLightningChargedUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ILightningChargedUCMutSetEffectObserver observer) {
    broadcaster.RemoveLightningChargedUCMutSetObserver(id, observer);
  }
  public void Add(LightningChargedUC element) {
      root.EffectLightningChargedUCMutSetAdd(id, element.id);
  }
  public void Remove(LightningChargedUC element) {
      root.EffectLightningChargedUCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectLightningChargedUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectLightningChargedUCMutSetRemove(id, element);
    }
  }
  public bool Contains(LightningChargedUC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<LightningChargedUC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetLightningChargedUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<LightningChargedUC>();
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
      if (!root.LightningChargedUCExists(element.id)) {
        violations.Add("Null constraint violated! LightningChargedUCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.LightningChargedUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
