using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DownStaircaseTTCMutSetCreateEffect : IDownStaircaseTTCMutSetEffect {
  public readonly int id;
  public DownStaircaseTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IDownStaircaseTTCMutSetEffect.id => id;
  public void visit(IDownStaircaseTTCMutSetEffectVisitor visitor) {
    visitor.visitDownStaircaseTTCMutSetCreateEffect(this);
  }
}

}
