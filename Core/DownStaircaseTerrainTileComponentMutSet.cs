using System;
using System.Collections.Generic;

namespace Atharia.Model {
public class DownStaircaseTerrainTileComponentMutSet {
  public readonly Root root;
  public readonly int id;
  public DownStaircaseTerrainTileComponentMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public DownStaircaseTerrainTileComponentMutSetIncarnation incarnation {
    get { return root.GetDownStaircaseTerrainTileComponentMutSetIncarnation(id); }
  }
  public void AddObserver(IDownStaircaseTerrainTileComponentMutSetEffectObserver observer) {
    root.AddDownStaircaseTerrainTileComponentMutSetObserver(id, observer);
  }
  public void RemoveObserver(IDownStaircaseTerrainTileComponentMutSetEffectObserver observer) {
    root.RemoveDownStaircaseTerrainTileComponentMutSetObserver(id, observer);
  }
  public void Add(DownStaircaseTerrainTileComponent element) {
    root.EffectDownStaircaseTerrainTileComponentMutSetAdd(id, element.id);
  }
  public void Remove(DownStaircaseTerrainTileComponent element) {
    root.EffectDownStaircaseTerrainTileComponentMutSetRemove(id, element.id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectDownStaircaseTerrainTileComponentMutSetRemove(id, elementId);
    }
  }
  public int Count { get { return incarnation.set.Count; } }
  //public int GetDeterministicHashCode() {
  //  return incarnation.GetDeterministicHashCode();
  //}
  public IEnumerator<DownStaircaseTerrainTileComponent> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetDownStaircaseTerrainTileComponent(element);
    }
  }
}
         
}
