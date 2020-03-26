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
  public void AddObserver(EffectBroadcaster broadcaster, IIItemStrongMutBunchEffectObserver observer) {
    broadcaster.AddIItemStrongMutBunchObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IIItemStrongMutBunchEffectObserver observer) {
    broadcaster.RemoveIItemStrongMutBunchObserver(id, observer);
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

    if (!root.ManaPotionStrongMutSetExists(membersManaPotionStrongMutSet.id)) {
      violations.Add("Null constraint violated! IItemStrongMutBunch#" + id + ".membersManaPotionStrongMutSet");
    }

    if (!root.HealthPotionStrongMutSetExists(membersHealthPotionStrongMutSet.id)) {
      violations.Add("Null constraint violated! IItemStrongMutBunch#" + id + ".membersHealthPotionStrongMutSet");
    }

    if (!root.SpeedRingStrongMutSetExists(membersSpeedRingStrongMutSet.id)) {
      violations.Add("Null constraint violated! IItemStrongMutBunch#" + id + ".membersSpeedRingStrongMutSet");
    }

    if (!root.GlaiveStrongMutSetExists(membersGlaiveStrongMutSet.id)) {
      violations.Add("Null constraint violated! IItemStrongMutBunch#" + id + ".membersGlaiveStrongMutSet");
    }

    if (!root.SlowRodStrongMutSetExists(membersSlowRodStrongMutSet.id)) {
      violations.Add("Null constraint violated! IItemStrongMutBunch#" + id + ".membersSlowRodStrongMutSet");
    }

    if (!root.BlastRodStrongMutSetExists(membersBlastRodStrongMutSet.id)) {
      violations.Add("Null constraint violated! IItemStrongMutBunch#" + id + ".membersBlastRodStrongMutSet");
    }

    if (!root.ArmorStrongMutSetExists(membersArmorStrongMutSet.id)) {
      violations.Add("Null constraint violated! IItemStrongMutBunch#" + id + ".membersArmorStrongMutSet");
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    if (root.ManaPotionStrongMutSetExists(membersManaPotionStrongMutSet.id)) {
      membersManaPotionStrongMutSet.FindReachableObjects(foundIds);
    }
    if (root.HealthPotionStrongMutSetExists(membersHealthPotionStrongMutSet.id)) {
      membersHealthPotionStrongMutSet.FindReachableObjects(foundIds);
    }
    if (root.SpeedRingStrongMutSetExists(membersSpeedRingStrongMutSet.id)) {
      membersSpeedRingStrongMutSet.FindReachableObjects(foundIds);
    }
    if (root.GlaiveStrongMutSetExists(membersGlaiveStrongMutSet.id)) {
      membersGlaiveStrongMutSet.FindReachableObjects(foundIds);
    }
    if (root.SlowRodStrongMutSetExists(membersSlowRodStrongMutSet.id)) {
      membersSlowRodStrongMutSet.FindReachableObjects(foundIds);
    }
    if (root.BlastRodStrongMutSetExists(membersBlastRodStrongMutSet.id)) {
      membersBlastRodStrongMutSet.FindReachableObjects(foundIds);
    }
    if (root.ArmorStrongMutSetExists(membersArmorStrongMutSet.id)) {
      membersArmorStrongMutSet.FindReachableObjects(foundIds);
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
  public SpeedRingStrongMutSet membersSpeedRingStrongMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersSpeedRingStrongMutSet of null!");
      }
      return new SpeedRingStrongMutSet(root, incarnation.membersSpeedRingStrongMutSet);
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
  public SlowRodStrongMutSet membersSlowRodStrongMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersSlowRodStrongMutSet of null!");
      }
      return new SlowRodStrongMutSet(root, incarnation.membersSlowRodStrongMutSet);
    }
                       }
  public BlastRodStrongMutSet membersBlastRodStrongMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersBlastRodStrongMutSet of null!");
      }
      return new BlastRodStrongMutSet(root, incarnation.membersBlastRodStrongMutSet);
    }
                       }
  public ArmorStrongMutSet membersArmorStrongMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersArmorStrongMutSet of null!");
      }
      return new ArmorStrongMutSet(root, incarnation.membersArmorStrongMutSet);
    }
                       }

  public static IItemStrongMutBunch New(Root root) {
    return root.EffectIItemStrongMutBunchCreate(
      root.EffectManaPotionStrongMutSetCreate()
,
      root.EffectHealthPotionStrongMutSetCreate()
,
      root.EffectSpeedRingStrongMutSetCreate()
,
      root.EffectGlaiveStrongMutSetCreate()
,
      root.EffectSlowRodStrongMutSetCreate()
,
      root.EffectBlastRodStrongMutSetCreate()
,
      root.EffectArmorStrongMutSetCreate()
        );
  }
  public void Add(IItem elementI) {

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

    // Can optimize, check the type of element directly somehow
    if (root.SpeedRingExists(elementI.id)) {
      this.membersSpeedRingStrongMutSet.Add(root.GetSpeedRing(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.GlaiveExists(elementI.id)) {
      this.membersGlaiveStrongMutSet.Add(root.GetGlaive(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.SlowRodExists(elementI.id)) {
      this.membersSlowRodStrongMutSet.Add(root.GetSlowRod(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.BlastRodExists(elementI.id)) {
      this.membersBlastRodStrongMutSet.Add(root.GetBlastRod(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.ArmorExists(elementI.id)) {
      this.membersArmorStrongMutSet.Add(root.GetArmor(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Remove(IItem elementI) {

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

    // Can optimize, check the type of element directly somehow
    if (root.SpeedRingExists(elementI.id)) {
      this.membersSpeedRingStrongMutSet.Remove(root.GetSpeedRing(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.GlaiveExists(elementI.id)) {
      this.membersGlaiveStrongMutSet.Remove(root.GetGlaive(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.SlowRodExists(elementI.id)) {
      this.membersSlowRodStrongMutSet.Remove(root.GetSlowRod(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.BlastRodExists(elementI.id)) {
      this.membersBlastRodStrongMutSet.Remove(root.GetBlastRod(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.ArmorExists(elementI.id)) {
      this.membersArmorStrongMutSet.Remove(root.GetArmor(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Clear() {
    this.membersManaPotionStrongMutSet.Clear();
    this.membersHealthPotionStrongMutSet.Clear();
    this.membersSpeedRingStrongMutSet.Clear();
    this.membersGlaiveStrongMutSet.Clear();
    this.membersSlowRodStrongMutSet.Clear();
    this.membersBlastRodStrongMutSet.Clear();
    this.membersArmorStrongMutSet.Clear();
  }
  public int Count {
    get {
      return
        this.membersManaPotionStrongMutSet.Count +
        this.membersHealthPotionStrongMutSet.Count +
        this.membersSpeedRingStrongMutSet.Count +
        this.membersGlaiveStrongMutSet.Count +
        this.membersSlowRodStrongMutSet.Count +
        this.membersBlastRodStrongMutSet.Count +
        this.membersArmorStrongMutSet.Count
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
    var tempMembersManaPotionStrongMutSet = this.membersManaPotionStrongMutSet;
    var tempMembersHealthPotionStrongMutSet = this.membersHealthPotionStrongMutSet;
    var tempMembersSpeedRingStrongMutSet = this.membersSpeedRingStrongMutSet;
    var tempMembersGlaiveStrongMutSet = this.membersGlaiveStrongMutSet;
    var tempMembersSlowRodStrongMutSet = this.membersSlowRodStrongMutSet;
    var tempMembersBlastRodStrongMutSet = this.membersBlastRodStrongMutSet;
    var tempMembersArmorStrongMutSet = this.membersArmorStrongMutSet;

    this.Delete();
    tempMembersManaPotionStrongMutSet.Destruct();
    tempMembersHealthPotionStrongMutSet.Destruct();
    tempMembersSpeedRingStrongMutSet.Destruct();
    tempMembersGlaiveStrongMutSet.Destruct();
    tempMembersSlowRodStrongMutSet.Destruct();
    tempMembersBlastRodStrongMutSet.Destruct();
    tempMembersArmorStrongMutSet.Destruct();
  }
  public IEnumerator<IItem> GetEnumerator() {
    foreach (var element in this.membersManaPotionStrongMutSet) {
      yield return new ManaPotionAsIItem(element);
    }
    foreach (var element in this.membersHealthPotionStrongMutSet) {
      yield return new HealthPotionAsIItem(element);
    }
    foreach (var element in this.membersSpeedRingStrongMutSet) {
      yield return new SpeedRingAsIItem(element);
    }
    foreach (var element in this.membersGlaiveStrongMutSet) {
      yield return new GlaiveAsIItem(element);
    }
    foreach (var element in this.membersSlowRodStrongMutSet) {
      yield return new SlowRodAsIItem(element);
    }
    foreach (var element in this.membersBlastRodStrongMutSet) {
      yield return new BlastRodAsIItem(element);
    }
    foreach (var element in this.membersArmorStrongMutSet) {
      yield return new ArmorAsIItem(element);
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
    public List<SpeedRing> GetAllSpeedRing() {
      var result = new List<SpeedRing>();
      foreach (var thing in this.membersSpeedRingStrongMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<SpeedRing> ClearAllSpeedRing() {
      var result = new List<SpeedRing>();
      this.membersSpeedRingStrongMutSet.Clear();
      return result;
    }
    public SpeedRing GetOnlySpeedRingOrNull() {
      var result = GetAllSpeedRing();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return SpeedRing.Null;
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
    public List<SlowRod> GetAllSlowRod() {
      var result = new List<SlowRod>();
      foreach (var thing in this.membersSlowRodStrongMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<SlowRod> ClearAllSlowRod() {
      var result = new List<SlowRod>();
      this.membersSlowRodStrongMutSet.Clear();
      return result;
    }
    public SlowRod GetOnlySlowRodOrNull() {
      var result = GetAllSlowRod();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return SlowRod.Null;
      }
    }
    public List<BlastRod> GetAllBlastRod() {
      var result = new List<BlastRod>();
      foreach (var thing in this.membersBlastRodStrongMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<BlastRod> ClearAllBlastRod() {
      var result = new List<BlastRod>();
      this.membersBlastRodStrongMutSet.Clear();
      return result;
    }
    public BlastRod GetOnlyBlastRodOrNull() {
      var result = GetAllBlastRod();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return BlastRod.Null;
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
    public List<IPickUpReactorItem> GetAllIPickUpReactorItem() {
      var result = new List<IPickUpReactorItem>();
      foreach (var obj in this.membersSlowRodStrongMutSet) {
        result.Add(
            new SlowRodAsIPickUpReactorItem(obj));
      }
      foreach (var obj in this.membersBlastRodStrongMutSet) {
        result.Add(
            new BlastRodAsIPickUpReactorItem(obj));
      }
      return result;
    }
    public List<IPickUpReactorItem> ClearAllIPickUpReactorItem() {
      var result = new List<IPickUpReactorItem>();
      this.membersSlowRodStrongMutSet.Clear();
      this.membersBlastRodStrongMutSet.Clear();
      return result;
    }
    public IPickUpReactorItem GetOnlyIPickUpReactorItemOrNull() {
      var result = GetAllIPickUpReactorItem();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIPickUpReactorItem.Null;
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
