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
  public void visit(ITreeTTCMutSetEffectVisitor visitor) {
    visitor.visitTreeTTCMutSetDeleteEffect(this);
  }
}

}
