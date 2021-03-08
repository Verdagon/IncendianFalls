using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISquareCaveLevelControllerEffectVisitor {
  void visitSquareCaveLevelControllerCreateEffect(SquareCaveLevelControllerCreateEffect effect);
  void visitSquareCaveLevelControllerDeleteEffect(SquareCaveLevelControllerDeleteEffect effect);
}

}
