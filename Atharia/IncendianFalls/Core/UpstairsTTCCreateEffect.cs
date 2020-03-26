using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct UpStairsTTCCreateEffect : IUpStairsTTCEffect {
  public readonly int id;
  public readonly UpStairsTTCIncarnation incarnation;
  public UpStairsTTCCreateEffect(int id, UpStairsTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IUpStairsTTCEffect.id => id;
  public void visitIUpStairsTTCEffect(IUpStairsTTCEffectVisitor visitor) {
    visitor.visitUpStairsTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitUpStairsTTCEffect(this);
  }
}

}
