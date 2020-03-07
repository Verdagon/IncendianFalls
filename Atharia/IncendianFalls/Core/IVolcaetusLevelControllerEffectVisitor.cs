using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IVolcaetusLevelControllerEffectVisitor {
  void visitVolcaetusLevelControllerCreateEffect(VolcaetusLevelControllerCreateEffect effect);
  void visitVolcaetusLevelControllerDeleteEffect(VolcaetusLevelControllerDeleteEffect effect);
}

}
