using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IEmberDeepLevelLinkerTTCMutSetEffect {
  int id { get; }
  void visit(IEmberDeepLevelLinkerTTCMutSetEffectVisitor visitor);
}

}
