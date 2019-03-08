using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IManaPotionEffect {
  int id { get; }
  void visit(IManaPotionEffectVisitor visitor);
}
       
}
