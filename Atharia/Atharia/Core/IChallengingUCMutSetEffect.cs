using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IChallengingUCMutSetEffect : IEffect {
  int id { get; }
  void visitIChallengingUCMutSetEffect(IChallengingUCMutSetEffectVisitor visitor);
}

}
