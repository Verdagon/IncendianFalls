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

    if (!root.WanderAICapabilityUCMutSetExists(membersWanderAICapabilityUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersWanderAICapabilityUCMutSet");
    }

    if (!root.SummonAICapabilityUCMutSetExists(membersSummonAICapabilityUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersSummonAICapabilityUCMutSet");
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

    if (!root.BaseMovementTimeUCMutSetExists(membersBaseMovementTimeUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersBaseMovementTimeUCMutSet");
    }

    if (!root.BaseCombatTimeUCMutSetExists(membersBaseCombatTimeUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersBaseCombatTimeUCMutSet");
    }

    if (!root.ManaPotionMutSetExists(membersManaPotionMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersManaPotionMutSet");
    }

    if (!root.HealthPotionMutSetExists(membersHealthPotionMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersHealthPotionMutSet");
    }

    if (!root.SpeedRingMutSetExists(membersSpeedRingMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersSpeedRingMutSet");
    }

    if (!root.GlaiveMutSetExists(membersGlaiveMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersGlaiveMutSet");
    }

    if (!root.ArmorMutSetExists(membersArmorMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersArmorMutSet");
    }

    if (!root.SorcerousUCMutSetExists(membersSorcerousUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersSorcerousUCMutSet");
    }

    if (!root.BaseOffenseUCMutSetExists(membersBaseOffenseUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersBaseOffenseUCMutSet");
    }

    if (!root.BaseDefenseUCMutSetExists(membersBaseDefenseUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersBaseDefenseUCMutSet");
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    if (root.WanderAICapabilityUCMutSetExists(membersWanderAICapabilityUCMutSet.id)) {
      membersWanderAICapabilityUCMutSet.FindReachableObjects(foundIds);
    }
    if (root.SummonAICapabilityUCMutSetExists(membersSummonAICapabilityUCMutSet.id)) {
      membersSummonAICapabilityUCMutSet.FindReachableObjects(foundIds);
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
    if (root.BaseMovementTimeUCMutSetExists(membersBaseMovementTimeUCMutSet.id)) {
      membersBaseMovementTimeUCMutSet.FindReachableObjects(foundIds);
    }
    if (root.BaseCombatTimeUCMutSetExists(membersBaseCombatTimeUCMutSet.id)) {
      membersBaseCombatTimeUCMutSet.FindReachableObjects(foundIds);
    }
    if (root.ManaPotionMutSetExists(membersManaPotionMutSet.id)) {
      membersManaPotionMutSet.FindReachableObjects(foundIds);
    }
    if (root.HealthPotionMutSetExists(membersHealthPotionMutSet.id)) {
      membersHealthPotionMutSet.FindReachableObjects(foundIds);
    }
    if (root.SpeedRingMutSetExists(membersSpeedRingMutSet.id)) {
      membersSpeedRingMutSet.FindReachableObjects(foundIds);
    }
    if (root.GlaiveMutSetExists(membersGlaiveMutSet.id)) {
      membersGlaiveMutSet.FindReachableObjects(foundIds);
    }
    if (root.ArmorMutSetExists(membersArmorMutSet.id)) {
      membersArmorMutSet.FindReachableObjects(foundIds);
    }
    if (root.SorcerousUCMutSetExists(membersSorcerousUCMutSet.id)) {
      membersSorcerousUCMutSet.FindReachableObjects(foundIds);
    }
    if (root.BaseOffenseUCMutSetExists(membersBaseOffenseUCMutSet.id)) {
      membersBaseOffenseUCMutSet.FindReachableObjects(foundIds);
    }
    if (root.BaseDefenseUCMutSetExists(membersBaseDefenseUCMutSet.id)) {
      membersBaseDefenseUCMutSet.FindReachableObjects(foundIds);
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
         public WanderAICapabilityUCMutSet membersWanderAICapabilityUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersWanderAICapabilityUCMutSet of null!");
      }
      return new WanderAICapabilityUCMutSet(root, incarnation.membersWanderAICapabilityUCMutSet);
    }
                       }
  public SummonAICapabilityUCMutSet membersSummonAICapabilityUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersSummonAICapabilityUCMutSet of null!");
      }
      return new SummonAICapabilityUCMutSet(root, incarnation.membersSummonAICapabilityUCMutSet);
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
  public BaseMovementTimeUCMutSet membersBaseMovementTimeUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersBaseMovementTimeUCMutSet of null!");
      }
      return new BaseMovementTimeUCMutSet(root, incarnation.membersBaseMovementTimeUCMutSet);
    }
                       }
  public BaseCombatTimeUCMutSet membersBaseCombatTimeUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersBaseCombatTimeUCMutSet of null!");
      }
      return new BaseCombatTimeUCMutSet(root, incarnation.membersBaseCombatTimeUCMutSet);
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
  public SpeedRingMutSet membersSpeedRingMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersSpeedRingMutSet of null!");
      }
      return new SpeedRingMutSet(root, incarnation.membersSpeedRingMutSet);
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
  public ArmorMutSet membersArmorMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersArmorMutSet of null!");
      }
      return new ArmorMutSet(root, incarnation.membersArmorMutSet);
    }
                       }
  public SorcerousUCMutSet membersSorcerousUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersSorcerousUCMutSet of null!");
      }
      return new SorcerousUCMutSet(root, incarnation.membersSorcerousUCMutSet);
    }
                       }
  public BaseOffenseUCMutSet membersBaseOffenseUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersBaseOffenseUCMutSet of null!");
      }
      return new BaseOffenseUCMutSet(root, incarnation.membersBaseOffenseUCMutSet);
    }
                       }
  public BaseDefenseUCMutSet membersBaseDefenseUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersBaseDefenseUCMutSet of null!");
      }
      return new BaseDefenseUCMutSet(root, incarnation.membersBaseDefenseUCMutSet);
    }
                       }

  public static IUnitComponentMutBunch New(Root root) {
    return root.EffectIUnitComponentMutBunchCreate(
      root.EffectWanderAICapabilityUCMutSetCreate()
,
      root.EffectSummonAICapabilityUCMutSetCreate()
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
,
      root.EffectBaseMovementTimeUCMutSetCreate()
,
      root.EffectBaseCombatTimeUCMutSetCreate()
,
      root.EffectManaPotionMutSetCreate()
,
      root.EffectHealthPotionMutSetCreate()
,
      root.EffectSpeedRingMutSetCreate()
,
      root.EffectGlaiveMutSetCreate()
,
      root.EffectArmorMutSetCreate()
,
      root.EffectSorcerousUCMutSetCreate()
,
      root.EffectBaseOffenseUCMutSetCreate()
,
      root.EffectBaseDefenseUCMutSetCreate()
        );
  }
  public void Add(IUnitComponent elementI) {

    // Can optimize, check the type of element directly somehow
    if (root.WanderAICapabilityUCExists(elementI.id)) {
      this.membersWanderAICapabilityUCMutSet.Add(root.GetWanderAICapabilityUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.SummonAICapabilityUCExists(elementI.id)) {
      this.membersSummonAICapabilityUCMutSet.Add(root.GetSummonAICapabilityUC(elementI.id));
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

    // Can optimize, check the type of element directly somehow
    if (root.BaseMovementTimeUCExists(elementI.id)) {
      this.membersBaseMovementTimeUCMutSet.Add(root.GetBaseMovementTimeUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.BaseCombatTimeUCExists(elementI.id)) {
      this.membersBaseCombatTimeUCMutSet.Add(root.GetBaseCombatTimeUC(elementI.id));
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
    if (root.SpeedRingExists(elementI.id)) {
      this.membersSpeedRingMutSet.Add(root.GetSpeedRing(elementI.id));
      return;
    }

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

    // Can optimize, check the type of element directly somehow
    if (root.SorcerousUCExists(elementI.id)) {
      this.membersSorcerousUCMutSet.Add(root.GetSorcerousUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.BaseOffenseUCExists(elementI.id)) {
      this.membersBaseOffenseUCMutSet.Add(root.GetBaseOffenseUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.BaseDefenseUCExists(elementI.id)) {
      this.membersBaseDefenseUCMutSet.Add(root.GetBaseDefenseUC(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Remove(IUnitComponent elementI) {

    // Can optimize, check the type of element directly somehow
    if (root.WanderAICapabilityUCExists(elementI.id)) {
      this.membersWanderAICapabilityUCMutSet.Remove(root.GetWanderAICapabilityUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.SummonAICapabilityUCExists(elementI.id)) {
      this.membersSummonAICapabilityUCMutSet.Remove(root.GetSummonAICapabilityUC(elementI.id));
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

    // Can optimize, check the type of element directly somehow
    if (root.BaseMovementTimeUCExists(elementI.id)) {
      this.membersBaseMovementTimeUCMutSet.Remove(root.GetBaseMovementTimeUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.BaseCombatTimeUCExists(elementI.id)) {
      this.membersBaseCombatTimeUCMutSet.Remove(root.GetBaseCombatTimeUC(elementI.id));
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
    if (root.SpeedRingExists(elementI.id)) {
      this.membersSpeedRingMutSet.Remove(root.GetSpeedRing(elementI.id));
      return;
    }

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

    // Can optimize, check the type of element directly somehow
    if (root.SorcerousUCExists(elementI.id)) {
      this.membersSorcerousUCMutSet.Remove(root.GetSorcerousUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.BaseOffenseUCExists(elementI.id)) {
      this.membersBaseOffenseUCMutSet.Remove(root.GetBaseOffenseUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.BaseDefenseUCExists(elementI.id)) {
      this.membersBaseDefenseUCMutSet.Remove(root.GetBaseDefenseUC(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Clear() {
    this.membersWanderAICapabilityUCMutSet.Clear();
    this.membersSummonAICapabilityUCMutSet.Clear();
    this.membersTimeCloneAICapabilityUCMutSet.Clear();
    this.membersAttackAICapabilityUCMutSet.Clear();
    this.membersCounteringUCMutSet.Clear();
    this.membersShieldingUCMutSet.Clear();
    this.membersBideAICapabilityUCMutSet.Clear();
    this.membersBaseMovementTimeUCMutSet.Clear();
    this.membersBaseCombatTimeUCMutSet.Clear();
    this.membersManaPotionMutSet.Clear();
    this.membersHealthPotionMutSet.Clear();
    this.membersSpeedRingMutSet.Clear();
    this.membersGlaiveMutSet.Clear();
    this.membersArmorMutSet.Clear();
    this.membersSorcerousUCMutSet.Clear();
    this.membersBaseOffenseUCMutSet.Clear();
    this.membersBaseDefenseUCMutSet.Clear();
  }
  public int Count {
    get {
      return
        this.membersWanderAICapabilityUCMutSet.Count +
        this.membersSummonAICapabilityUCMutSet.Count +
        this.membersTimeCloneAICapabilityUCMutSet.Count +
        this.membersAttackAICapabilityUCMutSet.Count +
        this.membersCounteringUCMutSet.Count +
        this.membersShieldingUCMutSet.Count +
        this.membersBideAICapabilityUCMutSet.Count +
        this.membersBaseMovementTimeUCMutSet.Count +
        this.membersBaseCombatTimeUCMutSet.Count +
        this.membersManaPotionMutSet.Count +
        this.membersHealthPotionMutSet.Count +
        this.membersSpeedRingMutSet.Count +
        this.membersGlaiveMutSet.Count +
        this.membersArmorMutSet.Count +
        this.membersSorcerousUCMutSet.Count +
        this.membersBaseOffenseUCMutSet.Count +
        this.membersBaseDefenseUCMutSet.Count
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
    var tempMembersWanderAICapabilityUCMutSet = this.membersWanderAICapabilityUCMutSet;
    var tempMembersSummonAICapabilityUCMutSet = this.membersSummonAICapabilityUCMutSet;
    var tempMembersTimeCloneAICapabilityUCMutSet = this.membersTimeCloneAICapabilityUCMutSet;
    var tempMembersAttackAICapabilityUCMutSet = this.membersAttackAICapabilityUCMutSet;
    var tempMembersCounteringUCMutSet = this.membersCounteringUCMutSet;
    var tempMembersShieldingUCMutSet = this.membersShieldingUCMutSet;
    var tempMembersBideAICapabilityUCMutSet = this.membersBideAICapabilityUCMutSet;
    var tempMembersBaseMovementTimeUCMutSet = this.membersBaseMovementTimeUCMutSet;
    var tempMembersBaseCombatTimeUCMutSet = this.membersBaseCombatTimeUCMutSet;
    var tempMembersManaPotionMutSet = this.membersManaPotionMutSet;
    var tempMembersHealthPotionMutSet = this.membersHealthPotionMutSet;
    var tempMembersSpeedRingMutSet = this.membersSpeedRingMutSet;
    var tempMembersGlaiveMutSet = this.membersGlaiveMutSet;
    var tempMembersArmorMutSet = this.membersArmorMutSet;
    var tempMembersSorcerousUCMutSet = this.membersSorcerousUCMutSet;
    var tempMembersBaseOffenseUCMutSet = this.membersBaseOffenseUCMutSet;
    var tempMembersBaseDefenseUCMutSet = this.membersBaseDefenseUCMutSet;

    this.Delete();
    tempMembersWanderAICapabilityUCMutSet.Destruct();
    tempMembersSummonAICapabilityUCMutSet.Destruct();
    tempMembersTimeCloneAICapabilityUCMutSet.Destruct();
    tempMembersAttackAICapabilityUCMutSet.Destruct();
    tempMembersCounteringUCMutSet.Destruct();
    tempMembersShieldingUCMutSet.Destruct();
    tempMembersBideAICapabilityUCMutSet.Destruct();
    tempMembersBaseMovementTimeUCMutSet.Destruct();
    tempMembersBaseCombatTimeUCMutSet.Destruct();
    tempMembersManaPotionMutSet.Destruct();
    tempMembersHealthPotionMutSet.Destruct();
    tempMembersSpeedRingMutSet.Destruct();
    tempMembersGlaiveMutSet.Destruct();
    tempMembersArmorMutSet.Destruct();
    tempMembersSorcerousUCMutSet.Destruct();
    tempMembersBaseOffenseUCMutSet.Destruct();
    tempMembersBaseDefenseUCMutSet.Destruct();
  }
  public IEnumerator<IUnitComponent> GetEnumerator() {
    foreach (var element in this.membersWanderAICapabilityUCMutSet) {
      yield return new WanderAICapabilityUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersSummonAICapabilityUCMutSet) {
      yield return new SummonAICapabilityUCAsIUnitComponent(element);
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
    foreach (var element in this.membersBaseMovementTimeUCMutSet) {
      yield return new BaseMovementTimeUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersBaseCombatTimeUCMutSet) {
      yield return new BaseCombatTimeUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersManaPotionMutSet) {
      yield return new ManaPotionAsIUnitComponent(element);
    }
    foreach (var element in this.membersHealthPotionMutSet) {
      yield return new HealthPotionAsIUnitComponent(element);
    }
    foreach (var element in this.membersSpeedRingMutSet) {
      yield return new SpeedRingAsIUnitComponent(element);
    }
    foreach (var element in this.membersGlaiveMutSet) {
      yield return new GlaiveAsIUnitComponent(element);
    }
    foreach (var element in this.membersArmorMutSet) {
      yield return new ArmorAsIUnitComponent(element);
    }
    foreach (var element in this.membersSorcerousUCMutSet) {
      yield return new SorcerousUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersBaseOffenseUCMutSet) {
      yield return new BaseOffenseUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersBaseDefenseUCMutSet) {
      yield return new BaseDefenseUCAsIUnitComponent(element);
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
    public List<SummonAICapabilityUC> GetAllSummonAICapabilityUC() {
      var result = new List<SummonAICapabilityUC>();
      foreach (var thing in this.membersSummonAICapabilityUCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<SummonAICapabilityUC> ClearAllSummonAICapabilityUC() {
      var result = new List<SummonAICapabilityUC>();
      this.membersSummonAICapabilityUCMutSet.Clear();
      return result;
    }
    public SummonAICapabilityUC GetOnlySummonAICapabilityUCOrNull() {
      var result = GetAllSummonAICapabilityUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return SummonAICapabilityUC.Null;
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
    public List<BaseMovementTimeUC> GetAllBaseMovementTimeUC() {
      var result = new List<BaseMovementTimeUC>();
      foreach (var thing in this.membersBaseMovementTimeUCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<BaseMovementTimeUC> ClearAllBaseMovementTimeUC() {
      var result = new List<BaseMovementTimeUC>();
      this.membersBaseMovementTimeUCMutSet.Clear();
      return result;
    }
    public BaseMovementTimeUC GetOnlyBaseMovementTimeUCOrNull() {
      var result = GetAllBaseMovementTimeUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return BaseMovementTimeUC.Null;
      }
    }
    public List<BaseCombatTimeUC> GetAllBaseCombatTimeUC() {
      var result = new List<BaseCombatTimeUC>();
      foreach (var thing in this.membersBaseCombatTimeUCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<BaseCombatTimeUC> ClearAllBaseCombatTimeUC() {
      var result = new List<BaseCombatTimeUC>();
      this.membersBaseCombatTimeUCMutSet.Clear();
      return result;
    }
    public BaseCombatTimeUC GetOnlyBaseCombatTimeUCOrNull() {
      var result = GetAllBaseCombatTimeUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return BaseCombatTimeUC.Null;
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
    public List<SpeedRing> GetAllSpeedRing() {
      var result = new List<SpeedRing>();
      foreach (var thing in this.membersSpeedRingMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<SpeedRing> ClearAllSpeedRing() {
      var result = new List<SpeedRing>();
      this.membersSpeedRingMutSet.Clear();
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
    public List<SorcerousUC> GetAllSorcerousUC() {
      var result = new List<SorcerousUC>();
      foreach (var thing in this.membersSorcerousUCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<SorcerousUC> ClearAllSorcerousUC() {
      var result = new List<SorcerousUC>();
      this.membersSorcerousUCMutSet.Clear();
      return result;
    }
    public SorcerousUC GetOnlySorcerousUCOrNull() {
      var result = GetAllSorcerousUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return SorcerousUC.Null;
      }
    }
    public List<BaseOffenseUC> GetAllBaseOffenseUC() {
      var result = new List<BaseOffenseUC>();
      foreach (var thing in this.membersBaseOffenseUCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<BaseOffenseUC> ClearAllBaseOffenseUC() {
      var result = new List<BaseOffenseUC>();
      this.membersBaseOffenseUCMutSet.Clear();
      return result;
    }
    public BaseOffenseUC GetOnlyBaseOffenseUCOrNull() {
      var result = GetAllBaseOffenseUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return BaseOffenseUC.Null;
      }
    }
    public List<BaseDefenseUC> GetAllBaseDefenseUC() {
      var result = new List<BaseDefenseUC>();
      foreach (var thing in this.membersBaseDefenseUCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<BaseDefenseUC> ClearAllBaseDefenseUC() {
      var result = new List<BaseDefenseUC>();
      this.membersBaseDefenseUCMutSet.Clear();
      return result;
    }
    public BaseDefenseUC GetOnlyBaseDefenseUCOrNull() {
      var result = GetAllBaseDefenseUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return BaseDefenseUC.Null;
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
                 public List<ICloneableUC> GetAllICloneableUC() {
      var result = new List<ICloneableUC>();
      foreach (var obj in this.membersSorcerousUCMutSet) {
        result.Add(
            new SorcerousUCAsICloneableUC(obj));
      }
      foreach (var obj in this.membersBaseOffenseUCMutSet) {
        result.Add(
            new BaseOffenseUCAsICloneableUC(obj));
      }
      foreach (var obj in this.membersBaseMovementTimeUCMutSet) {
        result.Add(
            new BaseMovementTimeUCAsICloneableUC(obj));
      }
      foreach (var obj in this.membersBaseDefenseUCMutSet) {
        result.Add(
            new BaseDefenseUCAsICloneableUC(obj));
      }
      foreach (var obj in this.membersBaseCombatTimeUCMutSet) {
        result.Add(
            new BaseCombatTimeUCAsICloneableUC(obj));
      }
      foreach (var obj in this.membersSpeedRingMutSet) {
        result.Add(
            new SpeedRingAsICloneableUC(obj));
      }
      foreach (var obj in this.membersGlaiveMutSet) {
        result.Add(
            new GlaiveAsICloneableUC(obj));
      }
      foreach (var obj in this.membersArmorMutSet) {
        result.Add(
            new ArmorAsICloneableUC(obj));
      }
      return result;
    }
    public List<ICloneableUC> ClearAllICloneableUC() {
      var result = new List<ICloneableUC>();
      this.membersSorcerousUCMutSet.Clear();
      this.membersBaseOffenseUCMutSet.Clear();
      this.membersBaseMovementTimeUCMutSet.Clear();
      this.membersBaseDefenseUCMutSet.Clear();
      this.membersBaseCombatTimeUCMutSet.Clear();
      this.membersSpeedRingMutSet.Clear();
      this.membersGlaiveMutSet.Clear();
      this.membersArmorMutSet.Clear();
      return result;
    }
    public ICloneableUC GetOnlyICloneableUCOrNull() {
      var result = GetAllICloneableUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullICloneableUC.Null;
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
                 public List<IMovementTimeFactorUC> GetAllIMovementTimeFactorUC() {
      var result = new List<IMovementTimeFactorUC>();
      foreach (var obj in this.membersBaseMovementTimeUCMutSet) {
        result.Add(
            new BaseMovementTimeUCAsIMovementTimeFactorUC(obj));
      }
      foreach (var obj in this.membersSpeedRingMutSet) {
        result.Add(
            new SpeedRingAsIMovementTimeFactorUC(obj));
      }
      return result;
    }
    public List<IMovementTimeFactorUC> ClearAllIMovementTimeFactorUC() {
      var result = new List<IMovementTimeFactorUC>();
      this.membersBaseMovementTimeUCMutSet.Clear();
      this.membersSpeedRingMutSet.Clear();
      return result;
    }
    public IMovementTimeFactorUC GetOnlyIMovementTimeFactorUCOrNull() {
      var result = GetAllIMovementTimeFactorUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIMovementTimeFactorUC.Null;
      }
    }
                 public List<IAICapabilityUC> GetAllIAICapabilityUC() {
      var result = new List<IAICapabilityUC>();
      foreach (var obj in this.membersWanderAICapabilityUCMutSet) {
        result.Add(
            new WanderAICapabilityUCAsIAICapabilityUC(obj));
      }
      foreach (var obj in this.membersSummonAICapabilityUCMutSet) {
        result.Add(
            new SummonAICapabilityUCAsIAICapabilityUC(obj));
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
      this.membersSummonAICapabilityUCMutSet.Clear();
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
                 public List<IDefenseFactorUC> GetAllIDefenseFactorUC() {
      var result = new List<IDefenseFactorUC>();
      foreach (var obj in this.membersBaseDefenseUCMutSet) {
        result.Add(
            new BaseDefenseUCAsIDefenseFactorUC(obj));
      }
      foreach (var obj in this.membersShieldingUCMutSet) {
        result.Add(
            new ShieldingUCAsIDefenseFactorUC(obj));
      }
      foreach (var obj in this.membersBideAICapabilityUCMutSet) {
        result.Add(
            new BideAICapabilityUCAsIDefenseFactorUC(obj));
      }
      foreach (var obj in this.membersArmorMutSet) {
        result.Add(
            new ArmorAsIDefenseFactorUC(obj));
      }
      return result;
    }
    public List<IDefenseFactorUC> ClearAllIDefenseFactorUC() {
      var result = new List<IDefenseFactorUC>();
      this.membersBaseDefenseUCMutSet.Clear();
      this.membersShieldingUCMutSet.Clear();
      this.membersBideAICapabilityUCMutSet.Clear();
      this.membersArmorMutSet.Clear();
      return result;
    }
    public IDefenseFactorUC GetOnlyIDefenseFactorUCOrNull() {
      var result = GetAllIDefenseFactorUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIDefenseFactorUC.Null;
      }
    }
                 public List<IOffenseFactorUC> GetAllIOffenseFactorUC() {
      var result = new List<IOffenseFactorUC>();
      foreach (var obj in this.membersBaseOffenseUCMutSet) {
        result.Add(
            new BaseOffenseUCAsIOffenseFactorUC(obj));
      }
      foreach (var obj in this.membersGlaiveMutSet) {
        result.Add(
            new GlaiveAsIOffenseFactorUC(obj));
      }
      return result;
    }
    public List<IOffenseFactorUC> ClearAllIOffenseFactorUC() {
      var result = new List<IOffenseFactorUC>();
      this.membersBaseOffenseUCMutSet.Clear();
      this.membersGlaiveMutSet.Clear();
      return result;
    }
    public IOffenseFactorUC GetOnlyIOffenseFactorUCOrNull() {
      var result = GetAllIOffenseFactorUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIOffenseFactorUC.Null;
      }
    }
                 public List<IPreActingUC> GetAllIPreActingUC() {
      var result = new List<IPreActingUC>();
      foreach (var obj in this.membersShieldingUCMutSet) {
        result.Add(
            new ShieldingUCAsIPreActingUC(obj));
      }
      foreach (var obj in this.membersCounteringUCMutSet) {
        result.Add(
            new CounteringUCAsIPreActingUC(obj));
      }
      foreach (var obj in this.membersAttackAICapabilityUCMutSet) {
        result.Add(
            new AttackAICapabilityUCAsIPreActingUC(obj));
      }
      return result;
    }
    public List<IPreActingUC> ClearAllIPreActingUC() {
      var result = new List<IPreActingUC>();
      this.membersShieldingUCMutSet.Clear();
      this.membersCounteringUCMutSet.Clear();
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
                 public List<IItem> GetAllIItem() {
      var result = new List<IItem>();
      foreach (var obj in this.membersManaPotionMutSet) {
        result.Add(
            new ManaPotionAsIItem(obj));
      }
      foreach (var obj in this.membersHealthPotionMutSet) {
        result.Add(
            new HealthPotionAsIItem(obj));
      }
      foreach (var obj in this.membersSpeedRingMutSet) {
        result.Add(
            new SpeedRingAsIItem(obj));
      }
      foreach (var obj in this.membersGlaiveMutSet) {
        result.Add(
            new GlaiveAsIItem(obj));
      }
      foreach (var obj in this.membersArmorMutSet) {
        result.Add(
            new ArmorAsIItem(obj));
      }
      return result;
    }
    public List<IItem> ClearAllIItem() {
      var result = new List<IItem>();
      this.membersManaPotionMutSet.Clear();
      this.membersHealthPotionMutSet.Clear();
      this.membersSpeedRingMutSet.Clear();
      this.membersGlaiveMutSet.Clear();
      this.membersArmorMutSet.Clear();
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
                 public List<ICombatTimeFactorUC> GetAllICombatTimeFactorUC() {
      var result = new List<ICombatTimeFactorUC>();
      foreach (var obj in this.membersBaseCombatTimeUCMutSet) {
        result.Add(
            new BaseCombatTimeUCAsICombatTimeFactorUC(obj));
      }
      return result;
    }
    public List<ICombatTimeFactorUC> ClearAllICombatTimeFactorUC() {
      var result = new List<ICombatTimeFactorUC>();
      this.membersBaseCombatTimeUCMutSet.Clear();
      return result;
    }
    public ICombatTimeFactorUC GetOnlyICombatTimeFactorUCOrNull() {
      var result = GetAllICombatTimeFactorUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullICombatTimeFactorUC.Null;
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
