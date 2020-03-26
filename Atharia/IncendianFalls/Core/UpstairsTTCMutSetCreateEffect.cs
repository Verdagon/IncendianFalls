using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct UpStairsTTCMutSetCreateEffect : IUpStairsTTCMutSetEffect {
  public readonly int id;
  public UpStairsTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IUpStairsTTCMutSetEffect.id => id;
  public void visitIUpStairsTTCMutSetEffect(IUpStairsTTCMutSetEffectVisitor visitor) {
    visitor.visitUpStairsTTCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitUpStairsTTCMutSetEffect(this);
  }
}

}
