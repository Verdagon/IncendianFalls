using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class LocationMutList : IEnumerable<Location> {
  public readonly Root root;
  public readonly int id;

  public LocationMutList(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public LocationMutListIncarnation incarnation {
    get { return root.GetLocationMutListIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, ILocationMutListEffectObserver observer) {
    broadcaster.AddLocationMutListObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ILocationMutListEffectObserver observer) {
    broadcaster.RemoveLocationMutListObserver(id, observer);
  }
  public void Delete() {
    root.EffectLocationMutListDelete(id);
  }
  public static LocationMutList Null = new LocationMutList(null, 0);
  public bool Exists() { return root != null && root.LocationMutListExists(id); }
  public bool NullableIs(LocationMutList that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public bool Is(LocationMutList that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
  public void Add(Location element) {
    root.EffectLocationMutListAdd(id, Count, element);
  }
  public void RemoveAt(int index) {
    root.EffectLocationMutListRemoveAt(id, index);
  }
  public void AddRange(IEnumerable<Location> range) {
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

  public Location this[int index] {
    get { return incarnation.list[index]; }
  }
  public void Destruct() {
    var elements = new List<Location>();
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
  public IEnumerator<Location> GetEnumerator() {
    foreach (var element in incarnation.list) {
      yield return element;
    }
  }
  System.Collections.IEnumerator IEnumerable.GetEnumerator() {
    return this.GetEnumerator();
  }
}
         
}
