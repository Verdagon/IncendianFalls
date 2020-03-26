using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class HoldPositionImpulseStrongMutSet {
  public readonly Root root;
  public readonly int id;
  public HoldPositionImpulseStrongMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public HoldPositionImpulseStrongMutSetIncarnation incarnation {
    get { return root.GetHoldPositionImpulseStrongMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IHoldPositionImpulseStrongMutSetEffectObserver observer) {
    broadcaster.AddHoldPositionImpulseStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IHoldPositionImpulseStrongMutSetEffectObserver observer) {
    broadcaster.RemoveHoldPositionImpulseStrongMutSetObserver(id, observer);
  }
  public void Add(HoldPositionImpulse element) {
      root.EffectHoldPositionImpulseStrongMutSetAdd(id, element.id);
  }
  public void Remove(HoldPositionImpulse element) {
      root.EffectHoldPositionImpulseStrongMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectHoldPositionImpulseStrongMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectHoldPositionImpulseStrongMutSetRemove(id, element);
    }
  }
  public bool Contains(HoldPositionImpulse element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<HoldPositionImpulse> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetHoldPositionImpulse(element);
    }
  }
  public void Destruct() {
    var elements = new List<HoldPositionImpulse>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.HoldPositionImpulseExists(element.id)) {
        violations.Add("Null constraint violated! HoldPositionImpulseStrongMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.HoldPositionImpulseExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
