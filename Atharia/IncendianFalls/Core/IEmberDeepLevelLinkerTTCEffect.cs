using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IEmberDeepLevelLinkerTTCEffect {
  int id { get; }
  void visit(IEmberDeepLevelLinkerTTCEffectVisitor visitor);
}
       
}
