﻿using Atharia.Model;
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
    (int, int) GetPageTextMaxWidthAndHeight(bool isPortrait, List<OverlayPresenter.PageButton> buttons);

    void ShowPage(
        List<string> pageLines,
        List<OverlayPresenter.PageButton> buttons,
        bool isFirstPage,
        bool isLastPage,
        bool isPortrait);
  }
}
