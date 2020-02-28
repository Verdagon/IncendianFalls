using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DownStairsTTCCreateEffect : IDownStairsTTCEffect {
  public readonly int id;
  public DownStairsTTCCreateEffect(int id) {
    this.id = id;
  }
  int IDownStairsTTCEffect.id => id;
  public void visit(IDownStairsTTCEffectVisitor visitor) {
    visitor.visitDownStairsTTCCreateEffect(this);
  }
}

}
