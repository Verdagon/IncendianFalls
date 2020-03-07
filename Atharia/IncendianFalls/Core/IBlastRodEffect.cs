using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IBlastRodEffect {
  int id { get; }
  void visit(IBlastRodEffectVisitor visitor);
}
       
}
