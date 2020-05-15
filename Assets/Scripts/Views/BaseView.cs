using Core;
using UnityEngine;

namespace Views
{
    public abstract class BaseView : MonoBehaviour, IView
    {     
        public  void Open()
            
        {
         gameObject.SetActive(true);
        }

        public void Close()            
        {
            gameObject.SetActive(false);         
        }
    }
}