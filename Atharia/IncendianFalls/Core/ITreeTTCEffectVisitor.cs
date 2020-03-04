using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITreeTTCEffectVisitor {
  void visitTreeTTCCreateEffect(TreeTTCCreateEffect effect);
  void visitTreeTTCDeleteEffect(TreeTTCDeleteEffect effect);
}

}
