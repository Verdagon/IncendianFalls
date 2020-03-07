using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ISotaventoLevelControllerEffect {
  int id { get; }
  void visit(ISotaventoLevelControllerEffectVisitor visitor);
}
       
}
