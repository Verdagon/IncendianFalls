using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LevelLinkTTCIncarnation : ILevelLinkTTCEffectVisitor {
  public readonly bool destroyThisLevel;
  public readonly int destinationLevel;
  public readonly Location destinationLevelLocation;
  public LevelLinkTTCIncarnation(
      bool destroyThisLevel,
      int destinationLevel,
      Location destinationLevelLocation) {
    this.destroyThisLevel = destroyThisLevel;
    this.destinationLevel = destinationLevel;
    this.destinationLevelLocation = destinationLevelLocation;
  }
  public LevelLinkTTCIncarnation Copy() {
    return new LevelLinkTTCIncarnation(
destroyThisLevel,
destinationLevel,
destinationLevelLocation    );
  }

  public void visitLevelLinkTTCCreateEffect(LevelLinkTTCCreateEffect e) {}
  public void visitLevelLinkTTCDeleteEffect(LevelLinkTTCDeleteEffect e) {}



  public void ApplyEffect(ILevelLinkTTCEffect effect) { effect.visitILevelLinkTTCEffect(this); }
}

}
