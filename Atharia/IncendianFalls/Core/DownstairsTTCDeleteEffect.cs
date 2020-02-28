using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DownStairsTTCDeleteEffect : IDownStairsTTCEffect {
  public readonly int id;
  public DownStairsTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IDownStairsTTCEffect.id => id;
  public void visit(IDownStairsTTCEffectVisitor visitor) {
    visitor.visitDownStairsTTCDeleteEffect(this);
  }
}

}
