using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IKamikazeTargetTTCEffect {
  int id { get; }
  void visit(IKamikazeTargetTTCEffectVisitor visitor);
}
       
}
