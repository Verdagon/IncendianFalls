using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct UpStairsTTCMutSetRemoveEffect : IUpStairsTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public UpStairsTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IUpStairsTTCMutSetEffect.id => id;
  public void visitIUpStairsTTCMutSetEffect(IUpStairsTTCMutSetEffectVisitor visitor) {
    visitor.visitUpStairsTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitUpStairsTTCMutSetEffect(this);
  }
}

}
