using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ManaPotionMutSet {
  public readonly Root root;
  public readonly int id;
  public ManaPotionMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ManaPotionMutSetIncarnation incarnation {
    get { return root.GetManaPotionMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IManaPotionMutSetEffectObserver observer) {
    broadcaster.AddManaPotionMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IManaPotionMutSetEffectObserver observer) {
    broadcaster.RemoveManaPotionMutSetObserver(id, observer);
  }
  public void Add(ManaPotion element) {
      root.EffectManaPotionMutSetAdd(id, element.id);
  }
  public void Remove(ManaPotion element) {
      root.EffectManaPotionMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectManaPotionMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectManaPotionMutSetRemove(id, element);
    }
  }
  public bool Contains(ManaPotion element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<ManaPotion> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetManaPotion(element);
    }
  }
  public void Destruct() {
    var elements = new List<ManaPotion>();
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
      if (!root.ManaPotionExists(element.id)) {
        violations.Add("Null constraint violated! ManaPotionMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.ManaPotionExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
