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
  public void visit(IEmberDeepLevelLinkerTTCEffectVisitor visitor) {
    visitor.visitEmberDeepLevelLinkerTTCDeleteEffect(this);
  }
}

}
