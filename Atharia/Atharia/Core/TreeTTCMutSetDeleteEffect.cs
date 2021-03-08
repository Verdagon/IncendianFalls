using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TreeTTCMutSetDeleteEffect : ITreeTTCMutSetEffect {
  public readonly int id;
  public TreeTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ITreeTTCMutSetEffect.id => id;
  public void visitITreeTTCMutSetEffect(ITreeTTCMutSetEffectVisitor visitor) {
    visitor.visitTreeTTCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTreeTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
