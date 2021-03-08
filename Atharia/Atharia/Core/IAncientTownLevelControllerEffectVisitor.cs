using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IAncientTownLevelControllerEffectVisitor {
  void visitAncientTownLevelControllerCreateEffect(AncientTownLevelControllerCreateEffect effect);
  void visitAncientTownLevelControllerDeleteEffect(AncientTownLevelControllerDeleteEffect effect);
}

}
