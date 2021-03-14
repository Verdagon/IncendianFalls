using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ILotusTTCMutSetEffectVisitor {
  void visitLotusTTCMutSetCreateEffect(LotusTTCMutSetCreateEffect effect);
  void visitLotusTTCMutSetDeleteEffect(LotusTTCMutSetDeleteEffect effect);
  void visitLotusTTCMutSetAddEffect(LotusTTCMutSetAddEffect effect);
  void visitLotusTTCMutSetRemoveEffect(LotusTTCMutSetRemoveEffect effect);
}
         
}
