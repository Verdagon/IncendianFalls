using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct EmberDeepLevelLinkerTTCDeleteEffect : IEmberDeepLevelLinkerTTCEffect {
  public readonly int id;
  public EmberDeepLevelLinkerTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IEmberDeepLevelLinkerTTCEffect.id => id;
  public void visitIEmberDeepLevelLinkerTTCEffect(IEmberDeepLevelLinkerTTCEffectVisitor visitor) {
    visitor.visitEmberDeepLevelLinkerTTCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitEmberDeepLevelLinkerTTCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
