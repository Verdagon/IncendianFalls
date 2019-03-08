using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ITerrainTileComponentMutBunchIncarnation {
  public readonly int membersArmorMutSet;
  public readonly int membersInertiaRingMutSet;
  public readonly int membersGlaiveMutSet;
  public readonly int membersManaPotionMutSet;
  public readonly int membersHealthPotionMutSet;
  public readonly int membersTimeAnchorTTCMutSet;
  public readonly int membersStaircaseTTCMutSet;
  public readonly int membersDecorativeTTCMutSet;
  public ITerrainTileComponentMutBunchIncarnation(
      int membersArmorMutSet,
      int membersInertiaRingMutSet,
      int membersGlaiveMutSet,
      int membersManaPotionMutSet,
      int membersHealthPotionMutSet,
      int membersTimeAnchorTTCMutSet,
      int membersStaircaseTTCMutSet,
      int membersDecorativeTTCMutSet) {
    this.membersArmorMutSet = membersArmorMutSet;
    this.membersInertiaRingMutSet = membersInertiaRingMutSet;
    this.membersGlaiveMutSet = membersGlaiveMutSet;
    this.membersManaPotionMutSet = membersManaPotionMutSet;
    this.membersHealthPotionMutSet = membersHealthPotionMutSet;
    this.membersTimeAnchorTTCMutSet = membersTimeAnchorTTCMutSet;
    this.membersStaircaseTTCMutSet = membersStaircaseTTCMutSet;
    this.membersDecorativeTTCMutSet = membersDecorativeTTCMutSet;
  }
}

}
