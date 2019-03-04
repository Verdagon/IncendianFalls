using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct UpStaircaseTTCDeleteEffect : IUpStaircaseTTCEffect {
  public readonly int id;
  public UpStaircaseTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IUpStaircaseTTCEffect.id => id;
  public void visit(IUpStaircaseTTCEffectVisitor visitor) {
    visitor.visitUpStaircaseTTCDeleteEffect(this);
  }
}

}
