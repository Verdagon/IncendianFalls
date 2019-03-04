using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DownStaircaseTTCCreateEffect : IDownStaircaseTTCEffect {
  public readonly int id;
  public DownStaircaseTTCCreateEffect(int id) {
    this.id = id;
  }
  int IDownStaircaseTTCEffect.id => id;
  public void visit(IDownStaircaseTTCEffectVisitor visitor) {
    visitor.visitDownStaircaseTTCCreateEffect(this);
  }
}

}
