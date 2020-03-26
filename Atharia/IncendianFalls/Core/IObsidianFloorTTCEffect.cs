using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IObsidianFloorTTCEffect : IEffect {
  int id { get; }
  void visitIObsidianFloorTTCEffect(IObsidianFloorTTCEffectVisitor visitor);
}
       
}
