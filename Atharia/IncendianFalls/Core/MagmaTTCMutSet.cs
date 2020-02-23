using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MagmaTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public MagmaTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public MagmaTTCMutSetIncarnation incarnation {
    get { return root.GetMagmaTTCMutSetIncarnation(id); }
  }
  public void AddObserver(IMagmaTTCMutSetEffectObserver observer) {
    root.AddMagmaTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(IMagmaTTCMutSetEffectObserver observer) {
    root.RemoveMagmaTTCMutSetObserver(id, observer);
  }
  public void Add(MagmaTTC element) {
    root.EffectMagmaTTCMutSetAdd(id, element.id);
  }
  public void Remove(MagmaTTC element) {
    root.EffectMagmaTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectMagmaTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectMagmaTTCMutSetRemove(id, elementId);
    }
  }
  public bool Contains(MagmaTTC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<MagmaTTC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetMagmaTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<MagmaTTC>();
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
      if (!root.MagmaTTCExists(element.id)) {
        violations.Add("Null constraint violated! MagmaTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.MagmaTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
