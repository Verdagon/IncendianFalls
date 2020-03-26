using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ILakeLevelControllerEffect : IEffect {
  int id { get; }
  void visitILakeLevelControllerEffect(ILakeLevelControllerEffectVisitor visitor);
}
       
}
