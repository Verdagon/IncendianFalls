using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct LevelDeleteEffect : ILevelEffect {
  public readonly int id;
  public LevelDeleteEffect(int id) {
    this.id = id;
  }
  int ILevelEffect.id => id;
  public void visit(ILevelEffectVisitor visitor) {
    visitor.visitLevelDeleteEffect(this);
  }
}

}
