using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IMireImpulseEffect : IEffect {
  int id { get; }
  void visitIMireImpulseEffect(IMireImpulseEffectVisitor visitor);
}
       
}
