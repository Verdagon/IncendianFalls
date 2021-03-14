using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IBlazeRodEffect : IEffect {
  int id { get; }
  void visitIBlazeRodEffect(IBlazeRodEffectVisitor visitor);
}
       
}
