using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class RocksTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public RocksTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public RocksTTCMutSetIncarnation incarnation {
    get { return root.GetRocksTTCMutSetIncarnation(id); }
  }
  public void AddObserver(IRocksTTCMutSetEffectObserver observer) {
    root.AddRocksTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(IRocksTTCMutSetEffectObserver observer) {
    root.RemoveRocksTTCMutSetObserver(id, observer);
  }
  public void Add(RocksTTC element) {
    root.EffectRocksTTCMutSetAdd(id, element.id);
  }
  public void Remove(RocksTTC element) {
    root.EffectRocksTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectRocksTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectRocksTTCMutSetRemove(id, elementId);
    }
  }
  public bool Contains(RocksTTC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<RocksTTC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetRocksTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<RocksTTC>();
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
      if (!root.RocksTTCExists(element.id)) {
        violations.Add("Null constraint violated! RocksTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.RocksTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
