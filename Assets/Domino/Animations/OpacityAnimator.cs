using System.Collections;
using System.Collections.Generic;
using Domino;
using UnityEngine;

public class OpacityAnimator : MonoBehaviour {
  public RenderPriority renderPriority;

  IFloatAnimation opacityAnimation_ = new IdentityFloatAnimation();
  public IFloatAnimation opacityAnimation {
    get { return opacityAnimation_; }
    set {
      opacityAnimation_ = value;
      Update();
    }
  }

  void Update() {
    opacityAnimation_ = opacityAnimation_.Simplify(Time.time);

    var colorChanger = GetComponent<ColorChanger>();
    var color = colorChanger.GetColor();
    color.a = opacityAnimation_.Get(Time.time);
    colorChanger.SetColor(color, renderPriority);

    if (opacityAnimation_ is ConstantFloatAnimation || opacityAnimation_ is IdentityFloatAnimation) {
      Destroy(this);
    }
  }
}
