using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IKamikazeJumpImpulseEffect {
  int id { get; }
  void visit(IKamikazeJumpImpulseEffectVisitor visitor);
}
       
}
