using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ITerrainTileComponentMutBunchBroadcaster:ISimplePresenceTriggerTTCMutSetEffectObserver, ISimplePresenceTriggerTTCMutSetEffectVisitor, IItemTTCMutSetEffectObserver, IItemTTCMutSetEffectVisitor, IKamikazeTargetTTCMutSetEffectObserver, IKamikazeTargetTTCMutSetEffectVisitor, IWarperTTCMutSetEffectObserver, IWarperTTCMutSetEffectVisitor, ITimeAnchorTTCMutSetEffectObserver, ITimeAnchorTTCMutSetEffectVisitor, IFireBombTTCMutSetEffectObserver, IFireBombTTCMutSetEffectVisitor, IMarkerTTCMutSetEffectObserver, IMarkerTTCMutSetEffectVisitor, ILevelLinkTTCMutSetEffectObserver, ILevelLinkTTCMutSetEffectVisitor, IMudTTCMutSetEffectObserver, IMudTTCMutSetEffectVisitor, IDirtTTCMutSetEffectObserver, IDirtTTCMutSetEffectVisitor, IObsidianTTCMutSetEffectObserver, IObsidianTTCMutSetEffectVisitor, IDownStairsTTCMutSetEffectObserver, IDownStairsTTCMutSetEffectVisitor, IUpStairsTTCMutSetEffectObserver, IUpStairsTTCMutSetEffectVisitor, IWallTTCMutSetEffectObserver, IWallTTCMutSetEffectVisitor, IBloodTTCMutSetEffectObserver, IBloodTTCMutSetEffectVisitor, IRocksTTCMutSetEffectObserver, IRocksTTCMutSetEffectVisitor, ITreeTTCMutSetEffectObserver, ITreeTTCMutSetEffectVisitor, IWaterTTCMutSetEffectObserver, IWaterTTCMutSetEffectVisitor, IFloorTTCMutSetEffectObserver, IFloorTTCMutSetEffectVisitor, ICaveWallTTCMutSetEffectObserver, ICaveWallTTCMutSetEffectVisitor, ICaveTTCMutSetEffectObserver, ICaveTTCMutSetEffectVisitor, IFallsTTCMutSetEffectObserver, IFallsTTCMutSetEffectVisitor, IFireTTCMutSetEffectObserver, IFireTTCMutSetEffectVisitor, IMagmaTTCMutSetEffectObserver, IMagmaTTCMutSetEffectVisitor, ICliffTTCMutSetEffectObserver, ICliffTTCMutSetEffectVisitor, IRavaNestTTCMutSetEffectObserver, IRavaNestTTCMutSetEffectVisitor, ICliffLandingTTCMutSetEffectObserver, ICliffLandingTTCMutSetEffectVisitor, IStoneTTCMutSetEffectObserver, IStoneTTCMutSetEffectVisitor, IGrassTTCMutSetEffectObserver, IGrassTTCMutSetEffectVisitor, IIncendianFallsLevelLinkerTTCMutSetEffectObserver, IIncendianFallsLevelLinkerTTCMutSetEffectVisitor, IEmberDeepLevelLinkerTTCMutSetEffectObserver, IEmberDeepLevelLinkerTTCMutSetEffectVisitor {
  ITerrainTileComponentMutBunch bunch;
  private List<IITerrainTileComponentMutBunchObserver> observers;

  public ITerrainTileComponentMutBunchBroadcaster(ITerrainTileComponentMutBunch bunch) {
    this.bunch = bunch;
    this.observers = new List<IITerrainTileComponentMutBunchObserver>();
    bunch.membersSimplePresenceTriggerTTCMutSet.AddObserver(this);
    bunch.membersItemTTCMutSet.AddObserver(this);
    bunch.membersKamikazeTargetTTCMutSet.AddObserver(this);
    bunch.membersWarperTTCMutSet.AddObserver(this);
    bunch.membersTimeAnchorTTCMutSet.AddObserver(this);
    bunch.membersFireBombTTCMutSet.AddObserver(this);
    bunch.membersMarkerTTCMutSet.AddObserver(this);
    bunch.membersLevelLinkTTCMutSet.AddObserver(this);
    bunch.membersMudTTCMutSet.AddObserver(this);
    bunch.membersDirtTTCMutSet.AddObserver(this);
    bunch.membersObsidianTTCMutSet.AddObserver(this);
    bunch.membersDownStairsTTCMutSet.AddObserver(this);
    bunch.membersUpStairsTTCMutSet.AddObserver(this);
    bunch.membersWallTTCMutSet.AddObserver(this);
    bunch.membersBloodTTCMutSet.AddObserver(this);
    bunch.membersRocksTTCMutSet.AddObserver(this);
    bunch.membersTreeTTCMutSet.AddObserver(this);
    bunch.membersWaterTTCMutSet.AddObserver(this);
    bunch.membersFloorTTCMutSet.AddObserver(this);
    bunch.membersCaveWallTTCMutSet.AddObserver(this);
    bunch.membersCaveTTCMutSet.AddObserver(this);
    bunch.membersFallsTTCMutSet.AddObserver(this);
    bunch.membersFireTTCMutSet.AddObserver(this);
    bunch.membersMagmaTTCMutSet.AddObserver(this);
    bunch.membersCliffTTCMutSet.AddObserver(this);
    bunch.membersRavaNestTTCMutSet.AddObserver(this);
    bunch.membersCliffLandingTTCMutSet.AddObserver(this);
    bunch.membersStoneTTCMutSet.AddObserver(this);
    bunch.membersGrassTTCMutSet.AddObserver(this);
    bunch.membersIncendianFallsLevelLinkerTTCMutSet.AddObserver(this);
    bunch.membersEmberDeepLevelLinkerTTCMutSet.AddObserver(this);

  }
  public void Stop() {
    bunch.membersSimplePresenceTriggerTTCMutSet.RemoveObserver(this);
    bunch.membersItemTTCMutSet.RemoveObserver(this);
    bunch.membersKamikazeTargetTTCMutSet.RemoveObserver(this);
    bunch.membersWarperTTCMutSet.RemoveObserver(this);
    bunch.membersTimeAnchorTTCMutSet.RemoveObserver(this);
    bunch.membersFireBombTTCMutSet.RemoveObserver(this);
    bunch.membersMarkerTTCMutSet.RemoveObserver(this);
    bunch.membersLevelLinkTTCMutSet.RemoveObserver(this);
    bunch.membersMudTTCMutSet.RemoveObserver(this);
    bunch.membersDirtTTCMutSet.RemoveObserver(this);
    bunch.membersObsidianTTCMutSet.RemoveObserver(this);
    bunch.membersDownStairsTTCMutSet.RemoveObserver(this);
    bunch.membersUpStairsTTCMutSet.RemoveObserver(this);
    bunch.membersWallTTCMutSet.RemoveObserver(this);
    bunch.membersBloodTTCMutSet.RemoveObserver(this);
    bunch.membersRocksTTCMutSet.RemoveObserver(this);
    bunch.membersTreeTTCMutSet.RemoveObserver(this);
    bunch.membersWaterTTCMutSet.RemoveObserver(this);
    bunch.membersFloorTTCMutSet.RemoveObserver(this);
    bunch.membersCaveWallTTCMutSet.RemoveObserver(this);
    bunch.membersCaveTTCMutSet.RemoveObserver(this);
    bunch.membersFallsTTCMutSet.RemoveObserver(this);
    bunch.membersFireTTCMutSet.RemoveObserver(this);
    bunch.membersMagmaTTCMutSet.RemoveObserver(this);
    bunch.membersCliffTTCMutSet.RemoveObserver(this);
    bunch.membersRavaNestTTCMutSet.RemoveObserver(this);
    bunch.membersCliffLandingTTCMutSet.RemoveObserver(this);
    bunch.membersStoneTTCMutSet.RemoveObserver(this);
    bunch.membersGrassTTCMutSet.RemoveObserver(this);
    bunch.membersIncendianFallsLevelLinkerTTCMutSet.RemoveObserver(this);
    bunch.membersEmberDeepLevelLinkerTTCMutSet.RemoveObserver(this);

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
  public void OnSimplePresenceTriggerTTCMutSetEffect(ISimplePresenceTriggerTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitSimplePresenceTriggerTTCMutSetAddEffect(SimplePresenceTriggerTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitSimplePresenceTriggerTTCMutSetRemoveEffect(SimplePresenceTriggerTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitSimplePresenceTriggerTTCMutSetCreateEffect(SimplePresenceTriggerTTCMutSetCreateEffect effect) { }
  public void visitSimplePresenceTriggerTTCMutSetDeleteEffect(SimplePresenceTriggerTTCMutSetDeleteEffect effect) { }
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
  public void OnKamikazeTargetTTCMutSetEffect(IKamikazeTargetTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitKamikazeTargetTTCMutSetAddEffect(KamikazeTargetTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitKamikazeTargetTTCMutSetRemoveEffect(KamikazeTargetTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitKamikazeTargetTTCMutSetCreateEffect(KamikazeTargetTTCMutSetCreateEffect effect) { }
  public void visitKamikazeTargetTTCMutSetDeleteEffect(KamikazeTargetTTCMutSetDeleteEffect effect) { }
  public void OnWarperTTCMutSetEffect(IWarperTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitWarperTTCMutSetAddEffect(WarperTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitWarperTTCMutSetRemoveEffect(WarperTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitWarperTTCMutSetCreateEffect(WarperTTCMutSetCreateEffect effect) { }
  public void visitWarperTTCMutSetDeleteEffect(WarperTTCMutSetDeleteEffect effect) { }
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
  public void OnFireBombTTCMutSetEffect(IFireBombTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitFireBombTTCMutSetAddEffect(FireBombTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitFireBombTTCMutSetRemoveEffect(FireBombTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitFireBombTTCMutSetCreateEffect(FireBombTTCMutSetCreateEffect effect) { }
  public void visitFireBombTTCMutSetDeleteEffect(FireBombTTCMutSetDeleteEffect effect) { }
  public void OnMarkerTTCMutSetEffect(IMarkerTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitMarkerTTCMutSetAddEffect(MarkerTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitMarkerTTCMutSetRemoveEffect(MarkerTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitMarkerTTCMutSetCreateEffect(MarkerTTCMutSetCreateEffect effect) { }
  public void visitMarkerTTCMutSetDeleteEffect(MarkerTTCMutSetDeleteEffect effect) { }
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
  public void OnObsidianTTCMutSetEffect(IObsidianTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitObsidianTTCMutSetAddEffect(ObsidianTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitObsidianTTCMutSetRemoveEffect(ObsidianTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitObsidianTTCMutSetCreateEffect(ObsidianTTCMutSetCreateEffect effect) { }
  public void visitObsidianTTCMutSetDeleteEffect(ObsidianTTCMutSetDeleteEffect effect) { }
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
  public void OnTreeTTCMutSetEffect(ITreeTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitTreeTTCMutSetAddEffect(TreeTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitTreeTTCMutSetRemoveEffect(TreeTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitTreeTTCMutSetCreateEffect(TreeTTCMutSetCreateEffect effect) { }
  public void visitTreeTTCMutSetDeleteEffect(TreeTTCMutSetDeleteEffect effect) { }
  public void OnWaterTTCMutSetEffect(IWaterTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitWaterTTCMutSetAddEffect(WaterTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitWaterTTCMutSetRemoveEffect(WaterTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitWaterTTCMutSetCreateEffect(WaterTTCMutSetCreateEffect effect) { }
  public void visitWaterTTCMutSetDeleteEffect(WaterTTCMutSetDeleteEffect effect) { }
  public void OnFloorTTCMutSetEffect(IFloorTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitFloorTTCMutSetAddEffect(FloorTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitFloorTTCMutSetRemoveEffect(FloorTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitFloorTTCMutSetCreateEffect(FloorTTCMutSetCreateEffect effect) { }
  public void visitFloorTTCMutSetDeleteEffect(FloorTTCMutSetDeleteEffect effect) { }
  public void OnCaveWallTTCMutSetEffect(ICaveWallTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitCaveWallTTCMutSetAddEffect(CaveWallTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitCaveWallTTCMutSetRemoveEffect(CaveWallTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitCaveWallTTCMutSetCreateEffect(CaveWallTTCMutSetCreateEffect effect) { }
  public void visitCaveWallTTCMutSetDeleteEffect(CaveWallTTCMutSetDeleteEffect effect) { }
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
  public void OnFireTTCMutSetEffect(IFireTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitFireTTCMutSetAddEffect(FireTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitFireTTCMutSetRemoveEffect(FireTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitFireTTCMutSetCreateEffect(FireTTCMutSetCreateEffect effect) { }
  public void visitFireTTCMutSetDeleteEffect(FireTTCMutSetDeleteEffect effect) { }
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

}
       
}
