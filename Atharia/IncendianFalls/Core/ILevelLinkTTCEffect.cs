using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ILevelLinkTTCEffect {
  int id { get; }
  void visit(ILevelLinkTTCEffectVisitor visitor);
}
       
}
