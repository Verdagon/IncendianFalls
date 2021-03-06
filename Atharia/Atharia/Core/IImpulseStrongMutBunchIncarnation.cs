using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class IImpulseStrongMutBunchIncarnation : IIImpulseStrongMutBunchEffectVisitor {
  public readonly int membersHoldPositionImpulseStrongMutSet;
  public readonly int membersTemporaryCloneImpulseStrongMutSet;
  public readonly int membersSummonImpulseStrongMutSet;
  public readonly int membersMireImpulseStrongMutSet;
  public readonly int membersEvaporateImpulseStrongMutSet;
  public readonly int membersMoveImpulseStrongMutSet;
  public readonly int membersKamikazeJumpImpulseStrongMutSet;
  public readonly int membersKamikazeTargetImpulseStrongMutSet;
  public readonly int membersNoImpulseStrongMutSet;
  public readonly int membersEvolvifyImpulseStrongMutSet;
  public readonly int membersFireImpulseStrongMutSet;
  public readonly int membersDefyImpulseStrongMutSet;
  public readonly int membersCounterImpulseStrongMutSet;
  public readonly int membersUnleashBideImpulseStrongMutSet;
  public readonly int membersContinueBidingImpulseStrongMutSet;
  public readonly int membersStartBidingImpulseStrongMutSet;
  public readonly int membersAttackImpulseStrongMutSet;
  public readonly int membersPursueImpulseStrongMutSet;
  public readonly int membersFireBombImpulseStrongMutSet;
  public IImpulseStrongMutBunchIncarnation(
      int membersHoldPositionImpulseStrongMutSet,
      int membersTemporaryCloneImpulseStrongMutSet,
      int membersSummonImpulseStrongMutSet,
      int membersMireImpulseStrongMutSet,
      int membersEvaporateImpulseStrongMutSet,
      int membersMoveImpulseStrongMutSet,
      int membersKamikazeJumpImpulseStrongMutSet,
      int membersKamikazeTargetImpulseStrongMutSet,
      int membersNoImpulseStrongMutSet,
      int membersEvolvifyImpulseStrongMutSet,
      int membersFireImpulseStrongMutSet,
      int membersDefyImpulseStrongMutSet,
      int membersCounterImpulseStrongMutSet,
      int membersUnleashBideImpulseStrongMutSet,
      int membersContinueBidingImpulseStrongMutSet,
      int membersStartBidingImpulseStrongMutSet,
      int membersAttackImpulseStrongMutSet,
      int membersPursueImpulseStrongMutSet,
      int membersFireBombImpulseStrongMutSet) {
    this.membersHoldPositionImpulseStrongMutSet = membersHoldPositionImpulseStrongMutSet;
    this.membersTemporaryCloneImpulseStrongMutSet = membersTemporaryCloneImpulseStrongMutSet;
    this.membersSummonImpulseStrongMutSet = membersSummonImpulseStrongMutSet;
    this.membersMireImpulseStrongMutSet = membersMireImpulseStrongMutSet;
    this.membersEvaporateImpulseStrongMutSet = membersEvaporateImpulseStrongMutSet;
    this.membersMoveImpulseStrongMutSet = membersMoveImpulseStrongMutSet;
    this.membersKamikazeJumpImpulseStrongMutSet = membersKamikazeJumpImpulseStrongMutSet;
    this.membersKamikazeTargetImpulseStrongMutSet = membersKamikazeTargetImpulseStrongMutSet;
    this.membersNoImpulseStrongMutSet = membersNoImpulseStrongMutSet;
    this.membersEvolvifyImpulseStrongMutSet = membersEvolvifyImpulseStrongMutSet;
    this.membersFireImpulseStrongMutSet = membersFireImpulseStrongMutSet;
    this.membersDefyImpulseStrongMutSet = membersDefyImpulseStrongMutSet;
    this.membersCounterImpulseStrongMutSet = membersCounterImpulseStrongMutSet;
    this.membersUnleashBideImpulseStrongMutSet = membersUnleashBideImpulseStrongMutSet;
    this.membersContinueBidingImpulseStrongMutSet = membersContinueBidingImpulseStrongMutSet;
    this.membersStartBidingImpulseStrongMutSet = membersStartBidingImpulseStrongMutSet;
    this.membersAttackImpulseStrongMutSet = membersAttackImpulseStrongMutSet;
    this.membersPursueImpulseStrongMutSet = membersPursueImpulseStrongMutSet;
    this.membersFireBombImpulseStrongMutSet = membersFireBombImpulseStrongMutSet;
  }
  public IImpulseStrongMutBunchIncarnation Copy() {
    return new IImpulseStrongMutBunchIncarnation(
membersHoldPositionImpulseStrongMutSet,
membersTemporaryCloneImpulseStrongMutSet,
membersSummonImpulseStrongMutSet,
membersMireImpulseStrongMutSet,
membersEvaporateImpulseStrongMutSet,
membersMoveImpulseStrongMutSet,
membersKamikazeJumpImpulseStrongMutSet,
membersKamikazeTargetImpulseStrongMutSet,
membersNoImpulseStrongMutSet,
membersEvolvifyImpulseStrongMutSet,
membersFireImpulseStrongMutSet,
membersDefyImpulseStrongMutSet,
membersCounterImpulseStrongMutSet,
membersUnleashBideImpulseStrongMutSet,
membersContinueBidingImpulseStrongMutSet,
membersStartBidingImpulseStrongMutSet,
membersAttackImpulseStrongMutSet,
membersPursueImpulseStrongMutSet,
membersFireBombImpulseStrongMutSet    );
  }

  public void visitIImpulseStrongMutBunchCreateEffect(IImpulseStrongMutBunchCreateEffect e) {}
  public void visitIImpulseStrongMutBunchDeleteEffect(IImpulseStrongMutBunchDeleteEffect e) {}



















  public void ApplyEffect(IIImpulseStrongMutBunchEffect effect) { effect.visitIIImpulseStrongMutBunchEffect(this); }
}

}
