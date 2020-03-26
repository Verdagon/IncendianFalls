using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class IPostActingUCWeakMutBunchIncarnation : IIPostActingUCWeakMutBunchEffectVisitor {
  public readonly int membersLightningChargedUCWeakMutSet;
  public readonly int membersTimeCloneAICapabilityUCWeakMutSet;
  public IPostActingUCWeakMutBunchIncarnation(
      int membersLightningChargedUCWeakMutSet,
      int membersTimeCloneAICapabilityUCWeakMutSet) {
    this.membersLightningChargedUCWeakMutSet = membersLightningChargedUCWeakMutSet;
    this.membersTimeCloneAICapabilityUCWeakMutSet = membersTimeCloneAICapabilityUCWeakMutSet;
  }
  public IPostActingUCWeakMutBunchIncarnation Copy() {
    return new IPostActingUCWeakMutBunchIncarnation(
membersLightningChargedUCWeakMutSet,
membersTimeCloneAICapabilityUCWeakMutSet    );
  }

  public void visitIPostActingUCWeakMutBunchCreateEffect(IPostActingUCWeakMutBunchCreateEffect e) {}
  public void visitIPostActingUCWeakMutBunchDeleteEffect(IPostActingUCWeakMutBunchDeleteEffect e) {}


  public void ApplyEffect(IIPostActingUCWeakMutBunchEffect effect) { effect.visitIIPostActingUCWeakMutBunchEffect(this); }
}

}
