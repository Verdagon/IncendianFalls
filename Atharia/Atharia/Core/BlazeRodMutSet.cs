using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BlazeRodMutSet {
  public readonly Root root;
  public readonly int id;
  public BlazeRodMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public BlazeRodMutSetIncarnation incarnation {
    get { return root.GetBlazeRodMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IBlazeRodMutSetEffectObserver observer) {
    broadcaster.AddBlazeRodMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IBlazeRodMutSetEffectObserver observer) {
    broadcaster.RemoveBlazeRodMutSetObserver(id, observer);
  }
  public void Add(BlazeRod element) {
      root.EffectBlazeRodMutSetAdd(id, element.id);
  }
  public void Remove(BlazeRod element) {
      root.EffectBlazeRodMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectBlazeRodMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectBlazeRodMutSetRemove(id, element);
    }
  }
  public bool Contains(BlazeRod element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<BlazeRod> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetBlazeRod(element);
    }
  }
  public void Destruct() {
    var elements = new List<BlazeRod>();
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
      if (!root.BlazeRodExists(element.id)) {
        violations.Add("Null constraint violated! BlazeRodMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.BlazeRodExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
