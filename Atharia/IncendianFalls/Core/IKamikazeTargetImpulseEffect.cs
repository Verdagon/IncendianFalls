using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IKamikazeTargetImpulseEffect {
  int id { get; }
  void visit(IKamikazeTargetImpulseEffectVisitor visitor);
}
       
}
