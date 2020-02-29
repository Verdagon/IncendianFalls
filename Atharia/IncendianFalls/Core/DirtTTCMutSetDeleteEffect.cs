using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DirtTTCMutSetDeleteEffect : IDirtTTCMutSetEffect {
  public readonly int id;
  public DirtTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IDirtTTCMutSetEffect.id => id;
  public void visit(IDirtTTCMutSetEffectVisitor visitor) {
    visitor.visitDirtTTCMutSetDeleteEffect(this);
  }
}

}
