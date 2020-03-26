using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IWallTTCEffect : IEffect {
  int id { get; }
  void visitIWallTTCEffect(IWallTTCEffectVisitor visitor);
}
       
}
