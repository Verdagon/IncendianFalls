using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IRoseTTCMutSetEffect : IEffect {
  int id { get; }
  void visitIRoseTTCMutSetEffect(IRoseTTCMutSetEffectVisitor visitor);
}

}
