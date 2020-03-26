using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DownStairsTTCCreateEffect : IDownStairsTTCEffect {
  public readonly int id;
  public readonly DownStairsTTCIncarnation incarnation;
  public DownStairsTTCCreateEffect(int id, DownStairsTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IDownStairsTTCEffect.id => id;
  public void visitIDownStairsTTCEffect(IDownStairsTTCEffectVisitor visitor) {
    visitor.visitDownStairsTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDownStairsTTCEffect(this);
  }
}

}
