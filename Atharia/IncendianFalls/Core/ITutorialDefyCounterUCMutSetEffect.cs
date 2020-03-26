using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITutorialDefyCounterUCMutSetEffect : IEffect {
  int id { get; }
  void visitITutorialDefyCounterUCMutSetEffect(ITutorialDefyCounterUCMutSetEffectVisitor visitor);
}

}
