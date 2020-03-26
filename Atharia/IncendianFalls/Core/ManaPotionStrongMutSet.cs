using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ManaPotionStrongMutSet {
  public readonly Root root;
  public readonly int id;
  public ManaPotionStrongMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ManaPotionStrongMutSetIncarnation incarnation {
    get { return root.GetManaPotionStrongMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IManaPotionStrongMutSetEffectObserver observer) {
    broadcaster.AddManaPotionStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IManaPotionStrongMutSetEffectObserver observer) {
    broadcaster.RemoveManaPotionStrongMutSetObserver(id, observer);
  }
  public void Add(ManaPotion element) {
      root.EffectManaPotionStrongMutSetAdd(id, element.id);
  }
  public void Remove(ManaPotion element) {
      root.EffectManaPotionStrongMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectManaPotionStrongMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectManaPotionStrongMutSetRemove(id, element);
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
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.ManaPotionExists(element.id)) {
        violations.Add("Null constraint violated! ManaPotionStrongMutSet#" + id + "." + element.id);
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
