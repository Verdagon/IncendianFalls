using Atharia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace AthPlayer {
  class OverlayPresenter : IGameEffectObserver, IGameEffectVisitor {
    ISuperstructure ss;
    Game game;
    OverlayPanelView overlayPanelView;
    float openTimeS;

    public OverlayPresenter(
        ISuperstructure ss,
        Game game,
        OverlayPanelView overlayPanelView) {
      this.ss = ss;
      this.game = game;
      this.overlayPanelView = overlayPanelView;

      game.AddObserver(this);
    }

    public void OnGameEffect(IGameEffect effect) {
      effect.visit(this);
    }

    public void Update() {
      if (overlayPanelView.gameObject.activeSelf) {
        var overlay = game.overlay;
        var automaticDismissDelayS = overlay.automaticDismissDelayMs / 1000f;
        if (automaticDismissDelayS != 0) {
          var timeSinceOpenS = Time.time - openTimeS;
          var fadeInS = overlay.fadeInMs / 1000f;
          var fadeOutS = overlay.fadeInMs / 1000f;
          var fadeOutStartS = automaticDismissDelayS - fadeOutS;
          if (timeSinceOpenS < fadeInS) {
            var ratio = timeSinceOpenS / fadeInS;
            SetFadeRatio(ratio);
          } else if (timeSinceOpenS < fadeOutStartS) {
            SetFadeRatio(1.0f);
          } else if (timeSinceOpenS < automaticDismissDelayS) {
            var ratio = (timeSinceOpenS - fadeOutStartS) / fadeOutS;
            SetFadeRatio(1.0f - ratio);
          } else {
            SetFadeRatio(0);
            overlayPanelView.gameObject.SetActive(false);
            Debug.LogError("requesting overlay action");
            ss.RequestOverlayAction(game.id, 0);
          }
        }
      }
    }

    public void visitGameCreateEffect(GameCreateEffect effect) { }
    public void visitGameDeleteEffect(GameDeleteEffect effect) { }
    public void visitGameSetPlayerEffect(GameSetPlayerEffect effect) { }
    public void visitGameSetTimeEffect(GameSetTimeEffect effect) { }
    public void visitGameSetLevelEffect(GameSetLevelEffect effect) { }
    public void visitGameSetOverlayEffect(GameSetOverlayEffect effect) {
      if (!overlayPanelView.gameObject.activeSelf && game.overlay.Exists()) {
        this.openTimeS = Time.time;
        overlayPanelView.SetStuff(
          0,
          game.overlay.sizePercent / 100f,
          game.overlay.backgroundColor.ToUnity(),
          game.overlay.overlayText,
          game.overlay.overlayTextColor.ToUnity(),
          game.overlay.topAligned,
          game.overlay.leftAligned);
        overlayPanelView.gameObject.SetActive(true);
      } else if (overlayPanelView.gameObject.activeSelf && !game.overlay.Exists()) {
        overlayPanelView.gameObject.SetActive(false);
      }
    }

    private void SetFadeRatio(float ratio) {
      var overlay = game.overlay;
      overlayPanelView.SetStuff(
        ratio,
        overlay.sizePercent / 100f,
        overlay.backgroundColor.ToUnity(),
        overlay.overlayText,
        overlay.overlayTextColor.ToUnity(),
        overlay.topAligned,
        overlay.leftAligned);
    }
  }
}
