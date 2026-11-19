using UnityEngine;

namespace GettingRidOfIt
{
    public class ScreenSettings : MonoBehaviour
    {


        //change the screen resolution based on the input from dropdown menu
        public void Resolution(int id)
        {
            switch (id)
            {
                case 1:
                    //4k
                    Screen.SetResolution(3840, 2160, true);
                    break;
                case 2:
                    Screen.SetResolution(2560, 1440, true);
                    break;
                case 3:
                    Screen.SetResolution(1920, 1080, true);
                    break;
                case 4:
                    Screen.SetResolution(1280, 720, true);
                    break;
                case 5:
                    Screen.SetResolution(1024, 768, true);
                    break;
                case 6:
                    Screen.SetResolution(800, 600, true);
                    break;
                case 7:
                    Screen.SetResolution(640, 480, true);
                    break;
            }
        }


        public void ScreenMode(int mode)
        {
            switch (mode)
            {
                case 0:
                    Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                    return;
                case 1:
                        Screen.fullScreenMode = FullScreenMode.Windowed;
                    return;
                case 2:
                    Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                    return;
            }
        }
    }
}
