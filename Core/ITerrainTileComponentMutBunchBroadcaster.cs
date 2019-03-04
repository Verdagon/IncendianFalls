using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ITerrainTileComponentMutBunchBroadcaster:ITimeAnchorTTCMutSetEffectObserver, ITimeAnchorTTCMutSetEffectVisitor, IItemTTCMutSetEffectObserver, IItemTTCMutSetEffectVisitor, IDecorativeTTCMutSetEffectObserver, IDecorativeTTCMutSetEffectVisitor, IUpStaircaseTTCMutSetEffectObserver, IUpStaircaseTTCMutSetEffectVisitor, IDownStaircaseTTCMutSetEffectObserver, IDownStaircaseTTCMutSetEffectVisitor {
  ITerrainTileComponentMutBunch bunch;
  private List<IITerrainTileComponentMutBunchObserver> observers;

  public ITerrainTileComponentMutBunchBroadcaster(ITerrainTileComponentMutBunch bunch) {
    this.bunch = bunch;
    this.observers = new List<IITerrainTileComponentMutBunchObserver>();
    bunch.membersTimeAnchorTTCMutSet.AddObserver(this);
    bunch.membersItemTTCMutSet.AddObserver(this);
    bunch.membersDecorativeTTCMutSet.AddObserver(this);
    bunch.membersUpStaircaseTTCMutSet.AddObserver(this);
    bunch.membersDownStaircaseTTCMutSet.AddObserver(this);

  }
  public void Stop() {
    bunch.membersTimeAnchorTTCMutSet.RemoveObserver(this);
    bunch.membersItemTTCMutSet.RemoveObserver(this);
    bunch.membersDecorativeTTCMutSet.RemoveObserver(this);
    bunch.membersUpStaircaseTTCMutSet.RemoveObserver(this);
    bunch.membersDownStaircaseTTCMutSet.RemoveObserver(this);

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
  public void OnUpStaircaseTTCMutSetEffect(IUpStaircaseTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitUpStaircaseTTCMutSetAddEffect(UpStaircaseTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitUpStaircaseTTCMutSetRemoveEffect(UpStaircaseTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitUpStaircaseTTCMutSetCreateEffect(UpStaircaseTTCMutSetCreateEffect effect) { }
  public void visitUpStaircaseTTCMutSetDeleteEffect(UpStaircaseTTCMutSetDeleteEffect effect) { }
  public void OnDownStaircaseTTCMutSetEffect(IDownStaircaseTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitDownStaircaseTTCMutSetAddEffect(DownStaircaseTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitDownStaircaseTTCMutSetRemoveEffect(DownStaircaseTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitDownStaircaseTTCMutSetCreateEffect(DownStaircaseTTCMutSetCreateEffect effect) { }
  public void visitDownStaircaseTTCMutSetDeleteEffect(DownStaircaseTTCMutSetDeleteEffect effect) { }

}
       
}
