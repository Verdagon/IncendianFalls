using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MudTTCMutSetAddEffect : IMudTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public MudTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IMudTTCMutSetEffect.id => id;
  public void visitIMudTTCMutSetEffect(IMudTTCMutSetEffectVisitor visitor) {
    visitor.visitMudTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMudTTCMutSetEffect(this);
  }
}

}
