using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class IItemStrongMutBunchBroadcaster:IManaPotionStrongMutSetEffectObserver, IManaPotionStrongMutSetEffectVisitor, IHealthPotionStrongMutSetEffectObserver, IHealthPotionStrongMutSetEffectVisitor, ISpeedRingStrongMutSetEffectObserver, ISpeedRingStrongMutSetEffectVisitor, IGlaiveStrongMutSetEffectObserver, IGlaiveStrongMutSetEffectVisitor, ISlowRodStrongMutSetEffectObserver, ISlowRodStrongMutSetEffectVisitor, IBlastRodStrongMutSetEffectObserver, IBlastRodStrongMutSetEffectVisitor, IArmorStrongMutSetEffectObserver, IArmorStrongMutSetEffectVisitor {
  EffectBroadcaster broadcaster;
  IItemStrongMutBunch bunch;
  private List<IIItemStrongMutBunchObserver> observers;

  public IItemStrongMutBunchBroadcaster(EffectBroadcaster broadcaster, IItemStrongMutBunch bunch) {
    this.broadcaster = broadcaster;
    this.bunch = bunch;
    this.observers = new List<IIItemStrongMutBunchObserver>();
    bunch.membersManaPotionStrongMutSet.AddObserver(broadcaster, this);
    bunch.membersHealthPotionStrongMutSet.AddObserver(broadcaster, this);
    bunch.membersSpeedRingStrongMutSet.AddObserver(broadcaster, this);
    bunch.membersGlaiveStrongMutSet.AddObserver(broadcaster, this);
    bunch.membersSlowRodStrongMutSet.AddObserver(broadcaster, this);
    bunch.membersBlastRodStrongMutSet.AddObserver(broadcaster, this);
    bunch.membersArmorStrongMutSet.AddObserver(broadcaster, this);

  }
  public void Stop() {
    bunch.membersManaPotionStrongMutSet.RemoveObserver(broadcaster, this);
    bunch.membersHealthPotionStrongMutSet.RemoveObserver(broadcaster, this);
    bunch.membersSpeedRingStrongMutSet.RemoveObserver(broadcaster, this);
    bunch.membersGlaiveStrongMutSet.RemoveObserver(broadcaster, this);
    bunch.membersSlowRodStrongMutSet.RemoveObserver(broadcaster, this);
    bunch.membersBlastRodStrongMutSet.RemoveObserver(broadcaster, this);
    bunch.membersArmorStrongMutSet.RemoveObserver(broadcaster, this);

  }
  public void AddObserver(IIItemStrongMutBunchObserver observer) {
    this.observers.Add(observer);
  }
  public void RemoveObserver(IIItemStrongMutBunchObserver observer) {
    this.observers.Remove(observer);
  }
  private void BroadcastAdd(int id) {
    foreach (var observer in observers) {
      observer.OnIItemStrongMutBunchAdd(id);
    }
  }
  private void BroadcastRemove(int id) {
    foreach (var observer in observers) {
      observer.OnIItemStrongMutBunchRemove(id);
    }
  }
  public void OnManaPotionStrongMutSetEffect(IManaPotionStrongMutSetEffect effect) {
    effect.visitIManaPotionStrongMutSetEffect(this);
  }
  public void visitManaPotionStrongMutSetAddEffect(ManaPotionStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitManaPotionStrongMutSetRemoveEffect(ManaPotionStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitManaPotionStrongMutSetCreateEffect(ManaPotionStrongMutSetCreateEffect effect) { }
  public void visitManaPotionStrongMutSetDeleteEffect(ManaPotionStrongMutSetDeleteEffect effect) { }
  public void OnHealthPotionStrongMutSetEffect(IHealthPotionStrongMutSetEffect effect) {
    effect.visitIHealthPotionStrongMutSetEffect(this);
  }
  public void visitHealthPotionStrongMutSetAddEffect(HealthPotionStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitHealthPotionStrongMutSetRemoveEffect(HealthPotionStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitHealthPotionStrongMutSetCreateEffect(HealthPotionStrongMutSetCreateEffect effect) { }
  public void visitHealthPotionStrongMutSetDeleteEffect(HealthPotionStrongMutSetDeleteEffect effect) { }
  public void OnSpeedRingStrongMutSetEffect(ISpeedRingStrongMutSetEffect effect) {
    effect.visitISpeedRingStrongMutSetEffect(this);
  }
  public void visitSpeedRingStrongMutSetAddEffect(SpeedRingStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitSpeedRingStrongMutSetRemoveEffect(SpeedRingStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitSpeedRingStrongMutSetCreateEffect(SpeedRingStrongMutSetCreateEffect effect) { }
  public void visitSpeedRingStrongMutSetDeleteEffect(SpeedRingStrongMutSetDeleteEffect effect) { }
  public void OnGlaiveStrongMutSetEffect(IGlaiveStrongMutSetEffect effect) {
    effect.visitIGlaiveStrongMutSetEffect(this);
  }
  public void visitGlaiveStrongMutSetAddEffect(GlaiveStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitGlaiveStrongMutSetRemoveEffect(GlaiveStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitGlaiveStrongMutSetCreateEffect(GlaiveStrongMutSetCreateEffect effect) { }
  public void visitGlaiveStrongMutSetDeleteEffect(GlaiveStrongMutSetDeleteEffect effect) { }
  public void OnSlowRodStrongMutSetEffect(ISlowRodStrongMutSetEffect effect) {
    effect.visitISlowRodStrongMutSetEffect(this);
  }
  public void visitSlowRodStrongMutSetAddEffect(SlowRodStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitSlowRodStrongMutSetRemoveEffect(SlowRodStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitSlowRodStrongMutSetCreateEffect(SlowRodStrongMutSetCreateEffect effect) { }
  public void visitSlowRodStrongMutSetDeleteEffect(SlowRodStrongMutSetDeleteEffect effect) { }
  public void OnBlastRodStrongMutSetEffect(IBlastRodStrongMutSetEffect effect) {
    effect.visitIBlastRodStrongMutSetEffect(this);
  }
  public void visitBlastRodStrongMutSetAddEffect(BlastRodStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitBlastRodStrongMutSetRemoveEffect(BlastRodStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitBlastRodStrongMutSetCreateEffect(BlastRodStrongMutSetCreateEffect effect) { }
  public void visitBlastRodStrongMutSetDeleteEffect(BlastRodStrongMutSetDeleteEffect effect) { }
  public void OnArmorStrongMutSetEffect(IArmorStrongMutSetEffect effect) {
    effect.visitIArmorStrongMutSetEffect(this);
  }
  public void visitArmorStrongMutSetAddEffect(ArmorStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitArmorStrongMutSetRemoveEffect(ArmorStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitArmorStrongMutSetCreateEffect(ArmorStrongMutSetCreateEffect effect) { }
  public void visitArmorStrongMutSetDeleteEffect(ArmorStrongMutSetDeleteEffect effect) { }

}
       
}
