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
  public void AddObserver(ILightningChargedUCMutSetEffectObserver observer) {
    root.AddLightningChargedUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(ILightningChargedUCMutSetEffectObserver observer) {
    root.RemoveLightningChargedUCMutSetObserver(id, observer);
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
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectLightningChargedUCMutSetRemove(id, elementId);
    }
  }
  public bool Contains(LightningChargedUC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<LightningChargedUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
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
