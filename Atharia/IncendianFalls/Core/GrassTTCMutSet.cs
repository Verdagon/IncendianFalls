using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class GrassTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public GrassTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public GrassTTCMutSetIncarnation incarnation {
    get { return root.GetGrassTTCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IGrassTTCMutSetEffectObserver observer) {
    broadcaster.AddGrassTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IGrassTTCMutSetEffectObserver observer) {
    broadcaster.RemoveGrassTTCMutSetObserver(id, observer);
  }
  public void Add(GrassTTC element) {
      root.EffectGrassTTCMutSetAdd(id, element.id);
  }
  public void Remove(GrassTTC element) {
      root.EffectGrassTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectGrassTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectGrassTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(GrassTTC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<GrassTTC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetGrassTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<GrassTTC>();
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
      if (!root.GrassTTCExists(element.id)) {
        violations.Add("Null constraint violated! GrassTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.GrassTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
