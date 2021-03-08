using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ITutorial2LevelControllerEffect : IEffect {
  int id { get; }
  void visitITutorial2LevelControllerEffect(ITutorial2LevelControllerEffectVisitor visitor);
}
       
}
