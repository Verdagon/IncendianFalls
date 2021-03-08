using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IPreGauntletLevelControllerEffect : IEffect {
  int id { get; }
  void visitIPreGauntletLevelControllerEffect(IPreGauntletLevelControllerEffectVisitor visitor);
}
       
}
