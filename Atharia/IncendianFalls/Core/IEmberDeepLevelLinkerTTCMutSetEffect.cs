using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IEmberDeepLevelLinkerTTCMutSetEffect : IEffect {
  int id { get; }
  void visitIEmberDeepLevelLinkerTTCMutSetEffect(IEmberDeepLevelLinkerTTCMutSetEffectVisitor visitor);
}

}
