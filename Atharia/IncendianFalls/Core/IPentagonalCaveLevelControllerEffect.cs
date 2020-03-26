using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IPentagonalCaveLevelControllerEffect : IEffect {
  int id { get; }
  void visitIPentagonalCaveLevelControllerEffect(IPentagonalCaveLevelControllerEffectVisitor visitor);
}
       
}
