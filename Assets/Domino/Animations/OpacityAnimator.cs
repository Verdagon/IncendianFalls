using System.Collections;
using System.Collections.Generic;
using Domino;
using UnityEngine;

public class OpacityAnimator : MonoBehaviour {
  private IClock clock;
  private RenderPriority renderPriority;

  public void Init(IClock clock, RenderPriority renderPriority) {
    this.clock = clock;
    this.renderPriority = renderPriority;
  }

  IFloatAnimation opacityAnimation_ = new IdentityFloatAnimation();
  public IFloatAnimation opacityAnimation {
    get { return opacityAnimation_; }
    set {
      opacityAnimation_ = value;
      Update();
    }
  }

  void Update() {
    opacityAnimation_ = opacityAnimation_.Simplify(clock.GetTimeMs());

    var colorChanger = GetComponent<ColorChanger>();
    var color = colorChanger.GetColor();
    color.a = opacityAnimation_.Get(clock.GetTimeMs());
    colorChanger.SetColor(color, renderPriority);

    if (opacityAnimation_ is ConstantFloatAnimation || opacityAnimation_ is IdentityFloatAnimation) {
      Destroy(this);
    }
  }
}
