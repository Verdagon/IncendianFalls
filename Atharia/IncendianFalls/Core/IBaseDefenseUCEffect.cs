using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IBaseDefenseUCEffect : IEffect {
  int id { get; }
  void visitIBaseDefenseUCEffect(IBaseDefenseUCEffectVisitor visitor);
}
       
}
