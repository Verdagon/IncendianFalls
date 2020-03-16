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

      //overlayPanelView.OverlayClosed += OverlayClosed;
    }

    public void ShowOverlay(ShowOverlayEvent overlay) {

      // ignore this, the presenter should determine it, perhaps based on some sort of
      // "this is cinematic and dramatic" vs "this is in-game" thing.
      //public readonly int fadeInEndMs;

      // ignore this, we should determine this in the presenter, based on semantic
      // information.
      //public readonly Color backgroundColor;

      // ignore this, determine this in the presenter based on semantic info.
      //public readonly int sizePercent;

      //public readonly string text;
      // ignore this, determine this in the presenter based on semantic info.
      //public readonly int textFadeOutEndMs;
      //public readonly int textFadeOutStartMs;
      //public readonly int textFadeInEndMs;
      //public readonly int textFadeInStartMs;
      // ignore this, determine this in the presenter based on semantic info.
      //public readonly Color textColor;
      //public readonly bool leftAligned;
      //public readonly bool topAligned;



      var font = new OverlayFont("cascadia", 1.75f);
      int panelWidth = 30;
      int maxHeight = 15;
      string[] textLines = LineWrapper.Wrap(overlay.text, panelWidth - 1);

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
      //int ellipsisWidth = 3;
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

      var panelView =
        overlayPaneler.MakePanel(
          cinematicTimer,
          50,
          100,
          100,
          50,
          panelWidth,
          panelHeight);
      //panelView.AddBackgroundAndBorder(new UnityEngine.Color(0, 0, 0, .9f), new UnityEngine.Color(0f, .25f, .5f, 1));
      int backgroundId = panelView.AddBackground(new UnityEngine.Color(0, 0, 0, .9f));
      panelView.SetFadeIn(backgroundId, new NewOverlayPanelView.FadeIn(0, 300));
      panelView.SetFadeOut(backgroundId, new NewOverlayPanelView.FadeOut(-300, 0));

      for (int i = 0; i < panelView.symbolsHigh && i < adjustedTextLines.Count; i++) {
        var textIds =
          panelView.AddString(
            0, 1f, panelView.symbolsHigh - 2 - i, panelView.symbolsWide,
            new UnityEngine.Color(0, .5f, 1, 1), font,
            adjustedTextLines[i]);
        foreach (var textId in textIds) {
          panelView.SetFadeIn(textId, new NewOverlayPanelView.FadeIn(300, 600));
          panelView.SetFadeOut(textId, new NewOverlayPanelView.FadeOut(-600, -300));
        }
      }

      foreach (var button in overlay.buttons) {
        var buttonTrigger = button.triggerName;
        var buttonText = button.label;
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
            new UnityEngine.Color(.3f, .3f, .3f, 1),
            () => {
              panelView.ScheduleClose(() => {
                timer.SetTimeSpeedMultiplier(1f);
                if (buttonTrigger == "_exitGame") {
                  Exit?.Invoke();
                } else {
                  ss.RequestTrigger(game.id, buttonTrigger);
                }
              });
            });
        panelView.SetFadeIn(buttonId, new NewOverlayPanelView.FadeIn(0, 300));
        panelView.SetFadeOut(buttonId, new NewOverlayPanelView.FadeOut(-300, 0));
        panelView.AddString(
          buttonId,
          panelWidth - rightBorderWidth - buttonWidth + buttonBorderWidth,
          2f,
          buttonTextWidth,
          new UnityEngine.Color(1f, 1f, 1f, 1),
            font, buttonText);
      }

      // ignore these. we shouldnt have automatic hiding.
      //public readonly string automaticActionTriggerName;
      //public readonly int fadeOutEndMs;
      //public readonly int fadeOutStartMs;

      timer.SetTimeSpeedMultiplier(0f);
    }

    //private void OverlayClosed(int buttonIndex) {
    //  timer.SetTimeSpeedMultiplier(1f);
    //  var triggerName = triggerNames[buttonIndex];
    //  if (triggerName == "_exitGame") {
    //    Exit?.Invoke();
    //  } else {
    //    ss.RequestTrigger(game.id, triggerName);
    //  }
    //}

    public void ShowTopOverlayWithButtons(string unwrapped) {
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
      //int ellipsisWidth = 3;
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

      var panelView = overlayPaneler.MakePanel(cinematicTimer, 50, 100, 100, 50, panelWidth, panelHeight);
      //panelView.AddBackgroundAndBorder(new UnityEngine.Color(0, 0, 0, .9f), new UnityEngine.Color(0f, .25f, .5f, 1));
      int backgroundId = panelView.AddBackground(new UnityEngine.Color(0, 0, 0, .9f));
      panelView.SetFadeIn(backgroundId, new NewOverlayPanelView.FadeIn(0, 300));
      panelView.SetFadeOut(backgroundId, new NewOverlayPanelView.FadeOut(-300, 0));

      for (int i = 0; i < panelView.symbolsHigh && i < adjustedTextLines.Count; i++) {
        var textIds =
          panelView.AddString(
            0, 1f, panelView.symbolsHigh - 2 - i, panelView.symbolsWide,
            new UnityEngine.Color(0, .5f, 1, 1), font,
            adjustedTextLines[i]);
        foreach (var textId in textIds) {
          panelView.SetFadeIn(textId, new NewOverlayPanelView.FadeIn(300, 600));
          panelView.SetFadeOut(textId, new NewOverlayPanelView.FadeOut(-600, -300));
        }
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
          new UnityEngine.Color(.3f, .3f, .3f, 1),
          () => {
            Debug.Log("Got button click!");
            panelView.ScheduleClose(() => {
              Debug.Log("Got close!");
            });
          });
      panelView.SetFadeIn(buttonId, new NewOverlayPanelView.FadeIn(0, 300));
      panelView.SetFadeOut(buttonId, new NewOverlayPanelView.FadeOut(-300, 0));
      panelView.AddString(
        buttonId,
        panelWidth - rightBorderWidth - buttonWidth + buttonBorderWidth,
        2f,
        buttonTextWidth,
        new UnityEngine.Color(1f, 1f, 1f, 1),
          font, buttonText);
    }
  }
}
