using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class IGameEventMutList : IEnumerable<IGameEvent> {
  public readonly Root root;
  public readonly int id;

  public IGameEventMutList(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public IGameEventMutListIncarnation incarnation {
    get { return root.GetIGameEventMutListIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IIGameEventMutListEffectObserver observer) {
    broadcaster.AddIGameEventMutListObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IIGameEventMutListEffectObserver observer) {
    broadcaster.RemoveIGameEventMutListObserver(id, observer);
  }
  public void Delete() {
    root.EffectIGameEventMutListDelete(id);
  }
  public static IGameEventMutList Null = new IGameEventMutList(null, 0);
  public bool Exists() { return root != null && root.IGameEventMutListExists(id); }
  public bool NullableIs(IGameEventMutList that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public bool Is(IGameEventMutList that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
  public void Add(IGameEvent element) {
    root.EffectIGameEventMutListAdd(id, Count, element);
  }
  public void RemoveAt(int index) {
    root.EffectIGameEventMutListRemoveAt(id, index);
  }
  public void AddRange(IEnumerable<IGameEvent> range) {
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

  public IGameEvent this[int index] {
    get { return incarnation.list[index]; }
  }
  public void Destruct() {
    var elements = new List<IGameEvent>();
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
  public IEnumerator<IGameEvent> GetEnumerator() {
    foreach (var element in incarnation.list) {
      yield return element;
    }
  }
  System.Collections.IEnumerator IEnumerable.GetEnumerator() {
    return this.GetEnumerator();
  }
}
         
}
