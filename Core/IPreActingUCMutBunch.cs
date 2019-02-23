using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class IPreActingUCMutBunch {
  public readonly Root root;
  public readonly int id;
  public IPreActingUCMutBunch(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public IPreActingUCMutBunchIncarnation incarnation { get { return root.GetIPreActingUCMutBunchIncarnation(id); } }
  public void AddObserver(IIPreActingUCMutBunchEffectObserver observer) {
    root.AddIPreActingUCMutBunchObserver(id, observer);
  }
  public void RemoveObserver(IIPreActingUCMutBunchEffectObserver observer) {
    root.RemoveIPreActingUCMutBunchObserver(id, observer);
  }
  public void Delete() {
    root.EffectIPreActingUCMutBunchDelete(id);
  }
  public static IPreActingUCMutBunch Null = new IPreActingUCMutBunch(null, 0);
  public bool Exists() { return root != null && root.IPreActingUCMutBunchExists(id); }
  public bool NullableIs(IPreActingUCMutBunch that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
    return this.Is(that);
  }
  public bool Is(IPreActingUCMutBunch that) {
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
  public AttackAICapabilityUCMutSet membersAttackAICapabilityUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersAttackAICapabilityUCMutSet of null!");
      }
      return new AttackAICapabilityUCMutSet(root, incarnation.membersAttackAICapabilityUCMutSet);
    }
                       }

  public static IPreActingUCMutBunch New(Root root) {
    return root.EffectIPreActingUCMutBunchCreate(
      root.EffectShieldingUCMutSetCreate()
,
      root.EffectAttackAICapabilityUCMutSetCreate()
        );
  }
  public void Add(IPreActingUC elementI) {

    // Can optimize, check the type of element directly somehow
    if (root.ShieldingUCExists(elementI.id)) {
      this.membersShieldingUCMutSet.Add(root.GetShieldingUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.AttackAICapabilityUCExists(elementI.id)) {
      this.membersAttackAICapabilityUCMutSet.Add(root.GetAttackAICapabilityUC(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Remove(IPreActingUC elementI) {

    // Can optimize, check the type of element directly somehow
    if (root.ShieldingUCExists(elementI.id)) {
      this.membersShieldingUCMutSet.Remove(root.GetShieldingUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.AttackAICapabilityUCExists(elementI.id)) {
      this.membersAttackAICapabilityUCMutSet.Remove(root.GetAttackAICapabilityUC(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Clear() {
    this.membersShieldingUCMutSet.Clear();
    this.membersAttackAICapabilityUCMutSet.Clear();
  }
  public int Count {
    get {
      return
        this.membersShieldingUCMutSet.Count +
        this.membersAttackAICapabilityUCMutSet.Count
        ;
    }
  }
  public IPreActingUC GetArbitrary() {
    foreach (var element in this) {
      return element;
    }
    throw new Exception("Can't get element from empty bunch!");
  }

  public IEnumerator<IPreActingUC> GetEnumerator() {
    foreach (var element in this.membersShieldingUCMutSet) {
      yield return new ShieldingUCAsIPreActingUC(element);
    }
    foreach (var element in this.membersAttackAICapabilityUCMutSet) {
      yield return new AttackAICapabilityUCAsIPreActingUC(element);
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
    public List<AttackAICapabilityUC> GetAllAttackAICapabilityUC() {
      var result = new List<AttackAICapabilityUC>();
      foreach (var thing in this.membersAttackAICapabilityUCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<AttackAICapabilityUC> ClearAllAttackAICapabilityUC() {
      var result = new List<AttackAICapabilityUC>();
      this.membersAttackAICapabilityUCMutSet.Clear();
      return result;
    }
    public AttackAICapabilityUC GetOnlyAttackAICapabilityUCOrNull() {
      var result = GetAllAttackAICapabilityUC();
      if (result.Count > 0) {
        return result[0];
      } else {
        return AttackAICapabilityUC.Null;
      }
    }
}
}
