using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class IImpulseStrongMutBunchBroadcaster:IHoldPositionImpulseStrongMutSetEffectObserver, IHoldPositionImpulseStrongMutSetEffectVisitor, ITemporaryCloneImpulseStrongMutSetEffectObserver, ITemporaryCloneImpulseStrongMutSetEffectVisitor, ISummonImpulseStrongMutSetEffectObserver, ISummonImpulseStrongMutSetEffectVisitor, IMireImpulseStrongMutSetEffectObserver, IMireImpulseStrongMutSetEffectVisitor, IEvaporateImpulseStrongMutSetEffectObserver, IEvaporateImpulseStrongMutSetEffectVisitor, IMoveImpulseStrongMutSetEffectObserver, IMoveImpulseStrongMutSetEffectVisitor, IKamikazeJumpImpulseStrongMutSetEffectObserver, IKamikazeJumpImpulseStrongMutSetEffectVisitor, IKamikazeTargetImpulseStrongMutSetEffectObserver, IKamikazeTargetImpulseStrongMutSetEffectVisitor, INoImpulseStrongMutSetEffectObserver, INoImpulseStrongMutSetEffectVisitor, IEvolvifyImpulseStrongMutSetEffectObserver, IEvolvifyImpulseStrongMutSetEffectVisitor, IFireImpulseStrongMutSetEffectObserver, IFireImpulseStrongMutSetEffectVisitor, IDefyImpulseStrongMutSetEffectObserver, IDefyImpulseStrongMutSetEffectVisitor, ICounterImpulseStrongMutSetEffectObserver, ICounterImpulseStrongMutSetEffectVisitor, IUnleashBideImpulseStrongMutSetEffectObserver, IUnleashBideImpulseStrongMutSetEffectVisitor, IContinueBidingImpulseStrongMutSetEffectObserver, IContinueBidingImpulseStrongMutSetEffectVisitor, IStartBidingImpulseStrongMutSetEffectObserver, IStartBidingImpulseStrongMutSetEffectVisitor, IAttackImpulseStrongMutSetEffectObserver, IAttackImpulseStrongMutSetEffectVisitor, IPursueImpulseStrongMutSetEffectObserver, IPursueImpulseStrongMutSetEffectVisitor, IFireBombImpulseStrongMutSetEffectObserver, IFireBombImpulseStrongMutSetEffectVisitor {
  EffectBroadcaster broadcaster;
  IImpulseStrongMutBunch bunch;
  private List<IIImpulseStrongMutBunchObserver> observers;

  public IImpulseStrongMutBunchBroadcaster(EffectBroadcaster broadcaster, IImpulseStrongMutBunch bunch) {
    this.broadcaster = broadcaster;
    this.bunch = bunch;
    this.observers = new List<IIImpulseStrongMutBunchObserver>();
    bunch.membersHoldPositionImpulseStrongMutSet.AddObserver(broadcaster, this);
    bunch.membersTemporaryCloneImpulseStrongMutSet.AddObserver(broadcaster, this);
    bunch.membersSummonImpulseStrongMutSet.AddObserver(broadcaster, this);
    bunch.membersMireImpulseStrongMutSet.AddObserver(broadcaster, this);
    bunch.membersEvaporateImpulseStrongMutSet.AddObserver(broadcaster, this);
    bunch.membersMoveImpulseStrongMutSet.AddObserver(broadcaster, this);
    bunch.membersKamikazeJumpImpulseStrongMutSet.AddObserver(broadcaster, this);
    bunch.membersKamikazeTargetImpulseStrongMutSet.AddObserver(broadcaster, this);
    bunch.membersNoImpulseStrongMutSet.AddObserver(broadcaster, this);
    bunch.membersEvolvifyImpulseStrongMutSet.AddObserver(broadcaster, this);
    bunch.membersFireImpulseStrongMutSet.AddObserver(broadcaster, this);
    bunch.membersDefyImpulseStrongMutSet.AddObserver(broadcaster, this);
    bunch.membersCounterImpulseStrongMutSet.AddObserver(broadcaster, this);
    bunch.membersUnleashBideImpulseStrongMutSet.AddObserver(broadcaster, this);
    bunch.membersContinueBidingImpulseStrongMutSet.AddObserver(broadcaster, this);
    bunch.membersStartBidingImpulseStrongMutSet.AddObserver(broadcaster, this);
    bunch.membersAttackImpulseStrongMutSet.AddObserver(broadcaster, this);
    bunch.membersPursueImpulseStrongMutSet.AddObserver(broadcaster, this);
    bunch.membersFireBombImpulseStrongMutSet.AddObserver(broadcaster, this);

  }
  public void Stop() {
    bunch.membersHoldPositionImpulseStrongMutSet.RemoveObserver(broadcaster, this);
    bunch.membersTemporaryCloneImpulseStrongMutSet.RemoveObserver(broadcaster, this);
    bunch.membersSummonImpulseStrongMutSet.RemoveObserver(broadcaster, this);
    bunch.membersMireImpulseStrongMutSet.RemoveObserver(broadcaster, this);
    bunch.membersEvaporateImpulseStrongMutSet.RemoveObserver(broadcaster, this);
    bunch.membersMoveImpulseStrongMutSet.RemoveObserver(broadcaster, this);
    bunch.membersKamikazeJumpImpulseStrongMutSet.RemoveObserver(broadcaster, this);
    bunch.membersKamikazeTargetImpulseStrongMutSet.RemoveObserver(broadcaster, this);
    bunch.membersNoImpulseStrongMutSet.RemoveObserver(broadcaster, this);
    bunch.membersEvolvifyImpulseStrongMutSet.RemoveObserver(broadcaster, this);
    bunch.membersFireImpulseStrongMutSet.RemoveObserver(broadcaster, this);
    bunch.membersDefyImpulseStrongMutSet.RemoveObserver(broadcaster, this);
    bunch.membersCounterImpulseStrongMutSet.RemoveObserver(broadcaster, this);
    bunch.membersUnleashBideImpulseStrongMutSet.RemoveObserver(broadcaster, this);
    bunch.membersContinueBidingImpulseStrongMutSet.RemoveObserver(broadcaster, this);
    bunch.membersStartBidingImpulseStrongMutSet.RemoveObserver(broadcaster, this);
    bunch.membersAttackImpulseStrongMutSet.RemoveObserver(broadcaster, this);
    bunch.membersPursueImpulseStrongMutSet.RemoveObserver(broadcaster, this);
    bunch.membersFireBombImpulseStrongMutSet.RemoveObserver(broadcaster, this);

  }
  public void AddObserver(IIImpulseStrongMutBunchObserver observer) {
    this.observers.Add(observer);
  }
  public void RemoveObserver(IIImpulseStrongMutBunchObserver observer) {
    this.observers.Remove(observer);
  }
  private void BroadcastAdd(int id) {
    foreach (var observer in observers) {
      observer.OnIImpulseStrongMutBunchAdd(id);
    }
  }
  private void BroadcastRemove(int id) {
    foreach (var observer in observers) {
      observer.OnIImpulseStrongMutBunchRemove(id);
    }
  }
  public void OnHoldPositionImpulseStrongMutSetEffect(IHoldPositionImpulseStrongMutSetEffect effect) {
    effect.visitIHoldPositionImpulseStrongMutSetEffect(this);
  }
  public void visitHoldPositionImpulseStrongMutSetAddEffect(HoldPositionImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitHoldPositionImpulseStrongMutSetRemoveEffect(HoldPositionImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitHoldPositionImpulseStrongMutSetCreateEffect(HoldPositionImpulseStrongMutSetCreateEffect effect) { }
  public void visitHoldPositionImpulseStrongMutSetDeleteEffect(HoldPositionImpulseStrongMutSetDeleteEffect effect) { }
  public void OnTemporaryCloneImpulseStrongMutSetEffect(ITemporaryCloneImpulseStrongMutSetEffect effect) {
    effect.visitITemporaryCloneImpulseStrongMutSetEffect(this);
  }
  public void visitTemporaryCloneImpulseStrongMutSetAddEffect(TemporaryCloneImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitTemporaryCloneImpulseStrongMutSetRemoveEffect(TemporaryCloneImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitTemporaryCloneImpulseStrongMutSetCreateEffect(TemporaryCloneImpulseStrongMutSetCreateEffect effect) { }
  public void visitTemporaryCloneImpulseStrongMutSetDeleteEffect(TemporaryCloneImpulseStrongMutSetDeleteEffect effect) { }
  public void OnSummonImpulseStrongMutSetEffect(ISummonImpulseStrongMutSetEffect effect) {
    effect.visitISummonImpulseStrongMutSetEffect(this);
  }
  public void visitSummonImpulseStrongMutSetAddEffect(SummonImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitSummonImpulseStrongMutSetRemoveEffect(SummonImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitSummonImpulseStrongMutSetCreateEffect(SummonImpulseStrongMutSetCreateEffect effect) { }
  public void visitSummonImpulseStrongMutSetDeleteEffect(SummonImpulseStrongMutSetDeleteEffect effect) { }
  public void OnMireImpulseStrongMutSetEffect(IMireImpulseStrongMutSetEffect effect) {
    effect.visitIMireImpulseStrongMutSetEffect(this);
  }
  public void visitMireImpulseStrongMutSetAddEffect(MireImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitMireImpulseStrongMutSetRemoveEffect(MireImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitMireImpulseStrongMutSetCreateEffect(MireImpulseStrongMutSetCreateEffect effect) { }
  public void visitMireImpulseStrongMutSetDeleteEffect(MireImpulseStrongMutSetDeleteEffect effect) { }
  public void OnEvaporateImpulseStrongMutSetEffect(IEvaporateImpulseStrongMutSetEffect effect) {
    effect.visitIEvaporateImpulseStrongMutSetEffect(this);
  }
  public void visitEvaporateImpulseStrongMutSetAddEffect(EvaporateImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitEvaporateImpulseStrongMutSetRemoveEffect(EvaporateImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitEvaporateImpulseStrongMutSetCreateEffect(EvaporateImpulseStrongMutSetCreateEffect effect) { }
  public void visitEvaporateImpulseStrongMutSetDeleteEffect(EvaporateImpulseStrongMutSetDeleteEffect effect) { }
  public void OnMoveImpulseStrongMutSetEffect(IMoveImpulseStrongMutSetEffect effect) {
    effect.visitIMoveImpulseStrongMutSetEffect(this);
  }
  public void visitMoveImpulseStrongMutSetAddEffect(MoveImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitMoveImpulseStrongMutSetRemoveEffect(MoveImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitMoveImpulseStrongMutSetCreateEffect(MoveImpulseStrongMutSetCreateEffect effect) { }
  public void visitMoveImpulseStrongMutSetDeleteEffect(MoveImpulseStrongMutSetDeleteEffect effect) { }
  public void OnKamikazeJumpImpulseStrongMutSetEffect(IKamikazeJumpImpulseStrongMutSetEffect effect) {
    effect.visitIKamikazeJumpImpulseStrongMutSetEffect(this);
  }
  public void visitKamikazeJumpImpulseStrongMutSetAddEffect(KamikazeJumpImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitKamikazeJumpImpulseStrongMutSetRemoveEffect(KamikazeJumpImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitKamikazeJumpImpulseStrongMutSetCreateEffect(KamikazeJumpImpulseStrongMutSetCreateEffect effect) { }
  public void visitKamikazeJumpImpulseStrongMutSetDeleteEffect(KamikazeJumpImpulseStrongMutSetDeleteEffect effect) { }
  public void OnKamikazeTargetImpulseStrongMutSetEffect(IKamikazeTargetImpulseStrongMutSetEffect effect) {
    effect.visitIKamikazeTargetImpulseStrongMutSetEffect(this);
  }
  public void visitKamikazeTargetImpulseStrongMutSetAddEffect(KamikazeTargetImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitKamikazeTargetImpulseStrongMutSetRemoveEffect(KamikazeTargetImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitKamikazeTargetImpulseStrongMutSetCreateEffect(KamikazeTargetImpulseStrongMutSetCreateEffect effect) { }
  public void visitKamikazeTargetImpulseStrongMutSetDeleteEffect(KamikazeTargetImpulseStrongMutSetDeleteEffect effect) { }
  public void OnNoImpulseStrongMutSetEffect(INoImpulseStrongMutSetEffect effect) {
    effect.visitINoImpulseStrongMutSetEffect(this);
  }
  public void visitNoImpulseStrongMutSetAddEffect(NoImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitNoImpulseStrongMutSetRemoveEffect(NoImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitNoImpulseStrongMutSetCreateEffect(NoImpulseStrongMutSetCreateEffect effect) { }
  public void visitNoImpulseStrongMutSetDeleteEffect(NoImpulseStrongMutSetDeleteEffect effect) { }
  public void OnEvolvifyImpulseStrongMutSetEffect(IEvolvifyImpulseStrongMutSetEffect effect) {
    effect.visitIEvolvifyImpulseStrongMutSetEffect(this);
  }
  public void visitEvolvifyImpulseStrongMutSetAddEffect(EvolvifyImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitEvolvifyImpulseStrongMutSetRemoveEffect(EvolvifyImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitEvolvifyImpulseStrongMutSetCreateEffect(EvolvifyImpulseStrongMutSetCreateEffect effect) { }
  public void visitEvolvifyImpulseStrongMutSetDeleteEffect(EvolvifyImpulseStrongMutSetDeleteEffect effect) { }
  public void OnFireImpulseStrongMutSetEffect(IFireImpulseStrongMutSetEffect effect) {
    effect.visitIFireImpulseStrongMutSetEffect(this);
  }
  public void visitFireImpulseStrongMutSetAddEffect(FireImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitFireImpulseStrongMutSetRemoveEffect(FireImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitFireImpulseStrongMutSetCreateEffect(FireImpulseStrongMutSetCreateEffect effect) { }
  public void visitFireImpulseStrongMutSetDeleteEffect(FireImpulseStrongMutSetDeleteEffect effect) { }
  public void OnDefyImpulseStrongMutSetEffect(IDefyImpulseStrongMutSetEffect effect) {
    effect.visitIDefyImpulseStrongMutSetEffect(this);
  }
  public void visitDefyImpulseStrongMutSetAddEffect(DefyImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitDefyImpulseStrongMutSetRemoveEffect(DefyImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitDefyImpulseStrongMutSetCreateEffect(DefyImpulseStrongMutSetCreateEffect effect) { }
  public void visitDefyImpulseStrongMutSetDeleteEffect(DefyImpulseStrongMutSetDeleteEffect effect) { }
  public void OnCounterImpulseStrongMutSetEffect(ICounterImpulseStrongMutSetEffect effect) {
    effect.visitICounterImpulseStrongMutSetEffect(this);
  }
  public void visitCounterImpulseStrongMutSetAddEffect(CounterImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitCounterImpulseStrongMutSetRemoveEffect(CounterImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitCounterImpulseStrongMutSetCreateEffect(CounterImpulseStrongMutSetCreateEffect effect) { }
  public void visitCounterImpulseStrongMutSetDeleteEffect(CounterImpulseStrongMutSetDeleteEffect effect) { }
  public void OnUnleashBideImpulseStrongMutSetEffect(IUnleashBideImpulseStrongMutSetEffect effect) {
    effect.visitIUnleashBideImpulseStrongMutSetEffect(this);
  }
  public void visitUnleashBideImpulseStrongMutSetAddEffect(UnleashBideImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitUnleashBideImpulseStrongMutSetRemoveEffect(UnleashBideImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitUnleashBideImpulseStrongMutSetCreateEffect(UnleashBideImpulseStrongMutSetCreateEffect effect) { }
  public void visitUnleashBideImpulseStrongMutSetDeleteEffect(UnleashBideImpulseStrongMutSetDeleteEffect effect) { }
  public void OnContinueBidingImpulseStrongMutSetEffect(IContinueBidingImpulseStrongMutSetEffect effect) {
    effect.visitIContinueBidingImpulseStrongMutSetEffect(this);
  }
  public void visitContinueBidingImpulseStrongMutSetAddEffect(ContinueBidingImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitContinueBidingImpulseStrongMutSetRemoveEffect(ContinueBidingImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitContinueBidingImpulseStrongMutSetCreateEffect(ContinueBidingImpulseStrongMutSetCreateEffect effect) { }
  public void visitContinueBidingImpulseStrongMutSetDeleteEffect(ContinueBidingImpulseStrongMutSetDeleteEffect effect) { }
  public void OnStartBidingImpulseStrongMutSetEffect(IStartBidingImpulseStrongMutSetEffect effect) {
    effect.visitIStartBidingImpulseStrongMutSetEffect(this);
  }
  public void visitStartBidingImpulseStrongMutSetAddEffect(StartBidingImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitStartBidingImpulseStrongMutSetRemoveEffect(StartBidingImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitStartBidingImpulseStrongMutSetCreateEffect(StartBidingImpulseStrongMutSetCreateEffect effect) { }
  public void visitStartBidingImpulseStrongMutSetDeleteEffect(StartBidingImpulseStrongMutSetDeleteEffect effect) { }
  public void OnAttackImpulseStrongMutSetEffect(IAttackImpulseStrongMutSetEffect effect) {
    effect.visitIAttackImpulseStrongMutSetEffect(this);
  }
  public void visitAttackImpulseStrongMutSetAddEffect(AttackImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitAttackImpulseStrongMutSetRemoveEffect(AttackImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitAttackImpulseStrongMutSetCreateEffect(AttackImpulseStrongMutSetCreateEffect effect) { }
  public void visitAttackImpulseStrongMutSetDeleteEffect(AttackImpulseStrongMutSetDeleteEffect effect) { }
  public void OnPursueImpulseStrongMutSetEffect(IPursueImpulseStrongMutSetEffect effect) {
    effect.visitIPursueImpulseStrongMutSetEffect(this);
  }
  public void visitPursueImpulseStrongMutSetAddEffect(PursueImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitPursueImpulseStrongMutSetRemoveEffect(PursueImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitPursueImpulseStrongMutSetCreateEffect(PursueImpulseStrongMutSetCreateEffect effect) { }
  public void visitPursueImpulseStrongMutSetDeleteEffect(PursueImpulseStrongMutSetDeleteEffect effect) { }
  public void OnFireBombImpulseStrongMutSetEffect(IFireBombImpulseStrongMutSetEffect effect) {
    effect.visitIFireBombImpulseStrongMutSetEffect(this);
  }
  public void visitFireBombImpulseStrongMutSetAddEffect(FireBombImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitFireBombImpulseStrongMutSetRemoveEffect(FireBombImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitFireBombImpulseStrongMutSetCreateEffect(FireBombImpulseStrongMutSetCreateEffect effect) { }
  public void visitFireBombImpulseStrongMutSetDeleteEffect(FireBombImpulseStrongMutSetDeleteEffect effect) { }

}
       
}
