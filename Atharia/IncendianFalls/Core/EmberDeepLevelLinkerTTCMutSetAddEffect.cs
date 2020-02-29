using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct EmberDeepLevelLinkerTTCMutSetAddEffect : IEmberDeepLevelLinkerTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public EmberDeepLevelLinkerTTCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IEmberDeepLevelLinkerTTCMutSetEffect.id => id;
  public void visit(IEmberDeepLevelLinkerTTCMutSetEffectVisitor visitor) {
    visitor.visitEmberDeepLevelLinkerTTCMutSetAddEffect(this);
  }
}

}
