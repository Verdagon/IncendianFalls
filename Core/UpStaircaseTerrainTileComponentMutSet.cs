using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UpStaircaseTerrainTileComponentMutSet {
  public readonly Root root;
  public readonly int id;
  public UpStaircaseTerrainTileComponentMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public UpStaircaseTerrainTileComponentMutSetIncarnation incarnation {
    get { return root.GetUpStaircaseTerrainTileComponentMutSetIncarnation(id); }
  }
  public void AddObserver(IUpStaircaseTerrainTileComponentMutSetEffectObserver observer) {
    root.AddUpStaircaseTerrainTileComponentMutSetObserver(id, observer);
  }
  public void RemoveObserver(IUpStaircaseTerrainTileComponentMutSetEffectObserver observer) {
    root.RemoveUpStaircaseTerrainTileComponentMutSetObserver(id, observer);
  }
  public void Add(UpStaircaseTerrainTileComponent element) {
    root.EffectUpStaircaseTerrainTileComponentMutSetAdd(id, element.id);
  }
  public void Remove(UpStaircaseTerrainTileComponent element) {
    root.EffectUpStaircaseTerrainTileComponentMutSetRemove(id, element.id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectUpStaircaseTerrainTileComponentMutSetRemove(id, elementId);
    }
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<UpStaircaseTerrainTileComponent> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetUpStaircaseTerrainTileComponent(element);
    }
  }
  public void Destruct() {
    foreach (var element in this) {
      element.Destruct();
    }
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.UpStaircaseTerrainTileComponentExists(element.id)) {
        violations.Add("Null constraint violated! UpStaircaseTerrainTileComponentMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.UpStaircaseTerrainTileComponentExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
