using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IVolcaetusLevelControllerEffect {
  int id { get; }
  void visit(IVolcaetusLevelControllerEffectVisitor visitor);
}
       
}
