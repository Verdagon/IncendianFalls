using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct LevelLinkTTCCreateEffect : ILevelLinkTTCEffect {
  public readonly int id;
  public readonly LevelLinkTTCIncarnation incarnation;
  public LevelLinkTTCCreateEffect(int id, LevelLinkTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ILevelLinkTTCEffect.id => id;
  public void visitILevelLinkTTCEffect(ILevelLinkTTCEffectVisitor visitor) {
    visitor.visitLevelLinkTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLevelLinkTTCEffect(this);
  }
}

}
