using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class CommMutList : IEnumerable<Comm> {
  public readonly Root root;
  public readonly int id;

  public CommMutList(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public CommMutListIncarnation incarnation {
    get { return root.GetCommMutListIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, ICommMutListEffectObserver observer) {
    broadcaster.AddCommMutListObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ICommMutListEffectObserver observer) {
    broadcaster.RemoveCommMutListObserver(id, observer);
  }
  public void Delete() {
    root.EffectCommMutListDelete(id);
  }
  public static CommMutList Null = new CommMutList(null, 0);
  public bool Exists() { return root != null && root.CommMutListExists(id); }
  public bool NullableIs(CommMutList that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public bool Is(CommMutList that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
  public void Add(Comm element) {
    root.EffectCommMutListAdd(id, Count, element.id);
  }
  public void RemoveAt(int index) {
    root.EffectCommMutListRemoveAt(id, index);
  }
  public void AddRange(IEnumerable<Comm> range) {
    foreach (var element in range) {
      Add(element);
    }
  }
  public void Clear() {
    while (Count > 0) {
      RemoveAt(Count - 1);
    }
  }
  public int Count { get { return incarnation.elements.Count; } }

  public Comm this[int index] {
    get { return root.GetComm(incarnation.elements[index]); }
  }
  public void Destruct() {
    var elements = new List<Comm>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
    foreach (var element in elements) {
      element.Destruct();
    }
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.CommExists(element.id)) {
        violations.Add("Null constraint violated! CommMutList#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.CommExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
  public IEnumerator<Comm> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetComm(element);
    }
  }
  System.Collections.IEnumerator IEnumerable.GetEnumerator() {
    return this.GetEnumerator();
  }
}
         
}
