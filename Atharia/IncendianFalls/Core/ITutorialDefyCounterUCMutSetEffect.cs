using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITutorialDefyCounterUCMutSetEffect {
  int id { get; }
  void visit(ITutorialDefyCounterUCMutSetEffectVisitor visitor);
}

}
