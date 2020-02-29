using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct EmberDeepLevelLinkerTTCMutSetCreateEffect : IEmberDeepLevelLinkerTTCMutSetEffect {
  public readonly int id;
  public EmberDeepLevelLinkerTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IEmberDeepLevelLinkerTTCMutSetEffect.id => id;
  public void visit(IEmberDeepLevelLinkerTTCMutSetEffectVisitor visitor) {
    visitor.visitEmberDeepLevelLinkerTTCMutSetCreateEffect(this);
  }
}

}
