using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DownStairsTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public DownStairsTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public DownStairsTTCMutSetIncarnation incarnation {
    get { return root.GetDownStairsTTCMutSetIncarnation(id); }
  }
  public void AddObserver(IDownStairsTTCMutSetEffectObserver observer) {
    root.AddDownStairsTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(IDownStairsTTCMutSetEffectObserver observer) {
    root.RemoveDownStairsTTCMutSetObserver(id, observer);
  }
  public void Add(DownStairsTTC element) {
    root.EffectDownStairsTTCMutSetAdd(id, element.id);
  }
  public void Remove(DownStairsTTC element) {
    root.EffectDownStairsTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectDownStairsTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectDownStairsTTCMutSetRemove(id, elementId);
    }
  }
  public bool Contains(DownStairsTTC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<DownStairsTTC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetDownStairsTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<DownStairsTTC>();
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
      if (!root.DownStairsTTCExists(element.id)) {
        violations.Add("Null constraint violated! DownStairsTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.DownStairsTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}