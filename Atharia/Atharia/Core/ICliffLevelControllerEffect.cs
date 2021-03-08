using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ICliffLevelControllerEffect : IEffect {
  int id { get; }
  void visitICliffLevelControllerEffect(ICliffLevelControllerEffectVisitor visitor);
}
       
}
