using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ITutorialDefyCounterUCEffect : IEffect {
  int id { get; }
  void visitITutorialDefyCounterUCEffect(ITutorialDefyCounterUCEffectVisitor visitor);
}
       
}
