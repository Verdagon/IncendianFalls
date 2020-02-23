using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ITerrainTileComponentMutBunchBroadcaster:IArmorMutSetEffectObserver, IArmorMutSetEffectVisitor, IInertiaRingMutSetEffectObserver, IInertiaRingMutSetEffectVisitor, IGlaiveMutSetEffectObserver, IGlaiveMutSetEffectVisitor, IManaPotionMutSetEffectObserver, IManaPotionMutSetEffectVisitor, IHealthPotionMutSetEffectObserver, IHealthPotionMutSetEffectVisitor, ITimeAnchorTTCMutSetEffectObserver, ITimeAnchorTTCMutSetEffectVisitor, IStaircaseTTCMutSetEffectObserver, IStaircaseTTCMutSetEffectVisitor, IWallTTCMutSetEffectObserver, IWallTTCMutSetEffectVisitor, IBloodTTCMutSetEffectObserver, IBloodTTCMutSetEffectVisitor, IRocksTTCMutSetEffectObserver, IRocksTTCMutSetEffectVisitor, IDownstairsTTCMutSetEffectObserver, IDownstairsTTCMutSetEffectVisitor, IUpstairsTTCMutSetEffectObserver, IUpstairsTTCMutSetEffectVisitor, ICaveTTCMutSetEffectObserver, ICaveTTCMutSetEffectVisitor, IFallsTTCMutSetEffectObserver, IFallsTTCMutSetEffectVisitor, IMagmaTTCMutSetEffectObserver, IMagmaTTCMutSetEffectVisitor, ICliffTTCMutSetEffectObserver, ICliffTTCMutSetEffectVisitor, IRavaNestTTCMutSetEffectObserver, IRavaNestTTCMutSetEffectVisitor, ICliffLandingTTCMutSetEffectObserver, ICliffLandingTTCMutSetEffectVisitor, IStoneTTCMutSetEffectObserver, IStoneTTCMutSetEffectVisitor, IGrassTTCMutSetEffectObserver, IGrassTTCMutSetEffectVisitor {
  ITerrainTileComponentMutBunch bunch;
  private List<IITerrainTileComponentMutBunchObserver> observers;

  public ITerrainTileComponentMutBunchBroadcaster(ITerrainTileComponentMutBunch bunch) {
    this.bunch = bunch;
    this.observers = new List<IITerrainTileComponentMutBunchObserver>();
    bunch.membersArmorMutSet.AddObserver(this);
    bunch.membersInertiaRingMutSet.AddObserver(this);
    bunch.membersGlaiveMutSet.AddObserver(this);
    bunch.membersManaPotionMutSet.AddObserver(this);
    bunch.membersHealthPotionMutSet.AddObserver(this);
    bunch.membersTimeAnchorTTCMutSet.AddObserver(this);
    bunch.membersStaircaseTTCMutSet.AddObserver(this);
    bunch.membersWallTTCMutSet.AddObserver(this);
    bunch.membersBloodTTCMutSet.AddObserver(this);
    bunch.membersRocksTTCMutSet.AddObserver(this);
    bunch.membersDownstairsTTCMutSet.AddObserver(this);
    bunch.membersUpstairsTTCMutSet.AddObserver(this);
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
    bunch.membersArmorMutSet.RemoveObserver(this);
    bunch.membersInertiaRingMutSet.RemoveObserver(this);
    bunch.membersGlaiveMutSet.RemoveObserver(this);
    bunch.membersManaPotionMutSet.RemoveObserver(this);
    bunch.membersHealthPotionMutSet.RemoveObserver(this);
    bunch.membersTimeAnchorTTCMutSet.RemoveObserver(this);
    bunch.membersStaircaseTTCMutSet.RemoveObserver(this);
    bunch.membersWallTTCMutSet.RemoveObserver(this);
    bunch.membersBloodTTCMutSet.RemoveObserver(this);
    bunch.membersRocksTTCMutSet.RemoveObserver(this);
    bunch.membersDownstairsTTCMutSet.RemoveObserver(this);
    bunch.membersUpstairsTTCMutSet.RemoveObserver(this);
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
  public void OnInertiaRingMutSetEffect(IInertiaRingMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitInertiaRingMutSetAddEffect(InertiaRingMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitInertiaRingMutSetRemoveEffect(InertiaRingMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitInertiaRingMutSetCreateEffect(InertiaRingMutSetCreateEffect effect) { }
  public void visitInertiaRingMutSetDeleteEffect(InertiaRingMutSetDeleteEffect effect) { }
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
  public void OnDownstairsTTCMutSetEffect(IDownstairsTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitDownstairsTTCMutSetAddEffect(DownstairsTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitDownstairsTTCMutSetRemoveEffect(DownstairsTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitDownstairsTTCMutSetCreateEffect(DownstairsTTCMutSetCreateEffect effect) { }
  public void visitDownstairsTTCMutSetDeleteEffect(DownstairsTTCMutSetDeleteEffect effect) { }
  public void OnUpstairsTTCMutSetEffect(IUpstairsTTCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitUpstairsTTCMutSetAddEffect(UpstairsTTCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitUpstairsTTCMutSetRemoveEffect(UpstairsTTCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitUpstairsTTCMutSetCreateEffect(UpstairsTTCMutSetCreateEffect effect) { }
  public void visitUpstairsTTCMutSetDeleteEffect(UpstairsTTCMutSetDeleteEffect effect) { }
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
