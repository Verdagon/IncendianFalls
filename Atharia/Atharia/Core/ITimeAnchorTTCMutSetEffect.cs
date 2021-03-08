using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITimeAnchorTTCMutSetEffect : IEffect {
  int id { get; }
  void visitITimeAnchorTTCMutSetEffect(ITimeAnchorTTCMutSetEffectVisitor visitor);
}

}
