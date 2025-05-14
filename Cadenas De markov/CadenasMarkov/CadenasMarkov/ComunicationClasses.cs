using System;
using System.Collections.Generic;
using System.Linq;

class CommunicationClasses
{
    public static List<HashSet<string>> FindCommunicationClasses(Dictionary<string, Dictionary<string, double>> transitionMatrix)
    {
        var visited = new HashSet<string>();
        var classes = new List<HashSet<string>>();

        foreach (var state in transitionMatrix.Keys)
        {
            if (!visited.Contains(state))
            {
                var reachableFromState = GetReachableStates(state, transitionMatrix);
                var statesThatReach = GetStatesThatReach(state, transitionMatrix);

                var communicationClass = new HashSet<string>(reachableFromState.Intersect(statesThatReach));

                if (communicationClass.Count > 0)
                {
                    classes.Add(communicationClass);
                    visited.UnionWith(communicationClass);
                }
            }
        }

        return classes;
    }

    private static HashSet<string> GetReachableStates(string start, Dictionary<string, Dictionary<string, double>> matrix)
    {
        var reachable = new HashSet<string>();
        var stack = new Stack<string>();
        stack.Push(start);

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            if (!reachable.Contains(current))
            {
                reachable.Add(current);

                if (matrix.ContainsKey(current))
                {
                    foreach (var next in matrix[current].Where(kv => kv.Value > 0).Select(kv => kv.Key))
                    {
                        stack.Push(next);
                    }
                }
            }
        }

        return reachable;
    }

    private static HashSet<string> GetStatesThatReach(string target, Dictionary<string, Dictionary<string, double>> matrix)
    {
        var reachable = new HashSet<string>();
        var stack = new Stack<string>();

        // Buscar todos los estados que tienen transición hacia target
        foreach (var state in matrix.Keys)
        {
            if (matrix[state].ContainsKey(target) && matrix[state][target] > 0)
                stack.Push(state);
        }

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            if (!reachable.Contains(current))
            {
                reachable.Add(current);

                // Buscar quién tiene transición hacia current
                foreach (var state in matrix.Keys)
                {
                    if (matrix[state].ContainsKey(current) && matrix[state][current] > 0)
                        stack.Push(state);
                }
            }
        }

        return reachable;
    }
}
