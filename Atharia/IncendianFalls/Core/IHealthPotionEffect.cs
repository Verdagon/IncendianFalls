using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IHealthPotionEffect : IEffect {
  int id { get; }
  void visitIHealthPotionEffect(IHealthPotionEffectVisitor visitor);
}
       
}
