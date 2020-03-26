using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class StartBidingImpulseStrongMutSet {
  public readonly Root root;
  public readonly int id;
  public StartBidingImpulseStrongMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public StartBidingImpulseStrongMutSetIncarnation incarnation {
    get { return root.GetStartBidingImpulseStrongMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IStartBidingImpulseStrongMutSetEffectObserver observer) {
    broadcaster.AddStartBidingImpulseStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IStartBidingImpulseStrongMutSetEffectObserver observer) {
    broadcaster.RemoveStartBidingImpulseStrongMutSetObserver(id, observer);
  }
  public void Add(StartBidingImpulse element) {
      root.EffectStartBidingImpulseStrongMutSetAdd(id, element.id);
  }
  public void Remove(StartBidingImpulse element) {
      root.EffectStartBidingImpulseStrongMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectStartBidingImpulseStrongMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectStartBidingImpulseStrongMutSetRemove(id, element);
    }
  }
  public bool Contains(StartBidingImpulse element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<StartBidingImpulse> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetStartBidingImpulse(element);
    }
  }
  public void Destruct() {
    var elements = new List<StartBidingImpulse>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.StartBidingImpulseExists(element.id)) {
        violations.Add("Null constraint violated! StartBidingImpulseStrongMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.StartBidingImpulseExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
