using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct UpStairsTTCCreateEffect : IUpStairsTTCEffect {
  public readonly int id;
  public UpStairsTTCCreateEffect(int id) {
    this.id = id;
  }
  int IUpStairsTTCEffect.id => id;
  public void visit(IUpStairsTTCEffectVisitor visitor) {
    visitor.visitUpStairsTTCCreateEffect(this);
  }
}

}
