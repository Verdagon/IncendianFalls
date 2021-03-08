using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ILevelLinkTTCEffect : IEffect {
  int id { get; }
  void visitILevelLinkTTCEffect(ILevelLinkTTCEffectVisitor visitor);
}
       
}
