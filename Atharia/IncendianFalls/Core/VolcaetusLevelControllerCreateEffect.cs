using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct VolcaetusLevelControllerCreateEffect : IVolcaetusLevelControllerEffect {
  public readonly int id;
  public VolcaetusLevelControllerCreateEffect(int id) {
    this.id = id;
  }
  int IVolcaetusLevelControllerEffect.id => id;
  public void visit(IVolcaetusLevelControllerEffectVisitor visitor) {
    visitor.visitVolcaetusLevelControllerCreateEffect(this);
  }
}

}
