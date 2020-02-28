using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct UpStairsTTCMutSetDeleteEffect : IUpStairsTTCMutSetEffect {
  public readonly int id;
  public UpStairsTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IUpStairsTTCMutSetEffect.id => id;
  public void visit(IUpStairsTTCMutSetEffectVisitor visitor) {
    visitor.visitUpStairsTTCMutSetDeleteEffect(this);
  }
}

}
