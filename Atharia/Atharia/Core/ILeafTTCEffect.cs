using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ILeafTTCEffect : IEffect {
  int id { get; }
  void visitILeafTTCEffect(ILeafTTCEffectVisitor visitor);
}
       
}
