using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DownstairsTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public DownstairsTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public DownstairsTTCMutSetIncarnation incarnation {
    get { return root.GetDownstairsTTCMutSetIncarnation(id); }
  }
  public void AddObserver(IDownstairsTTCMutSetEffectObserver observer) {
    root.AddDownstairsTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(IDownstairsTTCMutSetEffectObserver observer) {
    root.RemoveDownstairsTTCMutSetObserver(id, observer);
  }
  public void Add(DownstairsTTC element) {
    root.EffectDownstairsTTCMutSetAdd(id, element.id);
  }
  public void Remove(DownstairsTTC element) {
    root.EffectDownstairsTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectDownstairsTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectDownstairsTTCMutSetRemove(id, elementId);
    }
  }
  public bool Contains(DownstairsTTC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<DownstairsTTC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetDownstairsTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<DownstairsTTC>();
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
      if (!root.DownstairsTTCExists(element.id)) {
        violations.Add("Null constraint violated! DownstairsTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.DownstairsTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
