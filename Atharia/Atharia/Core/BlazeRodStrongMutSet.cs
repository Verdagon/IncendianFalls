using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BlazeRodStrongMutSet {
  public readonly Root root;
  public readonly int id;
  public BlazeRodStrongMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public BlazeRodStrongMutSetIncarnation incarnation {
    get { return root.GetBlazeRodStrongMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IBlazeRodStrongMutSetEffectObserver observer) {
    broadcaster.AddBlazeRodStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IBlazeRodStrongMutSetEffectObserver observer) {
    broadcaster.RemoveBlazeRodStrongMutSetObserver(id, observer);
  }
  public void Add(BlazeRod element) {
      root.EffectBlazeRodStrongMutSetAdd(id, element.id);
  }
  public void Remove(BlazeRod element) {
      root.EffectBlazeRodStrongMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectBlazeRodStrongMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectBlazeRodStrongMutSetRemove(id, element);
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
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.BlazeRodExists(element.id)) {
        violations.Add("Null constraint violated! BlazeRodStrongMutSet#" + id + "." + element.id);
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
