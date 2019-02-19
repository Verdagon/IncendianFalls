using System;
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

  public void AddObserver(ITerrainTileByLocationMutMapEffectObserver observer) {
    root.AddTerrainTileByLocationMutMapObserver(id, observer);
  }
  public void RemoveObserver(ITerrainTileByLocationMutMapEffectObserver observer) {
    root.RemoveTerrainTileByLocationMutMapObserver(id, observer);
  }

  public void Add(Location key, TerrainTile value) {
      root.EffectTerrainTileByLocationMutMapAdd(id, key, value.id);
  }

  public void Remove(Location key) {
    root.EffectTerrainTileByLocationMutMapRemove(id, key);
  }

  public int Count { get { return incarnation.map.Count; } }

  public int GetDeterministicHashCode() {
    return incarnation.GetDeterministicHashCode();
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
