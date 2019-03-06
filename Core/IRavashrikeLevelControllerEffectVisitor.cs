using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IRavashrikeLevelControllerEffectVisitor {
  void visitRavashrikeLevelControllerCreateEffect(RavashrikeLevelControllerCreateEffect effect);
  void visitRavashrikeLevelControllerDeleteEffect(RavashrikeLevelControllerDeleteEffect effect);
}

}
