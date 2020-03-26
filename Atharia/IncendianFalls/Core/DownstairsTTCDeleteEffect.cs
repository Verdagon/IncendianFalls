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
  public void visitIDownStairsTTCEffect(IDownStairsTTCEffectVisitor visitor) {
    visitor.visitDownStairsTTCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDownStairsTTCEffect(this);
  }
}

}
