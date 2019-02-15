using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IGameEffect {
  int id { get; }
  void visit(IGameEffectVisitor visitor);
}
       
}
