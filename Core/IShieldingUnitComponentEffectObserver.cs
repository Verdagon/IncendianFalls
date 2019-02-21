using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IShieldingUnitComponentEffectObserver {
  void OnShieldingUnitComponentEffect(IShieldingUnitComponentEffect effect);
}

}
