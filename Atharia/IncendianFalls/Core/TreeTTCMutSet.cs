using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TreeTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public TreeTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public TreeTTCMutSetIncarnation incarnation {
    get { return root.GetTreeTTCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, ITreeTTCMutSetEffectObserver observer) {
    broadcaster.AddTreeTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ITreeTTCMutSetEffectObserver observer) {
    broadcaster.RemoveTreeTTCMutSetObserver(id, observer);
  }
  public void Add(TreeTTC element) {
      root.EffectTreeTTCMutSetAdd(id, element.id);
  }
  public void Remove(TreeTTC element) {
      root.EffectTreeTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectTreeTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectTreeTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(TreeTTC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<TreeTTC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetTreeTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<TreeTTC>();
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
      if (!root.TreeTTCExists(element.id)) {
        violations.Add("Null constraint violated! TreeTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.TreeTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
