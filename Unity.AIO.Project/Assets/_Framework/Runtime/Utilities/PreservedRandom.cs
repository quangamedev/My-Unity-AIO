using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public static class PreservedRandom
{
    public static readonly Dictionary<int, Random.State> PreservedStatesBySeeds = new Dictionary<int, Random.State>();
    private static Random.State s_uncontrolledState;
    private static int? s_currentControlledStateSeed;

    public static void EnterPreservedState(int seed)
    {
        if (s_currentControlledStateSeed != null)
        {
            throw new AlreadyInControlledStateException();
        }

        s_currentControlledStateSeed = seed;
        s_uncontrolledState = Random.state;

        if (!PreservedStatesBySeeds.ContainsKey(seed))
        {
            Random.InitState(seed);
            PreservedStatesBySeeds.Add(seed, default);
            return;
        }

        Random.state = PreservedStatesBySeeds[seed];
    }

    public static void ExitPreservedState(int seed)
    {
        if (s_currentControlledStateSeed == null)
        {
            throw new NotInAnyControlledStateException();
        }

        if (s_currentControlledStateSeed != seed)
        {
            throw new InDifferentControlledStateException();
        }

        PreservedStatesBySeeds[seed] = Random.state;
        s_currentControlledStateSeed = null;
        Random.state = s_uncontrolledState;
    }

    private class AlreadyInControlledStateException : Exception { }

    private class NotInAnyControlledStateException : Exception { }

    private class InDifferentControlledStateException : Exception { }
}