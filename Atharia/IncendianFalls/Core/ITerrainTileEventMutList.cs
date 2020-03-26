using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class ITerrainTileEventMutList : IEnumerable<ITerrainTileEvent> {
  public readonly Root root;
  public readonly int id;

  public ITerrainTileEventMutList(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ITerrainTileEventMutListIncarnation incarnation {
    get { return root.GetITerrainTileEventMutListIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IITerrainTileEventMutListEffectObserver observer) {
    broadcaster.AddITerrainTileEventMutListObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IITerrainTileEventMutListEffectObserver observer) {
    broadcaster.RemoveITerrainTileEventMutListObserver(id, observer);
  }
  public void Delete() {
    root.EffectITerrainTileEventMutListDelete(id);
  }
  public static ITerrainTileEventMutList Null = new ITerrainTileEventMutList(null, 0);
  public bool Exists() { return root != null && root.ITerrainTileEventMutListExists(id); }
  public bool NullableIs(ITerrainTileEventMutList that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public bool Is(ITerrainTileEventMutList that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
  public void Add(ITerrainTileEvent element) {
    root.EffectITerrainTileEventMutListAdd(id, Count, element);
  }
  public void RemoveAt(int index) {
    root.EffectITerrainTileEventMutListRemoveAt(id, index);
  }
  public void AddRange(IEnumerable<ITerrainTileEvent> range) {
    foreach (var element in range) {
      Add(element);
    }
  }
  public void Clear() {
    while (Count > 0) {
      RemoveAt(Count - 1);
    }
  }
  public int Count { get { return incarnation.list.Count; } }

  public ITerrainTileEvent this[int index] {
    get { return incarnation.list[index]; }
  }
  public void Destruct() {
    var elements = new List<ITerrainTileEvent>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
  }
  public IEnumerator<ITerrainTileEvent> GetEnumerator() {
    foreach (var element in incarnation.list) {
      yield return element;
    }
  }
  System.Collections.IEnumerator IEnumerable.GetEnumerator() {
    return this.GetEnumerator();
  }
}
         
}
