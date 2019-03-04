using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct UpStaircaseTTCMutSetCreateEffect : IUpStaircaseTTCMutSetEffect {
  public readonly int id;
  public UpStaircaseTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IUpStaircaseTTCMutSetEffect.id => id;
  public void visit(IUpStaircaseTTCMutSetEffectVisitor visitor) {
    visitor.visitUpStaircaseTTCMutSetCreateEffect(this);
  }
}

}
