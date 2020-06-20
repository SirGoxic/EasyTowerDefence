using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefence.Player
{
    public class PlayerInput : MonoBehaviour
    {

        [SerializeField]
        private UI.UIController uiController = null;

        private Tower.Tower lastSelectedTower = null;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    RaycastHit hit;
                    Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, 10000f))
                    {

                        Tower.Tower selectedTower;
                        if ((selectedTower = hit.collider.GetComponent<Tower.Tower>()) != null)
                        {
                            uiController.VisibilityLevelUpPanel(true);
                            uiController.ChangeTowerInfo(selectedTower);

                            if (lastSelectedTower != null)
                            {
                                lastSelectedTower.SelectorActive(false);
                            }
                            lastSelectedTower = selectedTower;

                            selectedTower.SelectorActive(true);
                        }
                        else
                        {
                            if (lastSelectedTower != null)
                            {
                                lastSelectedTower.SelectorActive(false);
                                lastSelectedTower = null;
                            }
                            uiController.VisibilityLevelUpPanel(false);
                        }
                    }
                }
            }
        }

    }
}
