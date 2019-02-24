using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class IItemMutBunchBroadcaster:IGlaiveMutSetEffectObserver, IGlaiveMutSetEffectVisitor, IArmorMutSetEffectObserver, IArmorMutSetEffectVisitor {
  IItemMutBunch bunch;
  private List<IIItemMutBunchObserver> observers;

  public IItemMutBunchBroadcaster(IItemMutBunch bunch) {
    this.bunch = bunch;
    this.observers = new List<IIItemMutBunchObserver>();
    bunch.membersGlaiveMutSet.AddObserver(this);
    bunch.membersArmorMutSet.AddObserver(this);

  }
  public void Stop() {
    bunch.membersGlaiveMutSet.RemoveObserver(this);
    bunch.membersArmorMutSet.RemoveObserver(this);

  }
  public void AddObserver(IIItemMutBunchObserver observer) {
    this.observers.Add(observer);
  }
  public void RemoveObserver(IIItemMutBunchObserver observer) {
    this.observers.Remove(observer);
  }
  private void BroadcastAdd(int id) {
    foreach (var observer in observers) {
      observer.OnIItemMutBunchAdd(id);
    }
  }
  private void BroadcastRemove(int id) {
    foreach (var observer in observers) {
      observer.OnIItemMutBunchRemove(id);
    }
  }
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

}
       
}
