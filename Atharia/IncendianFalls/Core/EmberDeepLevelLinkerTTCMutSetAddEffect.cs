using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct EmberDeepLevelLinkerTTCMutSetAddEffect : IEmberDeepLevelLinkerTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public EmberDeepLevelLinkerTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IEmberDeepLevelLinkerTTCMutSetEffect.id => id;
  public void visitIEmberDeepLevelLinkerTTCMutSetEffect(IEmberDeepLevelLinkerTTCMutSetEffectVisitor visitor) {
    visitor.visitEmberDeepLevelLinkerTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitEmberDeepLevelLinkerTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
