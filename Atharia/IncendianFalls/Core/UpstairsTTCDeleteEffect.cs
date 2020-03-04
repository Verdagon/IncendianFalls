using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct UpStairsTTCDeleteEffect : IUpStairsTTCEffect {
  public readonly int id;
  public UpStairsTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IUpStairsTTCEffect.id => id;
  public void visit(IUpStairsTTCEffectVisitor visitor) {
    visitor.visitUpStairsTTCDeleteEffect(this);
  }
}

}