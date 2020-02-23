using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct UpstairsTTCMutSetDeleteEffect : IUpstairsTTCMutSetEffect {
  public readonly int id;
  public UpstairsTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IUpstairsTTCMutSetEffect.id => id;
  public void visit(IUpstairsTTCMutSetEffectVisitor visitor) {
    visitor.visitUpstairsTTCMutSetDeleteEffect(this);
  }
}

}
