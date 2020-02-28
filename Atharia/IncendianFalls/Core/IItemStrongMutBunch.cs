using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class IItemStrongMutBunch {
  public readonly Root root;
  public readonly int id;
  public IItemStrongMutBunch(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public IItemStrongMutBunchIncarnation incarnation { get { return root.GetIItemStrongMutBunchIncarnation(id); } }
  public void AddObserver(IIItemStrongMutBunchEffectObserver observer) {
    root.AddIItemStrongMutBunchObserver(id, observer);
  }
  public void RemoveObserver(IIItemStrongMutBunchEffectObserver observer) {
    root.RemoveIItemStrongMutBunchObserver(id, observer);
  }
  public void Delete() {
    root.EffectIItemStrongMutBunchDelete(id);
  }
  public static IItemStrongMutBunch Null = new IItemStrongMutBunch(null, 0);
  public bool Exists() { return root != null && root.IItemStrongMutBunchExists(id); }
  public bool NullableIs(IItemStrongMutBunch that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public void CheckForNullViolations(List<string> violations) {

    if (!root.ArmorStrongMutSetExists(membersArmorStrongMutSet.id)) {
      violations.Add("Null constraint violated! IItemStrongMutBunch#" + id + ".membersArmorStrongMutSet");
    }

    if (!root.InertiaRingStrongMutSetExists(membersInertiaRingStrongMutSet.id)) {
      violations.Add("Null constraint violated! IItemStrongMutBunch#" + id + ".membersInertiaRingStrongMutSet");
    }

    if (!root.GlaiveStrongMutSetExists(membersGlaiveStrongMutSet.id)) {
      violations.Add("Null constraint violated! IItemStrongMutBunch#" + id + ".membersGlaiveStrongMutSet");
    }

    if (!root.ManaPotionStrongMutSetExists(membersManaPotionStrongMutSet.id)) {
      violations.Add("Null constraint violated! IItemStrongMutBunch#" + id + ".membersManaPotionStrongMutSet");
    }

    if (!root.HealthPotionStrongMutSetExists(membersHealthPotionStrongMutSet.id)) {
      violations.Add("Null constraint violated! IItemStrongMutBunch#" + id + ".membersHealthPotionStrongMutSet");
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    if (root.ArmorStrongMutSetExists(membersArmorStrongMutSet.id)) {
      membersArmorStrongMutSet.FindReachableObjects(foundIds);
    }
    if (root.InertiaRingStrongMutSetExists(membersInertiaRingStrongMutSet.id)) {
      membersInertiaRingStrongMutSet.FindReachableObjects(foundIds);
    }
    if (root.GlaiveStrongMutSetExists(membersGlaiveStrongMutSet.id)) {
      membersGlaiveStrongMutSet.FindReachableObjects(foundIds);
    }
    if (root.ManaPotionStrongMutSetExists(membersManaPotionStrongMutSet.id)) {
      membersManaPotionStrongMutSet.FindReachableObjects(foundIds);
    }
    if (root.HealthPotionStrongMutSetExists(membersHealthPotionStrongMutSet.id)) {
      membersHealthPotionStrongMutSet.FindReachableObjects(foundIds);
    }
  }
  public bool Is(IItemStrongMutBunch that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public ArmorStrongMutSet membersArmorStrongMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersArmorStrongMutSet of null!");
      }
      return new ArmorStrongMutSet(root, incarnation.membersArmorStrongMutSet);
    }
                       }
  public InertiaRingStrongMutSet membersInertiaRingStrongMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersInertiaRingStrongMutSet of null!");
      }
      return new InertiaRingStrongMutSet(root, incarnation.membersInertiaRingStrongMutSet);
    }
                       }
  public GlaiveStrongMutSet membersGlaiveStrongMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersGlaiveStrongMutSet of null!");
      }
      return new GlaiveStrongMutSet(root, incarnation.membersGlaiveStrongMutSet);
    }
                       }
  public ManaPotionStrongMutSet membersManaPotionStrongMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersManaPotionStrongMutSet of null!");
      }
      return new ManaPotionStrongMutSet(root, incarnation.membersManaPotionStrongMutSet);
    }
                       }
  public HealthPotionStrongMutSet membersHealthPotionStrongMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersHealthPotionStrongMutSet of null!");
      }
      return new HealthPotionStrongMutSet(root, incarnation.membersHealthPotionStrongMutSet);
    }
                       }

  public static IItemStrongMutBunch New(Root root) {
    return root.EffectIItemStrongMutBunchCreate(
      root.EffectArmorStrongMutSetCreate()
,
      root.EffectInertiaRingStrongMutSetCreate()
,
      root.EffectGlaiveStrongMutSetCreate()
,
      root.EffectManaPotionStrongMutSetCreate()
,
      root.EffectHealthPotionStrongMutSetCreate()
        );
  }
  public void Add(IItem elementI) {

    // Can optimize, check the type of element directly somehow
    if (root.ArmorExists(elementI.id)) {
      this.membersArmorStrongMutSet.Add(root.GetArmor(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.InertiaRingExists(elementI.id)) {
      this.membersInertiaRingStrongMutSet.Add(root.GetInertiaRing(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.GlaiveExists(elementI.id)) {
      this.membersGlaiveStrongMutSet.Add(root.GetGlaive(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.ManaPotionExists(elementI.id)) {
      this.membersManaPotionStrongMutSet.Add(root.GetManaPotion(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.HealthPotionExists(elementI.id)) {
      this.membersHealthPotionStrongMutSet.Add(root.GetHealthPotion(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Remove(IItem elementI) {

    // Can optimize, check the type of element directly somehow
    if (root.ArmorExists(elementI.id)) {
      this.membersArmorStrongMutSet.Remove(root.GetArmor(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.InertiaRingExists(elementI.id)) {
      this.membersInertiaRingStrongMutSet.Remove(root.GetInertiaRing(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.GlaiveExists(elementI.id)) {
      this.membersGlaiveStrongMutSet.Remove(root.GetGlaive(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.ManaPotionExists(elementI.id)) {
      this.membersManaPotionStrongMutSet.Remove(root.GetManaPotion(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.HealthPotionExists(elementI.id)) {
      this.membersHealthPotionStrongMutSet.Remove(root.GetHealthPotion(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Clear() {
    this.membersArmorStrongMutSet.Clear();
    this.membersInertiaRingStrongMutSet.Clear();
    this.membersGlaiveStrongMutSet.Clear();
    this.membersManaPotionStrongMutSet.Clear();
    this.membersHealthPotionStrongMutSet.Clear();
  }
  public int Count {
    get {
      return
        this.membersArmorStrongMutSet.Count +
        this.membersInertiaRingStrongMutSet.Count +
        this.membersGlaiveStrongMutSet.Count +
        this.membersManaPotionStrongMutSet.Count +
        this.membersHealthPotionStrongMutSet.Count
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
    var tempMembersArmorStrongMutSet = this.membersArmorStrongMutSet;
    var tempMembersInertiaRingStrongMutSet = this.membersInertiaRingStrongMutSet;
    var tempMembersGlaiveStrongMutSet = this.membersGlaiveStrongMutSet;
    var tempMembersManaPotionStrongMutSet = this.membersManaPotionStrongMutSet;
    var tempMembersHealthPotionStrongMutSet = this.membersHealthPotionStrongMutSet;

    this.Delete();
    tempMembersArmorStrongMutSet.Destruct();
    tempMembersInertiaRingStrongMutSet.Destruct();
    tempMembersGlaiveStrongMutSet.Destruct();
    tempMembersManaPotionStrongMutSet.Destruct();
    tempMembersHealthPotionStrongMutSet.Destruct();
  }
  public IEnumerator<IItem> GetEnumerator() {
    foreach (var element in this.membersArmorStrongMutSet) {
      yield return new ArmorAsIItem(element);
    }
    foreach (var element in this.membersInertiaRingStrongMutSet) {
      yield return new InertiaRingAsIItem(element);
    }
    foreach (var element in this.membersGlaiveStrongMutSet) {
      yield return new GlaiveAsIItem(element);
    }
    foreach (var element in this.membersManaPotionStrongMutSet) {
      yield return new ManaPotionAsIItem(element);
    }
    foreach (var element in this.membersHealthPotionStrongMutSet) {
      yield return new HealthPotionAsIItem(element);
    }
  }
    public List<Armor> GetAllArmor() {
      var result = new List<Armor>();
      foreach (var thing in this.membersArmorStrongMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<Armor> ClearAllArmor() {
      var result = new List<Armor>();
      this.membersArmorStrongMutSet.Clear();
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
    public List<InertiaRing> GetAllInertiaRing() {
      var result = new List<InertiaRing>();
      foreach (var thing in this.membersInertiaRingStrongMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<InertiaRing> ClearAllInertiaRing() {
      var result = new List<InertiaRing>();
      this.membersInertiaRingStrongMutSet.Clear();
      return result;
    }
    public InertiaRing GetOnlyInertiaRingOrNull() {
      var result = GetAllInertiaRing();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return InertiaRing.Null;
      }
    }
    public List<Glaive> GetAllGlaive() {
      var result = new List<Glaive>();
      foreach (var thing in this.membersGlaiveStrongMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<Glaive> ClearAllGlaive() {
      var result = new List<Glaive>();
      this.membersGlaiveStrongMutSet.Clear();
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
    public List<ManaPotion> GetAllManaPotion() {
      var result = new List<ManaPotion>();
      foreach (var thing in this.membersManaPotionStrongMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<ManaPotion> ClearAllManaPotion() {
      var result = new List<ManaPotion>();
      this.membersManaPotionStrongMutSet.Clear();
      return result;
    }
    public ManaPotion GetOnlyManaPotionOrNull() {
      var result = GetAllManaPotion();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return ManaPotion.Null;
      }
    }
    public List<HealthPotion> GetAllHealthPotion() {
      var result = new List<HealthPotion>();
      foreach (var thing in this.membersHealthPotionStrongMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<HealthPotion> ClearAllHealthPotion() {
      var result = new List<HealthPotion>();
      this.membersHealthPotionStrongMutSet.Clear();
      return result;
    }
    public HealthPotion GetOnlyHealthPotionOrNull() {
      var result = GetAllHealthPotion();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return HealthPotion.Null;
      }
    }
    public List<IOffenseItem> GetAllIOffenseItem() {
      var result = new List<IOffenseItem>();
      foreach (var obj in this.membersGlaiveStrongMutSet) {
        result.Add(
            new GlaiveAsIOffenseItem(obj));
      }
      return result;
    }
    public List<IOffenseItem> ClearAllIOffenseItem() {
      var result = new List<IOffenseItem>();
      this.membersGlaiveStrongMutSet.Clear();
      return result;
    }
    public IOffenseItem GetOnlyIOffenseItemOrNull() {
      var result = GetAllIOffenseItem();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIOffenseItem.Null;
      }
    }
                 public List<IImmediatelyUseItem> GetAllIImmediatelyUseItem() {
      var result = new List<IImmediatelyUseItem>();
      foreach (var obj in this.membersManaPotionStrongMutSet) {
        result.Add(
            new ManaPotionAsIImmediatelyUseItem(obj));
      }
      foreach (var obj in this.membersHealthPotionStrongMutSet) {
        result.Add(
            new HealthPotionAsIImmediatelyUseItem(obj));
      }
      return result;
    }
    public List<IImmediatelyUseItem> ClearAllIImmediatelyUseItem() {
      var result = new List<IImmediatelyUseItem>();
      this.membersManaPotionStrongMutSet.Clear();
      this.membersHealthPotionStrongMutSet.Clear();
      return result;
    }
    public IImmediatelyUseItem GetOnlyIImmediatelyUseItemOrNull() {
      var result = GetAllIImmediatelyUseItem();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIImmediatelyUseItem.Null;
      }
    }
                 public List<IInertiaItem> GetAllIInertiaItem() {
      var result = new List<IInertiaItem>();
      foreach (var obj in this.membersInertiaRingStrongMutSet) {
        result.Add(
            new InertiaRingAsIInertiaItem(obj));
      }
      return result;
    }
    public List<IInertiaItem> ClearAllIInertiaItem() {
      var result = new List<IInertiaItem>();
      this.membersInertiaRingStrongMutSet.Clear();
      return result;
    }
    public IInertiaItem GetOnlyIInertiaItemOrNull() {
      var result = GetAllIInertiaItem();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIInertiaItem.Null;
      }
    }
                 public List<IDefenseItem> GetAllIDefenseItem() {
      var result = new List<IDefenseItem>();
      foreach (var obj in this.membersArmorStrongMutSet) {
        result.Add(
            new ArmorAsIDefenseItem(obj));
      }
      return result;
    }
    public List<IDefenseItem> ClearAllIDefenseItem() {
      var result = new List<IDefenseItem>();
      this.membersArmorStrongMutSet.Clear();
      return result;
    }
    public IDefenseItem GetOnlyIDefenseItemOrNull() {
      var result = GetAllIDefenseItem();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIDefenseItem.Null;
      }
    }
                 public List<IUsableItem> GetAllIUsableItem() {
      var result = new List<IUsableItem>();
      foreach (var obj in this.membersManaPotionStrongMutSet) {
        result.Add(
            new ManaPotionAsIUsableItem(obj));
      }
      foreach (var obj in this.membersHealthPotionStrongMutSet) {
        result.Add(
            new HealthPotionAsIUsableItem(obj));
      }
      return result;
    }
    public List<IUsableItem> ClearAllIUsableItem() {
      var result = new List<IUsableItem>();
      this.membersManaPotionStrongMutSet.Clear();
      this.membersHealthPotionStrongMutSet.Clear();
      return result;
    }
    public IUsableItem GetOnlyIUsableItemOrNull() {
      var result = GetAllIUsableItem();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIUsableItem.Null;
      }
    }
             }
}
