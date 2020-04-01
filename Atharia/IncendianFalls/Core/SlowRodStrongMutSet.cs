using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SlowRodStrongMutSet {
  public readonly Root root;
  public readonly int id;
  public SlowRodStrongMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public SlowRodStrongMutSetIncarnation incarnation {
    get { return root.GetSlowRodStrongMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, ISlowRodStrongMutSetEffectObserver observer) {
    broadcaster.AddSlowRodStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ISlowRodStrongMutSetEffectObserver observer) {
    broadcaster.RemoveSlowRodStrongMutSetObserver(id, observer);
  }
  public void Add(SlowRod element) {
      root.EffectSlowRodStrongMutSetAdd(id, element.id);
  }
  public void Remove(SlowRod element) {
      root.EffectSlowRodStrongMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectSlowRodStrongMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectSlowRodStrongMutSetRemove(id, element);
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
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.SlowRodExists(element.id)) {
        violations.Add("Null constraint violated! SlowRodStrongMutSet#" + id + "." + element.id);
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
