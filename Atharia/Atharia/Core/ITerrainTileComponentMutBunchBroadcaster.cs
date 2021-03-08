using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ITerrainTileComponentMutBunchBroadcaster:ISimplePresenceTriggerTTCMutSetEffectObserver, ISimplePresenceTriggerTTCMutSetEffectVisitor, IItemTTCMutSetEffectObserver, IItemTTCMutSetEffectVisitor, IKamikazeTargetTTCMutSetEffectObserver, IKamikazeTargetTTCMutSetEffectVisitor, IWarperTTCMutSetEffectObserver, IWarperTTCMutSetEffectVisitor, ITimeAnchorTTCMutSetEffectObserver, ITimeAnchorTTCMutSetEffectVisitor, IFireBombTTCMutSetEffectObserver, IFireBombTTCMutSetEffectVisitor, IMarkerTTCMutSetEffectObserver, IMarkerTTCMutSetEffectVisitor, ILevelLinkTTCMutSetEffectObserver, ILevelLinkTTCMutSetEffectVisitor, IMudTTCMutSetEffectObserver, IMudTTCMutSetEffectVisitor, IDirtTTCMutSetEffectObserver, IDirtTTCMutSetEffectVisitor, IObsidianTTCMutSetEffectObserver, IObsidianTTCMutSetEffectVisitor, IDownStairsTTCMutSetEffectObserver, IDownStairsTTCMutSetEffectVisitor, IUpStairsTTCMutSetEffectObserver, IUpStairsTTCMutSetEffectVisitor, IWallTTCMutSetEffectObserver, IWallTTCMutSetEffectVisitor, IBloodTTCMutSetEffectObserver, IBloodTTCMutSetEffectVisitor, IRocksTTCMutSetEffectObserver, IRocksTTCMutSetEffectVisitor, ITreeTTCMutSetEffectObserver, ITreeTTCMutSetEffectVisitor, IWaterTTCMutSetEffectObserver, IWaterTTCMutSetEffectVisitor, IFloorTTCMutSetEffectObserver, IFloorTTCMutSetEffectVisitor, ICaveWallTTCMutSetEffectObserver, ICaveWallTTCMutSetEffectVisitor, ICaveTTCMutSetEffectObserver, ICaveTTCMutSetEffectVisitor, IFallsTTCMutSetEffectObserver, IFallsTTCMutSetEffectVisitor, IFireTTCMutSetEffectObserver, IFireTTCMutSetEffectVisitor, IObsidianFloorTTCMutSetEffectObserver, IObsidianFloorTTCMutSetEffectVisitor, IMagmaTTCMutSetEffectObserver, IMagmaTTCMutSetEffectVisitor, ICliffTTCMutSetEffectObserver, ICliffTTCMutSetEffectVisitor, IRavaNestTTCMutSetEffectObserver, IRavaNestTTCMutSetEffectVisitor, ICliffLandingTTCMutSetEffectObserver, ICliffLandingTTCMutSetEffectVisitor, IStoneTTCMutSetEffectObserver, IStoneTTCMutSetEffectVisitor, IGrassTTCMutSetEffectObserver, IGrassTTCMutSetEffectVisitor, IEmberDeepLevelLinkerTTCMutSetEffectObserver, IEmberDeepLevelLinkerTTCMutSetEffectVisitor, IIncendianFallsLevelLinkerTTCMutSetEffectObserver, IIncendianFallsLevelLinkerTTCMutSetEffectVisitor, IRavaArcanaLevelLinkerTTCMutSetEffectObserver, IRavaArcanaLevelLinkerTTCMutSetEffectVisitor {
  EffectBroadcaster broadcaster;
  ITerrainTileComponentMutBunch bunch;
  private List<IITerrainTileComponentMutBunchObserver> observers;

  public ITerrainTileComponentMutBunchBroadcaster(EffectBroadcaster broadcaster, ITerrainTileComponentMutBunch bunch) {
    this.broadcaster = broadcaster;
    this.bunch = bunch;
    this.observers = new List<IITerrainTileComponentMutBunchObserver>();
    bunch.membersSimplePresenceTriggerTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersItemTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersKamikazeTargetTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersWarperTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersTimeAnchorTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersFireBombTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersMarkerTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersLevelLinkTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersMudTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersDirtTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersObsidianTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersDownStairsTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersUpStairsTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersWallTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersBloodTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersRocksTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersTreeTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersWaterTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersFloorTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersCaveWallTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersCaveTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersFallsTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersFireTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersObsidianFloorTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersMagmaTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersCliffTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersRavaNestTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersCliffLandingTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersStoneTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersGrassTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersEmberDeepLevelLinkerTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersIncendianFallsLevelLinkerTTCMutSet.AddObserver(broadcaster, this);
    bunch.membersRavaArcanaLevelLinkerTTCMutSet.AddObserver(broadcaster, this);

  }
  public void Stop() {
    bunch.membersSimplePresenceTriggerTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersItemTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersKamikazeTargetTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersWarperTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersTimeAnchorTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersFireBombTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersMarkerTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersLevelLinkTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersMudTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersDirtTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersObsidianTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersDownStairsTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersUpStairsTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersWallTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersBloodTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersRocksTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersTreeTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersWaterTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersFloorTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersCaveWallTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersCaveTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersFallsTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersFireTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersObsidianFloorTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersMagmaTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersCliffTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersRavaNestTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersCliffLandingTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersStoneTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersGrassTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersEmberDeepLevelLinkerTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersIncendianFallsLevelLinkerTTCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersRavaArcanaLevelLinkerTTCMutSet.RemoveObserver(broadcaster, this);

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
    effect.visitISimplePresenceTriggerTTCMutSetEffect(this);
  }
  public void visitSimplePresenceTriggerTTCMutSetAddEffect(SimplePresenceTriggerTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitSimplePresenceTriggerTTCMutSetRemoveEffect(SimplePresenceTriggerTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitSimplePresenceTriggerTTCMutSetCreateEffect(SimplePresenceTriggerTTCMutSetCreateEffect effect) { }
  public void visitSimplePresenceTriggerTTCMutSetDeleteEffect(SimplePresenceTriggerTTCMutSetDeleteEffect effect) { }
  public void OnItemTTCMutSetEffect(IItemTTCMutSetEffect effect) {
    effect.visitIItemTTCMutSetEffect(this);
  }
  public void visitItemTTCMutSetAddEffect(ItemTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitItemTTCMutSetRemoveEffect(ItemTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitItemTTCMutSetCreateEffect(ItemTTCMutSetCreateEffect effect) { }
  public void visitItemTTCMutSetDeleteEffect(ItemTTCMutSetDeleteEffect effect) { }
  public void OnKamikazeTargetTTCMutSetEffect(IKamikazeTargetTTCMutSetEffect effect) {
    effect.visitIKamikazeTargetTTCMutSetEffect(this);
  }
  public void visitKamikazeTargetTTCMutSetAddEffect(KamikazeTargetTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitKamikazeTargetTTCMutSetRemoveEffect(KamikazeTargetTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitKamikazeTargetTTCMutSetCreateEffect(KamikazeTargetTTCMutSetCreateEffect effect) { }
  public void visitKamikazeTargetTTCMutSetDeleteEffect(KamikazeTargetTTCMutSetDeleteEffect effect) { }
  public void OnWarperTTCMutSetEffect(IWarperTTCMutSetEffect effect) {
    effect.visitIWarperTTCMutSetEffect(this);
  }
  public void visitWarperTTCMutSetAddEffect(WarperTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitWarperTTCMutSetRemoveEffect(WarperTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitWarperTTCMutSetCreateEffect(WarperTTCMutSetCreateEffect effect) { }
  public void visitWarperTTCMutSetDeleteEffect(WarperTTCMutSetDeleteEffect effect) { }
  public void OnTimeAnchorTTCMutSetEffect(ITimeAnchorTTCMutSetEffect effect) {
    effect.visitITimeAnchorTTCMutSetEffect(this);
  }
  public void visitTimeAnchorTTCMutSetAddEffect(TimeAnchorTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitTimeAnchorTTCMutSetRemoveEffect(TimeAnchorTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitTimeAnchorTTCMutSetCreateEffect(TimeAnchorTTCMutSetCreateEffect effect) { }
  public void visitTimeAnchorTTCMutSetDeleteEffect(TimeAnchorTTCMutSetDeleteEffect effect) { }
  public void OnFireBombTTCMutSetEffect(IFireBombTTCMutSetEffect effect) {
    effect.visitIFireBombTTCMutSetEffect(this);
  }
  public void visitFireBombTTCMutSetAddEffect(FireBombTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitFireBombTTCMutSetRemoveEffect(FireBombTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitFireBombTTCMutSetCreateEffect(FireBombTTCMutSetCreateEffect effect) { }
  public void visitFireBombTTCMutSetDeleteEffect(FireBombTTCMutSetDeleteEffect effect) { }
  public void OnMarkerTTCMutSetEffect(IMarkerTTCMutSetEffect effect) {
    effect.visitIMarkerTTCMutSetEffect(this);
  }
  public void visitMarkerTTCMutSetAddEffect(MarkerTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitMarkerTTCMutSetRemoveEffect(MarkerTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitMarkerTTCMutSetCreateEffect(MarkerTTCMutSetCreateEffect effect) { }
  public void visitMarkerTTCMutSetDeleteEffect(MarkerTTCMutSetDeleteEffect effect) { }
  public void OnLevelLinkTTCMutSetEffect(ILevelLinkTTCMutSetEffect effect) {
    effect.visitILevelLinkTTCMutSetEffect(this);
  }
  public void visitLevelLinkTTCMutSetAddEffect(LevelLinkTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitLevelLinkTTCMutSetRemoveEffect(LevelLinkTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitLevelLinkTTCMutSetCreateEffect(LevelLinkTTCMutSetCreateEffect effect) { }
  public void visitLevelLinkTTCMutSetDeleteEffect(LevelLinkTTCMutSetDeleteEffect effect) { }
  public void OnMudTTCMutSetEffect(IMudTTCMutSetEffect effect) {
    effect.visitIMudTTCMutSetEffect(this);
  }
  public void visitMudTTCMutSetAddEffect(MudTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitMudTTCMutSetRemoveEffect(MudTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitMudTTCMutSetCreateEffect(MudTTCMutSetCreateEffect effect) { }
  public void visitMudTTCMutSetDeleteEffect(MudTTCMutSetDeleteEffect effect) { }
  public void OnDirtTTCMutSetEffect(IDirtTTCMutSetEffect effect) {
    effect.visitIDirtTTCMutSetEffect(this);
  }
  public void visitDirtTTCMutSetAddEffect(DirtTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitDirtTTCMutSetRemoveEffect(DirtTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitDirtTTCMutSetCreateEffect(DirtTTCMutSetCreateEffect effect) { }
  public void visitDirtTTCMutSetDeleteEffect(DirtTTCMutSetDeleteEffect effect) { }
  public void OnObsidianTTCMutSetEffect(IObsidianTTCMutSetEffect effect) {
    effect.visitIObsidianTTCMutSetEffect(this);
  }
  public void visitObsidianTTCMutSetAddEffect(ObsidianTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitObsidianTTCMutSetRemoveEffect(ObsidianTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitObsidianTTCMutSetCreateEffect(ObsidianTTCMutSetCreateEffect effect) { }
  public void visitObsidianTTCMutSetDeleteEffect(ObsidianTTCMutSetDeleteEffect effect) { }
  public void OnDownStairsTTCMutSetEffect(IDownStairsTTCMutSetEffect effect) {
    effect.visitIDownStairsTTCMutSetEffect(this);
  }
  public void visitDownStairsTTCMutSetAddEffect(DownStairsTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitDownStairsTTCMutSetRemoveEffect(DownStairsTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitDownStairsTTCMutSetCreateEffect(DownStairsTTCMutSetCreateEffect effect) { }
  public void visitDownStairsTTCMutSetDeleteEffect(DownStairsTTCMutSetDeleteEffect effect) { }
  public void OnUpStairsTTCMutSetEffect(IUpStairsTTCMutSetEffect effect) {
    effect.visitIUpStairsTTCMutSetEffect(this);
  }
  public void visitUpStairsTTCMutSetAddEffect(UpStairsTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitUpStairsTTCMutSetRemoveEffect(UpStairsTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitUpStairsTTCMutSetCreateEffect(UpStairsTTCMutSetCreateEffect effect) { }
  public void visitUpStairsTTCMutSetDeleteEffect(UpStairsTTCMutSetDeleteEffect effect) { }
  public void OnWallTTCMutSetEffect(IWallTTCMutSetEffect effect) {
    effect.visitIWallTTCMutSetEffect(this);
  }
  public void visitWallTTCMutSetAddEffect(WallTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitWallTTCMutSetRemoveEffect(WallTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitWallTTCMutSetCreateEffect(WallTTCMutSetCreateEffect effect) { }
  public void visitWallTTCMutSetDeleteEffect(WallTTCMutSetDeleteEffect effect) { }
  public void OnBloodTTCMutSetEffect(IBloodTTCMutSetEffect effect) {
    effect.visitIBloodTTCMutSetEffect(this);
  }
  public void visitBloodTTCMutSetAddEffect(BloodTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitBloodTTCMutSetRemoveEffect(BloodTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitBloodTTCMutSetCreateEffect(BloodTTCMutSetCreateEffect effect) { }
  public void visitBloodTTCMutSetDeleteEffect(BloodTTCMutSetDeleteEffect effect) { }
  public void OnRocksTTCMutSetEffect(IRocksTTCMutSetEffect effect) {
    effect.visitIRocksTTCMutSetEffect(this);
  }
  public void visitRocksTTCMutSetAddEffect(RocksTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitRocksTTCMutSetRemoveEffect(RocksTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitRocksTTCMutSetCreateEffect(RocksTTCMutSetCreateEffect effect) { }
  public void visitRocksTTCMutSetDeleteEffect(RocksTTCMutSetDeleteEffect effect) { }
  public void OnTreeTTCMutSetEffect(ITreeTTCMutSetEffect effect) {
    effect.visitITreeTTCMutSetEffect(this);
  }
  public void visitTreeTTCMutSetAddEffect(TreeTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitTreeTTCMutSetRemoveEffect(TreeTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitTreeTTCMutSetCreateEffect(TreeTTCMutSetCreateEffect effect) { }
  public void visitTreeTTCMutSetDeleteEffect(TreeTTCMutSetDeleteEffect effect) { }
  public void OnWaterTTCMutSetEffect(IWaterTTCMutSetEffect effect) {
    effect.visitIWaterTTCMutSetEffect(this);
  }
  public void visitWaterTTCMutSetAddEffect(WaterTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitWaterTTCMutSetRemoveEffect(WaterTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitWaterTTCMutSetCreateEffect(WaterTTCMutSetCreateEffect effect) { }
  public void visitWaterTTCMutSetDeleteEffect(WaterTTCMutSetDeleteEffect effect) { }
  public void OnFloorTTCMutSetEffect(IFloorTTCMutSetEffect effect) {
    effect.visitIFloorTTCMutSetEffect(this);
  }
  public void visitFloorTTCMutSetAddEffect(FloorTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitFloorTTCMutSetRemoveEffect(FloorTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitFloorTTCMutSetCreateEffect(FloorTTCMutSetCreateEffect effect) { }
  public void visitFloorTTCMutSetDeleteEffect(FloorTTCMutSetDeleteEffect effect) { }
  public void OnCaveWallTTCMutSetEffect(ICaveWallTTCMutSetEffect effect) {
    effect.visitICaveWallTTCMutSetEffect(this);
  }
  public void visitCaveWallTTCMutSetAddEffect(CaveWallTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitCaveWallTTCMutSetRemoveEffect(CaveWallTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitCaveWallTTCMutSetCreateEffect(CaveWallTTCMutSetCreateEffect effect) { }
  public void visitCaveWallTTCMutSetDeleteEffect(CaveWallTTCMutSetDeleteEffect effect) { }
  public void OnCaveTTCMutSetEffect(ICaveTTCMutSetEffect effect) {
    effect.visitICaveTTCMutSetEffect(this);
  }
  public void visitCaveTTCMutSetAddEffect(CaveTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitCaveTTCMutSetRemoveEffect(CaveTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitCaveTTCMutSetCreateEffect(CaveTTCMutSetCreateEffect effect) { }
  public void visitCaveTTCMutSetDeleteEffect(CaveTTCMutSetDeleteEffect effect) { }
  public void OnFallsTTCMutSetEffect(IFallsTTCMutSetEffect effect) {
    effect.visitIFallsTTCMutSetEffect(this);
  }
  public void visitFallsTTCMutSetAddEffect(FallsTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitFallsTTCMutSetRemoveEffect(FallsTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitFallsTTCMutSetCreateEffect(FallsTTCMutSetCreateEffect effect) { }
  public void visitFallsTTCMutSetDeleteEffect(FallsTTCMutSetDeleteEffect effect) { }
  public void OnFireTTCMutSetEffect(IFireTTCMutSetEffect effect) {
    effect.visitIFireTTCMutSetEffect(this);
  }
  public void visitFireTTCMutSetAddEffect(FireTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitFireTTCMutSetRemoveEffect(FireTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitFireTTCMutSetCreateEffect(FireTTCMutSetCreateEffect effect) { }
  public void visitFireTTCMutSetDeleteEffect(FireTTCMutSetDeleteEffect effect) { }
  public void OnObsidianFloorTTCMutSetEffect(IObsidianFloorTTCMutSetEffect effect) {
    effect.visitIObsidianFloorTTCMutSetEffect(this);
  }
  public void visitObsidianFloorTTCMutSetAddEffect(ObsidianFloorTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitObsidianFloorTTCMutSetRemoveEffect(ObsidianFloorTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitObsidianFloorTTCMutSetCreateEffect(ObsidianFloorTTCMutSetCreateEffect effect) { }
  public void visitObsidianFloorTTCMutSetDeleteEffect(ObsidianFloorTTCMutSetDeleteEffect effect) { }
  public void OnMagmaTTCMutSetEffect(IMagmaTTCMutSetEffect effect) {
    effect.visitIMagmaTTCMutSetEffect(this);
  }
  public void visitMagmaTTCMutSetAddEffect(MagmaTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitMagmaTTCMutSetRemoveEffect(MagmaTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitMagmaTTCMutSetCreateEffect(MagmaTTCMutSetCreateEffect effect) { }
  public void visitMagmaTTCMutSetDeleteEffect(MagmaTTCMutSetDeleteEffect effect) { }
  public void OnCliffTTCMutSetEffect(ICliffTTCMutSetEffect effect) {
    effect.visitICliffTTCMutSetEffect(this);
  }
  public void visitCliffTTCMutSetAddEffect(CliffTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitCliffTTCMutSetRemoveEffect(CliffTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitCliffTTCMutSetCreateEffect(CliffTTCMutSetCreateEffect effect) { }
  public void visitCliffTTCMutSetDeleteEffect(CliffTTCMutSetDeleteEffect effect) { }
  public void OnRavaNestTTCMutSetEffect(IRavaNestTTCMutSetEffect effect) {
    effect.visitIRavaNestTTCMutSetEffect(this);
  }
  public void visitRavaNestTTCMutSetAddEffect(RavaNestTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitRavaNestTTCMutSetRemoveEffect(RavaNestTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitRavaNestTTCMutSetCreateEffect(RavaNestTTCMutSetCreateEffect effect) { }
  public void visitRavaNestTTCMutSetDeleteEffect(RavaNestTTCMutSetDeleteEffect effect) { }
  public void OnCliffLandingTTCMutSetEffect(ICliffLandingTTCMutSetEffect effect) {
    effect.visitICliffLandingTTCMutSetEffect(this);
  }
  public void visitCliffLandingTTCMutSetAddEffect(CliffLandingTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitCliffLandingTTCMutSetRemoveEffect(CliffLandingTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitCliffLandingTTCMutSetCreateEffect(CliffLandingTTCMutSetCreateEffect effect) { }
  public void visitCliffLandingTTCMutSetDeleteEffect(CliffLandingTTCMutSetDeleteEffect effect) { }
  public void OnStoneTTCMutSetEffect(IStoneTTCMutSetEffect effect) {
    effect.visitIStoneTTCMutSetEffect(this);
  }
  public void visitStoneTTCMutSetAddEffect(StoneTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitStoneTTCMutSetRemoveEffect(StoneTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitStoneTTCMutSetCreateEffect(StoneTTCMutSetCreateEffect effect) { }
  public void visitStoneTTCMutSetDeleteEffect(StoneTTCMutSetDeleteEffect effect) { }
  public void OnGrassTTCMutSetEffect(IGrassTTCMutSetEffect effect) {
    effect.visitIGrassTTCMutSetEffect(this);
  }
  public void visitGrassTTCMutSetAddEffect(GrassTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitGrassTTCMutSetRemoveEffect(GrassTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitGrassTTCMutSetCreateEffect(GrassTTCMutSetCreateEffect effect) { }
  public void visitGrassTTCMutSetDeleteEffect(GrassTTCMutSetDeleteEffect effect) { }
  public void OnEmberDeepLevelLinkerTTCMutSetEffect(IEmberDeepLevelLinkerTTCMutSetEffect effect) {
    effect.visitIEmberDeepLevelLinkerTTCMutSetEffect(this);
  }
  public void visitEmberDeepLevelLinkerTTCMutSetAddEffect(EmberDeepLevelLinkerTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitEmberDeepLevelLinkerTTCMutSetRemoveEffect(EmberDeepLevelLinkerTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitEmberDeepLevelLinkerTTCMutSetCreateEffect(EmberDeepLevelLinkerTTCMutSetCreateEffect effect) { }
  public void visitEmberDeepLevelLinkerTTCMutSetDeleteEffect(EmberDeepLevelLinkerTTCMutSetDeleteEffect effect) { }
  public void OnIncendianFallsLevelLinkerTTCMutSetEffect(IIncendianFallsLevelLinkerTTCMutSetEffect effect) {
    effect.visitIIncendianFallsLevelLinkerTTCMutSetEffect(this);
  }
  public void visitIncendianFallsLevelLinkerTTCMutSetAddEffect(IncendianFallsLevelLinkerTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitIncendianFallsLevelLinkerTTCMutSetRemoveEffect(IncendianFallsLevelLinkerTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitIncendianFallsLevelLinkerTTCMutSetCreateEffect(IncendianFallsLevelLinkerTTCMutSetCreateEffect effect) { }
  public void visitIncendianFallsLevelLinkerTTCMutSetDeleteEffect(IncendianFallsLevelLinkerTTCMutSetDeleteEffect effect) { }
  public void OnRavaArcanaLevelLinkerTTCMutSetEffect(IRavaArcanaLevelLinkerTTCMutSetEffect effect) {
    effect.visitIRavaArcanaLevelLinkerTTCMutSetEffect(this);
  }
  public void visitRavaArcanaLevelLinkerTTCMutSetAddEffect(RavaArcanaLevelLinkerTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitRavaArcanaLevelLinkerTTCMutSetRemoveEffect(RavaArcanaLevelLinkerTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitRavaArcanaLevelLinkerTTCMutSetCreateEffect(RavaArcanaLevelLinkerTTCMutSetCreateEffect effect) { }
  public void visitRavaArcanaLevelLinkerTTCMutSetDeleteEffect(RavaArcanaLevelLinkerTTCMutSetDeleteEffect effect) { }

}
       
}
