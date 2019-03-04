using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DownStaircaseTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public DownStaircaseTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public DownStaircaseTTCMutSetIncarnation incarnation {
    get { return root.GetDownStaircaseTTCMutSetIncarnation(id); }
  }
  public void AddObserver(IDownStaircaseTTCMutSetEffectObserver observer) {
    root.AddDownStaircaseTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(IDownStaircaseTTCMutSetEffectObserver observer) {
    root.RemoveDownStaircaseTTCMutSetObserver(id, observer);
  }
  public void Add(DownStaircaseTTC element) {
    root.EffectDownStaircaseTTCMutSetAdd(id, element.id);
  }
  public void Remove(DownStaircaseTTC element) {
    root.EffectDownStaircaseTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectDownStaircaseTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectDownStaircaseTTCMutSetRemove(id, elementId);
    }
  }
  public bool Contains(DownStaircaseTTC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<DownStaircaseTTC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetDownStaircaseTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<DownStaircaseTTC>();
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
      if (!root.DownStaircaseTTCExists(element.id)) {
        violations.Add("Null constraint violated! DownStaircaseTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.DownStaircaseTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
