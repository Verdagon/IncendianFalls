using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ILevelEffectVisitor {
  void visitLevelCreateEffect(LevelCreateEffect effect);
  void visitLevelDeleteEffect(LevelDeleteEffect effect);
}

}
