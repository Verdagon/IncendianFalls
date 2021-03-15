using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BequeathUCMutSet {
  public readonly Root root;
  public readonly int id;
  public BequeathUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public BequeathUCMutSetIncarnation incarnation {
    get { return root.GetBequeathUCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IBequeathUCMutSetEffectObserver observer) {
    broadcaster.AddBequeathUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IBequeathUCMutSetEffectObserver observer) {
    broadcaster.RemoveBequeathUCMutSetObserver(id, observer);
  }
  public void Add(BequeathUC element) {
      root.EffectBequeathUCMutSetAdd(id, element.id);
  }
  public void Remove(BequeathUC element) {
      root.EffectBequeathUCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectBequeathUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectBequeathUCMutSetRemove(id, element);
    }
  }
  public bool Contains(BequeathUC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<BequeathUC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetBequeathUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<BequeathUC>();
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
      if (!root.BequeathUCExists(element.id)) {
        violations.Add("Null constraint violated! BequeathUCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.BequeathUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
