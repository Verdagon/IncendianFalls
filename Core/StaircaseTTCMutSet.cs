using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class StaircaseTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public StaircaseTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public StaircaseTTCMutSetIncarnation incarnation {
    get { return root.GetStaircaseTTCMutSetIncarnation(id); }
  }
  public void AddObserver(IStaircaseTTCMutSetEffectObserver observer) {
    root.AddStaircaseTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(IStaircaseTTCMutSetEffectObserver observer) {
    root.RemoveStaircaseTTCMutSetObserver(id, observer);
  }
  public void Add(StaircaseTTC element) {
    root.EffectStaircaseTTCMutSetAdd(id, element.id);
  }
  public void Remove(StaircaseTTC element) {
    root.EffectStaircaseTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectStaircaseTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectStaircaseTTCMutSetRemove(id, elementId);
    }
  }
  public bool Contains(StaircaseTTC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<StaircaseTTC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetStaircaseTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<StaircaseTTC>();
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
      if (!root.StaircaseTTCExists(element.id)) {
        violations.Add("Null constraint violated! StaircaseTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.StaircaseTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
