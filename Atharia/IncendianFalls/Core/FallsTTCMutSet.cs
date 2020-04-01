using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FallsTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public FallsTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public FallsTTCMutSetIncarnation incarnation {
    get { return root.GetFallsTTCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IFallsTTCMutSetEffectObserver observer) {
    broadcaster.AddFallsTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IFallsTTCMutSetEffectObserver observer) {
    broadcaster.RemoveFallsTTCMutSetObserver(id, observer);
  }
  public void Add(FallsTTC element) {
      root.EffectFallsTTCMutSetAdd(id, element.id);
  }
  public void Remove(FallsTTC element) {
      root.EffectFallsTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectFallsTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectFallsTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(FallsTTC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<FallsTTC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetFallsTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<FallsTTC>();
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
      if (!root.FallsTTCExists(element.id)) {
        violations.Add("Null constraint violated! FallsTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.FallsTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
