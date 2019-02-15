using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface ILevelEffectObserver {
  void OnLevelEffect(ILevelEffect effect);
}

}
