using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct SotaventoLevelControllerCreateEffect : ISotaventoLevelControllerEffect {
  public readonly int id;
  public SotaventoLevelControllerCreateEffect(int id) {
    this.id = id;
  }
  int ISotaventoLevelControllerEffect.id => id;
  public void visit(ISotaventoLevelControllerEffectVisitor visitor) {
    visitor.visitSotaventoLevelControllerCreateEffect(this);
  }
}

}
