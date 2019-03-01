using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class IItemMutBunch {
  public readonly Root root;
  public readonly int id;
  public IItemMutBunch(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public IItemMutBunchIncarnation incarnation { get { return root.GetIItemMutBunchIncarnation(id); } }
  public void AddObserver(IIItemMutBunchEffectObserver observer) {
    root.AddIItemMutBunchObserver(id, observer);
  }
  public void RemoveObserver(IIItemMutBunchEffectObserver observer) {
    root.RemoveIItemMutBunchObserver(id, observer);
  }
  public void Delete() {
    root.EffectIItemMutBunchDelete(id);
  }
  public static IItemMutBunch Null = new IItemMutBunch(null, 0);
  public bool Exists() { return root != null && root.IItemMutBunchExists(id); }
  public bool NullableIs(IItemMutBunch that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public void CheckForNullViolations(List<string> violations) {

    if (!root.GlaiveMutSetExists(membersGlaiveMutSet.id)) {
      violations.Add("Null constraint violated! IItemMutBunch#" + id + ".membersGlaiveMutSet");
    }

    if (!root.ArmorMutSetExists(membersArmorMutSet.id)) {
      violations.Add("Null constraint violated! IItemMutBunch#" + id + ".membersArmorMutSet");
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    if (root.GlaiveMutSetExists(membersGlaiveMutSet.id)) {
      membersGlaiveMutSet.FindReachableObjects(foundIds);
    }
    if (root.ArmorMutSetExists(membersArmorMutSet.id)) {
      membersArmorMutSet.FindReachableObjects(foundIds);
    }
  }
  public bool Is(IItemMutBunch that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public GlaiveMutSet membersGlaiveMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersGlaiveMutSet of null!");
      }
      return new GlaiveMutSet(root, incarnation.membersGlaiveMutSet);
    }
                       }
  public ArmorMutSet membersArmorMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersArmorMutSet of null!");
      }
      return new ArmorMutSet(root, incarnation.membersArmorMutSet);
    }
                       }

  public static IItemMutBunch New(Root root) {
    return root.EffectIItemMutBunchCreate(
      root.EffectGlaiveMutSetCreate()
,
      root.EffectArmorMutSetCreate()
        );
  }
  public void Add(IItem elementI) {

    // Can optimize, check the type of element directly somehow
    if (root.GlaiveExists(elementI.id)) {
      this.membersGlaiveMutSet.Add(root.GetGlaive(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.ArmorExists(elementI.id)) {
      this.membersArmorMutSet.Add(root.GetArmor(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Remove(IItem elementI) {

    // Can optimize, check the type of element directly somehow
    if (root.GlaiveExists(elementI.id)) {
      this.membersGlaiveMutSet.Remove(root.GetGlaive(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.ArmorExists(elementI.id)) {
      this.membersArmorMutSet.Remove(root.GetArmor(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Clear() {
    this.membersGlaiveMutSet.Clear();
    this.membersArmorMutSet.Clear();
  }
  public int Count {
    get {
      return
        this.membersGlaiveMutSet.Count +
        this.membersArmorMutSet.Count
        ;
    }
  }
  public IItem GetArbitrary() {
    foreach (var element in this) {
      return element;
    }
    throw new Exception("Can't get element from empty bunch!");
  }

  public void Destruct() {
    var tempMembersGlaiveMutSet = this.membersGlaiveMutSet;
    var tempMembersArmorMutSet = this.membersArmorMutSet;

    this.Delete();
    tempMembersGlaiveMutSet.Destruct();
    tempMembersArmorMutSet.Destruct();
  }
  public IEnumerator<IItem> GetEnumerator() {
    foreach (var element in this.membersGlaiveMutSet) {
      yield return new GlaiveAsIItem(element);
    }
    foreach (var element in this.membersArmorMutSet) {
      yield return new ArmorAsIItem(element);
    }
  }
    public List<Glaive> GetAllGlaive() {
      var result = new List<Glaive>();
      foreach (var thing in this.membersGlaiveMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<Glaive> ClearAllGlaive() {
      var result = new List<Glaive>();
      this.membersGlaiveMutSet.Clear();
      return result;
    }
    public Glaive GetOnlyGlaiveOrNull() {
      var result = GetAllGlaive();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return Glaive.Null;
      }
    }
    public List<Armor> GetAllArmor() {
      var result = new List<Armor>();
      foreach (var thing in this.membersArmorMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<Armor> ClearAllArmor() {
      var result = new List<Armor>();
      this.membersArmorMutSet.Clear();
      return result;
    }
    public Armor GetOnlyArmorOrNull() {
      var result = GetAllArmor();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return Armor.Null;
      }
    }
}
}
