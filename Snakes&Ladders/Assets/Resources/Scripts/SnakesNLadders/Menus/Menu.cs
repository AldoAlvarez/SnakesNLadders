using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AGAC.SnakesLadders.Menus
{
    [System.Serializable]
    public class Menu 
    {
        public Menu(uint totalElements) 
        {
            Elements = new Text[totalElements];
        }

        #region VARIABLES
        [SerializeField]
        private GameObject Panel;
        [SerializeField]
        private Text[] Elements;
        #endregion

        #region PUBLIC METHODS
        public void Show() { SetVisibility(true); }
        public void Hide() { SetVisibility(false); }

        public void UpdateElement(uint index, string text, Color color) 
        {
            if (index >= Elements.Length) return;
            Elements[index].color = color;
            Elements[index].text = text;
        }
        #endregion

        #region PRIVATE METHODS
        private void SetVisibility(bool state) 
        {
            if (Panel == null) return;
            Panel.SetActive(state);
        }
        #endregion
    }
}