using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class IItemStrongMutBunchIncarnation : IIItemStrongMutBunchEffectVisitor {
  public readonly int membersManaPotionStrongMutSet;
  public readonly int membersHealthPotionStrongMutSet;
  public readonly int membersSpeedRingStrongMutSet;
  public readonly int membersGlaiveStrongMutSet;
  public readonly int membersSlowRodStrongMutSet;
  public readonly int membersExplosionRodStrongMutSet;
  public readonly int membersBlazeRodStrongMutSet;
  public readonly int membersBlastRodStrongMutSet;
  public readonly int membersArmorStrongMutSet;
  public IItemStrongMutBunchIncarnation(
      int membersManaPotionStrongMutSet,
      int membersHealthPotionStrongMutSet,
      int membersSpeedRingStrongMutSet,
      int membersGlaiveStrongMutSet,
      int membersSlowRodStrongMutSet,
      int membersExplosionRodStrongMutSet,
      int membersBlazeRodStrongMutSet,
      int membersBlastRodStrongMutSet,
      int membersArmorStrongMutSet) {
    this.membersManaPotionStrongMutSet = membersManaPotionStrongMutSet;
    this.membersHealthPotionStrongMutSet = membersHealthPotionStrongMutSet;
    this.membersSpeedRingStrongMutSet = membersSpeedRingStrongMutSet;
    this.membersGlaiveStrongMutSet = membersGlaiveStrongMutSet;
    this.membersSlowRodStrongMutSet = membersSlowRodStrongMutSet;
    this.membersExplosionRodStrongMutSet = membersExplosionRodStrongMutSet;
    this.membersBlazeRodStrongMutSet = membersBlazeRodStrongMutSet;
    this.membersBlastRodStrongMutSet = membersBlastRodStrongMutSet;
    this.membersArmorStrongMutSet = membersArmorStrongMutSet;
  }
  public IItemStrongMutBunchIncarnation Copy() {
    return new IItemStrongMutBunchIncarnation(
membersManaPotionStrongMutSet,
membersHealthPotionStrongMutSet,
membersSpeedRingStrongMutSet,
membersGlaiveStrongMutSet,
membersSlowRodStrongMutSet,
membersExplosionRodStrongMutSet,
membersBlazeRodStrongMutSet,
membersBlastRodStrongMutSet,
membersArmorStrongMutSet    );
  }

  public void visitIItemStrongMutBunchCreateEffect(IItemStrongMutBunchCreateEffect e) {}
  public void visitIItemStrongMutBunchDeleteEffect(IItemStrongMutBunchDeleteEffect e) {}









  public void ApplyEffect(IIItemStrongMutBunchEffect effect) { effect.visitIIItemStrongMutBunchEffect(this); }
}

}
