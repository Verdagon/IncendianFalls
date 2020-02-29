using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MudTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public MudTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public MudTTCMutSetIncarnation incarnation {
    get { return root.GetMudTTCMutSetIncarnation(id); }
  }
  public void AddObserver(IMudTTCMutSetEffectObserver observer) {
    root.AddMudTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(IMudTTCMutSetEffectObserver observer) {
    root.RemoveMudTTCMutSetObserver(id, observer);
  }
  public void Add(MudTTC element) {
    root.EffectMudTTCMutSetAdd(id, element.id);
  }
  public void Remove(MudTTC element) {
    root.EffectMudTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectMudTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectMudTTCMutSetRemove(id, elementId);
    }
  }
  public bool Contains(MudTTC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<MudTTC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetMudTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<MudTTC>();
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
      if (!root.MudTTCExists(element.id)) {
        violations.Add("Null constraint violated! MudTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.MudTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
