using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class WallTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public WallTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public WallTTCMutSetIncarnation incarnation {
    get { return root.GetWallTTCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IWallTTCMutSetEffectObserver observer) {
    broadcaster.AddWallTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IWallTTCMutSetEffectObserver observer) {
    broadcaster.RemoveWallTTCMutSetObserver(id, observer);
  }
  public void Add(WallTTC element) {
      root.EffectWallTTCMutSetAdd(id, element.id);
  }
  public void Remove(WallTTC element) {
      root.EffectWallTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectWallTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectWallTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(WallTTC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<WallTTC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetWallTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<WallTTC>();
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
      if (!root.WallTTCExists(element.id)) {
        violations.Add("Null constraint violated! WallTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.WallTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
