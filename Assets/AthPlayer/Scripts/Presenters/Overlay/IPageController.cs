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
  public interface IPageController {
    // G = in grid units
    (int, int) GetPageTextMaxGWAndGH(
        int maxGW,
        int maxGH,
        List<OverlayPresenter.PageButton> buttons);

    OverlayPanelView ShowPage(
        List<string> pageLines,
        UnityEngine.Color textColor,
        List<OverlayPresenter.PageButton> buttons,
        bool fadeInBackground,
        bool fadeOutBackground,
        bool isPortrait,
        bool callCallbackAfterFadeOut);
  }
}
