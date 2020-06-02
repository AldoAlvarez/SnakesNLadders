using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AGAC.SnakesLadders.Menus;
using AGAC.General;

namespace AGAC.SnakesLadders
{
    [DisallowMultipleComponent]
    public class MenusController : MonoBehaviour
    {
        public MenusController() 
        {
            Initialize();
        }

        #region VARIABLES
        private static MenusController instance;
        public static MenusController Instance 
        {
            get 
            {
                SetInstance();
                return instance;
            }
        }
        public Menu this[uint menu] 
        {
            get
            {
                if (menu >= menus.Length) 
                    return null;
                return menus[menu];
            }
        }

        [SerializeField]
        private Menu[] menus;
        #endregion

        #region PUBLIC METHODS
        public void Show(GameMenus menu) 
        {
            int menuIndex = (int)menu;
            if (menuIndex < 0 || menuIndex >= menus.Length) return;
            if (menus[menuIndex] == null) return;
            HideAll();
            menus[menuIndex].Show();
        }
        #endregion

        #region PRIVATE METHODS
        private void HideAll() 
        {
            foreach (Menu menu in menus)
                if(menu!=null)
                    menu.Hide();
        }
        private void Initialize() 
        {
            if (menus != null) return;
            int totalMenus = (int)GameMenus._counter;
            menus = new Menu[totalMenus];
            for (uint menu = 0; menu < totalMenus; menu++)
            {
                uint elements = GetElementsFor(menu);
                menus[menu] = new Menu(elements);
            }
        }
        private uint GetElementsFor(uint menu) 
        {
            switch ((GameMenus)menu) 
            {
                case GameMenus.MAIN_MENU:           return (uint)MainMenuElements._counter;
                case GameMenus.PLAYER_SELECTION:    return (uint)PlayerSelectionElements._counter;
                case GameMenus.IN_GAME:             return (uint)InGameMenuElements._counter;
                case GameMenus.AFTER_MATCH:         return (uint)AfterMatchElements._counter;
                default:return 0;
            }
        }

        private static void SetInstance() 
        {
            if (instance != null) return;
            instance = GeneralMethods.GetInstance<MenusController>("Menus Controller");
        }
        #endregion
    }
}