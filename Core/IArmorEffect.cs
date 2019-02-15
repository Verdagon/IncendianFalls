using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IArmorEffect {
  int id { get; }
  void visit(IArmorEffectVisitor visitor);
}
       
}
