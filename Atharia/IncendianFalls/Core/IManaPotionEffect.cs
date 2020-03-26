using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IManaPotionEffect : IEffect {
  int id { get; }
  void visitIManaPotionEffect(IManaPotionEffectVisitor visitor);
}
       
}
