using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ExplosionRodMutSet {
  public readonly Root root;
  public readonly int id;
  public ExplosionRodMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ExplosionRodMutSetIncarnation incarnation {
    get { return root.GetExplosionRodMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IExplosionRodMutSetEffectObserver observer) {
    broadcaster.AddExplosionRodMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IExplosionRodMutSetEffectObserver observer) {
    broadcaster.RemoveExplosionRodMutSetObserver(id, observer);
  }
  public void Add(ExplosionRod element) {
      root.EffectExplosionRodMutSetAdd(id, element.id);
  }
  public void Remove(ExplosionRod element) {
      root.EffectExplosionRodMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectExplosionRodMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectExplosionRodMutSetRemove(id, element);
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
    foreach (var element in elements) {
      element.Destruct();
    }
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.ExplosionRodExists(element.id)) {
        violations.Add("Null constraint violated! ExplosionRodMutSet#" + id + "." + element.id);
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
