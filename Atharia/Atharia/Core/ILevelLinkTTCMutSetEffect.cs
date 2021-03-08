using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ILevelLinkTTCMutSetEffect : IEffect {
  int id { get; }
  void visitILevelLinkTTCMutSetEffect(ILevelLinkTTCMutSetEffectVisitor visitor);
}

}
