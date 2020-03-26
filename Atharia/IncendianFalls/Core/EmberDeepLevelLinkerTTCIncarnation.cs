using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class EmberDeepLevelLinkerTTCIncarnation : IEmberDeepLevelLinkerTTCEffectVisitor {
  public readonly int nextLevelDepth;
  public EmberDeepLevelLinkerTTCIncarnation(
      int nextLevelDepth) {
    this.nextLevelDepth = nextLevelDepth;
  }
  public EmberDeepLevelLinkerTTCIncarnation Copy() {
    return new EmberDeepLevelLinkerTTCIncarnation(
nextLevelDepth    );
  }

  public void visitEmberDeepLevelLinkerTTCCreateEffect(EmberDeepLevelLinkerTTCCreateEffect e) {}
  public void visitEmberDeepLevelLinkerTTCDeleteEffect(EmberDeepLevelLinkerTTCDeleteEffect e) {}

  public void ApplyEffect(IEmberDeepLevelLinkerTTCEffect effect) { effect.visitIEmberDeepLevelLinkerTTCEffect(this); }
}

}
