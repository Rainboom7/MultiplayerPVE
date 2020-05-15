using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core
{
    public interface IController<in Tview>
        where Tview : IView
    {
        void OnOpen(Tview view);
        void OnClose(Tview view);
    }
}
