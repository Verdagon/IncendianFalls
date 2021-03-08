using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IMagmaTTCEffect : IEffect {
  int id { get; }
  void visitIMagmaTTCEffect(IMagmaTTCEffectVisitor visitor);
}
       
}
