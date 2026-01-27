using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bas.Pennings.UnityTools
{
    public struct ModalWindowData
    {
        public ModalWindowContent Content;
        public ModalWindowContentLayout Layout;
        public ModalButtonData ConfirmBtnData;
        public ModalButtonData DeclineBtnData;
        public List<ModalButtonData> AlternateButtonsData;
    }

    public struct ModalWindowContent
    {
        public string Title;
        public string Message;
        public Sprite Sprite;
    }

    public class ModalButtonData
    {
        public string LabelText;
        public Action CallBack;

        public ModalButtonData(string labelText, Action callback)
        {
            LabelText = labelText;
            CallBack = callback;
        }

        public bool IsValid => !string.IsNullOrEmpty(LabelText);
    }

    public enum ModalWindowContentLayout
    {
        Horizontal,
        Vertical
    }
}
