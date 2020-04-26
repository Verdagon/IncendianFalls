using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IUpStairsTTCEffectVisitor {
  void visitUpStairsTTCCreateEffect(UpStairsTTCCreateEffect effect);
  void visitUpStairsTTCDeleteEffect(UpStairsTTCDeleteEffect effect);
}

}
