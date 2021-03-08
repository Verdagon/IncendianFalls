using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IRetreatLevelControllerEffectVisitor {
  void visitRetreatLevelControllerCreateEffect(RetreatLevelControllerCreateEffect effect);
  void visitRetreatLevelControllerDeleteEffect(RetreatLevelControllerDeleteEffect effect);
}

}
