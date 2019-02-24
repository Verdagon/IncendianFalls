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

    if (!root.ShieldingUCWeakMutSetExists(membersShieldingUCWeakMutSet.id)) {
      violations.Add("Null constraint violated! IPostActingUCWeakMutBunch#" + id + ".membersShieldingUCWeakMutSet");
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    if (root.ShieldingUCWeakMutSetExists(membersShieldingUCWeakMutSet.id)) {
      membersShieldingUCWeakMutSet.FindReachableObjects(foundIds);
    }
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
         public ShieldingUCWeakMutSet membersShieldingUCWeakMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersShieldingUCWeakMutSet of null!");
      }
      return new ShieldingUCWeakMutSet(root, incarnation.membersShieldingUCWeakMutSet);
    }
                       }

  public static IPostActingUCWeakMutBunch New(Root root) {
    return root.EffectIPostActingUCWeakMutBunchCreate(
      root.EffectShieldingUCWeakMutSetCreate()
        );
  }
  public void Add(IPostActingUC elementI) {

    // Can optimize, check the type of element directly somehow
    if (root.ShieldingUCExists(elementI.id)) {
      this.membersShieldingUCWeakMutSet.Add(root.GetShieldingUC(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Remove(IPostActingUC elementI) {

    // Can optimize, check the type of element directly somehow
    if (root.ShieldingUCExists(elementI.id)) {
      this.membersShieldingUCWeakMutSet.Remove(root.GetShieldingUC(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Clear() {
    this.membersShieldingUCWeakMutSet.Clear();
  }
  public int Count {
    get {
      return
        this.membersShieldingUCWeakMutSet.Count
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
    var tempMembersShieldingUCWeakMutSet = this.membersShieldingUCWeakMutSet;

    this.Delete();
    tempMembersShieldingUCWeakMutSet.Destruct();
  }
  public IEnumerator<IPostActingUC> GetEnumerator() {
    foreach (var element in this.membersShieldingUCWeakMutSet) {
      yield return new ShieldingUCAsIPostActingUC(element);
    }
  }
    public List<ShieldingUC> GetAllShieldingUC() {
      var result = new List<ShieldingUC>();
      foreach (var thing in this.membersShieldingUCWeakMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<ShieldingUC> ClearAllShieldingUC() {
      var result = new List<ShieldingUC>();
      this.membersShieldingUCWeakMutSet.Clear();
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
