using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ArmorStrongMutSet {
  public readonly Root root;
  public readonly int id;
  public ArmorStrongMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ArmorStrongMutSetIncarnation incarnation {
    get { return root.GetArmorStrongMutSetIncarnation(id); }
  }
  public void AddObserver(IArmorStrongMutSetEffectObserver observer) {
    root.AddArmorStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(IArmorStrongMutSetEffectObserver observer) {
    root.RemoveArmorStrongMutSetObserver(id, observer);
  }
  public void Add(Armor element) {
    root.EffectArmorStrongMutSetAdd(id, element.id);
  }
  public void Remove(Armor element) {
    root.EffectArmorStrongMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectArmorStrongMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectArmorStrongMutSetRemove(id, elementId);
    }
  }
  public bool Contains(Armor element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<Armor> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetArmor(element);
    }
  }
  public void Destruct() {
    var elements = new List<Armor>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.ArmorExists(element.id)) {
        violations.Add("Null constraint violated! ArmorStrongMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.ArmorExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
