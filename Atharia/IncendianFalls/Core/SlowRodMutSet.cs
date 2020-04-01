using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SlowRodMutSet {
  public readonly Root root;
  public readonly int id;
  public SlowRodMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public SlowRodMutSetIncarnation incarnation {
    get { return root.GetSlowRodMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, ISlowRodMutSetEffectObserver observer) {
    broadcaster.AddSlowRodMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ISlowRodMutSetEffectObserver observer) {
    broadcaster.RemoveSlowRodMutSetObserver(id, observer);
  }
  public void Add(SlowRod element) {
      root.EffectSlowRodMutSetAdd(id, element.id);
  }
  public void Remove(SlowRod element) {
      root.EffectSlowRodMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectSlowRodMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectSlowRodMutSetRemove(id, element);
    }
  }
  public bool Contains(SlowRod element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<SlowRod> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetSlowRod(element);
    }
  }
  public void Destruct() {
    var elements = new List<SlowRod>();
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
      if (!root.SlowRodExists(element.id)) {
        violations.Add("Null constraint violated! SlowRodMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.SlowRodExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
