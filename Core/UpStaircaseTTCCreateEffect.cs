using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct UpStaircaseTTCCreateEffect : IUpStaircaseTTCEffect {
  public readonly int id;
  public UpStaircaseTTCCreateEffect(int id) {
    this.id = id;
  }
  int IUpStaircaseTTCEffect.id => id;
  public void visit(IUpStaircaseTTCEffectVisitor visitor) {
    visitor.visitUpStaircaseTTCCreateEffect(this);
  }
}

}
