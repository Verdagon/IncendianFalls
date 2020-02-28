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
  public void visit(IUpStairsTTCMutSetEffectVisitor visitor) {
    visitor.visitUpStairsTTCMutSetCreateEffect(this);
  }
}

}
