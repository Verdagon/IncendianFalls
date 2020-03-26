using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ITimeAnchorTTCEffect : IEffect {
  int id { get; }
  void visitITimeAnchorTTCEffect(ITimeAnchorTTCEffectVisitor visitor);
}
       
}
