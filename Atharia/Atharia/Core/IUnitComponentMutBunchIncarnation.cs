using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class IUnitComponentMutBunchIncarnation : IIUnitComponentMutBunchEffectVisitor {
  public readonly int membersDeathTriggerUCMutSet;
  public readonly int membersBequeathUCMutSet;
  public readonly int membersTutorialDefyCounterUCMutSet;
  public readonly int membersLightningChargingUCMutSet;
  public readonly int membersWanderAICapabilityUCMutSet;
  public readonly int membersTemporaryCloneAICapabilityUCMutSet;
  public readonly int membersSummonAICapabilityUCMutSet;
  public readonly int membersKamikazeAICapabilityUCMutSet;
  public readonly int membersGuardAICapabilityUCMutSet;
  public readonly int membersEvolvifyAICapabilityUCMutSet;
  public readonly int membersTimeCloneAICapabilityUCMutSet;
  public readonly int membersDoomedUCMutSet;
  public readonly int membersMiredUCMutSet;
  public readonly int membersOnFireUCMutSet;
  public readonly int membersAttackAICapabilityUCMutSet;
  public readonly int membersCounteringUCMutSet;
  public readonly int membersLightningChargedUCMutSet;
  public readonly int membersInvincibilityUCMutSet;
  public readonly int membersDefyingUCMutSet;
  public readonly int membersBideAICapabilityUCMutSet;
  public readonly int membersBaseSightRangeUCMutSet;
  public readonly int membersBaseMovementTimeUCMutSet;
  public readonly int membersBaseCombatTimeUCMutSet;
  public readonly int membersManaPotionMutSet;
  public readonly int membersHealthPotionMutSet;
  public readonly int membersSpeedRingMutSet;
  public readonly int membersGlaiveMutSet;
  public readonly int membersSlowRodMutSet;
  public readonly int membersExplosionRodMutSet;
  public readonly int membersBlazeRodMutSet;
  public readonly int membersBlastRodMutSet;
  public readonly int membersArmorMutSet;
  public readonly int membersChallengingUCMutSet;
  public readonly int membersSorcerousUCMutSet;
  public readonly int membersBaseOffenseUCMutSet;
  public readonly int membersBaseDefenseUCMutSet;
  public IUnitComponentMutBunchIncarnation(
      int membersDeathTriggerUCMutSet,
      int membersBequeathUCMutSet,
      int membersTutorialDefyCounterUCMutSet,
      int membersLightningChargingUCMutSet,
      int membersWanderAICapabilityUCMutSet,
      int membersTemporaryCloneAICapabilityUCMutSet,
      int membersSummonAICapabilityUCMutSet,
      int membersKamikazeAICapabilityUCMutSet,
      int membersGuardAICapabilityUCMutSet,
      int membersEvolvifyAICapabilityUCMutSet,
      int membersTimeCloneAICapabilityUCMutSet,
      int membersDoomedUCMutSet,
      int membersMiredUCMutSet,
      int membersOnFireUCMutSet,
      int membersAttackAICapabilityUCMutSet,
      int membersCounteringUCMutSet,
      int membersLightningChargedUCMutSet,
      int membersInvincibilityUCMutSet,
      int membersDefyingUCMutSet,
      int membersBideAICapabilityUCMutSet,
      int membersBaseSightRangeUCMutSet,
      int membersBaseMovementTimeUCMutSet,
      int membersBaseCombatTimeUCMutSet,
      int membersManaPotionMutSet,
      int membersHealthPotionMutSet,
      int membersSpeedRingMutSet,
      int membersGlaiveMutSet,
      int membersSlowRodMutSet,
      int membersExplosionRodMutSet,
      int membersBlazeRodMutSet,
      int membersBlastRodMutSet,
      int membersArmorMutSet,
      int membersChallengingUCMutSet,
      int membersSorcerousUCMutSet,
      int membersBaseOffenseUCMutSet,
      int membersBaseDefenseUCMutSet) {
    this.membersDeathTriggerUCMutSet = membersDeathTriggerUCMutSet;
    this.membersBequeathUCMutSet = membersBequeathUCMutSet;
    this.membersTutorialDefyCounterUCMutSet = membersTutorialDefyCounterUCMutSet;
    this.membersLightningChargingUCMutSet = membersLightningChargingUCMutSet;
    this.membersWanderAICapabilityUCMutSet = membersWanderAICapabilityUCMutSet;
    this.membersTemporaryCloneAICapabilityUCMutSet = membersTemporaryCloneAICapabilityUCMutSet;
    this.membersSummonAICapabilityUCMutSet = membersSummonAICapabilityUCMutSet;
    this.membersKamikazeAICapabilityUCMutSet = membersKamikazeAICapabilityUCMutSet;
    this.membersGuardAICapabilityUCMutSet = membersGuardAICapabilityUCMutSet;
    this.membersEvolvifyAICapabilityUCMutSet = membersEvolvifyAICapabilityUCMutSet;
    this.membersTimeCloneAICapabilityUCMutSet = membersTimeCloneAICapabilityUCMutSet;
    this.membersDoomedUCMutSet = membersDoomedUCMutSet;
    this.membersMiredUCMutSet = membersMiredUCMutSet;
    this.membersOnFireUCMutSet = membersOnFireUCMutSet;
    this.membersAttackAICapabilityUCMutSet = membersAttackAICapabilityUCMutSet;
    this.membersCounteringUCMutSet = membersCounteringUCMutSet;
    this.membersLightningChargedUCMutSet = membersLightningChargedUCMutSet;
    this.membersInvincibilityUCMutSet = membersInvincibilityUCMutSet;
    this.membersDefyingUCMutSet = membersDefyingUCMutSet;
    this.membersBideAICapabilityUCMutSet = membersBideAICapabilityUCMutSet;
    this.membersBaseSightRangeUCMutSet = membersBaseSightRangeUCMutSet;
    this.membersBaseMovementTimeUCMutSet = membersBaseMovementTimeUCMutSet;
    this.membersBaseCombatTimeUCMutSet = membersBaseCombatTimeUCMutSet;
    this.membersManaPotionMutSet = membersManaPotionMutSet;
    this.membersHealthPotionMutSet = membersHealthPotionMutSet;
    this.membersSpeedRingMutSet = membersSpeedRingMutSet;
    this.membersGlaiveMutSet = membersGlaiveMutSet;
    this.membersSlowRodMutSet = membersSlowRodMutSet;
    this.membersExplosionRodMutSet = membersExplosionRodMutSet;
    this.membersBlazeRodMutSet = membersBlazeRodMutSet;
    this.membersBlastRodMutSet = membersBlastRodMutSet;
    this.membersArmorMutSet = membersArmorMutSet;
    this.membersChallengingUCMutSet = membersChallengingUCMutSet;
    this.membersSorcerousUCMutSet = membersSorcerousUCMutSet;
    this.membersBaseOffenseUCMutSet = membersBaseOffenseUCMutSet;
    this.membersBaseDefenseUCMutSet = membersBaseDefenseUCMutSet;
  }
  public IUnitComponentMutBunchIncarnation Copy() {
    return new IUnitComponentMutBunchIncarnation(
membersDeathTriggerUCMutSet,
membersBequeathUCMutSet,
membersTutorialDefyCounterUCMutSet,
membersLightningChargingUCMutSet,
membersWanderAICapabilityUCMutSet,
membersTemporaryCloneAICapabilityUCMutSet,
membersSummonAICapabilityUCMutSet,
membersKamikazeAICapabilityUCMutSet,
membersGuardAICapabilityUCMutSet,
membersEvolvifyAICapabilityUCMutSet,
membersTimeCloneAICapabilityUCMutSet,
membersDoomedUCMutSet,
membersMiredUCMutSet,
membersOnFireUCMutSet,
membersAttackAICapabilityUCMutSet,
membersCounteringUCMutSet,
membersLightningChargedUCMutSet,
membersInvincibilityUCMutSet,
membersDefyingUCMutSet,
membersBideAICapabilityUCMutSet,
membersBaseSightRangeUCMutSet,
membersBaseMovementTimeUCMutSet,
membersBaseCombatTimeUCMutSet,
membersManaPotionMutSet,
membersHealthPotionMutSet,
membersSpeedRingMutSet,
membersGlaiveMutSet,
membersSlowRodMutSet,
membersExplosionRodMutSet,
membersBlazeRodMutSet,
membersBlastRodMutSet,
membersArmorMutSet,
membersChallengingUCMutSet,
membersSorcerousUCMutSet,
membersBaseOffenseUCMutSet,
membersBaseDefenseUCMutSet    );
  }

  public void visitIUnitComponentMutBunchCreateEffect(IUnitComponentMutBunchCreateEffect e) {}
  public void visitIUnitComponentMutBunchDeleteEffect(IUnitComponentMutBunchDeleteEffect e) {}




































  public void ApplyEffect(IIUnitComponentMutBunchEffect effect) { effect.visitIIUnitComponentMutBunchEffect(this); }
}

}
