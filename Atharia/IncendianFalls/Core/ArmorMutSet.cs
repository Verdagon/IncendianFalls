using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ArmorMutSet {
  public readonly Root root;
  public readonly int id;
  public ArmorMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ArmorMutSetIncarnation incarnation {
    get { return root.GetArmorMutSetIncarnation(id); }
  }
  public void AddObserver(IArmorMutSetEffectObserver observer) {
    root.AddArmorMutSetObserver(id, observer);
  }
  public void RemoveObserver(IArmorMutSetEffectObserver observer) {
    root.RemoveArmorMutSetObserver(id, observer);
  }
  public void Add(Armor element) {
    root.EffectArmorMutSetAdd(id, element.id);
  }
  public void Remove(Armor element) {
    root.EffectArmorMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectArmorMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectArmorMutSetRemove(id, elementId);
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
    foreach (var element in elements) {
      element.Destruct();
    }
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.ArmorExists(element.id)) {
        violations.Add("Null constraint violated! ArmorMutSet#" + id + "." + element.id);
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
