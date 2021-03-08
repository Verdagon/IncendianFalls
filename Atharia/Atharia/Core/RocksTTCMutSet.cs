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
  public void AddObserver(EffectBroadcaster broadcaster, IRocksTTCMutSetEffectObserver observer) {
    broadcaster.AddRocksTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IRocksTTCMutSetEffectObserver observer) {
    broadcaster.RemoveRocksTTCMutSetObserver(id, observer);
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
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectRocksTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(RocksTTC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<RocksTTC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
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
