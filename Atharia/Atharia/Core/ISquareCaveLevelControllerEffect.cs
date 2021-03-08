using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ISquareCaveLevelControllerEffect : IEffect {
  int id { get; }
  void visitISquareCaveLevelControllerEffect(ISquareCaveLevelControllerEffectVisitor visitor);
}
       
}
