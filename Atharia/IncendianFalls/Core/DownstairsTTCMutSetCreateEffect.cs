using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DownStairsTTCMutSetCreateEffect : IDownStairsTTCMutSetEffect {
  public readonly int id;
  public DownStairsTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IDownStairsTTCMutSetEffect.id => id;
  public void visitIDownStairsTTCMutSetEffect(IDownStairsTTCMutSetEffectVisitor visitor) {
    visitor.visitDownStairsTTCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDownStairsTTCMutSetEffect(this);
  }
}

}
