using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IBloodTTCEffect : IEffect {
  int id { get; }
  void visitIBloodTTCEffect(IBloodTTCEffectVisitor visitor);
}
       
}
