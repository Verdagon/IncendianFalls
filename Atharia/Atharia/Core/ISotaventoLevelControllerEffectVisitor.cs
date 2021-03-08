using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISotaventoLevelControllerEffectVisitor {
  void visitSotaventoLevelControllerCreateEffect(SotaventoLevelControllerCreateEffect effect);
  void visitSotaventoLevelControllerDeleteEffect(SotaventoLevelControllerDeleteEffect effect);
}

}
