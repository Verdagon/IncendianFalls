using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IChallengingUCEffect : IEffect {
  int id { get; }
  void visitIChallengingUCEffect(IChallengingUCEffectVisitor visitor);
}
       
}
