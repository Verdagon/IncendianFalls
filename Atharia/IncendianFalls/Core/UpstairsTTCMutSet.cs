using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UpStairsTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public UpStairsTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public UpStairsTTCMutSetIncarnation incarnation {
    get { return root.GetUpStairsTTCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IUpStairsTTCMutSetEffectObserver observer) {
    broadcaster.AddUpStairsTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IUpStairsTTCMutSetEffectObserver observer) {
    broadcaster.RemoveUpStairsTTCMutSetObserver(id, observer);
  }
  public void Add(UpStairsTTC element) {
      root.EffectUpStairsTTCMutSetAdd(id, element.id);
  }
  public void Remove(UpStairsTTC element) {
      root.EffectUpStairsTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectUpStairsTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectUpStairsTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(UpStairsTTC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<UpStairsTTC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetUpStairsTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<UpStairsTTC>();
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
      if (!root.UpStairsTTCExists(element.id)) {
        violations.Add("Null constraint violated! UpStairsTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.UpStairsTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
