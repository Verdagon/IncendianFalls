using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IUnitEffect : IEffect {
  int id { get; }
  void visitIUnitEffect(IUnitEffectVisitor visitor);
}
       
}
