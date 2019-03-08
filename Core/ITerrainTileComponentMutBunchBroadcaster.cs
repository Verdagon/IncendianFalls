using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ITerrainTileComponentMutBunchBroadcaster:IArmorMutSetEffectObserver, IArmorMutSetEffectVisitor, IGlaiveMutSetEffectObserver, IGlaiveMutSetEffectVisitor, IManaPotionMutSetEffectObserver, IManaPotionMutSetEffectVisitor, IHealthPotionMutSetEffectObserver, IHealthPotionMutSetEffectVisitor, ITimeAnchorTTCMutSetEffectObserver, ITimeAnchorTTCMutSetEffectVisitor, IStaircaseTTCMutSetEffectObserver, IStaircaseTTCMutSetEffectVisitor, IDecorativeTTCMutSetEffectObserver, IDecorativeTTCMutSetEffectVisitor {
  ITerrainTileComponentMutBunch bunch;
  private List<IITerrainTileComponentMutBunchObserver> observers;

  public ITerrainTileComponentMutBunchBroadcaster(ITerrainTileComponentMutBunch bunch) {
    this.bunch = bunch;
    this.observers = new List<IITerrainTileComponentMutBunchObserver>();
    bunch.membersArmorMutSet.AddObserver(this);
    bunch.membersGlaiveMutSet.AddObserver(this);
    bunch.membersManaPotionMutSet.AddObserver(this);
    bunch.membersHealthPotionMutSet.AddObserver(this);
    bunch.membersTimeAnchorTTCMutSet.AddObserver(this);
    bunch.membersStaircaseTTCMutSet.AddObserver(this);
    bunch.membersDecorativeTTCMutSet.AddObserver(this);

  }
  public void Stop() {
    bunch.membersArmorMutSet.RemoveObserver(this);
    bunch.membersGlaiveMutSet.RemoveObserver(this);
    bunch.membersManaPotionMutSet.RemoveObserver(this);
    bunch.membersHealthPotionMutSet.RemoveObserver(this);
    bunch.membersTimeAnchorTTCMutSet.RemoveObserver(this);
    bunch.membersStaircaseTTCMutSet.RemoveObserver(this);
    bunch.membersDecorativeTTCMutSet.RemoveObserver(this);

  }
  public void AddObserver(IITerrainTileComponentMutBunchObserver observer) {
    this.observers.Add(observer);
  }
  public void RemoveObserver(IITerrainTileComponentMutBunchObserver observer) {
    this.observers.Remove(observer);
  }
  private void BroadcastAdd(int id) {
    foreach (var observer in observers) {
      observer.OnITerrainTileComponentMutBunchAdd(id);
    }
  }
  private void BroadcastRemove(int id) {
    foreach (var observer in observers) {
      observer.OnITerrainTileComponentMutBunchRemove(id);
    }
  }
  public void OnArmorMutSetEffect(IArmorMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitArmorMutSetAddEffect(ArmorMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitArmorMutSetRemoveEffect(ArmorMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitArmorMutSetCreateEffect(ArmorMutSetCreateEffect effect) { }
  public void visitArmorMutSetDeleteEffect(ArmorMutSetDeleteEffect effect) { }
  public void OnGlaiveMutSetEffect(IGlaiveMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitGlaiveMutSetAddEffect(GlaiveMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitGlaiveMutSetRemoveEffect(GlaiveMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitGlaiveMutSetCreateEffect(GlaiveMutSetCreateEffect effect) { }
  public void visitGlaiveMutSetDeleteEffect(GlaiveMutSetDeleteEffect effect) { }
  public void OnManaPotionMutSetEffect(IManaPotionMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitManaPotionMutSetAddEffect(ManaPotionMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitManaPotionMutSetRemoveEffect(ManaPotionMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitManaPotionMutSetCreateEffect(ManaPotionMutSetCreateEffect effect) { }
  public void visitManaPotionMutSetDeleteEffect(ManaPotionMutSetDeleteEffect effect) { }
  public void OnHealthPotionMutSetEffect(IHealthPotionMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitHealthPotionMutSetAddEffect(HealthPotionMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitHealthPotionMutSetRemoveEffect(HealthPotionMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitHealthPotionMutSetCreateEffect(HealthPotionMutSetCreateEffect effect) { }
  public void visitHealthPotionMutSetDeleteEffect(HealthPotionMutSetDeleteEffect effect) { }
  public void OnTimeAnchorTTCMutSetEffect(ITimeAnchorTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitTimeAnchorTTCMutSetAddEffect(TimeAnchorTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitTimeAnchorTTCMutSetRemoveEffect(TimeAnchorTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitTimeAnchorTTCMutSetCreateEffect(TimeAnchorTTCMutSetCreateEffect effect) { }
  public void visitTimeAnchorTTCMutSetDeleteEffect(TimeAnchorTTCMutSetDeleteEffect effect) { }
  public void OnStaircaseTTCMutSetEffect(IStaircaseTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitStaircaseTTCMutSetAddEffect(StaircaseTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitStaircaseTTCMutSetRemoveEffect(StaircaseTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitStaircaseTTCMutSetCreateEffect(StaircaseTTCMutSetCreateEffect effect) { }
  public void visitStaircaseTTCMutSetDeleteEffect(StaircaseTTCMutSetDeleteEffect effect) { }
  public void OnDecorativeTTCMutSetEffect(IDecorativeTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitDecorativeTTCMutSetAddEffect(DecorativeTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitDecorativeTTCMutSetRemoveEffect(DecorativeTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitDecorativeTTCMutSetCreateEffect(DecorativeTTCMutSetCreateEffect effect) { }
  public void visitDecorativeTTCMutSetDeleteEffect(DecorativeTTCMutSetDeleteEffect effect) { }

}
       
}
