using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class IPostActingUCMutBunch {
  public readonly Root root;
  public readonly int id;
  public IPostActingUCMutBunch(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public IPostActingUCMutBunchIncarnation incarnation { get { return root.GetIPostActingUCMutBunchIncarnation(id); } }
  public void AddObserver(IIPostActingUCMutBunchEffectObserver observer) {
    root.AddIPostActingUCMutBunchObserver(id, observer);
  }
  public void RemoveObserver(IIPostActingUCMutBunchEffectObserver observer) {
    root.RemoveIPostActingUCMutBunchObserver(id, observer);
  }
  public void Delete() {
    root.EffectIPostActingUCMutBunchDelete(id);
  }
  public static IPostActingUCMutBunch Null = new IPostActingUCMutBunch(null, 0);
  public bool Exists() { return root != null && root.IPostActingUCMutBunchExists(id); }
  public bool NullableIs(IPostActingUCMutBunch that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
    return this.Is(that);
  }
  public bool Is(IPostActingUCMutBunch that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public ShieldingUCMutSet membersShieldingUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersShieldingUCMutSet of null!");
      }
      return new ShieldingUCMutSet(root, incarnation.membersShieldingUCMutSet);
    }
                       }

  public static IPostActingUCMutBunch New(Root root) {
    return root.EffectIPostActingUCMutBunchCreate(
      root.EffectShieldingUCMutSetCreate()
        );
  }
  public void Add(IPostActingUC elementI) {

    if (root.ShieldingUCExists(elementI.id)) {
      this.membersShieldingUCMutSet.Add(root.GetShieldingUC(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Remove(IPostActingUC elementI) {

    if (root.ShieldingUCExists(elementI.id)) {
      this.membersShieldingUCMutSet.Remove(root.GetShieldingUC(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Clear() {
    this.membersShieldingUCMutSet.Clear();
  }
  public int Count {
    get {
      return
        this.membersShieldingUCMutSet.Count
        ;
    }
  }
  public IPostActingUC GetArbitrary() {
    foreach (var element in this) {
      return element;
    }
    throw new Exception("Can't get element from empty bunch!");
  }

  public IEnumerator<IPostActingUC> GetEnumerator() {
    foreach (var element in this.membersShieldingUCMutSet) {
      yield return new ShieldingUCAsIPostActingUC(element);
    }
  }
    public List<ShieldingUC> GetAllShieldingUC() {
      var result = new List<ShieldingUC>();
      foreach (var thing in this.membersShieldingUCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<ShieldingUC> ClearAllShieldingUC() {
      var result = new List<ShieldingUC>();
      this.membersShieldingUCMutSet.Clear();
      return result;
    }
    public ShieldingUC GetOnlyShieldingUCOrNull() {
      var result = GetAllShieldingUC();
      if (result.Count > 0) {
        return result[0];
      } else {
        return ShieldingUC.Null;
      }
    }
}
}
