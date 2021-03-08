using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MiredUCWeakMutSet {
  public readonly Root root;
  public readonly int id;
  public MiredUCWeakMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public MiredUCWeakMutSetIncarnation incarnation {
    get { return root.GetMiredUCWeakMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IMiredUCWeakMutSetEffectObserver observer) {
    broadcaster.AddMiredUCWeakMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IMiredUCWeakMutSetEffectObserver observer) {
    broadcaster.RemoveMiredUCWeakMutSetObserver(id, observer);
  }
  public void Add(MiredUC element) {
      root.EffectMiredUCWeakMutSetAdd(id, element.id);
  }
  public void Remove(MiredUC element) {
      root.EffectMiredUCWeakMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectMiredUCWeakMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectMiredUCWeakMutSetRemove(id, element);
    }
  }
  public bool Contains(MiredUC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<MiredUC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetMiredUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<MiredUC>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.MiredUCExists(element.id)) {
        violations.Add("Null constraint violated! MiredUCWeakMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.MiredUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
