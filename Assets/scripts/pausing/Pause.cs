using UnityEngine;

public static class Pause
{
    public static bool IsPaused { get; private set; }//this pauses everything (for the escape screen)
    public static bool IsWorldPaused { get; private set; }//this pauses things if you're in a menu
    public static bool IsAIPaused { get; private set; }//this pauses things like enemy ai and npc ai if you're in dialog for example
    public static bool PauseSFX { get; private set; }//this pauses things like characters mumbling and explosions
    public static bool PauseMusic { get; private set; }//this pauses things like music, boss music and stuff
    //special care needs to fade the music out and back in

    //many pauses 
    //pause sfx , pause music 
    public static void SetGlobalPause(bool value) {
        IsPaused = value;
    }
}
