using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IManaPotionEffectObserver {
  void OnManaPotionEffect(IManaPotionEffect effect);
}

}
