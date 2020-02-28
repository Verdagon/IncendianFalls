using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct LevelLinkTTCCreateEffect : ILevelLinkTTCEffect {
  public readonly int id;
  public LevelLinkTTCCreateEffect(int id) {
    this.id = id;
  }
  int ILevelLinkTTCEffect.id => id;
  public void visit(ILevelLinkTTCEffectVisitor visitor) {
    visitor.visitLevelLinkTTCCreateEffect(this);
  }
}

}
