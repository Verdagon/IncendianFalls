using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class IPreActingUCWeakMutBunch {
  public readonly Root root;
  public readonly int id;
  public IPreActingUCWeakMutBunch(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public IPreActingUCWeakMutBunchIncarnation incarnation { get { return root.GetIPreActingUCWeakMutBunchIncarnation(id); } }
  public void AddObserver(IIPreActingUCWeakMutBunchEffectObserver observer) {
    root.AddIPreActingUCWeakMutBunchObserver(id, observer);
  }
  public void RemoveObserver(IIPreActingUCWeakMutBunchEffectObserver observer) {
    root.RemoveIPreActingUCWeakMutBunchObserver(id, observer);
  }
  public void Delete() {
    root.EffectIPreActingUCWeakMutBunchDelete(id);
  }
  public static IPreActingUCWeakMutBunch Null = new IPreActingUCWeakMutBunch(null, 0);
  public bool Exists() { return root != null && root.IPreActingUCWeakMutBunchExists(id); }
  public bool NullableIs(IPreActingUCWeakMutBunch that) {
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
      violations.Add("Null constraint violated! IPreActingUCWeakMutBunch#" + id + ".membersShieldingUCWeakMutSet");
    }

    if (!root.CounteringUCWeakMutSetExists(membersCounteringUCWeakMutSet.id)) {
      violations.Add("Null constraint violated! IPreActingUCWeakMutBunch#" + id + ".membersCounteringUCWeakMutSet");
    }

    if (!root.AttackAICapabilityUCWeakMutSetExists(membersAttackAICapabilityUCWeakMutSet.id)) {
      violations.Add("Null constraint violated! IPreActingUCWeakMutBunch#" + id + ".membersAttackAICapabilityUCWeakMutSet");
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
    if (root.CounteringUCWeakMutSetExists(membersCounteringUCWeakMutSet.id)) {
      membersCounteringUCWeakMutSet.FindReachableObjects(foundIds);
    }
    if (root.AttackAICapabilityUCWeakMutSetExists(membersAttackAICapabilityUCWeakMutSet.id)) {
      membersAttackAICapabilityUCWeakMutSet.FindReachableObjects(foundIds);
    }
  }
  public bool Is(IPreActingUCWeakMutBunch that) {
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
  public CounteringUCWeakMutSet membersCounteringUCWeakMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersCounteringUCWeakMutSet of null!");
      }
      return new CounteringUCWeakMutSet(root, incarnation.membersCounteringUCWeakMutSet);
    }
                       }
  public AttackAICapabilityUCWeakMutSet membersAttackAICapabilityUCWeakMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersAttackAICapabilityUCWeakMutSet of null!");
      }
      return new AttackAICapabilityUCWeakMutSet(root, incarnation.membersAttackAICapabilityUCWeakMutSet);
    }
                       }

  public static IPreActingUCWeakMutBunch New(Root root) {
    return root.EffectIPreActingUCWeakMutBunchCreate(
      root.EffectShieldingUCWeakMutSetCreate()
,
      root.EffectCounteringUCWeakMutSetCreate()
,
      root.EffectAttackAICapabilityUCWeakMutSetCreate()
        );
  }
  public void Add(IPreActingUC elementI) {

    // Can optimize, check the type of element directly somehow
    if (root.ShieldingUCExists(elementI.id)) {
      this.membersShieldingUCWeakMutSet.Add(root.GetShieldingUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.CounteringUCExists(elementI.id)) {
      this.membersCounteringUCWeakMutSet.Add(root.GetCounteringUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.AttackAICapabilityUCExists(elementI.id)) {
      this.membersAttackAICapabilityUCWeakMutSet.Add(root.GetAttackAICapabilityUC(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Remove(IPreActingUC elementI) {

    // Can optimize, check the type of element directly somehow
    if (root.ShieldingUCExists(elementI.id)) {
      this.membersShieldingUCWeakMutSet.Remove(root.GetShieldingUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.CounteringUCExists(elementI.id)) {
      this.membersCounteringUCWeakMutSet.Remove(root.GetCounteringUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.AttackAICapabilityUCExists(elementI.id)) {
      this.membersAttackAICapabilityUCWeakMutSet.Remove(root.GetAttackAICapabilityUC(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Clear() {
    this.membersShieldingUCWeakMutSet.Clear();
    this.membersCounteringUCWeakMutSet.Clear();
    this.membersAttackAICapabilityUCWeakMutSet.Clear();
  }
  public int Count {
    get {
      return
        this.membersShieldingUCWeakMutSet.Count +
        this.membersCounteringUCWeakMutSet.Count +
        this.membersAttackAICapabilityUCWeakMutSet.Count
        ;
    }
  }
  public IPreActingUC GetArbitrary() {
    foreach (var element in this) {
      return element;
    }
    throw new Exception("Can't get element from empty bunch!");
  }

  public void Destruct() {
    var tempMembersShieldingUCWeakMutSet = this.membersShieldingUCWeakMutSet;
    var tempMembersCounteringUCWeakMutSet = this.membersCounteringUCWeakMutSet;
    var tempMembersAttackAICapabilityUCWeakMutSet = this.membersAttackAICapabilityUCWeakMutSet;

    this.Delete();
    tempMembersShieldingUCWeakMutSet.Destruct();
    tempMembersCounteringUCWeakMutSet.Destruct();
    tempMembersAttackAICapabilityUCWeakMutSet.Destruct();
  }
  public IEnumerator<IPreActingUC> GetEnumerator() {
    foreach (var element in this.membersShieldingUCWeakMutSet) {
      yield return new ShieldingUCAsIPreActingUC(element);
    }
    foreach (var element in this.membersCounteringUCWeakMutSet) {
      yield return new CounteringUCAsIPreActingUC(element);
    }
    foreach (var element in this.membersAttackAICapabilityUCWeakMutSet) {
      yield return new AttackAICapabilityUCAsIPreActingUC(element);
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
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return ShieldingUC.Null;
      }
    }
    public List<CounteringUC> GetAllCounteringUC() {
      var result = new List<CounteringUC>();
      foreach (var thing in this.membersCounteringUCWeakMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<CounteringUC> ClearAllCounteringUC() {
      var result = new List<CounteringUC>();
      this.membersCounteringUCWeakMutSet.Clear();
      return result;
    }
    public CounteringUC GetOnlyCounteringUCOrNull() {
      var result = GetAllCounteringUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return CounteringUC.Null;
      }
    }
    public List<AttackAICapabilityUC> GetAllAttackAICapabilityUC() {
      var result = new List<AttackAICapabilityUC>();
      foreach (var thing in this.membersAttackAICapabilityUCWeakMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<AttackAICapabilityUC> ClearAllAttackAICapabilityUC() {
      var result = new List<AttackAICapabilityUC>();
      this.membersAttackAICapabilityUCWeakMutSet.Clear();
      return result;
    }
    public AttackAICapabilityUC GetOnlyAttackAICapabilityUCOrNull() {
      var result = GetAllAttackAICapabilityUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return AttackAICapabilityUC.Null;
      }
    }
}
}
