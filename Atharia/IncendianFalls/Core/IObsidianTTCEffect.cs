using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IObsidianTTCEffect {
  int id { get; }
  void visit(IObsidianTTCEffectVisitor visitor);
}
       
}
