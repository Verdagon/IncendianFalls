using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BlazeRodIncarnation : IBlazeRodEffectVisitor {
  public BlazeRodIncarnation(
) {
  }
  public BlazeRodIncarnation Copy() {
    return new BlazeRodIncarnation(
    );
  }

  public void visitBlazeRodCreateEffect(BlazeRodCreateEffect e) {}
  public void visitBlazeRodDeleteEffect(BlazeRodDeleteEffect e) {}

  public void ApplyEffect(IBlazeRodEffect effect) { effect.visitIBlazeRodEffect(this); }
}

}
