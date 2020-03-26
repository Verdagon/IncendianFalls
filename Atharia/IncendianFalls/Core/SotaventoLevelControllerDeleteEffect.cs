using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct SotaventoLevelControllerDeleteEffect : ISotaventoLevelControllerEffect {
  public readonly int id;
  public SotaventoLevelControllerDeleteEffect(int id) {
    this.id = id;
  }
  int ISotaventoLevelControllerEffect.id => id;
  public void visitISotaventoLevelControllerEffect(ISotaventoLevelControllerEffectVisitor visitor) {
    visitor.visitSotaventoLevelControllerDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSotaventoLevelControllerEffect(this);
  }
}

}
