using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct EmberDeepLevelLinkerTTCMutSetRemoveEffect : IEmberDeepLevelLinkerTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public EmberDeepLevelLinkerTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IEmberDeepLevelLinkerTTCMutSetEffect.id => id;
  public void visit(IEmberDeepLevelLinkerTTCMutSetEffectVisitor visitor) {
    visitor.visitEmberDeepLevelLinkerTTCMutSetRemoveEffect(this);
  }
}

}
