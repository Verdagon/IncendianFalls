using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CaveWallTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public CaveWallTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public CaveWallTTCMutSetIncarnation incarnation {
    get { return root.GetCaveWallTTCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, ICaveWallTTCMutSetEffectObserver observer) {
    broadcaster.AddCaveWallTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ICaveWallTTCMutSetEffectObserver observer) {
    broadcaster.RemoveCaveWallTTCMutSetObserver(id, observer);
  }
  public void Add(CaveWallTTC element) {
      root.EffectCaveWallTTCMutSetAdd(id, element.id);
  }
  public void Remove(CaveWallTTC element) {
      root.EffectCaveWallTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectCaveWallTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectCaveWallTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(CaveWallTTC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<CaveWallTTC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetCaveWallTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<CaveWallTTC>();
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
      if (!root.CaveWallTTCExists(element.id)) {
        violations.Add("Null constraint violated! CaveWallTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.CaveWallTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
