using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct SotaventoLevelControllerCreateEffect : ISotaventoLevelControllerEffect {
  public readonly int id;
  public readonly SotaventoLevelControllerIncarnation incarnation;
  public SotaventoLevelControllerCreateEffect(int id, SotaventoLevelControllerIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ISotaventoLevelControllerEffect.id => id;
  public void visitISotaventoLevelControllerEffect(ISotaventoLevelControllerEffectVisitor visitor) {
    visitor.visitSotaventoLevelControllerCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSotaventoLevelControllerEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
