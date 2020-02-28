using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class IItemStrongMutBunchIncarnation {
  public readonly int membersArmorStrongMutSet;
  public readonly int membersInertiaRingStrongMutSet;
  public readonly int membersGlaiveStrongMutSet;
  public readonly int membersManaPotionStrongMutSet;
  public readonly int membersHealthPotionStrongMutSet;
  public IItemStrongMutBunchIncarnation(
      int membersArmorStrongMutSet,
      int membersInertiaRingStrongMutSet,
      int membersGlaiveStrongMutSet,
      int membersManaPotionStrongMutSet,
      int membersHealthPotionStrongMutSet) {
    this.membersArmorStrongMutSet = membersArmorStrongMutSet;
    this.membersInertiaRingStrongMutSet = membersInertiaRingStrongMutSet;
    this.membersGlaiveStrongMutSet = membersGlaiveStrongMutSet;
    this.membersManaPotionStrongMutSet = membersManaPotionStrongMutSet;
    this.membersHealthPotionStrongMutSet = membersHealthPotionStrongMutSet;
  }
}

}
