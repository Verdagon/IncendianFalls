using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct UpstairsTTCDeleteEffect : IUpstairsTTCEffect {
  public readonly int id;
  public UpstairsTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IUpstairsTTCEffect.id => id;
  public void visit(IUpstairsTTCEffectVisitor visitor) {
    visitor.visitUpstairsTTCDeleteEffect(this);
  }
}

}
