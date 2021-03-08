using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IRocksTTCMutSetEffect : IEffect {
  int id { get; }
  void visitIRocksTTCMutSetEffect(IRocksTTCMutSetEffectVisitor visitor);
}

}
