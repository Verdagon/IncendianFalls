using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct LevelMutBunchDeleteEffect : ILevelMutBunchEffect {
  public readonly int id;
  public LevelMutBunchDeleteEffect(int id) {
    this.id = id;
  }
  int ILevelMutBunchEffect.id => id;
  public void visit(ILevelMutBunchEffectVisitor visitor) {
    visitor.visitLevelMutBunchDeleteEffect(this);
  }
}

}
