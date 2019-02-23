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
  public bool Is(IUnitComponentMutBunch that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
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
  public AttackAICapabilityUCMutSet membersAttackAICapabilityUCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersAttackAICapabilityUCMutSet of null!");
      }
      return new AttackAICapabilityUCMutSet(root, incarnation.membersAttackAICapabilityUCMutSet);
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
      root.EffectKillDirectiveUCMutSetCreate()
,
      root.EffectMoveDirectiveUCMutSetCreate()
,
      root.EffectWanderAICapabilityUCMutSetCreate()
,
      root.EffectBideAICapabilityUCMutSetCreate()
,
      root.EffectAttackAICapabilityUCMutSetCreate()
,
      root.EffectShieldingUCMutSetCreate()
,
      root.EffectBidingOperationUCMutSetCreate()
        );
  }
  public void Add(IUnitComponent elementI) {

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
    if (root.AttackAICapabilityUCExists(elementI.id)) {
      this.membersAttackAICapabilityUCMutSet.Add(root.GetAttackAICapabilityUC(elementI.id));
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
    if (root.AttackAICapabilityUCExists(elementI.id)) {
      this.membersAttackAICapabilityUCMutSet.Remove(root.GetAttackAICapabilityUC(elementI.id));
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
    this.membersKillDirectiveUCMutSet.Clear();
    this.membersMoveDirectiveUCMutSet.Clear();
    this.membersWanderAICapabilityUCMutSet.Clear();
    this.membersBideAICapabilityUCMutSet.Clear();
    this.membersAttackAICapabilityUCMutSet.Clear();
    this.membersShieldingUCMutSet.Clear();
    this.membersBidingOperationUCMutSet.Clear();
  }
  public int Count {
    get {
      return
        this.membersKillDirectiveUCMutSet.Count +
        this.membersMoveDirectiveUCMutSet.Count +
        this.membersWanderAICapabilityUCMutSet.Count +
        this.membersBideAICapabilityUCMutSet.Count +
        this.membersAttackAICapabilityUCMutSet.Count +
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

  public IEnumerator<IUnitComponent> GetEnumerator() {
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
    foreach (var element in this.membersAttackAICapabilityUCMutSet) {
      yield return new AttackAICapabilityUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersShieldingUCMutSet) {
      yield return new ShieldingUCAsIUnitComponent(element);
    }
    foreach (var element in this.membersBidingOperationUCMutSet) {
      yield return new BidingOperationUCAsIUnitComponent(element);
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
      if (result.Count > 0) {
        return result[0];
      } else {
        return BideAICapabilityUC.Null;
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
      if (result.Count > 0) {
        return result[0];
      } else {
        return BidingOperationUC.Null;
      }
    }
    public List<IAICapabilityUC> GetAllIAICapabilityUC() {
      var result = new List<IAICapabilityUC>();
      foreach (var obj in this.membersWanderAICapabilityUCMutSet) {
        result.Add(
            new WanderAICapabilityUCAsIAICapabilityUC(obj));
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
      this.membersBideAICapabilityUCMutSet.Clear();
      this.membersAttackAICapabilityUCMutSet.Clear();
      return result;
    }
    public IAICapabilityUC GetOnlyIAICapabilityUCOrNull() {
      var result = GetAllIAICapabilityUC();
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
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIOperationUC.Null;
      }
    }
                 public List<IPreActingUC> GetAllIPreActingUC() {
      var result = new List<IPreActingUC>();
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
      this.membersShieldingUCMutSet.Clear();
      this.membersAttackAICapabilityUCMutSet.Clear();
      return result;
    }
    public IPreActingUC GetOnlyIPreActingUCOrNull() {
      var result = GetAllIPreActingUC();
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIPreActingUC.Null;
      }
    }
                 public List<IPostActingUC> GetAllIPostActingUC() {
      var result = new List<IPostActingUC>();
      foreach (var obj in this.membersShieldingUCMutSet) {
        result.Add(
            new ShieldingUCAsIPostActingUC(obj));
      }
      return result;
    }
    public List<IPostActingUC> ClearAllIPostActingUC() {
      var result = new List<IPostActingUC>();
      this.membersShieldingUCMutSet.Clear();
      return result;
    }
    public IPostActingUC GetOnlyIPostActingUCOrNull() {
      var result = GetAllIPostActingUC();
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
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIDefenseUC.Null;
      }
    }
                 public List<IDirectiveUC> GetAllIDirectiveUC() {
      var result = new List<IDirectiveUC>();
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
      this.membersKillDirectiveUCMutSet.Clear();
      this.membersMoveDirectiveUCMutSet.Clear();
      return result;
    }
    public IDirectiveUC GetOnlyIDirectiveUCOrNull() {
      var result = GetAllIDirectiveUC();
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIDirectiveUC.Null;
      }
    }
             }
}
