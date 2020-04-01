using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LightningChargedUCWeakMutSet {
  public readonly Root root;
  public readonly int id;
  public LightningChargedUCWeakMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public LightningChargedUCWeakMutSetIncarnation incarnation {
    get { return root.GetLightningChargedUCWeakMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, ILightningChargedUCWeakMutSetEffectObserver observer) {
    broadcaster.AddLightningChargedUCWeakMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ILightningChargedUCWeakMutSetEffectObserver observer) {
    broadcaster.RemoveLightningChargedUCWeakMutSetObserver(id, observer);
  }
  public void Add(LightningChargedUC element) {
      root.EffectLightningChargedUCWeakMutSetAdd(id, element.id);
  }
  public void Remove(LightningChargedUC element) {
      root.EffectLightningChargedUCWeakMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectLightningChargedUCWeakMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectLightningChargedUCWeakMutSetRemove(id, element);
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
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.LightningChargedUCExists(element.id)) {
        violations.Add("Null constraint violated! LightningChargedUCWeakMutSet#" + id + "." + element.id);
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
