using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class TerrainTileByLocationMutMap {
  public readonly Root root;
  public readonly int id;


  public TerrainTileByLocationMutMap(Root root, int id) {
    this.root = root;
    this.id = id;
  }

  public TerrainTileByLocationMutMapIncarnation incarnation {
    get { return root.GetTerrainTileByLocationMutMapIncarnation(id); }
  }

  public bool Exists() { return root != null && root.TerrainTileByLocationMutMapExists(id); }

  public void AddObserver(EffectBroadcaster broadcaster, ITerrainTileByLocationMutMapEffectObserver observer) {
    broadcaster.AddTerrainTileByLocationMutMapObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ITerrainTileByLocationMutMapEffectObserver observer) {
    broadcaster.RemoveTerrainTileByLocationMutMapObserver(id, observer);
  }

  public void Add(Location key, TerrainTile value) {
      root.EffectTerrainTileByLocationMutMapAdd(id, key, value.id);
  }

  public void Remove(Location key) {
    root.EffectTerrainTileByLocationMutMapRemove(id, key);
  }

  public int Count { get { return incarnation.map.Count; } }

  public void CheckForNullViolations(List<string> violations) {
    foreach (var entry in this) {
      var element = entry.Value;
      if (!root.TerrainTileExists(element.id)) {
        violations.Add("Null constraint violated! TerrainTileByLocationMutMap#" + id + "." + element.id);
      }
    }
  }

  public void Delete() {
    root.EffectTerrainTileByLocationMutMapDelete(id);
  }
  public void Destruct() {
    var elements = new List<TerrainTile>();
    foreach (var entry in this) {
      elements.Add(entry.Value);
    }
    this.Delete();
    foreach (var element in elements) {
      element.Destruct();
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var entry in this) {
      var element = entry.Value;
      if (root.TerrainTileExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
  public bool ContainsKey(Location key) {
    return incarnation.map.ContainsKey(key);
  }

  public List<Location> Keys {
    // Could be optimized, I think SortedDictionary uses an inner class instead of making a List
    // like this.
    get { return new List<Location>(incarnation.map.Keys); }
  }

  public TerrainTile this[Location key] {
    get { return new TerrainTile(root, incarnation.map[key]); }
  }

  public IEnumerator<KeyValuePair<Location, TerrainTile>> GetEnumerator() {
    foreach (var keyAndValue in incarnation.map) {
      yield return new KeyValuePair<Location, TerrainTile>(
          keyAndValue.Key,
          new TerrainTile(root, keyAndValue.Value));
    }
  }
}
         
}
