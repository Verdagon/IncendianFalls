using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class IPreActingUCWeakMutBunchIncarnation : IIPreActingUCWeakMutBunchEffectVisitor {
  public readonly int membersDoomedUCWeakMutSet;
  public readonly int membersMiredUCWeakMutSet;
  public readonly int membersInvincibilityUCWeakMutSet;
  public readonly int membersOnFireUCWeakMutSet;
  public readonly int membersDefyingUCWeakMutSet;
  public readonly int membersCounteringUCWeakMutSet;
  public readonly int membersAttackAICapabilityUCWeakMutSet;
  public IPreActingUCWeakMutBunchIncarnation(
      int membersDoomedUCWeakMutSet,
      int membersMiredUCWeakMutSet,
      int membersInvincibilityUCWeakMutSet,
      int membersOnFireUCWeakMutSet,
      int membersDefyingUCWeakMutSet,
      int membersCounteringUCWeakMutSet,
      int membersAttackAICapabilityUCWeakMutSet) {
    this.membersDoomedUCWeakMutSet = membersDoomedUCWeakMutSet;
    this.membersMiredUCWeakMutSet = membersMiredUCWeakMutSet;
    this.membersInvincibilityUCWeakMutSet = membersInvincibilityUCWeakMutSet;
    this.membersOnFireUCWeakMutSet = membersOnFireUCWeakMutSet;
    this.membersDefyingUCWeakMutSet = membersDefyingUCWeakMutSet;
    this.membersCounteringUCWeakMutSet = membersCounteringUCWeakMutSet;
    this.membersAttackAICapabilityUCWeakMutSet = membersAttackAICapabilityUCWeakMutSet;
  }
  public IPreActingUCWeakMutBunchIncarnation Copy() {
    return new IPreActingUCWeakMutBunchIncarnation(
membersDoomedUCWeakMutSet,
membersMiredUCWeakMutSet,
membersInvincibilityUCWeakMutSet,
membersOnFireUCWeakMutSet,
membersDefyingUCWeakMutSet,
membersCounteringUCWeakMutSet,
membersAttackAICapabilityUCWeakMutSet    );
  }

  public void visitIPreActingUCWeakMutBunchCreateEffect(IPreActingUCWeakMutBunchCreateEffect e) {}
  public void visitIPreActingUCWeakMutBunchDeleteEffect(IPreActingUCWeakMutBunchDeleteEffect e) {}







  public void ApplyEffect(IIPreActingUCWeakMutBunchEffect effect) { effect.visitIIPreActingUCWeakMutBunchEffect(this); }
}

}
