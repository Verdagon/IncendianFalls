using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ICounteringUCEffect : IEffect {
  int id { get; }
  void visitICounteringUCEffect(ICounteringUCEffectVisitor visitor);
}
       
}
