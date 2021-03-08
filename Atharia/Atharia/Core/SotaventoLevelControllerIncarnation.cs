using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SotaventoLevelControllerIncarnation : ISotaventoLevelControllerEffectVisitor {
  public readonly int level;
  public SotaventoLevelControllerIncarnation(
      int level) {
    this.level = level;
  }
  public SotaventoLevelControllerIncarnation Copy() {
    return new SotaventoLevelControllerIncarnation(
level    );
  }

  public void visitSotaventoLevelControllerCreateEffect(SotaventoLevelControllerCreateEffect e) {}
  public void visitSotaventoLevelControllerDeleteEffect(SotaventoLevelControllerDeleteEffect e) {}

  public void ApplyEffect(ISotaventoLevelControllerEffect effect) { effect.visitISotaventoLevelControllerEffect(this); }
}

}
