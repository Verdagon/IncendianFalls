using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IOnFireTTCEffectVisitor {
  void visitOnFireTTCCreateEffect(OnFireTTCCreateEffect effect);
  void visitOnFireTTCDeleteEffect(OnFireTTCDeleteEffect effect);
  void visitOnFireTTCSetTurnsRemainingEffect(OnFireTTCSetTurnsRemainingEffect effect);
}

}
