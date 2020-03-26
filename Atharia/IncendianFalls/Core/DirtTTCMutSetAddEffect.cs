using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DirtTTCMutSetAddEffect : IDirtTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public DirtTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IDirtTTCMutSetEffect.id => id;
  public void visitIDirtTTCMutSetEffect(IDirtTTCMutSetEffectVisitor visitor) {
    visitor.visitDirtTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDirtTTCMutSetEffect(this);
  }
}

}
