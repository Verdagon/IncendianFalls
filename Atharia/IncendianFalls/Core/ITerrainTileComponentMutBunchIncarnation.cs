using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ITerrainTileComponentMutBunchIncarnation : IITerrainTileComponentMutBunchEffectVisitor {
  public readonly int membersSimplePresenceTriggerTTCMutSet;
  public readonly int membersItemTTCMutSet;
  public readonly int membersKamikazeTargetTTCMutSet;
  public readonly int membersWarperTTCMutSet;
  public readonly int membersTimeAnchorTTCMutSet;
  public readonly int membersFireBombTTCMutSet;
  public readonly int membersMarkerTTCMutSet;
  public readonly int membersLevelLinkTTCMutSet;
  public readonly int membersMudTTCMutSet;
  public readonly int membersDirtTTCMutSet;
  public readonly int membersObsidianTTCMutSet;
  public readonly int membersDownStairsTTCMutSet;
  public readonly int membersUpStairsTTCMutSet;
  public readonly int membersWallTTCMutSet;
  public readonly int membersBloodTTCMutSet;
  public readonly int membersRocksTTCMutSet;
  public readonly int membersTreeTTCMutSet;
  public readonly int membersWaterTTCMutSet;
  public readonly int membersFloorTTCMutSet;
  public readonly int membersCaveWallTTCMutSet;
  public readonly int membersCaveTTCMutSet;
  public readonly int membersFallsTTCMutSet;
  public readonly int membersFireTTCMutSet;
  public readonly int membersObsidianFloorTTCMutSet;
  public readonly int membersMagmaTTCMutSet;
  public readonly int membersCliffTTCMutSet;
  public readonly int membersRavaNestTTCMutSet;
  public readonly int membersCliffLandingTTCMutSet;
  public readonly int membersStoneTTCMutSet;
  public readonly int membersGrassTTCMutSet;
  public readonly int membersIncendianFallsLevelLinkerTTCMutSet;
  public readonly int membersEmberDeepLevelLinkerTTCMutSet;
  public ITerrainTileComponentMutBunchIncarnation(
      int membersSimplePresenceTriggerTTCMutSet,
      int membersItemTTCMutSet,
      int membersKamikazeTargetTTCMutSet,
      int membersWarperTTCMutSet,
      int membersTimeAnchorTTCMutSet,
      int membersFireBombTTCMutSet,
      int membersMarkerTTCMutSet,
      int membersLevelLinkTTCMutSet,
      int membersMudTTCMutSet,
      int membersDirtTTCMutSet,
      int membersObsidianTTCMutSet,
      int membersDownStairsTTCMutSet,
      int membersUpStairsTTCMutSet,
      int membersWallTTCMutSet,
      int membersBloodTTCMutSet,
      int membersRocksTTCMutSet,
      int membersTreeTTCMutSet,
      int membersWaterTTCMutSet,
      int membersFloorTTCMutSet,
      int membersCaveWallTTCMutSet,
      int membersCaveTTCMutSet,
      int membersFallsTTCMutSet,
      int membersFireTTCMutSet,
      int membersObsidianFloorTTCMutSet,
      int membersMagmaTTCMutSet,
      int membersCliffTTCMutSet,
      int membersRavaNestTTCMutSet,
      int membersCliffLandingTTCMutSet,
      int membersStoneTTCMutSet,
      int membersGrassTTCMutSet,
      int membersIncendianFallsLevelLinkerTTCMutSet,
      int membersEmberDeepLevelLinkerTTCMutSet) {
    this.membersSimplePresenceTriggerTTCMutSet = membersSimplePresenceTriggerTTCMutSet;
    this.membersItemTTCMutSet = membersItemTTCMutSet;
    this.membersKamikazeTargetTTCMutSet = membersKamikazeTargetTTCMutSet;
    this.membersWarperTTCMutSet = membersWarperTTCMutSet;
    this.membersTimeAnchorTTCMutSet = membersTimeAnchorTTCMutSet;
    this.membersFireBombTTCMutSet = membersFireBombTTCMutSet;
    this.membersMarkerTTCMutSet = membersMarkerTTCMutSet;
    this.membersLevelLinkTTCMutSet = membersLevelLinkTTCMutSet;
    this.membersMudTTCMutSet = membersMudTTCMutSet;
    this.membersDirtTTCMutSet = membersDirtTTCMutSet;
    this.membersObsidianTTCMutSet = membersObsidianTTCMutSet;
    this.membersDownStairsTTCMutSet = membersDownStairsTTCMutSet;
    this.membersUpStairsTTCMutSet = membersUpStairsTTCMutSet;
    this.membersWallTTCMutSet = membersWallTTCMutSet;
    this.membersBloodTTCMutSet = membersBloodTTCMutSet;
    this.membersRocksTTCMutSet = membersRocksTTCMutSet;
    this.membersTreeTTCMutSet = membersTreeTTCMutSet;
    this.membersWaterTTCMutSet = membersWaterTTCMutSet;
    this.membersFloorTTCMutSet = membersFloorTTCMutSet;
    this.membersCaveWallTTCMutSet = membersCaveWallTTCMutSet;
    this.membersCaveTTCMutSet = membersCaveTTCMutSet;
    this.membersFallsTTCMutSet = membersFallsTTCMutSet;
    this.membersFireTTCMutSet = membersFireTTCMutSet;
    this.membersObsidianFloorTTCMutSet = membersObsidianFloorTTCMutSet;
    this.membersMagmaTTCMutSet = membersMagmaTTCMutSet;
    this.membersCliffTTCMutSet = membersCliffTTCMutSet;
    this.membersRavaNestTTCMutSet = membersRavaNestTTCMutSet;
    this.membersCliffLandingTTCMutSet = membersCliffLandingTTCMutSet;
    this.membersStoneTTCMutSet = membersStoneTTCMutSet;
    this.membersGrassTTCMutSet = membersGrassTTCMutSet;
    this.membersIncendianFallsLevelLinkerTTCMutSet = membersIncendianFallsLevelLinkerTTCMutSet;
    this.membersEmberDeepLevelLinkerTTCMutSet = membersEmberDeepLevelLinkerTTCMutSet;
  }
  public ITerrainTileComponentMutBunchIncarnation Copy() {
    return new ITerrainTileComponentMutBunchIncarnation(
membersSimplePresenceTriggerTTCMutSet,
membersItemTTCMutSet,
membersKamikazeTargetTTCMutSet,
membersWarperTTCMutSet,
membersTimeAnchorTTCMutSet,
membersFireBombTTCMutSet,
membersMarkerTTCMutSet,
membersLevelLinkTTCMutSet,
membersMudTTCMutSet,
membersDirtTTCMutSet,
membersObsidianTTCMutSet,
membersDownStairsTTCMutSet,
membersUpStairsTTCMutSet,
membersWallTTCMutSet,
membersBloodTTCMutSet,
membersRocksTTCMutSet,
membersTreeTTCMutSet,
membersWaterTTCMutSet,
membersFloorTTCMutSet,
membersCaveWallTTCMutSet,
membersCaveTTCMutSet,
membersFallsTTCMutSet,
membersFireTTCMutSet,
membersObsidianFloorTTCMutSet,
membersMagmaTTCMutSet,
membersCliffTTCMutSet,
membersRavaNestTTCMutSet,
membersCliffLandingTTCMutSet,
membersStoneTTCMutSet,
membersGrassTTCMutSet,
membersIncendianFallsLevelLinkerTTCMutSet,
membersEmberDeepLevelLinkerTTCMutSet    );
  }

  public void visitITerrainTileComponentMutBunchCreateEffect(ITerrainTileComponentMutBunchCreateEffect e) {}
  public void visitITerrainTileComponentMutBunchDeleteEffect(ITerrainTileComponentMutBunchDeleteEffect e) {}
































  public void ApplyEffect(IITerrainTileComponentMutBunchEffect effect) { effect.visitIITerrainTileComponentMutBunchEffect(this); }
}

}
