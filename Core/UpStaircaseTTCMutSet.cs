using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UpStaircaseTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public UpStaircaseTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public UpStaircaseTTCMutSetIncarnation incarnation {
    get { return root.GetUpStaircaseTTCMutSetIncarnation(id); }
  }
  public void AddObserver(IUpStaircaseTTCMutSetEffectObserver observer) {
    root.AddUpStaircaseTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(IUpStaircaseTTCMutSetEffectObserver observer) {
    root.RemoveUpStaircaseTTCMutSetObserver(id, observer);
  }
  public void Add(UpStaircaseTTC element) {
    root.EffectUpStaircaseTTCMutSetAdd(id, element.id);
  }
  public void Remove(UpStaircaseTTC element) {
    root.EffectUpStaircaseTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectUpStaircaseTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectUpStaircaseTTCMutSetRemove(id, elementId);
    }
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<UpStaircaseTTC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetUpStaircaseTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<UpStaircaseTTC>();
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
      if (!root.UpStaircaseTTCExists(element.id)) {
        violations.Add("Null constraint violated! UpStaircaseTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.UpStaircaseTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
