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

    if (!root.GlaiveMutSetExists(membersGlaiveMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersGlaiveMutSet");
    }

    if (!root.ManaPotionMutSetExists(membersManaPotionMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersManaPotionMutSet");
    }

    if (!root.HealthPotionMutSetExists(membersHealthPotionMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersHealthPotionMutSet");
    }

    if (!root.TimeScriptDirectiveUCMutSetExists(membersTimeScriptDirectiveUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersTimeScriptDirectiveUCMutSet");
    }

    if (!root.KillDirectiveUCMutSetExists(membersKillDirectiveUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersKillDirectiveUCMutSet");
    }

    if (!root.MoveDirectiveUCMutSetExists(membersMoveDirectiveUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersMoveDirectiveUCMutSet");
    }

    if (!root.WanderAICapabilityUCMutSetExists(membersWanderAICapabilityUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersWanderAICapabilityUCMutSet");
    }

    if (!root.BideAICapabilityUCMutSetExists(membersBideAICapabilityUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersBideAICapabilityUCMutSet");
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

    if (!root.BidingOperationUCMutSetExists(membersBidingOperationUCMutSet.id)) {
      violations.Add("Null constraint violated! IUnitComponentMutBunch#" + id + ".membersBidingOperationUCMutSet");
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
    if (root.GlaiveMutSetExists(membersGlaiveMutSet.id)) {
      membersGlaiveMutSet.FindReachableObjects(foundIds);
    }
    if (root.ManaPotionMutSetExists(membersManaPotionMutSet.id)) {
      membersManaPotionMutSet.FindReachableObjects(foundIds);
    }
    if (root.HealthPotionMutSetExists(membersHealthPotionMutSet.id)) {
      membersHealthPotionMutSet.FindReachableObjects(foundIds);
    }
    if (root.TimeScriptDirectiveUCMutSetExists(membersTimeScriptDirectiveUCMutSet.id)) {
      membersTimeScriptDirectiveUCMutSet.FindReachableObjects(foundIds);
    }
    if (root.KillDirectiveUCMutSetExists(membersKillDirectiveUCMutSet.id)) {
      membersKillDirectiveUCMutSet.FindReachableObjects(foundIds);
    }
    if (root.MoveDirectiveUCMutSetExists(membersMoveDirectiveUCMutSet.id)) {
      membersMoveDirectiveUCMutSet.FindReachableObjects(foundIds);
    }
    if (root.WanderAICapabilityUCMutSetExists(membersWanderAICapabilityUCMutSet.id)) {
      membersWanderAICapabilityUCMutSet.FindReachableObjects(foundIds);
    }
    if (root.BideAICapabilityUCMutSetExists(membersBideAICapabilityUCMutSet.id)) {
      membersBideAICapabilityUCMutSet.FindReachableObjects(foundIds);
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
    if (root.BidingOperationUCMutSetExists(membersBidingOperationUCMutSet.id)) {
      membersBidingOperationUCMutSet.FindReachableObjects(foundIds);
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
  public TimeScriptDirectiveUCMutSet membersTimeScriptDirectiveUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersTimeScriptDirectiveUCMutSet of null!");
      }
      return new TimeScriptDirectiveUCMutSet(root, incarnation.membersTimeScriptDirectiveUCMutSet);
    }
                       }
  public KillDirectiveUCMutSet membersKillDirectiveUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersKillDirectiveUCMutSet of null!");
      }
      return new KillDirectiveUCMutSet(root, incarnation.membersKillDirectiveUCMutSet);
    }
                       }
  public MoveDirectiveUCMutSet membersMoveDirectiveUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersMoveDirectiveUCMutSet of null!");
      }
      return new MoveDirectiveUCMutSet(root, incarnation.membersMoveDirectiveUCMutSet);
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
  public BideAICapabilityUCMutSet membersBideAICapabilityUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersBideAICapabilityUCMutSet of null!");
      }
      return new BideAICapabilityUCMutSet(root, incarnation.membersBideAICapabilityUCMutSet);
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
  public BidingOperationUCMutSet membersBidingOperationUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersBidingOperationUCMutSet of null!");
      }
      return new BidingOperationUCMutSet(root, incarnation.membersBidingOperationUCMutSet);
    }
                       }

  public static IUnitComponentMutBunch New(Root root) {
    return root.EffectIUnitComponentMutBunchCreate(
      root.EffectArmorMutSetCreate()
,
      root.EffectGlaiveMutSetCreate()
,
      root.EffectManaPotionMutSetCreate()
,
      root.EffectHealthPotionMutSetCreate()
,
      root.EffectTimeScriptDirectiveUCMutSetCreate()
,
      root.EffectKillDirectiveUCMutSetCreate()
,
      root.EffectMoveDirectiveUCMutSetCreate()
,
      root.EffectWanderAICapabilityUCMutSetCreate()
,
      root.EffectBideAICapabilityUCMutSetCreate()
,
      root.EffectTimeCloneAICapabilityUCMutSetCreate()
,
      root.EffectAttackAICapabilityUCMutSetCreate()
,
      root.EffectCounteringUCMutSetCreate()
,
      root.EffectShieldingUCMutSetCreate()
,
      root.EffectBidingOperationUCMutSetCreate()
        );
  }
  public void Add(IUnitComponent elementI) {

    // Can optimize, check the type of element directly somehow
    if (root.ArmorExists(elementI.id)) {
      this.membersArmorMutSet.Add(root.GetArmor(elementI.id));
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
    if (root.TimeScriptDirectiveUCExists(elementI.id)) {
      this.membersTimeScriptDirectiveUCMutSet.Add(root.GetTimeScriptDirectiveUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.KillDirectiveUCExists(elementI.id)) {
      this.membersKillDirectiveUCMutSet.Add(root.GetKillDirectiveUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.MoveDirectiveUCExists(elementI.id)) {
      this.membersMoveDirectiveUCMutSet.Add(root.GetMoveDirectiveUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.WanderAICapabilityUCExists(elementI.id)) {
      this.membersWanderAICapabilityUCMutSet.Add(root.GetWanderAICapabilityUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.BideAICapabilityUCExists(elementI.id)) {
      this.membersBideAICapabilityUCMutSet.Add(root.GetBideAICapabilityUC(elementI.id));
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
    if (root.BidingOperationUCExists(elementI.id)) {
      this.membersBidingOperationUCMutSet.Add(root.GetBidingOperationUC(elementI.id));
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
    if (root.TimeScriptDirectiveUCExists(elementI.id)) {
      this.membersTimeScriptDirectiveUCMutSet.Remove(root.GetTimeScriptDirectiveUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.KillDirectiveUCExists(elementI.id)) {
      this.membersKillDirectiveUCMutSet.Remove(root.GetKillDirectiveUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.MoveDirectiveUCExists(elementI.id)) {
      this.membersMoveDirectiveUCMutSet.Remove(root.GetMoveDirectiveUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.WanderAICapabilityUCExists(elementI.id)) {
      this.membersWanderAICapabilityUCMutSet.Remove(root.GetWanderAICapabilityUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.BideAICapabilityUCExists(elementI.id)) {
      this.membersBideAICapabilityUCMutSet.Remove(root.GetBideAICapabilityUC(elementI.id));
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
    if (root.BidingOperationUCExists(elementI.id)) {
      this.membersBidingOperationUCMutSet.Remove(root.GetBidingOperationUC(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Clear() {
    this.membersArmorMutSet.Clear();
    this.membersGlaiveMutSet.Clear();
    this.membersManaPotionMutSet.Clear();
    this.membersHealthPotionMutSet.Clear();
    this.membersTimeScriptDirectiveUCMutSet.Clear();
    this.membersKillDirectiveUCMutSet.Clear();
    this.membersMoveDirectiveUCMutSet.Clear();
    this.membersWanderAICapabilityUCMutSet.Clear();
    this.membersBideAICapabilityUCMutSet.Clear();
    this.membersTimeCloneAICapabilityUCMutSet.Clear();
    this.membersAttackAICapabilityUCMutSet.Clear();
    this.membersCounteringUCMutSet.Clear();
    this.membersShieldingUCMutSet.Clear();
    this.membersBidingOperationUCMutSet.Clear();
  }
  public int Count {
    get {
      return
        this.membersArmorMutSet.Count +
        this.membersGlaiveMutSet.Count +
        this.membersManaPotionMutSet.Count +
        this.membersHealthPotionMutSet.Count +
        this.membersTimeScriptDirectiveUCMutSet.Count +
        this.membersKillDirectiveUCMutSet.Count +
        this.membersMoveDirectiveUCMutSet.Count +
        this.membersWanderAICapabilityUCMutSet.Count +
        this.membersBideAICapabilityUCMutSet.Count +
        this.membersTimeCloneAICapabilityUCMutSet.Count +
        this.membersAttackAICapabilityUCMutSet.Count +
        this.membersCounteringUCMutSet.Count +
        this.membersShieldingUCMutSet.Count +
        this.membersBidingOperationUCMutSet.Count
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
    var tempMembersGlaiveMutSet = this.membersGlaiveMutSet;
    var tempMembersManaPotionMutSet = this.membersManaPotionMutSet;
    var tempMembersHealthPotionMutSet = this.membersHealthPotionMutSet;
    var tempMembersTimeScriptDirectiveUCMutSet = this.membersTimeScriptDirectiveUCMutSet;
    var tempMembersKillDirectiveUCMutSet = this.membersKillDirectiveUCMutSet;
    var tempMembersMoveDirectiveUCMutSet = this.membersMoveDirectiveUCMutSet;
    var tempMembersWanderAICapabilityUCMutSet = this.membersWanderAICapabilityUCMutSet;
    var tempMembersBideAICapabilityUCMutSet = this.membersBideAICapabilityUCMutSet;
    var tempMembersTimeCloneAICapabilityUCMutSet = this.membersTimeCloneAICapabilityUCMutSet;
    var tempMembersAttackAICapabilityUCMutSet = this.membersAttackAICapabilityUCMutSet;
    var tempMembersCounteringUCMutSet = this.membersCounteringUCMutSet;
    var tempMembersShieldingUCMutSet = this.membersShieldingUCMutSet;
    var tempMembersBidingOperationUCMutSet = this.membersBidingOperationUCMutSet;

    this.Delete();
    tempMembersArmorMutSet.Destruct();
    tempMembersGlaiveMutSet.Destruct();
    tempMembersManaPotionMutSet.Destruct();
    tempMembersHealthPotionMutSet.Destruct();
    tempMembersTimeScriptDirectiveUCMutSet.Destruct();
    tempMembersKillDirectiveUCMutSet.Destruct();
    tempMembersMoveDirectiveUCMutSet.Destruct();
    tempMembersWanderAICapabilityUCMutSet.Destruct();
    tempMembersBideAICapabilityUCMutSet.Destruct();
    tempMembersTimeCloneAICapabilityUCMutSet.Destruct();
    tempMembersAttackAICapabilityUCMutSet.Destruct();
    tempMembersCounteringUCMutSet.Destruct();
    tempMembersShieldingUCMutSet.Destruct();
    tempMembersBidingOperationUCMutSet.Destruct();
  }
  public IEnumerator<IUnitComponent> GetEnumerator() {
    foreach (var element in this.membersArmorMutSet) {
      yield return new ArmorAsIUnitComponent(element);
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
    foreach (var element in this.membersTimeScriptDirectiveUCMutSet) {
      yield return new TimeScriptDirectiveUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersKillDirectiveUCMutSet) {
      yield return new KillDirectiveUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersMoveDirectiveUCMutSet) {
      yield return new MoveDirectiveUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersWanderAICapabilityUCMutSet) {
      yield return new WanderAICapabilityUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersBideAICapabilityUCMutSet) {
      yield return new BideAICapabilityUCAsIUnitComponent(element);
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
    foreach (var element in this.membersBidingOperationUCMutSet) {
      yield return new BidingOperationUCAsIUnitComponent(element);
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
    public List<TimeScriptDirectiveUC> GetAllTimeScriptDirectiveUC() {
      var result = new List<TimeScriptDirectiveUC>();
      foreach (var thing in this.membersTimeScriptDirectiveUCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<TimeScriptDirectiveUC> ClearAllTimeScriptDirectiveUC() {
      var result = new List<TimeScriptDirectiveUC>();
      this.membersTimeScriptDirectiveUCMutSet.Clear();
      return result;
    }
    public TimeScriptDirectiveUC GetOnlyTimeScriptDirectiveUCOrNull() {
      var result = GetAllTimeScriptDirectiveUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return TimeScriptDirectiveUC.Null;
      }
    }
    public List<KillDirectiveUC> GetAllKillDirectiveUC() {
      var result = new List<KillDirectiveUC>();
      foreach (var thing in this.membersKillDirectiveUCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<KillDirectiveUC> ClearAllKillDirectiveUC() {
      var result = new List<KillDirectiveUC>();
      this.membersKillDirectiveUCMutSet.Clear();
      return result;
    }
    public KillDirectiveUC GetOnlyKillDirectiveUCOrNull() {
      var result = GetAllKillDirectiveUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return KillDirectiveUC.Null;
      }
    }
    public List<MoveDirectiveUC> GetAllMoveDirectiveUC() {
      var result = new List<MoveDirectiveUC>();
      foreach (var thing in this.membersMoveDirectiveUCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<MoveDirectiveUC> ClearAllMoveDirectiveUC() {
      var result = new List<MoveDirectiveUC>();
      this.membersMoveDirectiveUCMutSet.Clear();
      return result;
    }
    public MoveDirectiveUC GetOnlyMoveDirectiveUCOrNull() {
      var result = GetAllMoveDirectiveUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return MoveDirectiveUC.Null;
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
    public List<BidingOperationUC> GetAllBidingOperationUC() {
      var result = new List<BidingOperationUC>();
      foreach (var thing in this.membersBidingOperationUCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<BidingOperationUC> ClearAllBidingOperationUC() {
      var result = new List<BidingOperationUC>();
      this.membersBidingOperationUCMutSet.Clear();
      return result;
    }
    public BidingOperationUC GetOnlyBidingOperationUCOrNull() {
      var result = GetAllBidingOperationUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return BidingOperationUC.Null;
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
                 public List<IOperationUC> GetAllIOperationUC() {
      var result = new List<IOperationUC>();
      foreach (var obj in this.membersBidingOperationUCMutSet) {
        result.Add(
            new BidingOperationUCAsIOperationUC(obj));
      }
      return result;
    }
    public List<IOperationUC> ClearAllIOperationUC() {
      var result = new List<IOperationUC>();
      this.membersBidingOperationUCMutSet.Clear();
      return result;
    }
    public IOperationUC GetOnlyIOperationUCOrNull() {
      var result = GetAllIOperationUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIOperationUC.Null;
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
                 public List<IDefenseUC> GetAllIDefenseUC() {
      var result = new List<IDefenseUC>();
      foreach (var obj in this.membersShieldingUCMutSet) {
        result.Add(
            new ShieldingUCAsIDefenseUC(obj));
      }
      foreach (var obj in this.membersBidingOperationUCMutSet) {
        result.Add(
            new BidingOperationUCAsIDefenseUC(obj));
      }
      return result;
    }
    public List<IDefenseUC> ClearAllIDefenseUC() {
      var result = new List<IDefenseUC>();
      this.membersShieldingUCMutSet.Clear();
      this.membersBidingOperationUCMutSet.Clear();
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
                 public List<IDirectiveUC> GetAllIDirectiveUC() {
      var result = new List<IDirectiveUC>();
      foreach (var obj in this.membersTimeScriptDirectiveUCMutSet) {
        result.Add(
            new TimeScriptDirectiveUCAsIDirectiveUC(obj));
      }
      foreach (var obj in this.membersKillDirectiveUCMutSet) {
        result.Add(
            new KillDirectiveUCAsIDirectiveUC(obj));
      }
      foreach (var obj in this.membersMoveDirectiveUCMutSet) {
        result.Add(
            new MoveDirectiveUCAsIDirectiveUC(obj));
      }
      return result;
    }
    public List<IDirectiveUC> ClearAllIDirectiveUC() {
      var result = new List<IDirectiveUC>();
      this.membersTimeScriptDirectiveUCMutSet.Clear();
      this.membersKillDirectiveUCMutSet.Clear();
      this.membersMoveDirectiveUCMutSet.Clear();
      return result;
    }
    public IDirectiveUC GetOnlyIDirectiveUCOrNull() {
      var result = GetAllIDirectiveUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIDirectiveUC.Null;
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
