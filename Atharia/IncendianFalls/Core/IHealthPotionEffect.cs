using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IHealthPotionEffect {
  int id { get; }
  void visit(IHealthPotionEffectVisitor visitor);
}
       
}
