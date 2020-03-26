using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ITutorial1LevelControllerEffect : IEffect {
  int id { get; }
  void visitITutorial1LevelControllerEffect(ITutorial1LevelControllerEffectVisitor visitor);
}
       
}
