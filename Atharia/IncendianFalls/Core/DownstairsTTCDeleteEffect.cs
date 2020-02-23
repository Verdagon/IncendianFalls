using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DownstairsTTCDeleteEffect : IDownstairsTTCEffect {
  public readonly int id;
  public DownstairsTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IDownstairsTTCEffect.id => id;
  public void visit(IDownstairsTTCEffectVisitor visitor) {
    visitor.visitDownstairsTTCDeleteEffect(this);
  }
}

}
