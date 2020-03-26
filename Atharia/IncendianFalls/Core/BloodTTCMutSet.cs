using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BloodTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public BloodTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public BloodTTCMutSetIncarnation incarnation {
    get { return root.GetBloodTTCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IBloodTTCMutSetEffectObserver observer) {
    broadcaster.AddBloodTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IBloodTTCMutSetEffectObserver observer) {
    broadcaster.RemoveBloodTTCMutSetObserver(id, observer);
  }
  public void Add(BloodTTC element) {
      root.EffectBloodTTCMutSetAdd(id, element.id);
  }
  public void Remove(BloodTTC element) {
      root.EffectBloodTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectBloodTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectBloodTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(BloodTTC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<BloodTTC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetBloodTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<BloodTTC>();
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
      if (!root.BloodTTCExists(element.id)) {
        violations.Add("Null constraint violated! BloodTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.BloodTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
