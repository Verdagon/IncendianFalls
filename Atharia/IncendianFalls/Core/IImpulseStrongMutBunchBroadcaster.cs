using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class IImpulseStrongMutBunchBroadcaster:ITemporaryCloneImpulseStrongMutSetEffectObserver, ITemporaryCloneImpulseStrongMutSetEffectVisitor, ISummonImpulseStrongMutSetEffectObserver, ISummonImpulseStrongMutSetEffectVisitor, IMireImpulseStrongMutSetEffectObserver, IMireImpulseStrongMutSetEffectVisitor, IEvaporateImpulseStrongMutSetEffectObserver, IEvaporateImpulseStrongMutSetEffectVisitor, IMoveImpulseStrongMutSetEffectObserver, IMoveImpulseStrongMutSetEffectVisitor, IKamikazeJumpImpulseStrongMutSetEffectObserver, IKamikazeJumpImpulseStrongMutSetEffectVisitor, IKamikazeTargetImpulseStrongMutSetEffectObserver, IKamikazeTargetImpulseStrongMutSetEffectVisitor, INoImpulseStrongMutSetEffectObserver, INoImpulseStrongMutSetEffectVisitor, IFireImpulseStrongMutSetEffectObserver, IFireImpulseStrongMutSetEffectVisitor, IDefyImpulseStrongMutSetEffectObserver, IDefyImpulseStrongMutSetEffectVisitor, ICounterImpulseStrongMutSetEffectObserver, ICounterImpulseStrongMutSetEffectVisitor, IUnleashBideImpulseStrongMutSetEffectObserver, IUnleashBideImpulseStrongMutSetEffectVisitor, IContinueBidingImpulseStrongMutSetEffectObserver, IContinueBidingImpulseStrongMutSetEffectVisitor, IStartBidingImpulseStrongMutSetEffectObserver, IStartBidingImpulseStrongMutSetEffectVisitor, IAttackImpulseStrongMutSetEffectObserver, IAttackImpulseStrongMutSetEffectVisitor, IPursueImpulseStrongMutSetEffectObserver, IPursueImpulseStrongMutSetEffectVisitor, IFireBombImpulseStrongMutSetEffectObserver, IFireBombImpulseStrongMutSetEffectVisitor {
  IImpulseStrongMutBunch bunch;
  private List<IIImpulseStrongMutBunchObserver> observers;

  public IImpulseStrongMutBunchBroadcaster(IImpulseStrongMutBunch bunch) {
    this.bunch = bunch;
    this.observers = new List<IIImpulseStrongMutBunchObserver>();
    bunch.membersTemporaryCloneImpulseStrongMutSet.AddObserver(this);
    bunch.membersSummonImpulseStrongMutSet.AddObserver(this);
    bunch.membersMireImpulseStrongMutSet.AddObserver(this);
    bunch.membersEvaporateImpulseStrongMutSet.AddObserver(this);
    bunch.membersMoveImpulseStrongMutSet.AddObserver(this);
    bunch.membersKamikazeJumpImpulseStrongMutSet.AddObserver(this);
    bunch.membersKamikazeTargetImpulseStrongMutSet.AddObserver(this);
    bunch.membersNoImpulseStrongMutSet.AddObserver(this);
    bunch.membersFireImpulseStrongMutSet.AddObserver(this);
    bunch.membersDefyImpulseStrongMutSet.AddObserver(this);
    bunch.membersCounterImpulseStrongMutSet.AddObserver(this);
    bunch.membersUnleashBideImpulseStrongMutSet.AddObserver(this);
    bunch.membersContinueBidingImpulseStrongMutSet.AddObserver(this);
    bunch.membersStartBidingImpulseStrongMutSet.AddObserver(this);
    bunch.membersAttackImpulseStrongMutSet.AddObserver(this);
    bunch.membersPursueImpulseStrongMutSet.AddObserver(this);
    bunch.membersFireBombImpulseStrongMutSet.AddObserver(this);

  }
  public void Stop() {
    bunch.membersTemporaryCloneImpulseStrongMutSet.RemoveObserver(this);
    bunch.membersSummonImpulseStrongMutSet.RemoveObserver(this);
    bunch.membersMireImpulseStrongMutSet.RemoveObserver(this);
    bunch.membersEvaporateImpulseStrongMutSet.RemoveObserver(this);
    bunch.membersMoveImpulseStrongMutSet.RemoveObserver(this);
    bunch.membersKamikazeJumpImpulseStrongMutSet.RemoveObserver(this);
    bunch.membersKamikazeTargetImpulseStrongMutSet.RemoveObserver(this);
    bunch.membersNoImpulseStrongMutSet.RemoveObserver(this);
    bunch.membersFireImpulseStrongMutSet.RemoveObserver(this);
    bunch.membersDefyImpulseStrongMutSet.RemoveObserver(this);
    bunch.membersCounterImpulseStrongMutSet.RemoveObserver(this);
    bunch.membersUnleashBideImpulseStrongMutSet.RemoveObserver(this);
    bunch.membersContinueBidingImpulseStrongMutSet.RemoveObserver(this);
    bunch.membersStartBidingImpulseStrongMutSet.RemoveObserver(this);
    bunch.membersAttackImpulseStrongMutSet.RemoveObserver(this);
    bunch.membersPursueImpulseStrongMutSet.RemoveObserver(this);
    bunch.membersFireBombImpulseStrongMutSet.RemoveObserver(this);

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
  public void OnTemporaryCloneImpulseStrongMutSetEffect(ITemporaryCloneImpulseStrongMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitTemporaryCloneImpulseStrongMutSetAddEffect(TemporaryCloneImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitTemporaryCloneImpulseStrongMutSetRemoveEffect(TemporaryCloneImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitTemporaryCloneImpulseStrongMutSetCreateEffect(TemporaryCloneImpulseStrongMutSetCreateEffect effect) { }
  public void visitTemporaryCloneImpulseStrongMutSetDeleteEffect(TemporaryCloneImpulseStrongMutSetDeleteEffect effect) { }
  public void OnSummonImpulseStrongMutSetEffect(ISummonImpulseStrongMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitSummonImpulseStrongMutSetAddEffect(SummonImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitSummonImpulseStrongMutSetRemoveEffect(SummonImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitSummonImpulseStrongMutSetCreateEffect(SummonImpulseStrongMutSetCreateEffect effect) { }
  public void visitSummonImpulseStrongMutSetDeleteEffect(SummonImpulseStrongMutSetDeleteEffect effect) { }
  public void OnMireImpulseStrongMutSetEffect(IMireImpulseStrongMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitMireImpulseStrongMutSetAddEffect(MireImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitMireImpulseStrongMutSetRemoveEffect(MireImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitMireImpulseStrongMutSetCreateEffect(MireImpulseStrongMutSetCreateEffect effect) { }
  public void visitMireImpulseStrongMutSetDeleteEffect(MireImpulseStrongMutSetDeleteEffect effect) { }
  public void OnEvaporateImpulseStrongMutSetEffect(IEvaporateImpulseStrongMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitEvaporateImpulseStrongMutSetAddEffect(EvaporateImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitEvaporateImpulseStrongMutSetRemoveEffect(EvaporateImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitEvaporateImpulseStrongMutSetCreateEffect(EvaporateImpulseStrongMutSetCreateEffect effect) { }
  public void visitEvaporateImpulseStrongMutSetDeleteEffect(EvaporateImpulseStrongMutSetDeleteEffect effect) { }
  public void OnMoveImpulseStrongMutSetEffect(IMoveImpulseStrongMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitMoveImpulseStrongMutSetAddEffect(MoveImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitMoveImpulseStrongMutSetRemoveEffect(MoveImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitMoveImpulseStrongMutSetCreateEffect(MoveImpulseStrongMutSetCreateEffect effect) { }
  public void visitMoveImpulseStrongMutSetDeleteEffect(MoveImpulseStrongMutSetDeleteEffect effect) { }
  public void OnKamikazeJumpImpulseStrongMutSetEffect(IKamikazeJumpImpulseStrongMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitKamikazeJumpImpulseStrongMutSetAddEffect(KamikazeJumpImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitKamikazeJumpImpulseStrongMutSetRemoveEffect(KamikazeJumpImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitKamikazeJumpImpulseStrongMutSetCreateEffect(KamikazeJumpImpulseStrongMutSetCreateEffect effect) { }
  public void visitKamikazeJumpImpulseStrongMutSetDeleteEffect(KamikazeJumpImpulseStrongMutSetDeleteEffect effect) { }
  public void OnKamikazeTargetImpulseStrongMutSetEffect(IKamikazeTargetImpulseStrongMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitKamikazeTargetImpulseStrongMutSetAddEffect(KamikazeTargetImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitKamikazeTargetImpulseStrongMutSetRemoveEffect(KamikazeTargetImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitKamikazeTargetImpulseStrongMutSetCreateEffect(KamikazeTargetImpulseStrongMutSetCreateEffect effect) { }
  public void visitKamikazeTargetImpulseStrongMutSetDeleteEffect(KamikazeTargetImpulseStrongMutSetDeleteEffect effect) { }
  public void OnNoImpulseStrongMutSetEffect(INoImpulseStrongMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitNoImpulseStrongMutSetAddEffect(NoImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitNoImpulseStrongMutSetRemoveEffect(NoImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitNoImpulseStrongMutSetCreateEffect(NoImpulseStrongMutSetCreateEffect effect) { }
  public void visitNoImpulseStrongMutSetDeleteEffect(NoImpulseStrongMutSetDeleteEffect effect) { }
  public void OnFireImpulseStrongMutSetEffect(IFireImpulseStrongMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitFireImpulseStrongMutSetAddEffect(FireImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitFireImpulseStrongMutSetRemoveEffect(FireImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitFireImpulseStrongMutSetCreateEffect(FireImpulseStrongMutSetCreateEffect effect) { }
  public void visitFireImpulseStrongMutSetDeleteEffect(FireImpulseStrongMutSetDeleteEffect effect) { }
  public void OnDefyImpulseStrongMutSetEffect(IDefyImpulseStrongMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitDefyImpulseStrongMutSetAddEffect(DefyImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitDefyImpulseStrongMutSetRemoveEffect(DefyImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitDefyImpulseStrongMutSetCreateEffect(DefyImpulseStrongMutSetCreateEffect effect) { }
  public void visitDefyImpulseStrongMutSetDeleteEffect(DefyImpulseStrongMutSetDeleteEffect effect) { }
  public void OnCounterImpulseStrongMutSetEffect(ICounterImpulseStrongMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitCounterImpulseStrongMutSetAddEffect(CounterImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitCounterImpulseStrongMutSetRemoveEffect(CounterImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitCounterImpulseStrongMutSetCreateEffect(CounterImpulseStrongMutSetCreateEffect effect) { }
  public void visitCounterImpulseStrongMutSetDeleteEffect(CounterImpulseStrongMutSetDeleteEffect effect) { }
  public void OnUnleashBideImpulseStrongMutSetEffect(IUnleashBideImpulseStrongMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitUnleashBideImpulseStrongMutSetAddEffect(UnleashBideImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitUnleashBideImpulseStrongMutSetRemoveEffect(UnleashBideImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitUnleashBideImpulseStrongMutSetCreateEffect(UnleashBideImpulseStrongMutSetCreateEffect effect) { }
  public void visitUnleashBideImpulseStrongMutSetDeleteEffect(UnleashBideImpulseStrongMutSetDeleteEffect effect) { }
  public void OnContinueBidingImpulseStrongMutSetEffect(IContinueBidingImpulseStrongMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitContinueBidingImpulseStrongMutSetAddEffect(ContinueBidingImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitContinueBidingImpulseStrongMutSetRemoveEffect(ContinueBidingImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitContinueBidingImpulseStrongMutSetCreateEffect(ContinueBidingImpulseStrongMutSetCreateEffect effect) { }
  public void visitContinueBidingImpulseStrongMutSetDeleteEffect(ContinueBidingImpulseStrongMutSetDeleteEffect effect) { }
  public void OnStartBidingImpulseStrongMutSetEffect(IStartBidingImpulseStrongMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitStartBidingImpulseStrongMutSetAddEffect(StartBidingImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitStartBidingImpulseStrongMutSetRemoveEffect(StartBidingImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitStartBidingImpulseStrongMutSetCreateEffect(StartBidingImpulseStrongMutSetCreateEffect effect) { }
  public void visitStartBidingImpulseStrongMutSetDeleteEffect(StartBidingImpulseStrongMutSetDeleteEffect effect) { }
  public void OnAttackImpulseStrongMutSetEffect(IAttackImpulseStrongMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitAttackImpulseStrongMutSetAddEffect(AttackImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitAttackImpulseStrongMutSetRemoveEffect(AttackImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitAttackImpulseStrongMutSetCreateEffect(AttackImpulseStrongMutSetCreateEffect effect) { }
  public void visitAttackImpulseStrongMutSetDeleteEffect(AttackImpulseStrongMutSetDeleteEffect effect) { }
  public void OnPursueImpulseStrongMutSetEffect(IPursueImpulseStrongMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitPursueImpulseStrongMutSetAddEffect(PursueImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitPursueImpulseStrongMutSetRemoveEffect(PursueImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitPursueImpulseStrongMutSetCreateEffect(PursueImpulseStrongMutSetCreateEffect effect) { }
  public void visitPursueImpulseStrongMutSetDeleteEffect(PursueImpulseStrongMutSetDeleteEffect effect) { }
  public void OnFireBombImpulseStrongMutSetEffect(IFireBombImpulseStrongMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitFireBombImpulseStrongMutSetAddEffect(FireBombImpulseStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitFireBombImpulseStrongMutSetRemoveEffect(FireBombImpulseStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitFireBombImpulseStrongMutSetCreateEffect(FireBombImpulseStrongMutSetCreateEffect effect) { }
  public void visitFireBombImpulseStrongMutSetDeleteEffect(FireBombImpulseStrongMutSetDeleteEffect effect) { }

}
       
}
