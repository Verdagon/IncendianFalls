using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class IItemStrongMutBunchBroadcaster:IManaPotionStrongMutSetEffectObserver, IManaPotionStrongMutSetEffectVisitor, IHealthPotionStrongMutSetEffectObserver, IHealthPotionStrongMutSetEffectVisitor, ISpeedRingStrongMutSetEffectObserver, ISpeedRingStrongMutSetEffectVisitor, IGlaiveStrongMutSetEffectObserver, IGlaiveStrongMutSetEffectVisitor, IBlastRodStrongMutSetEffectObserver, IBlastRodStrongMutSetEffectVisitor, IArmorStrongMutSetEffectObserver, IArmorStrongMutSetEffectVisitor {
  IItemStrongMutBunch bunch;
  private List<IIItemStrongMutBunchObserver> observers;

  public IItemStrongMutBunchBroadcaster(IItemStrongMutBunch bunch) {
    this.bunch = bunch;
    this.observers = new List<IIItemStrongMutBunchObserver>();
    bunch.membersManaPotionStrongMutSet.AddObserver(this);
    bunch.membersHealthPotionStrongMutSet.AddObserver(this);
    bunch.membersSpeedRingStrongMutSet.AddObserver(this);
    bunch.membersGlaiveStrongMutSet.AddObserver(this);
    bunch.membersBlastRodStrongMutSet.AddObserver(this);
    bunch.membersArmorStrongMutSet.AddObserver(this);

  }
  public void Stop() {
    bunch.membersManaPotionStrongMutSet.RemoveObserver(this);
    bunch.membersHealthPotionStrongMutSet.RemoveObserver(this);
    bunch.membersSpeedRingStrongMutSet.RemoveObserver(this);
    bunch.membersGlaiveStrongMutSet.RemoveObserver(this);
    bunch.membersBlastRodStrongMutSet.RemoveObserver(this);
    bunch.membersArmorStrongMutSet.RemoveObserver(this);

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
    effect.visit(this);
  }
  public void visitManaPotionStrongMutSetAddEffect(ManaPotionStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitManaPotionStrongMutSetRemoveEffect(ManaPotionStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitManaPotionStrongMutSetCreateEffect(ManaPotionStrongMutSetCreateEffect effect) { }
  public void visitManaPotionStrongMutSetDeleteEffect(ManaPotionStrongMutSetDeleteEffect effect) { }
  public void OnHealthPotionStrongMutSetEffect(IHealthPotionStrongMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitHealthPotionStrongMutSetAddEffect(HealthPotionStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitHealthPotionStrongMutSetRemoveEffect(HealthPotionStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitHealthPotionStrongMutSetCreateEffect(HealthPotionStrongMutSetCreateEffect effect) { }
  public void visitHealthPotionStrongMutSetDeleteEffect(HealthPotionStrongMutSetDeleteEffect effect) { }
  public void OnSpeedRingStrongMutSetEffect(ISpeedRingStrongMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitSpeedRingStrongMutSetAddEffect(SpeedRingStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitSpeedRingStrongMutSetRemoveEffect(SpeedRingStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitSpeedRingStrongMutSetCreateEffect(SpeedRingStrongMutSetCreateEffect effect) { }
  public void visitSpeedRingStrongMutSetDeleteEffect(SpeedRingStrongMutSetDeleteEffect effect) { }
  public void OnGlaiveStrongMutSetEffect(IGlaiveStrongMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitGlaiveStrongMutSetAddEffect(GlaiveStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitGlaiveStrongMutSetRemoveEffect(GlaiveStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitGlaiveStrongMutSetCreateEffect(GlaiveStrongMutSetCreateEffect effect) { }
  public void visitGlaiveStrongMutSetDeleteEffect(GlaiveStrongMutSetDeleteEffect effect) { }
  public void OnBlastRodStrongMutSetEffect(IBlastRodStrongMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitBlastRodStrongMutSetAddEffect(BlastRodStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitBlastRodStrongMutSetRemoveEffect(BlastRodStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitBlastRodStrongMutSetCreateEffect(BlastRodStrongMutSetCreateEffect effect) { }
  public void visitBlastRodStrongMutSetDeleteEffect(BlastRodStrongMutSetDeleteEffect effect) { }
  public void OnArmorStrongMutSetEffect(IArmorStrongMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitArmorStrongMutSetAddEffect(ArmorStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitArmorStrongMutSetRemoveEffect(ArmorStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitArmorStrongMutSetCreateEffect(ArmorStrongMutSetCreateEffect effect) { }
  public void visitArmorStrongMutSetDeleteEffect(ArmorStrongMutSetDeleteEffect effect) { }

}
       
}
