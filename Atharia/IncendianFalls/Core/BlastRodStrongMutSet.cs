using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BlastRodStrongMutSet {
  public readonly Root root;
  public readonly int id;
  public BlastRodStrongMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public BlastRodStrongMutSetIncarnation incarnation {
    get { return root.GetBlastRodStrongMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IBlastRodStrongMutSetEffectObserver observer) {
    broadcaster.AddBlastRodStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IBlastRodStrongMutSetEffectObserver observer) {
    broadcaster.RemoveBlastRodStrongMutSetObserver(id, observer);
  }
  public void Add(BlastRod element) {
      root.EffectBlastRodStrongMutSetAdd(id, element.id);
  }
  public void Remove(BlastRod element) {
      root.EffectBlastRodStrongMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectBlastRodStrongMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectBlastRodStrongMutSetRemove(id, element);
    }
  }
  public bool Contains(BlastRod element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<BlastRod> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetBlastRod(element);
    }
  }
  public void Destruct() {
    var elements = new List<BlastRod>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.BlastRodExists(element.id)) {
        violations.Add("Null constraint violated! BlastRodStrongMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.BlastRodExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
