using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TreeTTCDeleteEffect : ITreeTTCEffect {
  public readonly int id;
  public TreeTTCDeleteEffect(int id) {
    this.id = id;
  }
  int ITreeTTCEffect.id => id;
  public void visit(ITreeTTCEffectVisitor visitor) {
    visitor.visitTreeTTCDeleteEffect(this);
  }
}

}
