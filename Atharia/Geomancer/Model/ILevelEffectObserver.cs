using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public interface ILevelEffectObserver {
  void OnLevelEffect(ILevelEffect effect);
}

}
