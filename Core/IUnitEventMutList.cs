using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class IUnitEventMutList : IEnumerable<IUnitEvent> {
  public readonly Root root;
  public readonly int id;

  public IUnitEventMutList(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public IUnitEventMutListIncarnation incarnation {
    get { return root.GetIUnitEventMutListIncarnation(id); }
  }
  public void AddObserver(IIUnitEventMutListEffectObserver observer) {
    root.AddIUnitEventMutListObserver(id, observer);
  }
  public void RemoveObserver(IIUnitEventMutListEffectObserver observer) {
    root.RemoveIUnitEventMutListObserver(id, observer);
  }
  public void Delete() {
    root.EffectIUnitEventMutListDelete(id);
  }
  public static IUnitEventMutList Null = new IUnitEventMutList(null, 0);
  public bool Exists() { return root != null && root.IUnitEventMutListExists(id); }
  public bool NullableIs(IUnitEventMutList that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public bool Is(IUnitEventMutList that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
  public void Add(IUnitEvent element) {
    root.EffectIUnitEventMutListAdd(id, element);
  }
  public void RemoveAt(int index) {
    root.EffectIUnitEventMutListRemoveAt(id, index);
  }
  public void AddRange(IEnumerable<IUnitEvent> range) {
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

  public IUnitEvent this[int index] {
    get { return incarnation.list[index]; }
  }
  public void Destruct() {
    var elements = new List<IUnitEvent>();
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
  public IEnumerator<IUnitEvent> GetEnumerator() {
    foreach (var element in incarnation.list) {
      yield return element;
    }
  }
  System.Collections.IEnumerator IEnumerable.GetEnumerator() {
    return this.GetEnumerator();
  }
}
         
}
