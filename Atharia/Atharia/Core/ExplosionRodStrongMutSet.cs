using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ExplosionRodStrongMutSet {
  public readonly Root root;
  public readonly int id;
  public ExplosionRodStrongMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ExplosionRodStrongMutSetIncarnation incarnation {
    get { return root.GetExplosionRodStrongMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IExplosionRodStrongMutSetEffectObserver observer) {
    broadcaster.AddExplosionRodStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IExplosionRodStrongMutSetEffectObserver observer) {
    broadcaster.RemoveExplosionRodStrongMutSetObserver(id, observer);
  }
  public void Add(ExplosionRod element) {
      root.EffectExplosionRodStrongMutSetAdd(id, element.id);
  }
  public void Remove(ExplosionRod element) {
      root.EffectExplosionRodStrongMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectExplosionRodStrongMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectExplosionRodStrongMutSetRemove(id, element);
    }
  }
  public bool Contains(ExplosionRod element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<ExplosionRod> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetExplosionRod(element);
    }
  }
  public void Destruct() {
    var elements = new List<ExplosionRod>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.ExplosionRodExists(element.id)) {
        violations.Add("Null constraint violated! ExplosionRodStrongMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.ExplosionRodExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
