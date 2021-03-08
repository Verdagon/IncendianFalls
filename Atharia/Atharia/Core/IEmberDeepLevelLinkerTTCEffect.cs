using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IEmberDeepLevelLinkerTTCEffect : IEffect {
  int id { get; }
  void visitIEmberDeepLevelLinkerTTCEffect(IEmberDeepLevelLinkerTTCEffectVisitor visitor);
}
       
}
