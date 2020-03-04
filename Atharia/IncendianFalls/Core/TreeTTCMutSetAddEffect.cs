using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TreeTTCMutSetAddEffect : ITreeTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public TreeTTCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ITreeTTCMutSetEffect.id => id;
  public void visit(ITreeTTCMutSetEffectVisitor visitor) {
    visitor.visitTreeTTCMutSetAddEffect(this);
  }
}

}
