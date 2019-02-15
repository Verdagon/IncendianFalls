using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IDefendingDetailEffect {
  int id { get; }
  void visit(IDefendingDetailEffectVisitor visitor);
}
       
}
