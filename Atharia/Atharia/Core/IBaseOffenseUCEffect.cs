using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IBaseOffenseUCEffect : IEffect {
  int id { get; }
  void visitIBaseOffenseUCEffect(IBaseOffenseUCEffectVisitor visitor);
}
       
}
