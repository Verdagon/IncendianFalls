using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class IImpulseStrongMutBunch {
  public readonly Root root;
  public readonly int id;
  public IImpulseStrongMutBunch(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public IImpulseStrongMutBunchIncarnation incarnation { get { return root.GetIImpulseStrongMutBunchIncarnation(id); } }
  public void AddObserver(IIImpulseStrongMutBunchEffectObserver observer) {
    root.AddIImpulseStrongMutBunchObserver(id, observer);
  }
  public void RemoveObserver(IIImpulseStrongMutBunchEffectObserver observer) {
    root.RemoveIImpulseStrongMutBunchObserver(id, observer);
  }
  public void Delete() {
    root.EffectIImpulseStrongMutBunchDelete(id);
  }
  public static IImpulseStrongMutBunch Null = new IImpulseStrongMutBunch(null, 0);
  public bool Exists() { return root != null && root.IImpulseStrongMutBunchExists(id); }
  public bool NullableIs(IImpulseStrongMutBunch that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public void CheckForNullViolations(List<string> violations) {

    if (!root.TemporaryCloneImpulseStrongMutSetExists(membersTemporaryCloneImpulseStrongMutSet.id)) {
      violations.Add("Null constraint violated! IImpulseStrongMutBunch#" + id + ".membersTemporaryCloneImpulseStrongMutSet");
    }

    if (!root.SummonImpulseStrongMutSetExists(membersSummonImpulseStrongMutSet.id)) {
      violations.Add("Null constraint violated! IImpulseStrongMutBunch#" + id + ".membersSummonImpulseStrongMutSet");
    }

    if (!root.MireImpulseStrongMutSetExists(membersMireImpulseStrongMutSet.id)) {
      violations.Add("Null constraint violated! IImpulseStrongMutBunch#" + id + ".membersMireImpulseStrongMutSet");
    }

    if (!root.EvaporateImpulseStrongMutSetExists(membersEvaporateImpulseStrongMutSet.id)) {
      violations.Add("Null constraint violated! IImpulseStrongMutBunch#" + id + ".membersEvaporateImpulseStrongMutSet");
    }

    if (!root.MoveImpulseStrongMutSetExists(membersMoveImpulseStrongMutSet.id)) {
      violations.Add("Null constraint violated! IImpulseStrongMutBunch#" + id + ".membersMoveImpulseStrongMutSet");
    }

    if (!root.KamikazeJumpImpulseStrongMutSetExists(membersKamikazeJumpImpulseStrongMutSet.id)) {
      violations.Add("Null constraint violated! IImpulseStrongMutBunch#" + id + ".membersKamikazeJumpImpulseStrongMutSet");
    }

    if (!root.KamikazeTargetImpulseStrongMutSetExists(membersKamikazeTargetImpulseStrongMutSet.id)) {
      violations.Add("Null constraint violated! IImpulseStrongMutBunch#" + id + ".membersKamikazeTargetImpulseStrongMutSet");
    }

    if (!root.NoImpulseStrongMutSetExists(membersNoImpulseStrongMutSet.id)) {
      violations.Add("Null constraint violated! IImpulseStrongMutBunch#" + id + ".membersNoImpulseStrongMutSet");
    }

    if (!root.FireImpulseStrongMutSetExists(membersFireImpulseStrongMutSet.id)) {
      violations.Add("Null constraint violated! IImpulseStrongMutBunch#" + id + ".membersFireImpulseStrongMutSet");
    }

    if (!root.DefyImpulseStrongMutSetExists(membersDefyImpulseStrongMutSet.id)) {
      violations.Add("Null constraint violated! IImpulseStrongMutBunch#" + id + ".membersDefyImpulseStrongMutSet");
    }

    if (!root.CounterImpulseStrongMutSetExists(membersCounterImpulseStrongMutSet.id)) {
      violations.Add("Null constraint violated! IImpulseStrongMutBunch#" + id + ".membersCounterImpulseStrongMutSet");
    }

    if (!root.UnleashBideImpulseStrongMutSetExists(membersUnleashBideImpulseStrongMutSet.id)) {
      violations.Add("Null constraint violated! IImpulseStrongMutBunch#" + id + ".membersUnleashBideImpulseStrongMutSet");
    }

    if (!root.ContinueBidingImpulseStrongMutSetExists(membersContinueBidingImpulseStrongMutSet.id)) {
      violations.Add("Null constraint violated! IImpulseStrongMutBunch#" + id + ".membersContinueBidingImpulseStrongMutSet");
    }

    if (!root.StartBidingImpulseStrongMutSetExists(membersStartBidingImpulseStrongMutSet.id)) {
      violations.Add("Null constraint violated! IImpulseStrongMutBunch#" + id + ".membersStartBidingImpulseStrongMutSet");
    }

    if (!root.AttackImpulseStrongMutSetExists(membersAttackImpulseStrongMutSet.id)) {
      violations.Add("Null constraint violated! IImpulseStrongMutBunch#" + id + ".membersAttackImpulseStrongMutSet");
    }

    if (!root.PursueImpulseStrongMutSetExists(membersPursueImpulseStrongMutSet.id)) {
      violations.Add("Null constraint violated! IImpulseStrongMutBunch#" + id + ".membersPursueImpulseStrongMutSet");
    }

    if (!root.FireBombImpulseStrongMutSetExists(membersFireBombImpulseStrongMutSet.id)) {
      violations.Add("Null constraint violated! IImpulseStrongMutBunch#" + id + ".membersFireBombImpulseStrongMutSet");
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    if (root.TemporaryCloneImpulseStrongMutSetExists(membersTemporaryCloneImpulseStrongMutSet.id)) {
      membersTemporaryCloneImpulseStrongMutSet.FindReachableObjects(foundIds);
    }
    if (root.SummonImpulseStrongMutSetExists(membersSummonImpulseStrongMutSet.id)) {
      membersSummonImpulseStrongMutSet.FindReachableObjects(foundIds);
    }
    if (root.MireImpulseStrongMutSetExists(membersMireImpulseStrongMutSet.id)) {
      membersMireImpulseStrongMutSet.FindReachableObjects(foundIds);
    }
    if (root.EvaporateImpulseStrongMutSetExists(membersEvaporateImpulseStrongMutSet.id)) {
      membersEvaporateImpulseStrongMutSet.FindReachableObjects(foundIds);
    }
    if (root.MoveImpulseStrongMutSetExists(membersMoveImpulseStrongMutSet.id)) {
      membersMoveImpulseStrongMutSet.FindReachableObjects(foundIds);
    }
    if (root.KamikazeJumpImpulseStrongMutSetExists(membersKamikazeJumpImpulseStrongMutSet.id)) {
      membersKamikazeJumpImpulseStrongMutSet.FindReachableObjects(foundIds);
    }
    if (root.KamikazeTargetImpulseStrongMutSetExists(membersKamikazeTargetImpulseStrongMutSet.id)) {
      membersKamikazeTargetImpulseStrongMutSet.FindReachableObjects(foundIds);
    }
    if (root.NoImpulseStrongMutSetExists(membersNoImpulseStrongMutSet.id)) {
      membersNoImpulseStrongMutSet.FindReachableObjects(foundIds);
    }
    if (root.FireImpulseStrongMutSetExists(membersFireImpulseStrongMutSet.id)) {
      membersFireImpulseStrongMutSet.FindReachableObjects(foundIds);
    }
    if (root.DefyImpulseStrongMutSetExists(membersDefyImpulseStrongMutSet.id)) {
      membersDefyImpulseStrongMutSet.FindReachableObjects(foundIds);
    }
    if (root.CounterImpulseStrongMutSetExists(membersCounterImpulseStrongMutSet.id)) {
      membersCounterImpulseStrongMutSet.FindReachableObjects(foundIds);
    }
    if (root.UnleashBideImpulseStrongMutSetExists(membersUnleashBideImpulseStrongMutSet.id)) {
      membersUnleashBideImpulseStrongMutSet.FindReachableObjects(foundIds);
    }
    if (root.ContinueBidingImpulseStrongMutSetExists(membersContinueBidingImpulseStrongMutSet.id)) {
      membersContinueBidingImpulseStrongMutSet.FindReachableObjects(foundIds);
    }
    if (root.StartBidingImpulseStrongMutSetExists(membersStartBidingImpulseStrongMutSet.id)) {
      membersStartBidingImpulseStrongMutSet.FindReachableObjects(foundIds);
    }
    if (root.AttackImpulseStrongMutSetExists(membersAttackImpulseStrongMutSet.id)) {
      membersAttackImpulseStrongMutSet.FindReachableObjects(foundIds);
    }
    if (root.PursueImpulseStrongMutSetExists(membersPursueImpulseStrongMutSet.id)) {
      membersPursueImpulseStrongMutSet.FindReachableObjects(foundIds);
    }
    if (root.FireBombImpulseStrongMutSetExists(membersFireBombImpulseStrongMutSet.id)) {
      membersFireBombImpulseStrongMutSet.FindReachableObjects(foundIds);
    }
  }
  public bool Is(IImpulseStrongMutBunch that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public TemporaryCloneImpulseStrongMutSet membersTemporaryCloneImpulseStrongMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersTemporaryCloneImpulseStrongMutSet of null!");
      }
      return new TemporaryCloneImpulseStrongMutSet(root, incarnation.membersTemporaryCloneImpulseStrongMutSet);
    }
                       }
  public SummonImpulseStrongMutSet membersSummonImpulseStrongMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersSummonImpulseStrongMutSet of null!");
      }
      return new SummonImpulseStrongMutSet(root, incarnation.membersSummonImpulseStrongMutSet);
    }
                       }
  public MireImpulseStrongMutSet membersMireImpulseStrongMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersMireImpulseStrongMutSet of null!");
      }
      return new MireImpulseStrongMutSet(root, incarnation.membersMireImpulseStrongMutSet);
    }
                       }
  public EvaporateImpulseStrongMutSet membersEvaporateImpulseStrongMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersEvaporateImpulseStrongMutSet of null!");
      }
      return new EvaporateImpulseStrongMutSet(root, incarnation.membersEvaporateImpulseStrongMutSet);
    }
                       }
  public MoveImpulseStrongMutSet membersMoveImpulseStrongMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersMoveImpulseStrongMutSet of null!");
      }
      return new MoveImpulseStrongMutSet(root, incarnation.membersMoveImpulseStrongMutSet);
    }
                       }
  public KamikazeJumpImpulseStrongMutSet membersKamikazeJumpImpulseStrongMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersKamikazeJumpImpulseStrongMutSet of null!");
      }
      return new KamikazeJumpImpulseStrongMutSet(root, incarnation.membersKamikazeJumpImpulseStrongMutSet);
    }
                       }
  public KamikazeTargetImpulseStrongMutSet membersKamikazeTargetImpulseStrongMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersKamikazeTargetImpulseStrongMutSet of null!");
      }
      return new KamikazeTargetImpulseStrongMutSet(root, incarnation.membersKamikazeTargetImpulseStrongMutSet);
    }
                       }
  public NoImpulseStrongMutSet membersNoImpulseStrongMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersNoImpulseStrongMutSet of null!");
      }
      return new NoImpulseStrongMutSet(root, incarnation.membersNoImpulseStrongMutSet);
    }
                       }
  public FireImpulseStrongMutSet membersFireImpulseStrongMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersFireImpulseStrongMutSet of null!");
      }
      return new FireImpulseStrongMutSet(root, incarnation.membersFireImpulseStrongMutSet);
    }
                       }
  public DefyImpulseStrongMutSet membersDefyImpulseStrongMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersDefyImpulseStrongMutSet of null!");
      }
      return new DefyImpulseStrongMutSet(root, incarnation.membersDefyImpulseStrongMutSet);
    }
                       }
  public CounterImpulseStrongMutSet membersCounterImpulseStrongMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersCounterImpulseStrongMutSet of null!");
      }
      return new CounterImpulseStrongMutSet(root, incarnation.membersCounterImpulseStrongMutSet);
    }
                       }
  public UnleashBideImpulseStrongMutSet membersUnleashBideImpulseStrongMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersUnleashBideImpulseStrongMutSet of null!");
      }
      return new UnleashBideImpulseStrongMutSet(root, incarnation.membersUnleashBideImpulseStrongMutSet);
    }
                       }
  public ContinueBidingImpulseStrongMutSet membersContinueBidingImpulseStrongMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersContinueBidingImpulseStrongMutSet of null!");
      }
      return new ContinueBidingImpulseStrongMutSet(root, incarnation.membersContinueBidingImpulseStrongMutSet);
    }
                       }
  public StartBidingImpulseStrongMutSet membersStartBidingImpulseStrongMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersStartBidingImpulseStrongMutSet of null!");
      }
      return new StartBidingImpulseStrongMutSet(root, incarnation.membersStartBidingImpulseStrongMutSet);
    }
                       }
  public AttackImpulseStrongMutSet membersAttackImpulseStrongMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersAttackImpulseStrongMutSet of null!");
      }
      return new AttackImpulseStrongMutSet(root, incarnation.membersAttackImpulseStrongMutSet);
    }
                       }
  public PursueImpulseStrongMutSet membersPursueImpulseStrongMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersPursueImpulseStrongMutSet of null!");
      }
      return new PursueImpulseStrongMutSet(root, incarnation.membersPursueImpulseStrongMutSet);
    }
                       }
  public FireBombImpulseStrongMutSet membersFireBombImpulseStrongMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersFireBombImpulseStrongMutSet of null!");
      }
      return new FireBombImpulseStrongMutSet(root, incarnation.membersFireBombImpulseStrongMutSet);
    }
                       }

  public static IImpulseStrongMutBunch New(Root root) {
    return root.EffectIImpulseStrongMutBunchCreate(
      root.EffectTemporaryCloneImpulseStrongMutSetCreate()
,
      root.EffectSummonImpulseStrongMutSetCreate()
,
      root.EffectMireImpulseStrongMutSetCreate()
,
      root.EffectEvaporateImpulseStrongMutSetCreate()
,
      root.EffectMoveImpulseStrongMutSetCreate()
,
      root.EffectKamikazeJumpImpulseStrongMutSetCreate()
,
      root.EffectKamikazeTargetImpulseStrongMutSetCreate()
,
      root.EffectNoImpulseStrongMutSetCreate()
,
      root.EffectFireImpulseStrongMutSetCreate()
,
      root.EffectDefyImpulseStrongMutSetCreate()
,
      root.EffectCounterImpulseStrongMutSetCreate()
,
      root.EffectUnleashBideImpulseStrongMutSetCreate()
,
      root.EffectContinueBidingImpulseStrongMutSetCreate()
,
      root.EffectStartBidingImpulseStrongMutSetCreate()
,
      root.EffectAttackImpulseStrongMutSetCreate()
,
      root.EffectPursueImpulseStrongMutSetCreate()
,
      root.EffectFireBombImpulseStrongMutSetCreate()
        );
  }
  public void Add(IImpulse elementI) {

    // Can optimize, check the type of element directly somehow
    if (root.TemporaryCloneImpulseExists(elementI.id)) {
      this.membersTemporaryCloneImpulseStrongMutSet.Add(root.GetTemporaryCloneImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.SummonImpulseExists(elementI.id)) {
      this.membersSummonImpulseStrongMutSet.Add(root.GetSummonImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.MireImpulseExists(elementI.id)) {
      this.membersMireImpulseStrongMutSet.Add(root.GetMireImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.EvaporateImpulseExists(elementI.id)) {
      this.membersEvaporateImpulseStrongMutSet.Add(root.GetEvaporateImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.MoveImpulseExists(elementI.id)) {
      this.membersMoveImpulseStrongMutSet.Add(root.GetMoveImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.KamikazeJumpImpulseExists(elementI.id)) {
      this.membersKamikazeJumpImpulseStrongMutSet.Add(root.GetKamikazeJumpImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.KamikazeTargetImpulseExists(elementI.id)) {
      this.membersKamikazeTargetImpulseStrongMutSet.Add(root.GetKamikazeTargetImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.NoImpulseExists(elementI.id)) {
      this.membersNoImpulseStrongMutSet.Add(root.GetNoImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.FireImpulseExists(elementI.id)) {
      this.membersFireImpulseStrongMutSet.Add(root.GetFireImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.DefyImpulseExists(elementI.id)) {
      this.membersDefyImpulseStrongMutSet.Add(root.GetDefyImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.CounterImpulseExists(elementI.id)) {
      this.membersCounterImpulseStrongMutSet.Add(root.GetCounterImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.UnleashBideImpulseExists(elementI.id)) {
      this.membersUnleashBideImpulseStrongMutSet.Add(root.GetUnleashBideImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.ContinueBidingImpulseExists(elementI.id)) {
      this.membersContinueBidingImpulseStrongMutSet.Add(root.GetContinueBidingImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.StartBidingImpulseExists(elementI.id)) {
      this.membersStartBidingImpulseStrongMutSet.Add(root.GetStartBidingImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.AttackImpulseExists(elementI.id)) {
      this.membersAttackImpulseStrongMutSet.Add(root.GetAttackImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.PursueImpulseExists(elementI.id)) {
      this.membersPursueImpulseStrongMutSet.Add(root.GetPursueImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.FireBombImpulseExists(elementI.id)) {
      this.membersFireBombImpulseStrongMutSet.Add(root.GetFireBombImpulse(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Remove(IImpulse elementI) {

    // Can optimize, check the type of element directly somehow
    if (root.TemporaryCloneImpulseExists(elementI.id)) {
      this.membersTemporaryCloneImpulseStrongMutSet.Remove(root.GetTemporaryCloneImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.SummonImpulseExists(elementI.id)) {
      this.membersSummonImpulseStrongMutSet.Remove(root.GetSummonImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.MireImpulseExists(elementI.id)) {
      this.membersMireImpulseStrongMutSet.Remove(root.GetMireImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.EvaporateImpulseExists(elementI.id)) {
      this.membersEvaporateImpulseStrongMutSet.Remove(root.GetEvaporateImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.MoveImpulseExists(elementI.id)) {
      this.membersMoveImpulseStrongMutSet.Remove(root.GetMoveImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.KamikazeJumpImpulseExists(elementI.id)) {
      this.membersKamikazeJumpImpulseStrongMutSet.Remove(root.GetKamikazeJumpImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.KamikazeTargetImpulseExists(elementI.id)) {
      this.membersKamikazeTargetImpulseStrongMutSet.Remove(root.GetKamikazeTargetImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.NoImpulseExists(elementI.id)) {
      this.membersNoImpulseStrongMutSet.Remove(root.GetNoImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.FireImpulseExists(elementI.id)) {
      this.membersFireImpulseStrongMutSet.Remove(root.GetFireImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.DefyImpulseExists(elementI.id)) {
      this.membersDefyImpulseStrongMutSet.Remove(root.GetDefyImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.CounterImpulseExists(elementI.id)) {
      this.membersCounterImpulseStrongMutSet.Remove(root.GetCounterImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.UnleashBideImpulseExists(elementI.id)) {
      this.membersUnleashBideImpulseStrongMutSet.Remove(root.GetUnleashBideImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.ContinueBidingImpulseExists(elementI.id)) {
      this.membersContinueBidingImpulseStrongMutSet.Remove(root.GetContinueBidingImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.StartBidingImpulseExists(elementI.id)) {
      this.membersStartBidingImpulseStrongMutSet.Remove(root.GetStartBidingImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.AttackImpulseExists(elementI.id)) {
      this.membersAttackImpulseStrongMutSet.Remove(root.GetAttackImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.PursueImpulseExists(elementI.id)) {
      this.membersPursueImpulseStrongMutSet.Remove(root.GetPursueImpulse(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.FireBombImpulseExists(elementI.id)) {
      this.membersFireBombImpulseStrongMutSet.Remove(root.GetFireBombImpulse(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Clear() {
    this.membersTemporaryCloneImpulseStrongMutSet.Clear();
    this.membersSummonImpulseStrongMutSet.Clear();
    this.membersMireImpulseStrongMutSet.Clear();
    this.membersEvaporateImpulseStrongMutSet.Clear();
    this.membersMoveImpulseStrongMutSet.Clear();
    this.membersKamikazeJumpImpulseStrongMutSet.Clear();
    this.membersKamikazeTargetImpulseStrongMutSet.Clear();
    this.membersNoImpulseStrongMutSet.Clear();
    this.membersFireImpulseStrongMutSet.Clear();
    this.membersDefyImpulseStrongMutSet.Clear();
    this.membersCounterImpulseStrongMutSet.Clear();
    this.membersUnleashBideImpulseStrongMutSet.Clear();
    this.membersContinueBidingImpulseStrongMutSet.Clear();
    this.membersStartBidingImpulseStrongMutSet.Clear();
    this.membersAttackImpulseStrongMutSet.Clear();
    this.membersPursueImpulseStrongMutSet.Clear();
    this.membersFireBombImpulseStrongMutSet.Clear();
  }
  public int Count {
    get {
      return
        this.membersTemporaryCloneImpulseStrongMutSet.Count +
        this.membersSummonImpulseStrongMutSet.Count +
        this.membersMireImpulseStrongMutSet.Count +
        this.membersEvaporateImpulseStrongMutSet.Count +
        this.membersMoveImpulseStrongMutSet.Count +
        this.membersKamikazeJumpImpulseStrongMutSet.Count +
        this.membersKamikazeTargetImpulseStrongMutSet.Count +
        this.membersNoImpulseStrongMutSet.Count +
        this.membersFireImpulseStrongMutSet.Count +
        this.membersDefyImpulseStrongMutSet.Count +
        this.membersCounterImpulseStrongMutSet.Count +
        this.membersUnleashBideImpulseStrongMutSet.Count +
        this.membersContinueBidingImpulseStrongMutSet.Count +
        this.membersStartBidingImpulseStrongMutSet.Count +
        this.membersAttackImpulseStrongMutSet.Count +
        this.membersPursueImpulseStrongMutSet.Count +
        this.membersFireBombImpulseStrongMutSet.Count
        ;
    }
  }
  public IImpulse GetArbitrary() {
    foreach (var element in this) {
      return element;
    }
    throw new Exception("Can't get element from empty bunch!");
  }

  public void Destruct() {
    var tempMembersTemporaryCloneImpulseStrongMutSet = this.membersTemporaryCloneImpulseStrongMutSet;
    var tempMembersSummonImpulseStrongMutSet = this.membersSummonImpulseStrongMutSet;
    var tempMembersMireImpulseStrongMutSet = this.membersMireImpulseStrongMutSet;
    var tempMembersEvaporateImpulseStrongMutSet = this.membersEvaporateImpulseStrongMutSet;
    var tempMembersMoveImpulseStrongMutSet = this.membersMoveImpulseStrongMutSet;
    var tempMembersKamikazeJumpImpulseStrongMutSet = this.membersKamikazeJumpImpulseStrongMutSet;
    var tempMembersKamikazeTargetImpulseStrongMutSet = this.membersKamikazeTargetImpulseStrongMutSet;
    var tempMembersNoImpulseStrongMutSet = this.membersNoImpulseStrongMutSet;
    var tempMembersFireImpulseStrongMutSet = this.membersFireImpulseStrongMutSet;
    var tempMembersDefyImpulseStrongMutSet = this.membersDefyImpulseStrongMutSet;
    var tempMembersCounterImpulseStrongMutSet = this.membersCounterImpulseStrongMutSet;
    var tempMembersUnleashBideImpulseStrongMutSet = this.membersUnleashBideImpulseStrongMutSet;
    var tempMembersContinueBidingImpulseStrongMutSet = this.membersContinueBidingImpulseStrongMutSet;
    var tempMembersStartBidingImpulseStrongMutSet = this.membersStartBidingImpulseStrongMutSet;
    var tempMembersAttackImpulseStrongMutSet = this.membersAttackImpulseStrongMutSet;
    var tempMembersPursueImpulseStrongMutSet = this.membersPursueImpulseStrongMutSet;
    var tempMembersFireBombImpulseStrongMutSet = this.membersFireBombImpulseStrongMutSet;

    this.Delete();
    tempMembersTemporaryCloneImpulseStrongMutSet.Destruct();
    tempMembersSummonImpulseStrongMutSet.Destruct();
    tempMembersMireImpulseStrongMutSet.Destruct();
    tempMembersEvaporateImpulseStrongMutSet.Destruct();
    tempMembersMoveImpulseStrongMutSet.Destruct();
    tempMembersKamikazeJumpImpulseStrongMutSet.Destruct();
    tempMembersKamikazeTargetImpulseStrongMutSet.Destruct();
    tempMembersNoImpulseStrongMutSet.Destruct();
    tempMembersFireImpulseStrongMutSet.Destruct();
    tempMembersDefyImpulseStrongMutSet.Destruct();
    tempMembersCounterImpulseStrongMutSet.Destruct();
    tempMembersUnleashBideImpulseStrongMutSet.Destruct();
    tempMembersContinueBidingImpulseStrongMutSet.Destruct();
    tempMembersStartBidingImpulseStrongMutSet.Destruct();
    tempMembersAttackImpulseStrongMutSet.Destruct();
    tempMembersPursueImpulseStrongMutSet.Destruct();
    tempMembersFireBombImpulseStrongMutSet.Destruct();
  }
  public IEnumerator<IImpulse> GetEnumerator() {
    foreach (var element in this.membersTemporaryCloneImpulseStrongMutSet) {
      yield return new TemporaryCloneImpulseAsIImpulse(element);
    }
    foreach (var element in this.membersSummonImpulseStrongMutSet) {
      yield return new SummonImpulseAsIImpulse(element);
    }
    foreach (var element in this.membersMireImpulseStrongMutSet) {
      yield return new MireImpulseAsIImpulse(element);
    }
    foreach (var element in this.membersEvaporateImpulseStrongMutSet) {
      yield return new EvaporateImpulseAsIImpulse(element);
    }
    foreach (var element in this.membersMoveImpulseStrongMutSet) {
      yield return new MoveImpulseAsIImpulse(element);
    }
    foreach (var element in this.membersKamikazeJumpImpulseStrongMutSet) {
      yield return new KamikazeJumpImpulseAsIImpulse(element);
    }
    foreach (var element in this.membersKamikazeTargetImpulseStrongMutSet) {
      yield return new KamikazeTargetImpulseAsIImpulse(element);
    }
    foreach (var element in this.membersNoImpulseStrongMutSet) {
      yield return new NoImpulseAsIImpulse(element);
    }
    foreach (var element in this.membersFireImpulseStrongMutSet) {
      yield return new FireImpulseAsIImpulse(element);
    }
    foreach (var element in this.membersDefyImpulseStrongMutSet) {
      yield return new DefyImpulseAsIImpulse(element);
    }
    foreach (var element in this.membersCounterImpulseStrongMutSet) {
      yield return new CounterImpulseAsIImpulse(element);
    }
    foreach (var element in this.membersUnleashBideImpulseStrongMutSet) {
      yield return new UnleashBideImpulseAsIImpulse(element);
    }
    foreach (var element in this.membersContinueBidingImpulseStrongMutSet) {
      yield return new ContinueBidingImpulseAsIImpulse(element);
    }
    foreach (var element in this.membersStartBidingImpulseStrongMutSet) {
      yield return new StartBidingImpulseAsIImpulse(element);
    }
    foreach (var element in this.membersAttackImpulseStrongMutSet) {
      yield return new AttackImpulseAsIImpulse(element);
    }
    foreach (var element in this.membersPursueImpulseStrongMutSet) {
      yield return new PursueImpulseAsIImpulse(element);
    }
    foreach (var element in this.membersFireBombImpulseStrongMutSet) {
      yield return new FireBombImpulseAsIImpulse(element);
    }
  }
    public List<TemporaryCloneImpulse> GetAllTemporaryCloneImpulse() {
      var result = new List<TemporaryCloneImpulse>();
      foreach (var thing in this.membersTemporaryCloneImpulseStrongMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<TemporaryCloneImpulse> ClearAllTemporaryCloneImpulse() {
      var result = new List<TemporaryCloneImpulse>();
      this.membersTemporaryCloneImpulseStrongMutSet.Clear();
      return result;
    }
    public TemporaryCloneImpulse GetOnlyTemporaryCloneImpulseOrNull() {
      var result = GetAllTemporaryCloneImpulse();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return TemporaryCloneImpulse.Null;
      }
    }
    public List<SummonImpulse> GetAllSummonImpulse() {
      var result = new List<SummonImpulse>();
      foreach (var thing in this.membersSummonImpulseStrongMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<SummonImpulse> ClearAllSummonImpulse() {
      var result = new List<SummonImpulse>();
      this.membersSummonImpulseStrongMutSet.Clear();
      return result;
    }
    public SummonImpulse GetOnlySummonImpulseOrNull() {
      var result = GetAllSummonImpulse();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return SummonImpulse.Null;
      }
    }
    public List<MireImpulse> GetAllMireImpulse() {
      var result = new List<MireImpulse>();
      foreach (var thing in this.membersMireImpulseStrongMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<MireImpulse> ClearAllMireImpulse() {
      var result = new List<MireImpulse>();
      this.membersMireImpulseStrongMutSet.Clear();
      return result;
    }
    public MireImpulse GetOnlyMireImpulseOrNull() {
      var result = GetAllMireImpulse();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return MireImpulse.Null;
      }
    }
    public List<EvaporateImpulse> GetAllEvaporateImpulse() {
      var result = new List<EvaporateImpulse>();
      foreach (var thing in this.membersEvaporateImpulseStrongMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<EvaporateImpulse> ClearAllEvaporateImpulse() {
      var result = new List<EvaporateImpulse>();
      this.membersEvaporateImpulseStrongMutSet.Clear();
      return result;
    }
    public EvaporateImpulse GetOnlyEvaporateImpulseOrNull() {
      var result = GetAllEvaporateImpulse();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return EvaporateImpulse.Null;
      }
    }
    public List<MoveImpulse> GetAllMoveImpulse() {
      var result = new List<MoveImpulse>();
      foreach (var thing in this.membersMoveImpulseStrongMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<MoveImpulse> ClearAllMoveImpulse() {
      var result = new List<MoveImpulse>();
      this.membersMoveImpulseStrongMutSet.Clear();
      return result;
    }
    public MoveImpulse GetOnlyMoveImpulseOrNull() {
      var result = GetAllMoveImpulse();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return MoveImpulse.Null;
      }
    }
    public List<KamikazeJumpImpulse> GetAllKamikazeJumpImpulse() {
      var result = new List<KamikazeJumpImpulse>();
      foreach (var thing in this.membersKamikazeJumpImpulseStrongMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<KamikazeJumpImpulse> ClearAllKamikazeJumpImpulse() {
      var result = new List<KamikazeJumpImpulse>();
      this.membersKamikazeJumpImpulseStrongMutSet.Clear();
      return result;
    }
    public KamikazeJumpImpulse GetOnlyKamikazeJumpImpulseOrNull() {
      var result = GetAllKamikazeJumpImpulse();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return KamikazeJumpImpulse.Null;
      }
    }
    public List<KamikazeTargetImpulse> GetAllKamikazeTargetImpulse() {
      var result = new List<KamikazeTargetImpulse>();
      foreach (var thing in this.membersKamikazeTargetImpulseStrongMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<KamikazeTargetImpulse> ClearAllKamikazeTargetImpulse() {
      var result = new List<KamikazeTargetImpulse>();
      this.membersKamikazeTargetImpulseStrongMutSet.Clear();
      return result;
    }
    public KamikazeTargetImpulse GetOnlyKamikazeTargetImpulseOrNull() {
      var result = GetAllKamikazeTargetImpulse();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return KamikazeTargetImpulse.Null;
      }
    }
    public List<NoImpulse> GetAllNoImpulse() {
      var result = new List<NoImpulse>();
      foreach (var thing in this.membersNoImpulseStrongMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<NoImpulse> ClearAllNoImpulse() {
      var result = new List<NoImpulse>();
      this.membersNoImpulseStrongMutSet.Clear();
      return result;
    }
    public NoImpulse GetOnlyNoImpulseOrNull() {
      var result = GetAllNoImpulse();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NoImpulse.Null;
      }
    }
    public List<FireImpulse> GetAllFireImpulse() {
      var result = new List<FireImpulse>();
      foreach (var thing in this.membersFireImpulseStrongMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<FireImpulse> ClearAllFireImpulse() {
      var result = new List<FireImpulse>();
      this.membersFireImpulseStrongMutSet.Clear();
      return result;
    }
    public FireImpulse GetOnlyFireImpulseOrNull() {
      var result = GetAllFireImpulse();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return FireImpulse.Null;
      }
    }
    public List<DefyImpulse> GetAllDefyImpulse() {
      var result = new List<DefyImpulse>();
      foreach (var thing in this.membersDefyImpulseStrongMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<DefyImpulse> ClearAllDefyImpulse() {
      var result = new List<DefyImpulse>();
      this.membersDefyImpulseStrongMutSet.Clear();
      return result;
    }
    public DefyImpulse GetOnlyDefyImpulseOrNull() {
      var result = GetAllDefyImpulse();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return DefyImpulse.Null;
      }
    }
    public List<CounterImpulse> GetAllCounterImpulse() {
      var result = new List<CounterImpulse>();
      foreach (var thing in this.membersCounterImpulseStrongMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<CounterImpulse> ClearAllCounterImpulse() {
      var result = new List<CounterImpulse>();
      this.membersCounterImpulseStrongMutSet.Clear();
      return result;
    }
    public CounterImpulse GetOnlyCounterImpulseOrNull() {
      var result = GetAllCounterImpulse();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return CounterImpulse.Null;
      }
    }
    public List<UnleashBideImpulse> GetAllUnleashBideImpulse() {
      var result = new List<UnleashBideImpulse>();
      foreach (var thing in this.membersUnleashBideImpulseStrongMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<UnleashBideImpulse> ClearAllUnleashBideImpulse() {
      var result = new List<UnleashBideImpulse>();
      this.membersUnleashBideImpulseStrongMutSet.Clear();
      return result;
    }
    public UnleashBideImpulse GetOnlyUnleashBideImpulseOrNull() {
      var result = GetAllUnleashBideImpulse();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return UnleashBideImpulse.Null;
      }
    }
    public List<ContinueBidingImpulse> GetAllContinueBidingImpulse() {
      var result = new List<ContinueBidingImpulse>();
      foreach (var thing in this.membersContinueBidingImpulseStrongMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<ContinueBidingImpulse> ClearAllContinueBidingImpulse() {
      var result = new List<ContinueBidingImpulse>();
      this.membersContinueBidingImpulseStrongMutSet.Clear();
      return result;
    }
    public ContinueBidingImpulse GetOnlyContinueBidingImpulseOrNull() {
      var result = GetAllContinueBidingImpulse();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return ContinueBidingImpulse.Null;
      }
    }
    public List<StartBidingImpulse> GetAllStartBidingImpulse() {
      var result = new List<StartBidingImpulse>();
      foreach (var thing in this.membersStartBidingImpulseStrongMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<StartBidingImpulse> ClearAllStartBidingImpulse() {
      var result = new List<StartBidingImpulse>();
      this.membersStartBidingImpulseStrongMutSet.Clear();
      return result;
    }
    public StartBidingImpulse GetOnlyStartBidingImpulseOrNull() {
      var result = GetAllStartBidingImpulse();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return StartBidingImpulse.Null;
      }
    }
    public List<AttackImpulse> GetAllAttackImpulse() {
      var result = new List<AttackImpulse>();
      foreach (var thing in this.membersAttackImpulseStrongMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<AttackImpulse> ClearAllAttackImpulse() {
      var result = new List<AttackImpulse>();
      this.membersAttackImpulseStrongMutSet.Clear();
      return result;
    }
    public AttackImpulse GetOnlyAttackImpulseOrNull() {
      var result = GetAllAttackImpulse();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return AttackImpulse.Null;
      }
    }
    public List<PursueImpulse> GetAllPursueImpulse() {
      var result = new List<PursueImpulse>();
      foreach (var thing in this.membersPursueImpulseStrongMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<PursueImpulse> ClearAllPursueImpulse() {
      var result = new List<PursueImpulse>();
      this.membersPursueImpulseStrongMutSet.Clear();
      return result;
    }
    public PursueImpulse GetOnlyPursueImpulseOrNull() {
      var result = GetAllPursueImpulse();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return PursueImpulse.Null;
      }
    }
    public List<FireBombImpulse> GetAllFireBombImpulse() {
      var result = new List<FireBombImpulse>();
      foreach (var thing in this.membersFireBombImpulseStrongMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<FireBombImpulse> ClearAllFireBombImpulse() {
      var result = new List<FireBombImpulse>();
      this.membersFireBombImpulseStrongMutSet.Clear();
      return result;
    }
    public FireBombImpulse GetOnlyFireBombImpulseOrNull() {
      var result = GetAllFireBombImpulse();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return FireBombImpulse.Null;
      }
    }
}
}
