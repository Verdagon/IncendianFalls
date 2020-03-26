using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IObsidianTTCEffect : IEffect {
  int id { get; }
  void visitIObsidianTTCEffect(IObsidianTTCEffectVisitor visitor);
}
       
}
