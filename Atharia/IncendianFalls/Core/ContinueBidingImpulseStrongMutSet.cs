using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ContinueBidingImpulseStrongMutSet {
  public readonly Root root;
  public readonly int id;
  public ContinueBidingImpulseStrongMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ContinueBidingImpulseStrongMutSetIncarnation incarnation {
    get { return root.GetContinueBidingImpulseStrongMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IContinueBidingImpulseStrongMutSetEffectObserver observer) {
    broadcaster.AddContinueBidingImpulseStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IContinueBidingImpulseStrongMutSetEffectObserver observer) {
    broadcaster.RemoveContinueBidingImpulseStrongMutSetObserver(id, observer);
  }
  public void Add(ContinueBidingImpulse element) {
      root.EffectContinueBidingImpulseStrongMutSetAdd(id, element.id);
  }
  public void Remove(ContinueBidingImpulse element) {
      root.EffectContinueBidingImpulseStrongMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectContinueBidingImpulseStrongMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectContinueBidingImpulseStrongMutSetRemove(id, element);
    }
  }
  public bool Contains(ContinueBidingImpulse element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<ContinueBidingImpulse> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetContinueBidingImpulse(element);
    }
  }
  public void Destruct() {
    var elements = new List<ContinueBidingImpulse>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.ContinueBidingImpulseExists(element.id)) {
        violations.Add("Null constraint violated! ContinueBidingImpulseStrongMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.ContinueBidingImpulseExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
