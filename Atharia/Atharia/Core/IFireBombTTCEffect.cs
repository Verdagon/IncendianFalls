using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IFireBombTTCEffect : IEffect {
  int id { get; }
  void visitIFireBombTTCEffect(IFireBombTTCEffectVisitor visitor);
}
       
}
