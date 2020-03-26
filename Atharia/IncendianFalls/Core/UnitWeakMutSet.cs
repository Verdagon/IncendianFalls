using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UnitWeakMutSet {
  public readonly Root root;
  public readonly int id;
  public UnitWeakMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public UnitWeakMutSetIncarnation incarnation {
    get { return root.GetUnitWeakMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IUnitWeakMutSetEffectObserver observer) {
    broadcaster.AddUnitWeakMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IUnitWeakMutSetEffectObserver observer) {
    broadcaster.RemoveUnitWeakMutSetObserver(id, observer);
  }
  public void Add(Unit element) {
      root.EffectUnitWeakMutSetAdd(id, element.id);
  }
  public void Remove(Unit element) {
      root.EffectUnitWeakMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectUnitWeakMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectUnitWeakMutSetRemove(id, element);
    }
  }
  public bool Contains(Unit element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<Unit> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetUnit(element);
    }
  }
  public void Destruct() {
    var elements = new List<Unit>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.UnitExists(element.id)) {
        violations.Add("Null constraint violated! UnitWeakMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.UnitExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
