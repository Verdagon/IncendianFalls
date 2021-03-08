using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class KamikazeAICapabilityUCIncarnation : IKamikazeAICapabilityUCEffectVisitor {
  public  int targetByLocation;
  public  Location targetLocationCenter;
  public KamikazeAICapabilityUCIncarnation(
      int targetByLocation,
      Location targetLocationCenter) {
    this.targetByLocation = targetByLocation;
    this.targetLocationCenter = targetLocationCenter;
  }
  public KamikazeAICapabilityUCIncarnation Copy() {
    return new KamikazeAICapabilityUCIncarnation(
targetByLocation,
targetLocationCenter    );
  }

  public void visitKamikazeAICapabilityUCCreateEffect(KamikazeAICapabilityUCCreateEffect e) {}
  public void visitKamikazeAICapabilityUCDeleteEffect(KamikazeAICapabilityUCDeleteEffect e) {}
public void visitKamikazeAICapabilityUCSetTargetByLocationEffect(KamikazeAICapabilityUCSetTargetByLocationEffect e) { this.targetByLocation = e.newValue; }
public void visitKamikazeAICapabilityUCSetTargetLocationCenterEffect(KamikazeAICapabilityUCSetTargetLocationCenterEffect e) { this.targetLocationCenter = e.newValue; }
  public void ApplyEffect(IKamikazeAICapabilityUCEffect effect) { effect.visitIKamikazeAICapabilityUCEffect(this); }
}

}
