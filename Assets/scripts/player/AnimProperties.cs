using UnityEditor;
using UnityEngine;

public static class AnimProperties
{
    public const int locked          = 0;
    public const int looping         = 1;
    public const int oneShot         = 2;
    public const int polite          = 3;
    public const int kill            = 4;
    public const int cutin           = 5;
    public const int fixedtime       = 6;
    public const int scriptable      = 7;
    public const int layable         = 8;
    public const int interlude       = 9;
    public const int mutable         = 10;
    public const int grounded        = 11;

    //    locked - cannot exit using regular interrupt
    //looping - this loops
    //one shot - runs and exits normally
    //polite - cannot interrupt
    //kill - does not allow interlude
    //cutin - does this animation allow dynamic cut ins and outs?
    //fixedtime - this animation has a fixed rate of execution and always shows x frame at x time
    //scriptable - this animation has scriptable component, and can change variables, execute functions and more
    //layable - this animation can play in the back, to be used in conjunction with scriptable
    //interlude - this animation can be paused by an interrupt, and resumes later
    //mutable - this animation has a sound component to be muted whenever placed in the background


}
