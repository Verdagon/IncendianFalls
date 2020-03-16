using Atharia.Model;
using Domino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AthPlayer {
  public delegate void OnExit();
  public class OldOverlayPresenter {
    public event OnExit Exit;

    SlowableTimerClock timer;
    SlowableTimerClock cinematicTimer;
    ISuperstructure ss;
    Game game;
    OverlayPaneler overlayPaneler;
    OldOverlayPanelView overlayPanelView;

    private List<string> triggerNames = new List<string>();

    public OldOverlayPresenter(
      SlowableTimerClock timer,
        SlowableTimerClock cinematicTimer,
        ISuperstructure ss,
        Game game,
        OverlayPaneler overlayPaneler,
        OldOverlayPanelView overlayPanelView) {
      this.timer = timer;
      this.cinematicTimer = cinematicTimer;
      this.ss = ss;
      this.game = game;
      this.overlayPaneler = overlayPaneler;
      this.overlayPanelView = overlayPanelView;

      overlayPanelView.OverlayClosed += OverlayClosed;
    }

    public void ShowOverlay(ShowOverlayEvent overlay) {
      triggerNames = new List<string>();
      var buttonTexts = new List<string>();
      foreach (var button in overlay.buttons) {
        buttonTexts.Add(button.label);
        triggerNames.Add(button.triggerName);
      }
      triggerNames.Add(overlay.automaticActionTriggerName);

      overlayPanelView.Init(
        cinematicTimer,

        overlay.sizePercent / 100f,
        overlay.backgroundColor.ToUnity(),
        overlay.fadeInEndMs,
        overlay.fadeOutStartMs,
        overlay.fadeOutEndMs,

        overlay.text,
        overlay.textColor.ToUnity(),
        overlay.textFadeInStartMs,
        overlay.textFadeInEndMs,
        overlay.textFadeOutStartMs,
        overlay.textFadeOutEndMs,
        overlay.topAligned,
        overlay.leftAligned,

        buttonTexts);

      //overlayPanelView.Init(
      //  .55f, // game.overlay.sizePercent / 100f,
      //  new UnityEngine.Color(.5f, 0, 0, .9f), //game.overlay.backgroundColor.ToUnity(),
      //  "What is the meaning of life?", // game.overlay.overlayText,
      //  new UnityEngine.Color(1, 1, 1, 1), // game.overlay.overlayTextColor.ToUnity(),
      //  game.overlay.topAligned,
      //  game.overlay.leftAligned,
      //  0,//game.overlay.automaticDismissDelayMs,
      //  50,//game.overlay.fadeInMs,
      //  50,//game.overlay.fadeOutMs,
      //  new List<string>() { "butts", "more butts" });//buttonTexts);

      //overlayPanelView.Init(
      //  1f, // game.overlay.sizePercent / 100f,
      //  new UnityEngine.Color(0, 0, 0, .9f), //game.overlay.backgroundColor.ToUnity(),
      //  1f, // fadeInEnd
      //  5f, // fadeOutStart
      //  6f, // fadeOutEnd,

      //  "My brother was an explorer...", // game.overlay.overlayText,
      //  new UnityEngine.Color(0, 1, 1, 1), // game.overlay.overlayTextColor.ToUnity(),
      //  1f, // textFadeInStartS
      //  2f, // textFadeInEndS
      //  4f, // textFadeOutStartS
      //  5f, // textFadeOutEndS
      //  true,
      //  false,
      //  new List<string>());//buttonTexts);

      timer.SetTimeSpeedMultiplier(0f);
    }

    private void OverlayClosed(int buttonIndex) {
      Debug.Log("Clicked " + buttonIndex);
      timer.SetTimeSpeedMultiplier(1f);
      var triggerName = triggerNames[buttonIndex];
      if (triggerName == "_exitGame") {
        Exit?.Invoke();
      } else {
        ss.RequestTrigger(game.id, triggerName);
      }
    }

    public void ShowTopOverlayWithConfirmButton(string unwrapped) {
      var font = new OverlayFont("cascadia", 1.75f);
      int panelWidth = 30;
      int maxHeight = 15;
      string[] textLines = LineWrapper.Wrap(unwrapped, panelWidth - 1);

      // - 1 line for borders
      // - as many lines for text as we can fit
      // - 1 line for space between text and buttons
      // - 2 lines for buttons
      int numLinesForTopBorder = 1;
      int numLinesForBottomBorder = 1;
      int numLinesBetweenTextAndButtons = 1;
      int rightBorderWidth = 1;
      int numLinesForButtons = 3; // 1 for top border, 1 for the text, 1 for bottom border.
      int numLinesForText = maxHeight - numLinesForTopBorder - numLinesBetweenTextAndButtons - numLinesForButtons - numLinesForBottomBorder;
      int ellipsisWidth = 3;
      Debug.LogError(numLinesForText);

      //bool wrapped;
      var adjustedTextLines = new List<string>();
      if (textLines.Length <= numLinesForText) {
        adjustedTextLines.AddRange(textLines);
        //wrapped = false;
      } else {
        for (int i = 0; i < numLinesForText; i++) {
          adjustedTextLines.Add(textLines[i]);
        }
        //wrapped = true;
      }

      int panelHeight = adjustedTextLines.Count + numLinesForTopBorder + numLinesBetweenTextAndButtons + numLinesForButtons + numLinesForBottomBorder;

      var panelView = overlayPaneler.MakePanel(50, 100, 100, 50, panelWidth, panelHeight);
      //panelView.AddBackgroundAndBorder(new UnityEngine.Color(0, 0, 0, .9f), new UnityEngine.Color(0f, .25f, .5f, 1));
      panelView.AddBackground(new UnityEngine.Color(0, 0, 0, .9f));

      for (int i = 0; i < panelView.symbolsHigh && i < adjustedTextLines.Count; i++) {
        panelView.AddString(
          0, 1f, panelView.symbolsHigh - 2 - i, panelView.symbolsWide,
          new UnityEngine.Color(0, .5f, 1, 1), font,
          adjustedTextLines[i]);
      }

      //if (wrapped) {
      //  panelView.AddString(
      //    0,
      //    panelWidth - ellipsisWidth - rightBorderWidth,
      //    numLinesForBottomBorder + numLinesForButtons + 0.4f,
      //    3,
      //    new UnityEngine.Color(1, 1, 1, 1), font,
      //    "...");
      //}

      var buttonText = "More";
      int buttonTextWidth = buttonText.Length;
      int buttonTextHeight = 1;
      int buttonBorderWidth = 1;
      int buttonWidth = buttonBorderWidth + buttonTextWidth + buttonBorderWidth;
      int buttonHeight = buttonBorderWidth + buttonTextHeight + buttonBorderWidth;

      int buttonId =
        panelView.AddButton(
          0,
          panelWidth - rightBorderWidth - buttonWidth,
          1,
          buttonWidth,
          buttonHeight,
          1,
          new UnityEngine.Color(.4f, .4f, .4f, 1),
          new UnityEngine.Color(.3f, .3f, .3f, 1));
      panelView.AddString(
        buttonId,
        panelWidth - rightBorderWidth - buttonWidth + buttonBorderWidth,
        2f,
        buttonTextWidth,
        new UnityEngine.Color(1f, 1f, 1f, 1),
          font, buttonText);

      panelView.Clicked += (id) => {
        if (id == buttonId) {
          Debug.LogError("clicked!");
        }
      };
    }
  }
}
