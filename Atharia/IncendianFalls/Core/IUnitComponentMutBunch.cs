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

    if (!root.TutorialDefyCounterUCMutSetExists(membersTutorialDefyCounterUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersTutorialDefyCounterUCMutSet");
    }

    if (!root.LightningChargingUCMutSetExists(membersLightningChargingUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersLightningChargingUCMutSet");
    }

    if (!root.WanderAICapabilityUCMutSetExists(membersWanderAICapabilityUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersWanderAICapabilityUCMutSet");
    }

    if (!root.TemporaryCloneAICapabilityUCMutSetExists(membersTemporaryCloneAICapabilityUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersTemporaryCloneAICapabilityUCMutSet");
    }

    if (!root.SummonAICapabilityUCMutSetExists(membersSummonAICapabilityUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersSummonAICapabilityUCMutSet");
    }

    if (!root.KamikazeAICapabilityUCMutSetExists(membersKamikazeAICapabilityUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersKamikazeAICapabilityUCMutSet");
    }

    if (!root.GuardAICapabilityUCMutSetExists(membersGuardAICapabilityUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersGuardAICapabilityUCMutSet");
    }

    if (!root.TimeCloneAICapabilityUCMutSetExists(membersTimeCloneAICapabilityUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersTimeCloneAICapabilityUCMutSet");
    }

    if (!root.DoomedUCMutSetExists(membersDoomedUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersDoomedUCMutSet");
    }

    if (!root.MiredUCMutSetExists(membersMiredUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersMiredUCMutSet");
    }

    if (!root.AttackAICapabilityUCMutSetExists(membersAttackAICapabilityUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersAttackAICapabilityUCMutSet");
    }

    if (!root.CounteringUCMutSetExists(membersCounteringUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersCounteringUCMutSet");
    }

    if (!root.LightningChargedUCMutSetExists(membersLightningChargedUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersLightningChargedUCMutSet");
    }

    if (!root.InvincibilityUCMutSetExists(membersInvincibilityUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersInvincibilityUCMutSet");
    }

    if (!root.DefyingUCMutSetExists(membersDefyingUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersDefyingUCMutSet");
    }

    if (!root.BideAICapabilityUCMutSetExists(membersBideAICapabilityUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersBideAICapabilityUCMutSet");
    }

    if (!root.BaseSightRangeUCMutSetExists(membersBaseSightRangeUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersBaseSightRangeUCMutSet");
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

    if (!root.SlowRodMutSetExists(membersSlowRodMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersSlowRodMutSet");
    }

    if (!root.BlastRodMutSetExists(membersBlastRodMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersBlastRodMutSet");
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
    if (root.TutorialDefyCounterUCMutSetExists(membersTutorialDefyCounterUCMutSet.id)) {
      membersTutorialDefyCounterUCMutSet.FindReachableObjects(foundIds);
    }
    if (root.LightningChargingUCMutSetExists(membersLightningChargingUCMutSet.id)) {
      membersLightningChargingUCMutSet.FindReachableObjects(foundIds);
    }
    if (root.WanderAICapabilityUCMutSetExists(membersWanderAICapabilityUCMutSet.id)) {
      membersWanderAICapabilityUCMutSet.FindReachableObjects(foundIds);
    }
    if (root.TemporaryCloneAICapabilityUCMutSetExists(membersTemporaryCloneAICapabilityUCMutSet.id)) {
      membersTemporaryCloneAICapabilityUCMutSet.FindReachableObjects(foundIds);
    }
    if (root.SummonAICapabilityUCMutSetExists(membersSummonAICapabilityUCMutSet.id)) {
      membersSummonAICapabilityUCMutSet.FindReachableObjects(foundIds);
    }
    if (root.KamikazeAICapabilityUCMutSetExists(membersKamikazeAICapabilityUCMutSet.id)) {
      membersKamikazeAICapabilityUCMutSet.FindReachableObjects(foundIds);
    }
    if (root.GuardAICapabilityUCMutSetExists(membersGuardAICapabilityUCMutSet.id)) {
      membersGuardAICapabilityUCMutSet.FindReachableObjects(foundIds);
    }
    if (root.TimeCloneAICapabilityUCMutSetExists(membersTimeCloneAICapabilityUCMutSet.id)) {
      membersTimeCloneAICapabilityUCMutSet.FindReachableObjects(foundIds);
    }
    if (root.DoomedUCMutSetExists(membersDoomedUCMutSet.id)) {
      membersDoomedUCMutSet.FindReachableObjects(foundIds);
    }
    if (root.MiredUCMutSetExists(membersMiredUCMutSet.id)) {
      membersMiredUCMutSet.FindReachableObjects(foundIds);
    }
    if (root.AttackAICapabilityUCMutSetExists(membersAttackAICapabilityUCMutSet.id)) {
      membersAttackAICapabilityUCMutSet.FindReachableObjects(foundIds);
    }
    if (root.CounteringUCMutSetExists(membersCounteringUCMutSet.id)) {
      membersCounteringUCMutSet.FindReachableObjects(foundIds);
    }
    if (root.LightningChargedUCMutSetExists(membersLightningChargedUCMutSet.id)) {
      membersLightningChargedUCMutSet.FindReachableObjects(foundIds);
    }
    if (root.InvincibilityUCMutSetExists(membersInvincibilityUCMutSet.id)) {
      membersInvincibilityUCMutSet.FindReachableObjects(foundIds);
    }
    if (root.DefyingUCMutSetExists(membersDefyingUCMutSet.id)) {
      membersDefyingUCMutSet.FindReachableObjects(foundIds);
    }
    if (root.BideAICapabilityUCMutSetExists(membersBideAICapabilityUCMutSet.id)) {
      membersBideAICapabilityUCMutSet.FindReachableObjects(foundIds);
    }
    if (root.BaseSightRangeUCMutSetExists(membersBaseSightRangeUCMutSet.id)) {
      membersBaseSightRangeUCMutSet.FindReachableObjects(foundIds);
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
    if (root.SlowRodMutSetExists(membersSlowRodMutSet.id)) {
      membersSlowRodMutSet.FindReachableObjects(foundIds);
    }
    if (root.BlastRodMutSetExists(membersBlastRodMutSet.id)) {
      membersBlastRodMutSet.FindReachableObjects(foundIds);
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
         public TutorialDefyCounterUCMutSet membersTutorialDefyCounterUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersTutorialDefyCounterUCMutSet of null!");
      }
      return new TutorialDefyCounterUCMutSet(root, incarnation.membersTutorialDefyCounterUCMutSet);
    }
                       }
  public LightningChargingUCMutSet membersLightningChargingUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersLightningChargingUCMutSet of null!");
      }
      return new LightningChargingUCMutSet(root, incarnation.membersLightningChargingUCMutSet);
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
  public TemporaryCloneAICapabilityUCMutSet membersTemporaryCloneAICapabilityUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersTemporaryCloneAICapabilityUCMutSet of null!");
      }
      return new TemporaryCloneAICapabilityUCMutSet(root, incarnation.membersTemporaryCloneAICapabilityUCMutSet);
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
  public KamikazeAICapabilityUCMutSet membersKamikazeAICapabilityUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersKamikazeAICapabilityUCMutSet of null!");
      }
      return new KamikazeAICapabilityUCMutSet(root, incarnation.membersKamikazeAICapabilityUCMutSet);
    }
                       }
  public GuardAICapabilityUCMutSet membersGuardAICapabilityUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersGuardAICapabilityUCMutSet of null!");
      }
      return new GuardAICapabilityUCMutSet(root, incarnation.membersGuardAICapabilityUCMutSet);
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
  public DoomedUCMutSet membersDoomedUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersDoomedUCMutSet of null!");
      }
      return new DoomedUCMutSet(root, incarnation.membersDoomedUCMutSet);
    }
                       }
  public MiredUCMutSet membersMiredUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersMiredUCMutSet of null!");
      }
      return new MiredUCMutSet(root, incarnation.membersMiredUCMutSet);
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
  public LightningChargedUCMutSet membersLightningChargedUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersLightningChargedUCMutSet of null!");
      }
      return new LightningChargedUCMutSet(root, incarnation.membersLightningChargedUCMutSet);
    }
                       }
  public InvincibilityUCMutSet membersInvincibilityUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersInvincibilityUCMutSet of null!");
      }
      return new InvincibilityUCMutSet(root, incarnation.membersInvincibilityUCMutSet);
    }
                       }
  public DefyingUCMutSet membersDefyingUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersDefyingUCMutSet of null!");
      }
      return new DefyingUCMutSet(root, incarnation.membersDefyingUCMutSet);
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
  public BaseSightRangeUCMutSet membersBaseSightRangeUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersBaseSightRangeUCMutSet of null!");
      }
      return new BaseSightRangeUCMutSet(root, incarnation.membersBaseSightRangeUCMutSet);
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
  public SlowRodMutSet membersSlowRodMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersSlowRodMutSet of null!");
      }
      return new SlowRodMutSet(root, incarnation.membersSlowRodMutSet);
    }
                       }
  public BlastRodMutSet membersBlastRodMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersBlastRodMutSet of null!");
      }
      return new BlastRodMutSet(root, incarnation.membersBlastRodMutSet);
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
      root.EffectTutorialDefyCounterUCMutSetCreate()
,
      root.EffectLightningChargingUCMutSetCreate()
,
      root.EffectWanderAICapabilityUCMutSetCreate()
,
      root.EffectTemporaryCloneAICapabilityUCMutSetCreate()
,
      root.EffectSummonAICapabilityUCMutSetCreate()
,
      root.EffectKamikazeAICapabilityUCMutSetCreate()
,
      root.EffectGuardAICapabilityUCMutSetCreate()
,
      root.EffectTimeCloneAICapabilityUCMutSetCreate()
,
      root.EffectDoomedUCMutSetCreate()
,
      root.EffectMiredUCMutSetCreate()
,
      root.EffectAttackAICapabilityUCMutSetCreate()
,
      root.EffectCounteringUCMutSetCreate()
,
      root.EffectLightningChargedUCMutSetCreate()
,
      root.EffectInvincibilityUCMutSetCreate()
,
      root.EffectDefyingUCMutSetCreate()
,
      root.EffectBideAICapabilityUCMutSetCreate()
,
      root.EffectBaseSightRangeUCMutSetCreate()
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
      root.EffectSlowRodMutSetCreate()
,
      root.EffectBlastRodMutSetCreate()
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
    if (root.TutorialDefyCounterUCExists(elementI.id)) {
      this.membersTutorialDefyCounterUCMutSet.Add(root.GetTutorialDefyCounterUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.LightningChargingUCExists(elementI.id)) {
      this.membersLightningChargingUCMutSet.Add(root.GetLightningChargingUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.WanderAICapabilityUCExists(elementI.id)) {
      this.membersWanderAICapabilityUCMutSet.Add(root.GetWanderAICapabilityUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.TemporaryCloneAICapabilityUCExists(elementI.id)) {
      this.membersTemporaryCloneAICapabilityUCMutSet.Add(root.GetTemporaryCloneAICapabilityUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.SummonAICapabilityUCExists(elementI.id)) {
      this.membersSummonAICapabilityUCMutSet.Add(root.GetSummonAICapabilityUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.KamikazeAICapabilityUCExists(elementI.id)) {
      this.membersKamikazeAICapabilityUCMutSet.Add(root.GetKamikazeAICapabilityUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.GuardAICapabilityUCExists(elementI.id)) {
      this.membersGuardAICapabilityUCMutSet.Add(root.GetGuardAICapabilityUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.TimeCloneAICapabilityUCExists(elementI.id)) {
      this.membersTimeCloneAICapabilityUCMutSet.Add(root.GetTimeCloneAICapabilityUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.DoomedUCExists(elementI.id)) {
      this.membersDoomedUCMutSet.Add(root.GetDoomedUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.MiredUCExists(elementI.id)) {
      this.membersMiredUCMutSet.Add(root.GetMiredUC(elementI.id));
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
    if (root.LightningChargedUCExists(elementI.id)) {
      this.membersLightningChargedUCMutSet.Add(root.GetLightningChargedUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.InvincibilityUCExists(elementI.id)) {
      this.membersInvincibilityUCMutSet.Add(root.GetInvincibilityUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.DefyingUCExists(elementI.id)) {
      this.membersDefyingUCMutSet.Add(root.GetDefyingUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.BideAICapabilityUCExists(elementI.id)) {
      this.membersBideAICapabilityUCMutSet.Add(root.GetBideAICapabilityUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.BaseSightRangeUCExists(elementI.id)) {
      this.membersBaseSightRangeUCMutSet.Add(root.GetBaseSightRangeUC(elementI.id));
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
    if (root.SlowRodExists(elementI.id)) {
      this.membersSlowRodMutSet.Add(root.GetSlowRod(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.BlastRodExists(elementI.id)) {
      this.membersBlastRodMutSet.Add(root.GetBlastRod(elementI.id));
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
    if (root.TutorialDefyCounterUCExists(elementI.id)) {
      this.membersTutorialDefyCounterUCMutSet.Remove(root.GetTutorialDefyCounterUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.LightningChargingUCExists(elementI.id)) {
      this.membersLightningChargingUCMutSet.Remove(root.GetLightningChargingUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.WanderAICapabilityUCExists(elementI.id)) {
      this.membersWanderAICapabilityUCMutSet.Remove(root.GetWanderAICapabilityUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.TemporaryCloneAICapabilityUCExists(elementI.id)) {
      this.membersTemporaryCloneAICapabilityUCMutSet.Remove(root.GetTemporaryCloneAICapabilityUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.SummonAICapabilityUCExists(elementI.id)) {
      this.membersSummonAICapabilityUCMutSet.Remove(root.GetSummonAICapabilityUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.KamikazeAICapabilityUCExists(elementI.id)) {
      this.membersKamikazeAICapabilityUCMutSet.Remove(root.GetKamikazeAICapabilityUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.GuardAICapabilityUCExists(elementI.id)) {
      this.membersGuardAICapabilityUCMutSet.Remove(root.GetGuardAICapabilityUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.TimeCloneAICapabilityUCExists(elementI.id)) {
      this.membersTimeCloneAICapabilityUCMutSet.Remove(root.GetTimeCloneAICapabilityUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.DoomedUCExists(elementI.id)) {
      this.membersDoomedUCMutSet.Remove(root.GetDoomedUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.MiredUCExists(elementI.id)) {
      this.membersMiredUCMutSet.Remove(root.GetMiredUC(elementI.id));
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
    if (root.LightningChargedUCExists(elementI.id)) {
      this.membersLightningChargedUCMutSet.Remove(root.GetLightningChargedUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.InvincibilityUCExists(elementI.id)) {
      this.membersInvincibilityUCMutSet.Remove(root.GetInvincibilityUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.DefyingUCExists(elementI.id)) {
      this.membersDefyingUCMutSet.Remove(root.GetDefyingUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.BideAICapabilityUCExists(elementI.id)) {
      this.membersBideAICapabilityUCMutSet.Remove(root.GetBideAICapabilityUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.BaseSightRangeUCExists(elementI.id)) {
      this.membersBaseSightRangeUCMutSet.Remove(root.GetBaseSightRangeUC(elementI.id));
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
    if (root.SlowRodExists(elementI.id)) {
      this.membersSlowRodMutSet.Remove(root.GetSlowRod(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.BlastRodExists(elementI.id)) {
      this.membersBlastRodMutSet.Remove(root.GetBlastRod(elementI.id));
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
    this.membersTutorialDefyCounterUCMutSet.Clear();
    this.membersLightningChargingUCMutSet.Clear();
    this.membersWanderAICapabilityUCMutSet.Clear();
    this.membersTemporaryCloneAICapabilityUCMutSet.Clear();
    this.membersSummonAICapabilityUCMutSet.Clear();
    this.membersKamikazeAICapabilityUCMutSet.Clear();
    this.membersGuardAICapabilityUCMutSet.Clear();
    this.membersTimeCloneAICapabilityUCMutSet.Clear();
    this.membersDoomedUCMutSet.Clear();
    this.membersMiredUCMutSet.Clear();
    this.membersAttackAICapabilityUCMutSet.Clear();
    this.membersCounteringUCMutSet.Clear();
    this.membersLightningChargedUCMutSet.Clear();
    this.membersInvincibilityUCMutSet.Clear();
    this.membersDefyingUCMutSet.Clear();
    this.membersBideAICapabilityUCMutSet.Clear();
    this.membersBaseSightRangeUCMutSet.Clear();
    this.membersBaseMovementTimeUCMutSet.Clear();
    this.membersBaseCombatTimeUCMutSet.Clear();
    this.membersManaPotionMutSet.Clear();
    this.membersHealthPotionMutSet.Clear();
    this.membersSpeedRingMutSet.Clear();
    this.membersGlaiveMutSet.Clear();
    this.membersSlowRodMutSet.Clear();
    this.membersBlastRodMutSet.Clear();
    this.membersArmorMutSet.Clear();
    this.membersSorcerousUCMutSet.Clear();
    this.membersBaseOffenseUCMutSet.Clear();
    this.membersBaseDefenseUCMutSet.Clear();
  }
  public int Count {
    get {
      return
        this.membersTutorialDefyCounterUCMutSet.Count +
        this.membersLightningChargingUCMutSet.Count +
        this.membersWanderAICapabilityUCMutSet.Count +
        this.membersTemporaryCloneAICapabilityUCMutSet.Count +
        this.membersSummonAICapabilityUCMutSet.Count +
        this.membersKamikazeAICapabilityUCMutSet.Count +
        this.membersGuardAICapabilityUCMutSet.Count +
        this.membersTimeCloneAICapabilityUCMutSet.Count +
        this.membersDoomedUCMutSet.Count +
        this.membersMiredUCMutSet.Count +
        this.membersAttackAICapabilityUCMutSet.Count +
        this.membersCounteringUCMutSet.Count +
        this.membersLightningChargedUCMutSet.Count +
        this.membersInvincibilityUCMutSet.Count +
        this.membersDefyingUCMutSet.Count +
        this.membersBideAICapabilityUCMutSet.Count +
        this.membersBaseSightRangeUCMutSet.Count +
        this.membersBaseMovementTimeUCMutSet.Count +
        this.membersBaseCombatTimeUCMutSet.Count +
        this.membersManaPotionMutSet.Count +
        this.membersHealthPotionMutSet.Count +
        this.membersSpeedRingMutSet.Count +
        this.membersGlaiveMutSet.Count +
        this.membersSlowRodMutSet.Count +
        this.membersBlastRodMutSet.Count +
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
    var tempMembersTutorialDefyCounterUCMutSet = this.membersTutorialDefyCounterUCMutSet;
    var tempMembersLightningChargingUCMutSet = this.membersLightningChargingUCMutSet;
    var tempMembersWanderAICapabilityUCMutSet = this.membersWanderAICapabilityUCMutSet;
    var tempMembersTemporaryCloneAICapabilityUCMutSet = this.membersTemporaryCloneAICapabilityUCMutSet;
    var tempMembersSummonAICapabilityUCMutSet = this.membersSummonAICapabilityUCMutSet;
    var tempMembersKamikazeAICapabilityUCMutSet = this.membersKamikazeAICapabilityUCMutSet;
    var tempMembersGuardAICapabilityUCMutSet = this.membersGuardAICapabilityUCMutSet;
    var tempMembersTimeCloneAICapabilityUCMutSet = this.membersTimeCloneAICapabilityUCMutSet;
    var tempMembersDoomedUCMutSet = this.membersDoomedUCMutSet;
    var tempMembersMiredUCMutSet = this.membersMiredUCMutSet;
    var tempMembersAttackAICapabilityUCMutSet = this.membersAttackAICapabilityUCMutSet;
    var tempMembersCounteringUCMutSet = this.membersCounteringUCMutSet;
    var tempMembersLightningChargedUCMutSet = this.membersLightningChargedUCMutSet;
    var tempMembersInvincibilityUCMutSet = this.membersInvincibilityUCMutSet;
    var tempMembersDefyingUCMutSet = this.membersDefyingUCMutSet;
    var tempMembersBideAICapabilityUCMutSet = this.membersBideAICapabilityUCMutSet;
    var tempMembersBaseSightRangeUCMutSet = this.membersBaseSightRangeUCMutSet;
    var tempMembersBaseMovementTimeUCMutSet = this.membersBaseMovementTimeUCMutSet;
    var tempMembersBaseCombatTimeUCMutSet = this.membersBaseCombatTimeUCMutSet;
    var tempMembersManaPotionMutSet = this.membersManaPotionMutSet;
    var tempMembersHealthPotionMutSet = this.membersHealthPotionMutSet;
    var tempMembersSpeedRingMutSet = this.membersSpeedRingMutSet;
    var tempMembersGlaiveMutSet = this.membersGlaiveMutSet;
    var tempMembersSlowRodMutSet = this.membersSlowRodMutSet;
    var tempMembersBlastRodMutSet = this.membersBlastRodMutSet;
    var tempMembersArmorMutSet = this.membersArmorMutSet;
    var tempMembersSorcerousUCMutSet = this.membersSorcerousUCMutSet;
    var tempMembersBaseOffenseUCMutSet = this.membersBaseOffenseUCMutSet;
    var tempMembersBaseDefenseUCMutSet = this.membersBaseDefenseUCMutSet;

    this.Delete();
    tempMembersTutorialDefyCounterUCMutSet.Destruct();
    tempMembersLightningChargingUCMutSet.Destruct();
    tempMembersWanderAICapabilityUCMutSet.Destruct();
    tempMembersTemporaryCloneAICapabilityUCMutSet.Destruct();
    tempMembersSummonAICapabilityUCMutSet.Destruct();
    tempMembersKamikazeAICapabilityUCMutSet.Destruct();
    tempMembersGuardAICapabilityUCMutSet.Destruct();
    tempMembersTimeCloneAICapabilityUCMutSet.Destruct();
    tempMembersDoomedUCMutSet.Destruct();
    tempMembersMiredUCMutSet.Destruct();
    tempMembersAttackAICapabilityUCMutSet.Destruct();
    tempMembersCounteringUCMutSet.Destruct();
    tempMembersLightningChargedUCMutSet.Destruct();
    tempMembersInvincibilityUCMutSet.Destruct();
    tempMembersDefyingUCMutSet.Destruct();
    tempMembersBideAICapabilityUCMutSet.Destruct();
    tempMembersBaseSightRangeUCMutSet.Destruct();
    tempMembersBaseMovementTimeUCMutSet.Destruct();
    tempMembersBaseCombatTimeUCMutSet.Destruct();
    tempMembersManaPotionMutSet.Destruct();
    tempMembersHealthPotionMutSet.Destruct();
    tempMembersSpeedRingMutSet.Destruct();
    tempMembersGlaiveMutSet.Destruct();
    tempMembersSlowRodMutSet.Destruct();
    tempMembersBlastRodMutSet.Destruct();
    tempMembersArmorMutSet.Destruct();
    tempMembersSorcerousUCMutSet.Destruct();
    tempMembersBaseOffenseUCMutSet.Destruct();
    tempMembersBaseDefenseUCMutSet.Destruct();
  }
  public IEnumerator<IUnitComponent> GetEnumerator() {
    foreach (var element in this.membersTutorialDefyCounterUCMutSet) {
      yield return new TutorialDefyCounterUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersLightningChargingUCMutSet) {
      yield return new LightningChargingUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersWanderAICapabilityUCMutSet) {
      yield return new WanderAICapabilityUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersTemporaryCloneAICapabilityUCMutSet) {
      yield return new TemporaryCloneAICapabilityUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersSummonAICapabilityUCMutSet) {
      yield return new SummonAICapabilityUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersKamikazeAICapabilityUCMutSet) {
      yield return new KamikazeAICapabilityUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersGuardAICapabilityUCMutSet) {
      yield return new GuardAICapabilityUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersTimeCloneAICapabilityUCMutSet) {
      yield return new TimeCloneAICapabilityUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersDoomedUCMutSet) {
      yield return new DoomedUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersMiredUCMutSet) {
      yield return new MiredUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersAttackAICapabilityUCMutSet) {
      yield return new AttackAICapabilityUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersCounteringUCMutSet) {
      yield return new CounteringUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersLightningChargedUCMutSet) {
      yield return new LightningChargedUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersInvincibilityUCMutSet) {
      yield return new InvincibilityUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersDefyingUCMutSet) {
      yield return new DefyingUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersBideAICapabilityUCMutSet) {
      yield return new BideAICapabilityUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersBaseSightRangeUCMutSet) {
      yield return new BaseSightRangeUCAsIUnitComponent(element);
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
    foreach (var element in this.membersSlowRodMutSet) {
      yield return new SlowRodAsIUnitComponent(element);
    }
    foreach (var element in this.membersBlastRodMutSet) {
      yield return new BlastRodAsIUnitComponent(element);
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
    public List<TutorialDefyCounterUC> GetAllTutorialDefyCounterUC() {
      var result = new List<TutorialDefyCounterUC>();
      foreach (var thing in this.membersTutorialDefyCounterUCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<TutorialDefyCounterUC> ClearAllTutorialDefyCounterUC() {
      var result = new List<TutorialDefyCounterUC>();
      this.membersTutorialDefyCounterUCMutSet.Clear();
      return result;
    }
    public TutorialDefyCounterUC GetOnlyTutorialDefyCounterUCOrNull() {
      var result = GetAllTutorialDefyCounterUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return TutorialDefyCounterUC.Null;
      }
    }
    public List<LightningChargingUC> GetAllLightningChargingUC() {
      var result = new List<LightningChargingUC>();
      foreach (var thing in this.membersLightningChargingUCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<LightningChargingUC> ClearAllLightningChargingUC() {
      var result = new List<LightningChargingUC>();
      this.membersLightningChargingUCMutSet.Clear();
      return result;
    }
    public LightningChargingUC GetOnlyLightningChargingUCOrNull() {
      var result = GetAllLightningChargingUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return LightningChargingUC.Null;
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
    public List<TemporaryCloneAICapabilityUC> GetAllTemporaryCloneAICapabilityUC() {
      var result = new List<TemporaryCloneAICapabilityUC>();
      foreach (var thing in this.membersTemporaryCloneAICapabilityUCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<TemporaryCloneAICapabilityUC> ClearAllTemporaryCloneAICapabilityUC() {
      var result = new List<TemporaryCloneAICapabilityUC>();
      this.membersTemporaryCloneAICapabilityUCMutSet.Clear();
      return result;
    }
    public TemporaryCloneAICapabilityUC GetOnlyTemporaryCloneAICapabilityUCOrNull() {
      var result = GetAllTemporaryCloneAICapabilityUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return TemporaryCloneAICapabilityUC.Null;
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
    public List<KamikazeAICapabilityUC> GetAllKamikazeAICapabilityUC() {
      var result = new List<KamikazeAICapabilityUC>();
      foreach (var thing in this.membersKamikazeAICapabilityUCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<KamikazeAICapabilityUC> ClearAllKamikazeAICapabilityUC() {
      var result = new List<KamikazeAICapabilityUC>();
      this.membersKamikazeAICapabilityUCMutSet.Clear();
      return result;
    }
    public KamikazeAICapabilityUC GetOnlyKamikazeAICapabilityUCOrNull() {
      var result = GetAllKamikazeAICapabilityUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return KamikazeAICapabilityUC.Null;
      }
    }
    public List<GuardAICapabilityUC> GetAllGuardAICapabilityUC() {
      var result = new List<GuardAICapabilityUC>();
      foreach (var thing in this.membersGuardAICapabilityUCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<GuardAICapabilityUC> ClearAllGuardAICapabilityUC() {
      var result = new List<GuardAICapabilityUC>();
      this.membersGuardAICapabilityUCMutSet.Clear();
      return result;
    }
    public GuardAICapabilityUC GetOnlyGuardAICapabilityUCOrNull() {
      var result = GetAllGuardAICapabilityUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return GuardAICapabilityUC.Null;
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
    public List<DoomedUC> GetAllDoomedUC() {
      var result = new List<DoomedUC>();
      foreach (var thing in this.membersDoomedUCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<DoomedUC> ClearAllDoomedUC() {
      var result = new List<DoomedUC>();
      this.membersDoomedUCMutSet.Clear();
      return result;
    }
    public DoomedUC GetOnlyDoomedUCOrNull() {
      var result = GetAllDoomedUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return DoomedUC.Null;
      }
    }
    public List<MiredUC> GetAllMiredUC() {
      var result = new List<MiredUC>();
      foreach (var thing in this.membersMiredUCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<MiredUC> ClearAllMiredUC() {
      var result = new List<MiredUC>();
      this.membersMiredUCMutSet.Clear();
      return result;
    }
    public MiredUC GetOnlyMiredUCOrNull() {
      var result = GetAllMiredUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return MiredUC.Null;
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
    public List<LightningChargedUC> GetAllLightningChargedUC() {
      var result = new List<LightningChargedUC>();
      foreach (var thing in this.membersLightningChargedUCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<LightningChargedUC> ClearAllLightningChargedUC() {
      var result = new List<LightningChargedUC>();
      this.membersLightningChargedUCMutSet.Clear();
      return result;
    }
    public LightningChargedUC GetOnlyLightningChargedUCOrNull() {
      var result = GetAllLightningChargedUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return LightningChargedUC.Null;
      }
    }
    public List<InvincibilityUC> GetAllInvincibilityUC() {
      var result = new List<InvincibilityUC>();
      foreach (var thing in this.membersInvincibilityUCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<InvincibilityUC> ClearAllInvincibilityUC() {
      var result = new List<InvincibilityUC>();
      this.membersInvincibilityUCMutSet.Clear();
      return result;
    }
    public InvincibilityUC GetOnlyInvincibilityUCOrNull() {
      var result = GetAllInvincibilityUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return InvincibilityUC.Null;
      }
    }
    public List<DefyingUC> GetAllDefyingUC() {
      var result = new List<DefyingUC>();
      foreach (var thing in this.membersDefyingUCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<DefyingUC> ClearAllDefyingUC() {
      var result = new List<DefyingUC>();
      this.membersDefyingUCMutSet.Clear();
      return result;
    }
    public DefyingUC GetOnlyDefyingUCOrNull() {
      var result = GetAllDefyingUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return DefyingUC.Null;
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
    public List<BaseSightRangeUC> GetAllBaseSightRangeUC() {
      var result = new List<BaseSightRangeUC>();
      foreach (var thing in this.membersBaseSightRangeUCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<BaseSightRangeUC> ClearAllBaseSightRangeUC() {
      var result = new List<BaseSightRangeUC>();
      this.membersBaseSightRangeUCMutSet.Clear();
      return result;
    }
    public BaseSightRangeUC GetOnlyBaseSightRangeUCOrNull() {
      var result = GetAllBaseSightRangeUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return BaseSightRangeUC.Null;
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
    public List<SlowRod> GetAllSlowRod() {
      var result = new List<SlowRod>();
      foreach (var thing in this.membersSlowRodMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<SlowRod> ClearAllSlowRod() {
      var result = new List<SlowRod>();
      this.membersSlowRodMutSet.Clear();
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
      foreach (var thing in this.membersBlastRodMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<BlastRod> ClearAllBlastRod() {
      var result = new List<BlastRod>();
      this.membersBlastRodMutSet.Clear();
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
      foreach (var obj in this.membersTutorialDefyCounterUCMutSet) {
        result.Add(
            new TutorialDefyCounterUCAsIImpulsePostReactor(obj));
      }
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
      this.membersTutorialDefyCounterUCMutSet.Clear();
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
      foreach (var obj in this.membersBaseSightRangeUCMutSet) {
        result.Add(
            new BaseSightRangeUCAsICloneableUC(obj));
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
      foreach (var obj in this.membersSlowRodMutSet) {
        result.Add(
            new SlowRodAsICloneableUC(obj));
      }
      foreach (var obj in this.membersBlastRodMutSet) {
        result.Add(
            new BlastRodAsICloneableUC(obj));
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
      this.membersBaseSightRangeUCMutSet.Clear();
      this.membersBaseMovementTimeUCMutSet.Clear();
      this.membersBaseDefenseUCMutSet.Clear();
      this.membersBaseCombatTimeUCMutSet.Clear();
      this.membersSpeedRingMutSet.Clear();
      this.membersGlaiveMutSet.Clear();
      this.membersSlowRodMutSet.Clear();
      this.membersBlastRodMutSet.Clear();
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
      foreach (var obj in this.membersLightningChargingUCMutSet) {
        result.Add(
            new LightningChargingUCAsIImpulsePreReactor(obj));
      }
      foreach (var obj in this.membersKamikazeAICapabilityUCMutSet) {
        result.Add(
            new KamikazeAICapabilityUCAsIImpulsePreReactor(obj));
      }
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
      this.membersLightningChargingUCMutSet.Clear();
      this.membersKamikazeAICapabilityUCMutSet.Clear();
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
                 public List<ISightRangeFactorUC> GetAllISightRangeFactorUC() {
      var result = new List<ISightRangeFactorUC>();
      foreach (var obj in this.membersBaseSightRangeUCMutSet) {
        result.Add(
            new BaseSightRangeUCAsISightRangeFactorUC(obj));
      }
      return result;
    }
    public List<ISightRangeFactorUC> ClearAllISightRangeFactorUC() {
      var result = new List<ISightRangeFactorUC>();
      this.membersBaseSightRangeUCMutSet.Clear();
      return result;
    }
    public ISightRangeFactorUC GetOnlyISightRangeFactorUCOrNull() {
      var result = GetAllISightRangeFactorUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullISightRangeFactorUC.Null;
      }
    }
                 public List<IMovementTimeFactorUC> GetAllIMovementTimeFactorUC() {
      var result = new List<IMovementTimeFactorUC>();
      foreach (var obj in this.membersLightningChargedUCMutSet) {
        result.Add(
            new LightningChargedUCAsIMovementTimeFactorUC(obj));
      }
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
      this.membersLightningChargedUCMutSet.Clear();
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
      foreach (var obj in this.membersTemporaryCloneAICapabilityUCMutSet) {
        result.Add(
            new TemporaryCloneAICapabilityUCAsIAICapabilityUC(obj));
      }
      foreach (var obj in this.membersSummonAICapabilityUCMutSet) {
        result.Add(
            new SummonAICapabilityUCAsIAICapabilityUC(obj));
      }
      foreach (var obj in this.membersTimeCloneAICapabilityUCMutSet) {
        result.Add(
            new TimeCloneAICapabilityUCAsIAICapabilityUC(obj));
      }
      foreach (var obj in this.membersKamikazeAICapabilityUCMutSet) {
        result.Add(
            new KamikazeAICapabilityUCAsIAICapabilityUC(obj));
      }
      foreach (var obj in this.membersGuardAICapabilityUCMutSet) {
        result.Add(
            new GuardAICapabilityUCAsIAICapabilityUC(obj));
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
      this.membersTemporaryCloneAICapabilityUCMutSet.Clear();
      this.membersSummonAICapabilityUCMutSet.Clear();
      this.membersTimeCloneAICapabilityUCMutSet.Clear();
      this.membersKamikazeAICapabilityUCMutSet.Clear();
      this.membersGuardAICapabilityUCMutSet.Clear();
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
      foreach (var obj in this.membersInvincibilityUCMutSet) {
        result.Add(
            new InvincibilityUCAsIDefenseFactorUC(obj));
      }
      foreach (var obj in this.membersDefyingUCMutSet) {
        result.Add(
            new DefyingUCAsIDefenseFactorUC(obj));
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
      this.membersInvincibilityUCMutSet.Clear();
      this.membersDefyingUCMutSet.Clear();
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
      foreach (var obj in this.membersInvincibilityUCMutSet) {
        result.Add(
            new InvincibilityUCAsIOffenseFactorUC(obj));
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
      this.membersInvincibilityUCMutSet.Clear();
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
                 public List<IPickUpReactorItem> GetAllIPickUpReactorItem() {
      var result = new List<IPickUpReactorItem>();
      foreach (var obj in this.membersSlowRodMutSet) {
        result.Add(
            new SlowRodAsIPickUpReactorItem(obj));
      }
      foreach (var obj in this.membersBlastRodMutSet) {
        result.Add(
            new BlastRodAsIPickUpReactorItem(obj));
      }
      return result;
    }
    public List<IPickUpReactorItem> ClearAllIPickUpReactorItem() {
      var result = new List<IPickUpReactorItem>();
      this.membersSlowRodMutSet.Clear();
      this.membersBlastRodMutSet.Clear();
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
                 public List<IPreActingUC> GetAllIPreActingUC() {
      var result = new List<IPreActingUC>();
      foreach (var obj in this.membersDoomedUCMutSet) {
        result.Add(
            new DoomedUCAsIPreActingUC(obj));
      }
      foreach (var obj in this.membersMiredUCMutSet) {
        result.Add(
            new MiredUCAsIPreActingUC(obj));
      }
      foreach (var obj in this.membersInvincibilityUCMutSet) {
        result.Add(
            new InvincibilityUCAsIPreActingUC(obj));
      }
      foreach (var obj in this.membersDefyingUCMutSet) {
        result.Add(
            new DefyingUCAsIPreActingUC(obj));
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
      this.membersDoomedUCMutSet.Clear();
      this.membersMiredUCMutSet.Clear();
      this.membersInvincibilityUCMutSet.Clear();
      this.membersDefyingUCMutSet.Clear();
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
      foreach (var obj in this.membersLightningChargedUCMutSet) {
        result.Add(
            new LightningChargedUCAsIPostActingUC(obj));
      }
      foreach (var obj in this.membersTimeCloneAICapabilityUCMutSet) {
        result.Add(
            new TimeCloneAICapabilityUCAsIPostActingUC(obj));
      }
      return result;
    }
    public List<IPostActingUC> ClearAllIPostActingUC() {
      var result = new List<IPostActingUC>();
      this.membersLightningChargedUCMutSet.Clear();
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
                 public List<IDeathPreReactor> GetAllIDeathPreReactor() {
      var result = new List<IDeathPreReactor>();
      foreach (var obj in this.membersKamikazeAICapabilityUCMutSet) {
        result.Add(
            new KamikazeAICapabilityUCAsIDeathPreReactor(obj));
      }
      return result;
    }
    public List<IDeathPreReactor> ClearAllIDeathPreReactor() {
      var result = new List<IDeathPreReactor>();
      this.membersKamikazeAICapabilityUCMutSet.Clear();
      return result;
    }
    public IDeathPreReactor GetOnlyIDeathPreReactorOrNull() {
      var result = GetAllIDeathPreReactor();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIDeathPreReactor.Null;
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
      foreach (var obj in this.membersSlowRodMutSet) {
        result.Add(
            new SlowRodAsIItem(obj));
      }
      foreach (var obj in this.membersBlastRodMutSet) {
        result.Add(
            new BlastRodAsIItem(obj));
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
      this.membersSlowRodMutSet.Clear();
      this.membersBlastRodMutSet.Clear();
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
