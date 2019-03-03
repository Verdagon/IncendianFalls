using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LevelMutSetDeleteEffect : ILevelMutSetEffect {
  public readonly int id;
  public LevelMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ILevelMutSetEffect.id => id;
  public void visit(ILevelMutSetEffectVisitor visitor) {
    visitor.visitLevelMutSetDeleteEffect(this);
  }
}

}
