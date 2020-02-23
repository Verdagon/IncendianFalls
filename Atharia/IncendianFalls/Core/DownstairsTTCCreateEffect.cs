using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DownstairsTTCCreateEffect : IDownstairsTTCEffect {
  public readonly int id;
  public DownstairsTTCCreateEffect(int id) {
    this.id = id;
  }
  int IDownstairsTTCEffect.id => id;
  public void visit(IDownstairsTTCEffectVisitor visitor) {
    visitor.visitDownstairsTTCCreateEffect(this);
  }
}

}
