using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class IUnitComponentMutBunch {
  public readonly Root root;
  public readonly int id;
  public IUnitComponentMutBunch(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public IUnitComponentMutBunchIncarnation incarnation { get { return root.GetIUnitComponentMutBunchIncarnation(id); } }
  public void AddObserver(IIUnitComponentMutBunchEffectObserver observer) {
    root.AddIUnitComponentMutBunchObserver(id, observer);
  }
  public void RemoveObserver(IIUnitComponentMutBunchEffectObserver observer) {
    root.RemoveIUnitComponentMutBunchObserver(id, observer);
  }
  public void Delete() {
    root.EffectIUnitComponentMutBunchDelete(id);
  }
  public static IUnitComponentMutBunch Null = new IUnitComponentMutBunch(null, 0);
  public bool Exists() { return root != null && root.IUnitComponentMutBunchExists(id); }
  public bool NullableIs(IUnitComponentMutBunch that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public void CheckForNullViolations(List<string> violations) {

    if (!root.ArmorMutSetExists(membersArmorMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersArmorMutSet");
    }

    if (!root.InertiaRingMutSetExists(membersInertiaRingMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersInertiaRingMutSet");
    }

    if (!root.GlaiveMutSetExists(membersGlaiveMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersGlaiveMutSet");
    }

    if (!root.ManaPotionMutSetExists(membersManaPotionMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersManaPotionMutSet");
    }

    if (!root.HealthPotionMutSetExists(membersHealthPotionMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersHealthPotionMutSet");
    }

    if (!root.WanderAICapabilityUCMutSetExists(membersWanderAICapabilityUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersWanderAICapabilityUCMutSet");
    }

    if (!root.TimeCloneAICapabilityUCMutSetExists(membersTimeCloneAICapabilityUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersTimeCloneAICapabilityUCMutSet");
    }

    if (!root.AttackAICapabilityUCMutSetExists(membersAttackAICapabilityUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersAttackAICapabilityUCMutSet");
    }

    if (!root.CounteringUCMutSetExists(membersCounteringUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersCounteringUCMutSet");
    }

    if (!root.ShieldingUCMutSetExists(membersShieldingUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersShieldingUCMutSet");
    }

    if (!root.BideAICapabilityUCMutSetExists(membersBideAICapabilityUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersBideAICapabilityUCMutSet");
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    if (root.ArmorMutSetExists(membersArmorMutSet.id)) {
      membersArmorMutSet.FindReachableObjects(foundIds);
    }
    if (root.InertiaRingMutSetExists(membersInertiaRingMutSet.id)) {
      membersInertiaRingMutSet.FindReachableObjects(foundIds);
    }
    if (root.GlaiveMutSetExists(membersGlaiveMutSet.id)) {
      membersGlaiveMutSet.FindReachableObjects(foundIds);
    }
    if (root.ManaPotionMutSetExists(membersManaPotionMutSet.id)) {
      membersManaPotionMutSet.FindReachableObjects(foundIds);
    }
    if (root.HealthPotionMutSetExists(membersHealthPotionMutSet.id)) {
      membersHealthPotionMutSet.FindReachableObjects(foundIds);
    }
    if (root.WanderAICapabilityUCMutSetExists(membersWanderAICapabilityUCMutSet.id)) {
      membersWanderAICapabilityUCMutSet.FindReachableObjects(foundIds);
    }
    if (root.TimeCloneAICapabilityUCMutSetExists(membersTimeCloneAICapabilityUCMutSet.id)) {
      membersTimeCloneAICapabilityUCMutSet.FindReachableObjects(foundIds);
    }
    if (root.AttackAICapabilityUCMutSetExists(membersAttackAICapabilityUCMutSet.id)) {
      membersAttackAICapabilityUCMutSet.FindReachableObjects(foundIds);
    }
    if (root.CounteringUCMutSetExists(membersCounteringUCMutSet.id)) {
      membersCounteringUCMutSet.FindReachableObjects(foundIds);
    }
    if (root.ShieldingUCMutSetExists(membersShieldingUCMutSet.id)) {
      membersShieldingUCMutSet.FindReachableObjects(foundIds);
    }
    if (root.BideAICapabilityUCMutSetExists(membersBideAICapabilityUCMutSet.id)) {
      membersBideAICapabilityUCMutSet.FindReachableObjects(foundIds);
    }
  }
  public bool Is(IUnitComponentMutBunch that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public ArmorMutSet membersArmorMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersArmorMutSet of null!");
      }
      return new ArmorMutSet(root, incarnation.membersArmorMutSet);
    }
                       }
  public InertiaRingMutSet membersInertiaRingMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersInertiaRingMutSet of null!");
      }
      return new InertiaRingMutSet(root, incarnation.membersInertiaRingMutSet);
    }
                       }
  public GlaiveMutSet membersGlaiveMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersGlaiveMutSet of null!");
      }
      return new GlaiveMutSet(root, incarnation.membersGlaiveMutSet);
    }
                       }
  public ManaPotionMutSet membersManaPotionMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersManaPotionMutSet of null!");
      }
      return new ManaPotionMutSet(root, incarnation.membersManaPotionMutSet);
    }
                       }
  public HealthPotionMutSet membersHealthPotionMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersHealthPotionMutSet of null!");
      }
      return new HealthPotionMutSet(root, incarnation.membersHealthPotionMutSet);
    }
                       }
  public WanderAICapabilityUCMutSet membersWanderAICapabilityUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersWanderAICapabilityUCMutSet of null!");
      }
      return new WanderAICapabilityUCMutSet(root, incarnation.membersWanderAICapabilityUCMutSet);
    }
                       }
  public TimeCloneAICapabilityUCMutSet membersTimeCloneAICapabilityUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersTimeCloneAICapabilityUCMutSet of null!");
      }
      return new TimeCloneAICapabilityUCMutSet(root, incarnation.membersTimeCloneAICapabilityUCMutSet);
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
  public CounteringUCMutSet membersCounteringUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersCounteringUCMutSet of null!");
      }
      return new CounteringUCMutSet(root, incarnation.membersCounteringUCMutSet);
    }
                       }
  public ShieldingUCMutSet membersShieldingUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersShieldingUCMutSet of null!");
      }
      return new ShieldingUCMutSet(root, incarnation.membersShieldingUCMutSet);
    }
                       }
  public BideAICapabilityUCMutSet membersBideAICapabilityUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersBideAICapabilityUCMutSet of null!");
      }
      return new BideAICapabilityUCMutSet(root, incarnation.membersBideAICapabilityUCMutSet);
    }
                       }

  public static IUnitComponentMutBunch New(Root root) {
    return root.EffectIUnitComponentMutBunchCreate(
      root.EffectArmorMutSetCreate()
,
      root.EffectInertiaRingMutSetCreate()
,
      root.EffectGlaiveMutSetCreate()
,
      root.EffectManaPotionMutSetCreate()
,
      root.EffectHealthPotionMutSetCreate()
,
      root.EffectWanderAICapabilityUCMutSetCreate()
,
      root.EffectTimeCloneAICapabilityUCMutSetCreate()
,
      root.EffectAttackAICapabilityUCMutSetCreate()
,
      root.EffectCounteringUCMutSetCreate()
,
      root.EffectShieldingUCMutSetCreate()
,
      root.EffectBideAICapabilityUCMutSetCreate()
        );
  }
  public void Add(IUnitComponent elementI) {

    // Can optimize, check the type of element directly somehow
    if (root.ArmorExists(elementI.id)) {
      this.membersArmorMutSet.Add(root.GetArmor(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.InertiaRingExists(elementI.id)) {
      this.membersInertiaRingMutSet.Add(root.GetInertiaRing(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.GlaiveExists(elementI.id)) {
      this.membersGlaiveMutSet.Add(root.GetGlaive(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.ManaPotionExists(elementI.id)) {
      this.membersManaPotionMutSet.Add(root.GetManaPotion(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.HealthPotionExists(elementI.id)) {
      this.membersHealthPotionMutSet.Add(root.GetHealthPotion(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.WanderAICapabilityUCExists(elementI.id)) {
      this.membersWanderAICapabilityUCMutSet.Add(root.GetWanderAICapabilityUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.TimeCloneAICapabilityUCExists(elementI.id)) {
      this.membersTimeCloneAICapabilityUCMutSet.Add(root.GetTimeCloneAICapabilityUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.AttackAICapabilityUCExists(elementI.id)) {
      this.membersAttackAICapabilityUCMutSet.Add(root.GetAttackAICapabilityUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.CounteringUCExists(elementI.id)) {
      this.membersCounteringUCMutSet.Add(root.GetCounteringUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.ShieldingUCExists(elementI.id)) {
      this.membersShieldingUCMutSet.Add(root.GetShieldingUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.BideAICapabilityUCExists(elementI.id)) {
      this.membersBideAICapabilityUCMutSet.Add(root.GetBideAICapabilityUC(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Remove(IUnitComponent elementI) {

    // Can optimize, check the type of element directly somehow
    if (root.ArmorExists(elementI.id)) {
      this.membersArmorMutSet.Remove(root.GetArmor(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.InertiaRingExists(elementI.id)) {
      this.membersInertiaRingMutSet.Remove(root.GetInertiaRing(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.GlaiveExists(elementI.id)) {
      this.membersGlaiveMutSet.Remove(root.GetGlaive(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.ManaPotionExists(elementI.id)) {
      this.membersManaPotionMutSet.Remove(root.GetManaPotion(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.HealthPotionExists(elementI.id)) {
      this.membersHealthPotionMutSet.Remove(root.GetHealthPotion(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.WanderAICapabilityUCExists(elementI.id)) {
      this.membersWanderAICapabilityUCMutSet.Remove(root.GetWanderAICapabilityUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.TimeCloneAICapabilityUCExists(elementI.id)) {
      this.membersTimeCloneAICapabilityUCMutSet.Remove(root.GetTimeCloneAICapabilityUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.AttackAICapabilityUCExists(elementI.id)) {
      this.membersAttackAICapabilityUCMutSet.Remove(root.GetAttackAICapabilityUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.CounteringUCExists(elementI.id)) {
      this.membersCounteringUCMutSet.Remove(root.GetCounteringUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.ShieldingUCExists(elementI.id)) {
      this.membersShieldingUCMutSet.Remove(root.GetShieldingUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.BideAICapabilityUCExists(elementI.id)) {
      this.membersBideAICapabilityUCMutSet.Remove(root.GetBideAICapabilityUC(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Clear() {
    this.membersArmorMutSet.Clear();
    this.membersInertiaRingMutSet.Clear();
    this.membersGlaiveMutSet.Clear();
    this.membersManaPotionMutSet.Clear();
    this.membersHealthPotionMutSet.Clear();
    this.membersWanderAICapabilityUCMutSet.Clear();
    this.membersTimeCloneAICapabilityUCMutSet.Clear();
    this.membersAttackAICapabilityUCMutSet.Clear();
    this.membersCounteringUCMutSet.Clear();
    this.membersShieldingUCMutSet.Clear();
    this.membersBideAICapabilityUCMutSet.Clear();
  }
  public int Count {
    get {
      return
        this.membersArmorMutSet.Count +
        this.membersInertiaRingMutSet.Count +
        this.membersGlaiveMutSet.Count +
        this.membersManaPotionMutSet.Count +
        this.membersHealthPotionMutSet.Count +
        this.membersWanderAICapabilityUCMutSet.Count +
        this.membersTimeCloneAICapabilityUCMutSet.Count +
        this.membersAttackAICapabilityUCMutSet.Count +
        this.membersCounteringUCMutSet.Count +
        this.membersShieldingUCMutSet.Count +
        this.membersBideAICapabilityUCMutSet.Count
        ;
    }
  }
  public IUnitComponent GetArbitrary() {
    foreach (var element in this) {
      return element;
    }
    throw new Exception("Can't get element from empty bunch!");
  }

  public void Destruct() {
    var tempMembersArmorMutSet = this.membersArmorMutSet;
    var tempMembersInertiaRingMutSet = this.membersInertiaRingMutSet;
    var tempMembersGlaiveMutSet = this.membersGlaiveMutSet;
    var tempMembersManaPotionMutSet = this.membersManaPotionMutSet;
    var tempMembersHealthPotionMutSet = this.membersHealthPotionMutSet;
    var tempMembersWanderAICapabilityUCMutSet = this.membersWanderAICapabilityUCMutSet;
    var tempMembersTimeCloneAICapabilityUCMutSet = this.membersTimeCloneAICapabilityUCMutSet;
    var tempMembersAttackAICapabilityUCMutSet = this.membersAttackAICapabilityUCMutSet;
    var tempMembersCounteringUCMutSet = this.membersCounteringUCMutSet;
    var tempMembersShieldingUCMutSet = this.membersShieldingUCMutSet;
    var tempMembersBideAICapabilityUCMutSet = this.membersBideAICapabilityUCMutSet;

    this.Delete();
    tempMembersArmorMutSet.Destruct();
    tempMembersInertiaRingMutSet.Destruct();
    tempMembersGlaiveMutSet.Destruct();
    tempMembersManaPotionMutSet.Destruct();
    tempMembersHealthPotionMutSet.Destruct();
    tempMembersWanderAICapabilityUCMutSet.Destruct();
    tempMembersTimeCloneAICapabilityUCMutSet.Destruct();
    tempMembersAttackAICapabilityUCMutSet.Destruct();
    tempMembersCounteringUCMutSet.Destruct();
    tempMembersShieldingUCMutSet.Destruct();
    tempMembersBideAICapabilityUCMutSet.Destruct();
  }
  public IEnumerator<IUnitComponent> GetEnumerator() {
    foreach (var element in this.membersArmorMutSet) {
      yield return new ArmorAsIUnitComponent(element);
    }
    foreach (var element in this.membersInertiaRingMutSet) {
      yield return new InertiaRingAsIUnitComponent(element);
    }
    foreach (var element in this.membersGlaiveMutSet) {
      yield return new GlaiveAsIUnitComponent(element);
    }
    foreach (var element in this.membersManaPotionMutSet) {
      yield return new ManaPotionAsIUnitComponent(element);
    }
    foreach (var element in this.membersHealthPotionMutSet) {
      yield return new HealthPotionAsIUnitComponent(element);
    }
    foreach (var element in this.membersWanderAICapabilityUCMutSet) {
      yield return new WanderAICapabilityUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersTimeCloneAICapabilityUCMutSet) {
      yield return new TimeCloneAICapabilityUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersAttackAICapabilityUCMutSet) {
      yield return new AttackAICapabilityUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersCounteringUCMutSet) {
      yield return new CounteringUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersShieldingUCMutSet) {
      yield return new ShieldingUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersBideAICapabilityUCMutSet) {
      yield return new BideAICapabilityUCAsIUnitComponent(element);
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
    public List<InertiaRing> GetAllInertiaRing() {
      var result = new List<InertiaRing>();
      foreach (var thing in this.membersInertiaRingMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<InertiaRing> ClearAllInertiaRing() {
      var result = new List<InertiaRing>();
      this.membersInertiaRingMutSet.Clear();
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
    public List<ManaPotion> GetAllManaPotion() {
      var result = new List<ManaPotion>();
      foreach (var thing in this.membersManaPotionMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<ManaPotion> ClearAllManaPotion() {
      var result = new List<ManaPotion>();
      this.membersManaPotionMutSet.Clear();
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
      foreach (var thing in this.membersHealthPotionMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<HealthPotion> ClearAllHealthPotion() {
      var result = new List<HealthPotion>();
      this.membersHealthPotionMutSet.Clear();
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
    public List<WanderAICapabilityUC> GetAllWanderAICapabilityUC() {
      var result = new List<WanderAICapabilityUC>();
      foreach (var thing in this.membersWanderAICapabilityUCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<WanderAICapabilityUC> ClearAllWanderAICapabilityUC() {
      var result = new List<WanderAICapabilityUC>();
      this.membersWanderAICapabilityUCMutSet.Clear();
      return result;
    }
    public WanderAICapabilityUC GetOnlyWanderAICapabilityUCOrNull() {
      var result = GetAllWanderAICapabilityUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return WanderAICapabilityUC.Null;
      }
    }
    public List<TimeCloneAICapabilityUC> GetAllTimeCloneAICapabilityUC() {
      var result = new List<TimeCloneAICapabilityUC>();
      foreach (var thing in this.membersTimeCloneAICapabilityUCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<TimeCloneAICapabilityUC> ClearAllTimeCloneAICapabilityUC() {
      var result = new List<TimeCloneAICapabilityUC>();
      this.membersTimeCloneAICapabilityUCMutSet.Clear();
      return result;
    }
    public TimeCloneAICapabilityUC GetOnlyTimeCloneAICapabilityUCOrNull() {
      var result = GetAllTimeCloneAICapabilityUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return TimeCloneAICapabilityUC.Null;
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
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return AttackAICapabilityUC.Null;
      }
    }
    public List<CounteringUC> GetAllCounteringUC() {
      var result = new List<CounteringUC>();
      foreach (var thing in this.membersCounteringUCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<CounteringUC> ClearAllCounteringUC() {
      var result = new List<CounteringUC>();
      this.membersCounteringUCMutSet.Clear();
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
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return ShieldingUC.Null;
      }
    }
    public List<BideAICapabilityUC> GetAllBideAICapabilityUC() {
      var result = new List<BideAICapabilityUC>();
      foreach (var thing in this.membersBideAICapabilityUCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<BideAICapabilityUC> ClearAllBideAICapabilityUC() {
      var result = new List<BideAICapabilityUC>();
      this.membersBideAICapabilityUCMutSet.Clear();
      return result;
    }
    public BideAICapabilityUC GetOnlyBideAICapabilityUCOrNull() {
      var result = GetAllBideAICapabilityUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return BideAICapabilityUC.Null;
      }
    }
    public List<IImpulsePostReactor> GetAllIImpulsePostReactor() {
      var result = new List<IImpulsePostReactor>();
      foreach (var obj in this.membersTimeCloneAICapabilityUCMutSet) {
        result.Add(
            new TimeCloneAICapabilityUCAsIImpulsePostReactor(obj));
      }
      foreach (var obj in this.membersAttackAICapabilityUCMutSet) {
        result.Add(
            new AttackAICapabilityUCAsIImpulsePostReactor(obj));
      }
      return result;
    }
    public List<IImpulsePostReactor> ClearAllIImpulsePostReactor() {
      var result = new List<IImpulsePostReactor>();
      this.membersTimeCloneAICapabilityUCMutSet.Clear();
      this.membersAttackAICapabilityUCMutSet.Clear();
      return result;
    }
    public IImpulsePostReactor GetOnlyIImpulsePostReactorOrNull() {
      var result = GetAllIImpulsePostReactor();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIImpulsePostReactor.Null;
      }
    }
                 public List<IReactingToAttacksUC> GetAllIReactingToAttacksUC() {
      var result = new List<IReactingToAttacksUC>();
      foreach (var obj in this.membersCounteringUCMutSet) {
        result.Add(
            new CounteringUCAsIReactingToAttacksUC(obj));
      }
      return result;
    }
    public List<IReactingToAttacksUC> ClearAllIReactingToAttacksUC() {
      var result = new List<IReactingToAttacksUC>();
      this.membersCounteringUCMutSet.Clear();
      return result;
    }
    public IReactingToAttacksUC GetOnlyIReactingToAttacksUCOrNull() {
      var result = GetAllIReactingToAttacksUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIReactingToAttacksUC.Null;
      }
    }
                 public List<IImpulsePreReactor> GetAllIImpulsePreReactor() {
      var result = new List<IImpulsePreReactor>();
      foreach (var obj in this.membersBideAICapabilityUCMutSet) {
        result.Add(
            new BideAICapabilityUCAsIImpulsePreReactor(obj));
      }
      foreach (var obj in this.membersAttackAICapabilityUCMutSet) {
        result.Add(
            new AttackAICapabilityUCAsIImpulsePreReactor(obj));
      }
      return result;
    }
    public List<IImpulsePreReactor> ClearAllIImpulsePreReactor() {
      var result = new List<IImpulsePreReactor>();
      this.membersBideAICapabilityUCMutSet.Clear();
      this.membersAttackAICapabilityUCMutSet.Clear();
      return result;
    }
    public IImpulsePreReactor GetOnlyIImpulsePreReactorOrNull() {
      var result = GetAllIImpulsePreReactor();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIImpulsePreReactor.Null;
      }
    }
                 public List<IOffenseItem> GetAllIOffenseItem() {
      var result = new List<IOffenseItem>();
      foreach (var obj in this.membersGlaiveMutSet) {
        result.Add(
            new GlaiveAsIOffenseItem(obj));
      }
      return result;
    }
    public List<IOffenseItem> ClearAllIOffenseItem() {
      var result = new List<IOffenseItem>();
      this.membersGlaiveMutSet.Clear();
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
                 public List<IAICapabilityUC> GetAllIAICapabilityUC() {
      var result = new List<IAICapabilityUC>();
      foreach (var obj in this.membersWanderAICapabilityUCMutSet) {
        result.Add(
            new WanderAICapabilityUCAsIAICapabilityUC(obj));
      }
      foreach (var obj in this.membersTimeCloneAICapabilityUCMutSet) {
        result.Add(
            new TimeCloneAICapabilityUCAsIAICapabilityUC(obj));
      }
      foreach (var obj in this.membersBideAICapabilityUCMutSet) {
        result.Add(
            new BideAICapabilityUCAsIAICapabilityUC(obj));
      }
      foreach (var obj in this.membersAttackAICapabilityUCMutSet) {
        result.Add(
            new AttackAICapabilityUCAsIAICapabilityUC(obj));
      }
      return result;
    }
    public List<IAICapabilityUC> ClearAllIAICapabilityUC() {
      var result = new List<IAICapabilityUC>();
      this.membersWanderAICapabilityUCMutSet.Clear();
      this.membersTimeCloneAICapabilityUCMutSet.Clear();
      this.membersBideAICapabilityUCMutSet.Clear();
      this.membersAttackAICapabilityUCMutSet.Clear();
      return result;
    }
    public IAICapabilityUC GetOnlyIAICapabilityUCOrNull() {
      var result = GetAllIAICapabilityUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIAICapabilityUC.Null;
      }
    }
                 public List<IPreActingUC> GetAllIPreActingUC() {
      var result = new List<IPreActingUC>();
      foreach (var obj in this.membersCounteringUCMutSet) {
        result.Add(
            new CounteringUCAsIPreActingUC(obj));
      }
      foreach (var obj in this.membersShieldingUCMutSet) {
        result.Add(
            new ShieldingUCAsIPreActingUC(obj));
      }
      foreach (var obj in this.membersAttackAICapabilityUCMutSet) {
        result.Add(
            new AttackAICapabilityUCAsIPreActingUC(obj));
      }
      return result;
    }
    public List<IPreActingUC> ClearAllIPreActingUC() {
      var result = new List<IPreActingUC>();
      this.membersCounteringUCMutSet.Clear();
      this.membersShieldingUCMutSet.Clear();
      this.membersAttackAICapabilityUCMutSet.Clear();
      return result;
    }
    public IPreActingUC GetOnlyIPreActingUCOrNull() {
      var result = GetAllIPreActingUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIPreActingUC.Null;
      }
    }
                 public List<IPostActingUC> GetAllIPostActingUC() {
      var result = new List<IPostActingUC>();
      foreach (var obj in this.membersTimeCloneAICapabilityUCMutSet) {
        result.Add(
            new TimeCloneAICapabilityUCAsIPostActingUC(obj));
      }
      return result;
    }
    public List<IPostActingUC> ClearAllIPostActingUC() {
      var result = new List<IPostActingUC>();
      this.membersTimeCloneAICapabilityUCMutSet.Clear();
      return result;
    }
    public IPostActingUC GetOnlyIPostActingUCOrNull() {
      var result = GetAllIPostActingUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIPostActingUC.Null;
      }
    }
                 public List<IImmediatelyUseItem> GetAllIImmediatelyUseItem() {
      var result = new List<IImmediatelyUseItem>();
      foreach (var obj in this.membersManaPotionMutSet) {
        result.Add(
            new ManaPotionAsIImmediatelyUseItem(obj));
      }
      foreach (var obj in this.membersHealthPotionMutSet) {
        result.Add(
            new HealthPotionAsIImmediatelyUseItem(obj));
      }
      return result;
    }
    public List<IImmediatelyUseItem> ClearAllIImmediatelyUseItem() {
      var result = new List<IImmediatelyUseItem>();
      this.membersManaPotionMutSet.Clear();
      this.membersHealthPotionMutSet.Clear();
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
                 public List<IDefenseUC> GetAllIDefenseUC() {
      var result = new List<IDefenseUC>();
      foreach (var obj in this.membersShieldingUCMutSet) {
        result.Add(
            new ShieldingUCAsIDefenseUC(obj));
      }
      foreach (var obj in this.membersBideAICapabilityUCMutSet) {
        result.Add(
            new BideAICapabilityUCAsIDefenseUC(obj));
      }
      return result;
    }
    public List<IDefenseUC> ClearAllIDefenseUC() {
      var result = new List<IDefenseUC>();
      this.membersShieldingUCMutSet.Clear();
      this.membersBideAICapabilityUCMutSet.Clear();
      return result;
    }
    public IDefenseUC GetOnlyIDefenseUCOrNull() {
      var result = GetAllIDefenseUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIDefenseUC.Null;
      }
    }
                 public List<IItem> GetAllIItem() {
      var result = new List<IItem>();
      foreach (var obj in this.membersArmorMutSet) {
        result.Add(
            new ArmorAsIItem(obj));
      }
      foreach (var obj in this.membersInertiaRingMutSet) {
        result.Add(
            new InertiaRingAsIItem(obj));
      }
      foreach (var obj in this.membersGlaiveMutSet) {
        result.Add(
            new GlaiveAsIItem(obj));
      }
      foreach (var obj in this.membersManaPotionMutSet) {
        result.Add(
            new ManaPotionAsIItem(obj));
      }
      foreach (var obj in this.membersHealthPotionMutSet) {
        result.Add(
            new HealthPotionAsIItem(obj));
      }
      return result;
    }
    public List<IItem> ClearAllIItem() {
      var result = new List<IItem>();
      this.membersArmorMutSet.Clear();
      this.membersInertiaRingMutSet.Clear();
      this.membersGlaiveMutSet.Clear();
      this.membersManaPotionMutSet.Clear();
      this.membersHealthPotionMutSet.Clear();
      return result;
    }
    public IItem GetOnlyIItemOrNull() {
      var result = GetAllIItem();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIItem.Null;
      }
    }
                 public List<IInertiaItem> GetAllIInertiaItem() {
      var result = new List<IInertiaItem>();
      foreach (var obj in this.membersInertiaRingMutSet) {
        result.Add(
            new InertiaRingAsIInertiaItem(obj));
      }
      return result;
    }
    public List<IInertiaItem> ClearAllIInertiaItem() {
      var result = new List<IInertiaItem>();
      this.membersInertiaRingMutSet.Clear();
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
      foreach (var obj in this.membersArmorMutSet) {
        result.Add(
            new ArmorAsIDefenseItem(obj));
      }
      return result;
    }
    public List<IDefenseItem> ClearAllIDefenseItem() {
      var result = new List<IDefenseItem>();
      this.membersArmorMutSet.Clear();
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
      foreach (var obj in this.membersManaPotionMutSet) {
        result.Add(
            new ManaPotionAsIUsableItem(obj));
      }
      foreach (var obj in this.membersHealthPotionMutSet) {
        result.Add(
            new HealthPotionAsIUsableItem(obj));
      }
      return result;
    }
    public List<IUsableItem> ClearAllIUsableItem() {
      var result = new List<IUsableItem>();
      this.membersManaPotionMutSet.Clear();
      this.membersHealthPotionMutSet.Clear();
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
