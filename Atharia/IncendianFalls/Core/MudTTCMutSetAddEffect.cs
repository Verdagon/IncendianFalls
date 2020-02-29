using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MudTTCMutSetAddEffect : IMudTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public MudTTCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IMudTTCMutSetEffect.id => id;
  public void visit(IMudTTCMutSetEffectVisitor visitor) {
    visitor.visitMudTTCMutSetAddEffect(this);
  }
}

}
