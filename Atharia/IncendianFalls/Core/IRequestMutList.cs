using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class IRequestMutList : IEnumerable<IRequest> {
  public readonly Root root;
  public readonly int id;

  public IRequestMutList(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public IRequestMutListIncarnation incarnation {
    get { return root.GetIRequestMutListIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IIRequestMutListEffectObserver observer) {
    broadcaster.AddIRequestMutListObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IIRequestMutListEffectObserver observer) {
    broadcaster.RemoveIRequestMutListObserver(id, observer);
  }
  public void Delete() {
    root.EffectIRequestMutListDelete(id);
  }
  public static IRequestMutList Null = new IRequestMutList(null, 0);
  public bool Exists() { return root != null && root.IRequestMutListExists(id); }
  public bool NullableIs(IRequestMutList that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public bool Is(IRequestMutList that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
  public void Add(IRequest element) {
    root.EffectIRequestMutListAdd(id, Count, element);
  }
  public void RemoveAt(int index) {
    root.EffectIRequestMutListRemoveAt(id, index);
  }
  public void AddRange(IEnumerable<IRequest> range) {
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

  public IRequest this[int index] {
    get { return incarnation.list[index]; }
  }
  public void Destruct() {
    var elements = new List<IRequest>();
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
  public IEnumerator<IRequest> GetEnumerator() {
    foreach (var element in incarnation.list) {
      yield return element;
    }
  }
  System.Collections.IEnumerator IEnumerable.GetEnumerator() {
    return this.GetEnumerator();
  }
}
         
}
