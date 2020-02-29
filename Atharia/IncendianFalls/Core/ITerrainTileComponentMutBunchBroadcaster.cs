using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ITerrainTileComponentMutBunchBroadcaster:IItemTTCMutSetEffectObserver, IItemTTCMutSetEffectVisitor, IEmberDeepLevelLinkerTTCMutSetEffectObserver, IEmberDeepLevelLinkerTTCMutSetEffectVisitor, IIncendianFallsLevelLinkerTTCMutSetEffectObserver, IIncendianFallsLevelLinkerTTCMutSetEffectVisitor, ITimeAnchorTTCMutSetEffectObserver, ITimeAnchorTTCMutSetEffectVisitor, ILevelLinkTTCMutSetEffectObserver, ILevelLinkTTCMutSetEffectVisitor, IMudTTCMutSetEffectObserver, IMudTTCMutSetEffectVisitor, IDirtTTCMutSetEffectObserver, IDirtTTCMutSetEffectVisitor, IDownStairsTTCMutSetEffectObserver, IDownStairsTTCMutSetEffectVisitor, IUpStairsTTCMutSetEffectObserver, IUpStairsTTCMutSetEffectVisitor, IWallTTCMutSetEffectObserver, IWallTTCMutSetEffectVisitor, IBloodTTCMutSetEffectObserver, IBloodTTCMutSetEffectVisitor, IRocksTTCMutSetEffectObserver, IRocksTTCMutSetEffectVisitor, ICaveTTCMutSetEffectObserver, ICaveTTCMutSetEffectVisitor, IFallsTTCMutSetEffectObserver, IFallsTTCMutSetEffectVisitor, IMagmaTTCMutSetEffectObserver, IMagmaTTCMutSetEffectVisitor, ICliffTTCMutSetEffectObserver, ICliffTTCMutSetEffectVisitor, IRavaNestTTCMutSetEffectObserver, IRavaNestTTCMutSetEffectVisitor, ICliffLandingTTCMutSetEffectObserver, ICliffLandingTTCMutSetEffectVisitor, IStoneTTCMutSetEffectObserver, IStoneTTCMutSetEffectVisitor, IGrassTTCMutSetEffectObserver, IGrassTTCMutSetEffectVisitor {
  ITerrainTileComponentMutBunch bunch;
  private List<IITerrainTileComponentMutBunchObserver> observers;

  public ITerrainTileComponentMutBunchBroadcaster(ITerrainTileComponentMutBunch bunch) {
    this.bunch = bunch;
    this.observers = new List<IITerrainTileComponentMutBunchObserver>();
    bunch.membersItemTTCMutSet.AddObserver(this);
    bunch.membersEmberDeepLevelLinkerTTCMutSet.AddObserver(this);
    bunch.membersIncendianFallsLevelLinkerTTCMutSet.AddObserver(this);
    bunch.membersTimeAnchorTTCMutSet.AddObserver(this);
    bunch.membersLevelLinkTTCMutSet.AddObserver(this);
    bunch.membersMudTTCMutSet.AddObserver(this);
    bunch.membersDirtTTCMutSet.AddObserver(this);
    bunch.membersDownStairsTTCMutSet.AddObserver(this);
    bunch.membersUpStairsTTCMutSet.AddObserver(this);
    bunch.membersWallTTCMutSet.AddObserver(this);
    bunch.membersBloodTTCMutSet.AddObserver(this);
    bunch.membersRocksTTCMutSet.AddObserver(this);
    bunch.membersCaveTTCMutSet.AddObserver(this);
    bunch.membersFallsTTCMutSet.AddObserver(this);
    bunch.membersMagmaTTCMutSet.AddObserver(this);
    bunch.membersCliffTTCMutSet.AddObserver(this);
    bunch.membersRavaNestTTCMutSet.AddObserver(this);
    bunch.membersCliffLandingTTCMutSet.AddObserver(this);
    bunch.membersStoneTTCMutSet.AddObserver(this);
    bunch.membersGrassTTCMutSet.AddObserver(this);

  }
  public void Stop() {
    bunch.membersItemTTCMutSet.RemoveObserver(this);
    bunch.membersEmberDeepLevelLinkerTTCMutSet.RemoveObserver(this);
    bunch.membersIncendianFallsLevelLinkerTTCMutSet.RemoveObserver(this);
    bunch.membersTimeAnchorTTCMutSet.RemoveObserver(this);
    bunch.membersLevelLinkTTCMutSet.RemoveObserver(this);
    bunch.membersMudTTCMutSet.RemoveObserver(this);
    bunch.membersDirtTTCMutSet.RemoveObserver(this);
    bunch.membersDownStairsTTCMutSet.RemoveObserver(this);
    bunch.membersUpStairsTTCMutSet.RemoveObserver(this);
    bunch.membersWallTTCMutSet.RemoveObserver(this);
    bunch.membersBloodTTCMutSet.RemoveObserver(this);
    bunch.membersRocksTTCMutSet.RemoveObserver(this);
    bunch.membersCaveTTCMutSet.RemoveObserver(this);
    bunch.membersFallsTTCMutSet.RemoveObserver(this);
    bunch.membersMagmaTTCMutSet.RemoveObserver(this);
    bunch.membersCliffTTCMutSet.RemoveObserver(this);
    bunch.membersRavaNestTTCMutSet.RemoveObserver(this);
    bunch.membersCliffLandingTTCMutSet.RemoveObserver(this);
    bunch.membersStoneTTCMutSet.RemoveObserver(this);
    bunch.membersGrassTTCMutSet.RemoveObserver(this);

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
  public void OnEmberDeepLevelLinkerTTCMutSetEffect(IEmberDeepLevelLinkerTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitEmberDeepLevelLinkerTTCMutSetAddEffect(EmberDeepLevelLinkerTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitEmberDeepLevelLinkerTTCMutSetRemoveEffect(EmberDeepLevelLinkerTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitEmberDeepLevelLinkerTTCMutSetCreateEffect(EmberDeepLevelLinkerTTCMutSetCreateEffect effect) { }
  public void visitEmberDeepLevelLinkerTTCMutSetDeleteEffect(EmberDeepLevelLinkerTTCMutSetDeleteEffect effect) { }
  public void OnIncendianFallsLevelLinkerTTCMutSetEffect(IIncendianFallsLevelLinkerTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitIncendianFallsLevelLinkerTTCMutSetAddEffect(IncendianFallsLevelLinkerTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitIncendianFallsLevelLinkerTTCMutSetRemoveEffect(IncendianFallsLevelLinkerTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitIncendianFallsLevelLinkerTTCMutSetCreateEffect(IncendianFallsLevelLinkerTTCMutSetCreateEffect effect) { }
  public void visitIncendianFallsLevelLinkerTTCMutSetDeleteEffect(IncendianFallsLevelLinkerTTCMutSetDeleteEffect effect) { }
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
  public void OnLevelLinkTTCMutSetEffect(ILevelLinkTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitLevelLinkTTCMutSetAddEffect(LevelLinkTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitLevelLinkTTCMutSetRemoveEffect(LevelLinkTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitLevelLinkTTCMutSetCreateEffect(LevelLinkTTCMutSetCreateEffect effect) { }
  public void visitLevelLinkTTCMutSetDeleteEffect(LevelLinkTTCMutSetDeleteEffect effect) { }
  public void OnMudTTCMutSetEffect(IMudTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitMudTTCMutSetAddEffect(MudTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitMudTTCMutSetRemoveEffect(MudTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitMudTTCMutSetCreateEffect(MudTTCMutSetCreateEffect effect) { }
  public void visitMudTTCMutSetDeleteEffect(MudTTCMutSetDeleteEffect effect) { }
  public void OnDirtTTCMutSetEffect(IDirtTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitDirtTTCMutSetAddEffect(DirtTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitDirtTTCMutSetRemoveEffect(DirtTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitDirtTTCMutSetCreateEffect(DirtTTCMutSetCreateEffect effect) { }
  public void visitDirtTTCMutSetDeleteEffect(DirtTTCMutSetDeleteEffect effect) { }
  public void OnDownStairsTTCMutSetEffect(IDownStairsTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitDownStairsTTCMutSetAddEffect(DownStairsTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitDownStairsTTCMutSetRemoveEffect(DownStairsTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitDownStairsTTCMutSetCreateEffect(DownStairsTTCMutSetCreateEffect effect) { }
  public void visitDownStairsTTCMutSetDeleteEffect(DownStairsTTCMutSetDeleteEffect effect) { }
  public void OnUpStairsTTCMutSetEffect(IUpStairsTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitUpStairsTTCMutSetAddEffect(UpStairsTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitUpStairsTTCMutSetRemoveEffect(UpStairsTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitUpStairsTTCMutSetCreateEffect(UpStairsTTCMutSetCreateEffect effect) { }
  public void visitUpStairsTTCMutSetDeleteEffect(UpStairsTTCMutSetDeleteEffect effect) { }
  public void OnWallTTCMutSetEffect(IWallTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitWallTTCMutSetAddEffect(WallTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitWallTTCMutSetRemoveEffect(WallTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitWallTTCMutSetCreateEffect(WallTTCMutSetCreateEffect effect) { }
  public void visitWallTTCMutSetDeleteEffect(WallTTCMutSetDeleteEffect effect) { }
  public void OnBloodTTCMutSetEffect(IBloodTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitBloodTTCMutSetAddEffect(BloodTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitBloodTTCMutSetRemoveEffect(BloodTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitBloodTTCMutSetCreateEffect(BloodTTCMutSetCreateEffect effect) { }
  public void visitBloodTTCMutSetDeleteEffect(BloodTTCMutSetDeleteEffect effect) { }
  public void OnRocksTTCMutSetEffect(IRocksTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitRocksTTCMutSetAddEffect(RocksTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitRocksTTCMutSetRemoveEffect(RocksTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitRocksTTCMutSetCreateEffect(RocksTTCMutSetCreateEffect effect) { }
  public void visitRocksTTCMutSetDeleteEffect(RocksTTCMutSetDeleteEffect effect) { }
  public void OnCaveTTCMutSetEffect(ICaveTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitCaveTTCMutSetAddEffect(CaveTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitCaveTTCMutSetRemoveEffect(CaveTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitCaveTTCMutSetCreateEffect(CaveTTCMutSetCreateEffect effect) { }
  public void visitCaveTTCMutSetDeleteEffect(CaveTTCMutSetDeleteEffect effect) { }
  public void OnFallsTTCMutSetEffect(IFallsTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitFallsTTCMutSetAddEffect(FallsTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitFallsTTCMutSetRemoveEffect(FallsTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitFallsTTCMutSetCreateEffect(FallsTTCMutSetCreateEffect effect) { }
  public void visitFallsTTCMutSetDeleteEffect(FallsTTCMutSetDeleteEffect effect) { }
  public void OnMagmaTTCMutSetEffect(IMagmaTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitMagmaTTCMutSetAddEffect(MagmaTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitMagmaTTCMutSetRemoveEffect(MagmaTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitMagmaTTCMutSetCreateEffect(MagmaTTCMutSetCreateEffect effect) { }
  public void visitMagmaTTCMutSetDeleteEffect(MagmaTTCMutSetDeleteEffect effect) { }
  public void OnCliffTTCMutSetEffect(ICliffTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitCliffTTCMutSetAddEffect(CliffTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitCliffTTCMutSetRemoveEffect(CliffTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitCliffTTCMutSetCreateEffect(CliffTTCMutSetCreateEffect effect) { }
  public void visitCliffTTCMutSetDeleteEffect(CliffTTCMutSetDeleteEffect effect) { }
  public void OnRavaNestTTCMutSetEffect(IRavaNestTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitRavaNestTTCMutSetAddEffect(RavaNestTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitRavaNestTTCMutSetRemoveEffect(RavaNestTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitRavaNestTTCMutSetCreateEffect(RavaNestTTCMutSetCreateEffect effect) { }
  public void visitRavaNestTTCMutSetDeleteEffect(RavaNestTTCMutSetDeleteEffect effect) { }
  public void OnCliffLandingTTCMutSetEffect(ICliffLandingTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitCliffLandingTTCMutSetAddEffect(CliffLandingTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitCliffLandingTTCMutSetRemoveEffect(CliffLandingTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitCliffLandingTTCMutSetCreateEffect(CliffLandingTTCMutSetCreateEffect effect) { }
  public void visitCliffLandingTTCMutSetDeleteEffect(CliffLandingTTCMutSetDeleteEffect effect) { }
  public void OnStoneTTCMutSetEffect(IStoneTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitStoneTTCMutSetAddEffect(StoneTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitStoneTTCMutSetRemoveEffect(StoneTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitStoneTTCMutSetCreateEffect(StoneTTCMutSetCreateEffect effect) { }
  public void visitStoneTTCMutSetDeleteEffect(StoneTTCMutSetDeleteEffect effect) { }
  public void OnGrassTTCMutSetEffect(IGrassTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitGrassTTCMutSetAddEffect(GrassTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitGrassTTCMutSetRemoveEffect(GrassTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitGrassTTCMutSetCreateEffect(GrassTTCMutSetCreateEffect effect) { }
  public void visitGrassTTCMutSetDeleteEffect(GrassTTCMutSetDeleteEffect effect) { }

}
       
}
