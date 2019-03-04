using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DecorativeTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public DecorativeTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public DecorativeTTCMutSetIncarnation incarnation {
    get { return root.GetDecorativeTTCMutSetIncarnation(id); }
  }
  public void AddObserver(IDecorativeTTCMutSetEffectObserver observer) {
    root.AddDecorativeTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(IDecorativeTTCMutSetEffectObserver observer) {
    root.RemoveDecorativeTTCMutSetObserver(id, observer);
  }
  public void Add(DecorativeTTC element) {
    root.EffectDecorativeTTCMutSetAdd(id, element.id);
  }
  public void Remove(DecorativeTTC element) {
    root.EffectDecorativeTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectDecorativeTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectDecorativeTTCMutSetRemove(id, elementId);
    }
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<DecorativeTTC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetDecorativeTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<DecorativeTTC>();
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
      if (!root.DecorativeTTCExists(element.id)) {
        violations.Add("Null constraint violated! DecorativeTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.DecorativeTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
