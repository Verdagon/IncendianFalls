using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ISotaventoLevelControllerEffect : IEffect {
  int id { get; }
  void visitISotaventoLevelControllerEffect(ISotaventoLevelControllerEffectVisitor visitor);
}
       
}
