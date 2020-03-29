using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public class StrMutList : IEnumerable<string> {
  public readonly Root root;
  public readonly int id;

  public StrMutList(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public StrMutListIncarnation incarnation {
    get { return root.GetStrMutListIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IStrMutListEffectObserver observer) {
    broadcaster.AddStrMutListObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IStrMutListEffectObserver observer) {
    broadcaster.RemoveStrMutListObserver(id, observer);
  }
  public void Delete() {
    root.EffectStrMutListDelete(id);
  }
  public static StrMutList Null = new StrMutList(null, 0);
  public bool Exists() { return root != null && root.StrMutListExists(id); }
  public bool NullableIs(StrMutList that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public bool Is(StrMutList that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
  public void Add(string element) {
    root.EffectStrMutListAdd(id, Count, element);
  }
  public void RemoveAt(int index) {
    root.EffectStrMutListRemoveAt(id, index);
  }
  public void AddRange(IEnumerable<string> range) {
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

  public string this[int index] {
    get { return incarnation.list[index]; }
  }
  public void Destruct() {
    var elements = new List<string>();
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
  public IEnumerator<string> GetEnumerator() {
    foreach (var element in incarnation.list) {
      yield return element;
    }
  }
  System.Collections.IEnumerator IEnumerable.GetEnumerator() {
    return this.GetEnumerator();
  }
}
         
}
