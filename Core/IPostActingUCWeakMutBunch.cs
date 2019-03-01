using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class IPostActingUCWeakMutBunch {
  public readonly Root root;
  public readonly int id;
  public IPostActingUCWeakMutBunch(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public IPostActingUCWeakMutBunchIncarnation incarnation { get { return root.GetIPostActingUCWeakMutBunchIncarnation(id); } }
  public void AddObserver(IIPostActingUCWeakMutBunchEffectObserver observer) {
    root.AddIPostActingUCWeakMutBunchObserver(id, observer);
  }
  public void RemoveObserver(IIPostActingUCWeakMutBunchEffectObserver observer) {
    root.RemoveIPostActingUCWeakMutBunchObserver(id, observer);
  }
  public void Delete() {
    root.EffectIPostActingUCWeakMutBunchDelete(id);
  }
  public static IPostActingUCWeakMutBunch Null = new IPostActingUCWeakMutBunch(null, 0);
  public bool Exists() { return root != null && root.IPostActingUCWeakMutBunchExists(id); }
  public bool NullableIs(IPostActingUCWeakMutBunch that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public void CheckForNullViolations(List<string> violations) {
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
  }
  public bool Is(IPostActingUCWeakMutBunch that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
       
  public static IPostActingUCWeakMutBunch New(Root root) {
    return root.EffectIPostActingUCWeakMutBunchCreate(
        );
  }
  public void Add(IPostActingUC elementI) {
    throw new Exception("Unknown type " + elementI);
  }
  public void Remove(IPostActingUC elementI) {
    throw new Exception("Unknown type " + elementI);
  }
  public void Clear() {
  }
  public int Count {
    get {
      return
0
        ;
    }
  }
  public IPostActingUC GetArbitrary() {
    foreach (var element in this) {
      return element;
    }
    throw new Exception("Can't get element from empty bunch!");
  }

  public void Destruct() {

    this.Delete();
  }
  public IEnumerator<IPostActingUC> GetEnumerator() {
    // Do nothing.
    foreach (var element in new IPostActingUC[0]) {
      yield return element;
    }
  }
}
}
