using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IVolcaetusLevelControllerEffect : IEffect {
  int id { get; }
  void visitIVolcaetusLevelControllerEffect(IVolcaetusLevelControllerEffectVisitor visitor);
}
       
}
