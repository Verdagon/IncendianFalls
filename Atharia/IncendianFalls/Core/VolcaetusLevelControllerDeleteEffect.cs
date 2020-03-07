using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct VolcaetusLevelControllerDeleteEffect : IVolcaetusLevelControllerEffect {
  public readonly int id;
  public VolcaetusLevelControllerDeleteEffect(int id) {
    this.id = id;
  }
  int IVolcaetusLevelControllerEffect.id => id;
  public void visit(IVolcaetusLevelControllerEffectVisitor visitor) {
    visitor.visitVolcaetusLevelControllerDeleteEffect(this);
  }
}

}
