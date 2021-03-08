using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface INestLevelControllerEffectVisitor {
  void visitNestLevelControllerCreateEffect(NestLevelControllerCreateEffect effect);
  void visitNestLevelControllerDeleteEffect(NestLevelControllerDeleteEffect effect);
}

}
