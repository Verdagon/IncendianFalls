using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TreeTTCMutSetRemoveEffect : ITreeTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public TreeTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ITreeTTCMutSetEffect.id => id;
  public void visit(ITreeTTCMutSetEffectVisitor visitor) {
    visitor.visitTreeTTCMutSetRemoveEffect(this);
  }
}

}
