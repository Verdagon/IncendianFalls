using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class IItemStrongMutBunchBroadcaster:IArmorStrongMutSetEffectObserver, IArmorStrongMutSetEffectVisitor, IInertiaRingStrongMutSetEffectObserver, IInertiaRingStrongMutSetEffectVisitor, IGlaiveStrongMutSetEffectObserver, IGlaiveStrongMutSetEffectVisitor, IManaPotionStrongMutSetEffectObserver, IManaPotionStrongMutSetEffectVisitor, IHealthPotionStrongMutSetEffectObserver, IHealthPotionStrongMutSetEffectVisitor {
  IItemStrongMutBunch bunch;
  private List<IIItemStrongMutBunchObserver> observers;

  public IItemStrongMutBunchBroadcaster(IItemStrongMutBunch bunch) {
    this.bunch = bunch;
    this.observers = new List<IIItemStrongMutBunchObserver>();
    bunch.membersArmorStrongMutSet.AddObserver(this);
    bunch.membersInertiaRingStrongMutSet.AddObserver(this);
    bunch.membersGlaiveStrongMutSet.AddObserver(this);
    bunch.membersManaPotionStrongMutSet.AddObserver(this);
    bunch.membersHealthPotionStrongMutSet.AddObserver(this);

  }
  public void Stop() {
    bunch.membersArmorStrongMutSet.RemoveObserver(this);
    bunch.membersInertiaRingStrongMutSet.RemoveObserver(this);
    bunch.membersGlaiveStrongMutSet.RemoveObserver(this);
    bunch.membersManaPotionStrongMutSet.RemoveObserver(this);
    bunch.membersHealthPotionStrongMutSet.RemoveObserver(this);

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
  public void OnInertiaRingStrongMutSetEffect(IInertiaRingStrongMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitInertiaRingStrongMutSetAddEffect(InertiaRingStrongMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitInertiaRingStrongMutSetRemoveEffect(InertiaRingStrongMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitInertiaRingStrongMutSetCreateEffect(InertiaRingStrongMutSetCreateEffect effect) { }
  public void visitInertiaRingStrongMutSetDeleteEffect(InertiaRingStrongMutSetDeleteEffect effect) { }
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

}
       
}
