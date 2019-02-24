using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ITerrainTileComponentMutBunchBroadcaster:IDecorativeTerrainTileComponentMutSetEffectObserver, IDecorativeTerrainTileComponentMutSetEffectVisitor, IUpStaircaseTerrainTileComponentMutSetEffectObserver, IUpStaircaseTerrainTileComponentMutSetEffectVisitor, IDownStaircaseTerrainTileComponentMutSetEffectObserver, IDownStaircaseTerrainTileComponentMutSetEffectVisitor {
  ITerrainTileComponentMutBunch bunch;
  private List<IITerrainTileComponentMutBunchObserver> observers;

  public ITerrainTileComponentMutBunchBroadcaster(ITerrainTileComponentMutBunch bunch) {
    this.bunch = bunch;
    this.observers = new List<IITerrainTileComponentMutBunchObserver>();
    bunch.membersDecorativeTerrainTileComponentMutSet.AddObserver(this);
    bunch.membersUpStaircaseTerrainTileComponentMutSet.AddObserver(this);
    bunch.membersDownStaircaseTerrainTileComponentMutSet.AddObserver(this);

  }
  public void Stop() {
    bunch.membersDecorativeTerrainTileComponentMutSet.RemoveObserver(this);
    bunch.membersUpStaircaseTerrainTileComponentMutSet.RemoveObserver(this);
    bunch.membersDownStaircaseTerrainTileComponentMutSet.RemoveObserver(this);

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
  public void OnDecorativeTerrainTileComponentMutSetEffect(IDecorativeTerrainTileComponentMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitDecorativeTerrainTileComponentMutSetAddEffect(DecorativeTerrainTileComponentMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitDecorativeTerrainTileComponentMutSetRemoveEffect(DecorativeTerrainTileComponentMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitDecorativeTerrainTileComponentMutSetCreateEffect(DecorativeTerrainTileComponentMutSetCreateEffect effect) { }
  public void visitDecorativeTerrainTileComponentMutSetDeleteEffect(DecorativeTerrainTileComponentMutSetDeleteEffect effect) { }
  public void OnUpStaircaseTerrainTileComponentMutSetEffect(IUpStaircaseTerrainTileComponentMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitUpStaircaseTerrainTileComponentMutSetAddEffect(UpStaircaseTerrainTileComponentMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitUpStaircaseTerrainTileComponentMutSetRemoveEffect(UpStaircaseTerrainTileComponentMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitUpStaircaseTerrainTileComponentMutSetCreateEffect(UpStaircaseTerrainTileComponentMutSetCreateEffect effect) { }
  public void visitUpStaircaseTerrainTileComponentMutSetDeleteEffect(UpStaircaseTerrainTileComponentMutSetDeleteEffect effect) { }
  public void OnDownStaircaseTerrainTileComponentMutSetEffect(IDownStaircaseTerrainTileComponentMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitDownStaircaseTerrainTileComponentMutSetAddEffect(DownStaircaseTerrainTileComponentMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitDownStaircaseTerrainTileComponentMutSetRemoveEffect(DownStaircaseTerrainTileComponentMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitDownStaircaseTerrainTileComponentMutSetCreateEffect(DownStaircaseTerrainTileComponentMutSetCreateEffect effect) { }
  public void visitDownStaircaseTerrainTileComponentMutSetDeleteEffect(DownStaircaseTerrainTileComponentMutSetDeleteEffect effect) { }

}
       
}
