using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ILakeLevelControllerEffectVisitor {
  void visitLakeLevelControllerCreateEffect(LakeLevelControllerCreateEffect effect);
  void visitLakeLevelControllerDeleteEffect(LakeLevelControllerDeleteEffect effect);
}

}
