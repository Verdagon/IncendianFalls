using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IUnitEffectObserver {
  void OnUnitEffect(IUnitEffect effect);
}

}
