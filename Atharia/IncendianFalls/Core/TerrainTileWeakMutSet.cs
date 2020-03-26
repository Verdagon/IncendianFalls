using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TerrainTileWeakMutSet {
  public readonly Root root;
  public readonly int id;
  public TerrainTileWeakMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public TerrainTileWeakMutSetIncarnation incarnation {
    get { return root.GetTerrainTileWeakMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, ITerrainTileWeakMutSetEffectObserver observer) {
    broadcaster.AddTerrainTileWeakMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ITerrainTileWeakMutSetEffectObserver observer) {
    broadcaster.RemoveTerrainTileWeakMutSetObserver(id, observer);
  }
  public void Add(TerrainTile element) {
      root.EffectTerrainTileWeakMutSetAdd(id, element.id);
  }
  public void Remove(TerrainTile element) {
      root.EffectTerrainTileWeakMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectTerrainTileWeakMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectTerrainTileWeakMutSetRemove(id, element);
    }
  }
  public bool Contains(TerrainTile element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<TerrainTile> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetTerrainTile(element);
    }
  }
  public void Destruct() {
    var elements = new List<TerrainTile>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.TerrainTileExists(element.id)) {
        violations.Add("Null constraint violated! TerrainTileWeakMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.TerrainTileExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
