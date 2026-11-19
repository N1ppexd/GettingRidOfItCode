using System;
using UnityEngine;

namespace GettingRidOfIt
{
    public static class GameEvents
    {

        public static Action<Vector3> OnPlayerTeleport;
        public static Action<bool> OnPlayerDrag;

        public static Action OnPauseGame;
        public static Action OnUnPauseGame;

        public static Action<int> OnTimeSkipped;

        public static Action OnStartCooking;
        public static Action OnStopCooking;

        public static Action OnBringTrash;

        public static Action OnHourPassed;

        public static Action OnPlayerLost;
        public static Action OnPlayerWin;

        public static Action<InteractData> OnActionPerformed;

        //when player teleports
        public static void PlayerTeleport(Vector3 pos) => OnPlayerTeleport?.Invoke(pos);
        //when player drags the body
        public static void PlayerDrag(bool drag) => OnPlayerDrag?.Invoke(drag);

        //when the player does an action that has some time, this happens
        public static void SkipTime(int minutes) => OnTimeSkipped?.Invoke(minutes);


        //pause game event
        public static void PauseGame() => OnPauseGame?.Invoke();
        //unpause game event
        public static void UnPauseGame() => OnUnPauseGame?.Invoke();


        public static void StartCooking() => OnStartCooking?.Invoke();
        public static void StopCooking() => OnStopCooking?.Invoke();


        //win and lose
        public static void LoseGame() => OnPlayerLost?.Invoke();
        public static void WinGame() => OnPlayerWin?.Invoke();

        public static void PerformAction(InteractData interactData) => OnActionPerformed?.Invoke(interactData);

        public static void BringTrash() => OnBringTrash?.Invoke();

        public static void HourPassed() => OnHourPassed?.Invoke();
    }
}
