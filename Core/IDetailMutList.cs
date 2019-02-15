using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class IDetailMutList {// : IEnumerable<IDetail> {
  public readonly Root root;
  public readonly int id;

  public IDetailMutList(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public IDetailMutListIncarnation incarnation {
    get { return root.GetIDetailMutListIncarnation(id); }
  }
  public void AddObserver(IIDetailMutListEffectObserver observer) {
    root.AddIDetailMutListObserver(id, observer);
  }
  public void RemoveObserver(IIDetailMutListEffectObserver observer) {
    root.RemoveIDetailMutListObserver(id, observer);
  }
  public void Delete() {
    root.EffectIDetailMutListDelete(id);
  }
  public static IDetailMutList Null = new IDetailMutList(null, 0);
  public bool Exists() { return root != null && root.IDetailMutListExists(id); }
  public bool NullableIs(IDetailMutList that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
    return this.Is(that);
  }
  public bool Is(IDetailMutList that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
  public void Add(IDetail element) {
    root.EffectIDetailMutListAdd(id, element.id);
  }
  public void RemoveAt(int index) {
    root.EffectIDetailMutListRemoveAt(id, index);
  }
  public void AddRange(IEnumerable<IDetail> range) {
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

  public IDetail this[int index] {
    get { return root.GetIDetail(incarnation.list[index]); }
  }
  public IEnumerator<IDetail> GetEnumerator() {
    foreach (var element in incarnation.list) {
      yield return root.GetIDetail(element);
    }
  }
}
         
}
