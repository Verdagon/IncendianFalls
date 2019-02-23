using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DecorativeTerrainTileComponentMutSet {
  public readonly Root root;
  public readonly int id;
  public DecorativeTerrainTileComponentMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public DecorativeTerrainTileComponentMutSetIncarnation incarnation {
    get { return root.GetDecorativeTerrainTileComponentMutSetIncarnation(id); }
  }
  public void AddObserver(IDecorativeTerrainTileComponentMutSetEffectObserver observer) {
    root.AddDecorativeTerrainTileComponentMutSetObserver(id, observer);
  }
  public void RemoveObserver(IDecorativeTerrainTileComponentMutSetEffectObserver observer) {
    root.RemoveDecorativeTerrainTileComponentMutSetObserver(id, observer);
  }
  public void Add(DecorativeTerrainTileComponent element) {
    root.EffectDecorativeTerrainTileComponentMutSetAdd(id, element.id);
  }
  public void Remove(DecorativeTerrainTileComponent element) {
    root.EffectDecorativeTerrainTileComponentMutSetRemove(id, element.id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectDecorativeTerrainTileComponentMutSetRemove(id, elementId);
    }
  }
  public int Count { get { return incarnation.set.Count; } }
  //public int GetDeterministicHashCode() {
  //  return incarnation.GetDeterministicHashCode();
  //}
  public IEnumerator<DecorativeTerrainTileComponent> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetDecorativeTerrainTileComponent(element);
    }
  }
}
         
}
