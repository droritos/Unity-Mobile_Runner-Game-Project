using System;
using UnityEngine;

public class PauseByMenuState : MonoBehaviour
{
   private void OnEnable()
   {
      PauseManager.Instance.SetPaused(true);
   }

   private void OnDisable()
   {
      PauseManager.Instance.SetPaused(false);
   }
}
