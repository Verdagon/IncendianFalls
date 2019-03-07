using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ITerrainTileComponentMutBunchBroadcaster:ITimeAnchorTTCMutSetEffectObserver, ITimeAnchorTTCMutSetEffectVisitor, IStaircaseTTCMutSetEffectObserver, IStaircaseTTCMutSetEffectVisitor, IItemTTCMutSetEffectObserver, IItemTTCMutSetEffectVisitor, IDecorativeTTCMutSetEffectObserver, IDecorativeTTCMutSetEffectVisitor {
  ITerrainTileComponentMutBunch bunch;
  private List<IITerrainTileComponentMutBunchObserver> observers;

  public ITerrainTileComponentMutBunchBroadcaster(ITerrainTileComponentMutBunch bunch) {
    this.bunch = bunch;
    this.observers = new List<IITerrainTileComponentMutBunchObserver>();
    bunch.membersTimeAnchorTTCMutSet.AddObserver(this);
    bunch.membersStaircaseTTCMutSet.AddObserver(this);
    bunch.membersItemTTCMutSet.AddObserver(this);
    bunch.membersDecorativeTTCMutSet.AddObserver(this);

  }
  public void Stop() {
    bunch.membersTimeAnchorTTCMutSet.RemoveObserver(this);
    bunch.membersStaircaseTTCMutSet.RemoveObserver(this);
    bunch.membersItemTTCMutSet.RemoveObserver(this);
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
  public void OnItemTTCMutSetEffect(IItemTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitItemTTCMutSetAddEffect(ItemTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitItemTTCMutSetRemoveEffect(ItemTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitItemTTCMutSetCreateEffect(ItemTTCMutSetCreateEffect effect) { }
  public void visitItemTTCMutSetDeleteEffect(ItemTTCMutSetDeleteEffect effect) { }
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
