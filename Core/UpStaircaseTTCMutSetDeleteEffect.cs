using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct UpStaircaseTTCMutSetDeleteEffect : IUpStaircaseTTCMutSetEffect {
  public readonly int id;
  public UpStaircaseTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IUpStaircaseTTCMutSetEffect.id => id;
  public void visit(IUpStaircaseTTCMutSetEffectVisitor visitor) {
    visitor.visitUpStaircaseTTCMutSetDeleteEffect(this);
  }
}

}
