namespace MyAtariCollection.Models;

/// <summary>
/// Wakeup State for MMU/GLUE (x=ws1/ws2/ws3/ws4/random, default ws3). When powering on, the STF will randomly
/// choose one of these wake up states. The state will then affect the timings where border removals and other
/// video tricks should be made, which can give different results on screen. For example, WS3 is known to be
/// compatible with many demos, while WS1 can show more problems.
/// </summary>
public enum VideoTiming
{
    /// <summary>
    /// Pick a randon wake state
    /// </summary>
    Random,
    
    /// <summary>
    /// Wake state 1
    /// </summary>
    One,
    
    /// <summary>
    /// Wake state 2, this is the default.
    /// </summary>
    Two,
    
    /// <summary>
    /// Wake state 3
    /// </summary>
    Three,
    
    /// <summary>
    /// Wake state 4
    /// </summary>
    Four
}