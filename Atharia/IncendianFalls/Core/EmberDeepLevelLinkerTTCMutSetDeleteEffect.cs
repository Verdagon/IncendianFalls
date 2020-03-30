using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct EmberDeepLevelLinkerTTCMutSetDeleteEffect : IEmberDeepLevelLinkerTTCMutSetEffect {
  public readonly int id;
  public EmberDeepLevelLinkerTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IEmberDeepLevelLinkerTTCMutSetEffect.id => id;
  public void visitIEmberDeepLevelLinkerTTCMutSetEffect(IEmberDeepLevelLinkerTTCMutSetEffectVisitor visitor) {
    visitor.visitEmberDeepLevelLinkerTTCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitEmberDeepLevelLinkerTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
