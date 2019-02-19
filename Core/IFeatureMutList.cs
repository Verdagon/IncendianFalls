using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class IFeatureMutList {// : IEnumerable<IFeature> {
  public readonly Root root;
  public readonly int id;

  public IFeatureMutList(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public IFeatureMutListIncarnation incarnation {
    get { return root.GetIFeatureMutListIncarnation(id); }
  }
  public void AddObserver(IIFeatureMutListEffectObserver observer) {
    root.AddIFeatureMutListObserver(id, observer);
  }
  public void RemoveObserver(IIFeatureMutListEffectObserver observer) {
    root.RemoveIFeatureMutListObserver(id, observer);
  }
  public void Delete() {
    root.EffectIFeatureMutListDelete(id);
  }
  public static IFeatureMutList Null = new IFeatureMutList(null, 0);
  public bool Exists() { return root != null && root.IFeatureMutListExists(id); }
  public bool NullableIs(IFeatureMutList that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
    return this.Is(that);
  }
  public bool Is(IFeatureMutList that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
  public void Add(IFeature element) {
    root.EffectIFeatureMutListAdd(id, element.id);
  }
  public void RemoveAt(int index) {
    root.EffectIFeatureMutListRemoveAt(id, index);
  }
  public void AddRange(IEnumerable<IFeature> range) {
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

  public IFeature this[int index] {
    get { return root.GetIFeature(incarnation.list[index]); }
  }
  public IEnumerator<IFeature> GetEnumerator() {
    foreach (var element in incarnation.list) {
      yield return root.GetIFeature(element);
    }
  }
}
         
}
