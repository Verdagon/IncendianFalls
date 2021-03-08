using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct EmberDeepLevelLinkerTTCCreateEffect : IEmberDeepLevelLinkerTTCEffect {
  public readonly int id;
  public readonly EmberDeepLevelLinkerTTCIncarnation incarnation;
  public EmberDeepLevelLinkerTTCCreateEffect(int id, EmberDeepLevelLinkerTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IEmberDeepLevelLinkerTTCEffect.id => id;
  public void visitIEmberDeepLevelLinkerTTCEffect(IEmberDeepLevelLinkerTTCEffectVisitor visitor) {
    visitor.visitEmberDeepLevelLinkerTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitEmberDeepLevelLinkerTTCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
