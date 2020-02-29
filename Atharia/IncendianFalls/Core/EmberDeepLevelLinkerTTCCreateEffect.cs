using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct EmberDeepLevelLinkerTTCCreateEffect : IEmberDeepLevelLinkerTTCEffect {
  public readonly int id;
  public EmberDeepLevelLinkerTTCCreateEffect(int id) {
    this.id = id;
  }
  int IEmberDeepLevelLinkerTTCEffect.id => id;
  public void visit(IEmberDeepLevelLinkerTTCEffectVisitor visitor) {
    visitor.visitEmberDeepLevelLinkerTTCCreateEffect(this);
  }
}

}
