using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UpstairsTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public UpstairsTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public UpstairsTTCMutSetIncarnation incarnation {
    get { return root.GetUpstairsTTCMutSetIncarnation(id); }
  }
  public void AddObserver(IUpstairsTTCMutSetEffectObserver observer) {
    root.AddUpstairsTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(IUpstairsTTCMutSetEffectObserver observer) {
    root.RemoveUpstairsTTCMutSetObserver(id, observer);
  }
  public void Add(UpstairsTTC element) {
    root.EffectUpstairsTTCMutSetAdd(id, element.id);
  }
  public void Remove(UpstairsTTC element) {
    root.EffectUpstairsTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectUpstairsTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectUpstairsTTCMutSetRemove(id, elementId);
    }
  }
  public bool Contains(UpstairsTTC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<UpstairsTTC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetUpstairsTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<UpstairsTTC>();
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
      if (!root.UpstairsTTCExists(element.id)) {
        violations.Add("Null constraint violated! UpstairsTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.UpstairsTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
