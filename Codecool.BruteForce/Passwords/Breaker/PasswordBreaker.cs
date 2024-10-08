﻿using Codecool.BruteForce.Passwords.Generator;
using Codecool.BruteForce.Passwords.Model;
using System;
using System.Text;

namespace Codecool.BruteForce.Passwords.Breaker;

public class PasswordBreaker : IPasswordBreaker
{
    private readonly AsciiTableRange[] _characterSets;

    public PasswordBreaker(params AsciiTableRange[] characterSets)
    {
        _characterSets = characterSets;
    }

    public IEnumerable<string> GetCombinations(int passwordLength)
    {
        List<string> combinations = new List<string>();

        GenerateCombinationsRecursive(combinations, "", passwordLength);

        return combinations;
    }

    private void GenerateCombinationsRecursive(List<string> combinations, string prefix, int length)
    {
        if (length == 0)
        {
            combinations.Add(prefix);
            return;
        }

        foreach (var characterSet in _characterSets)
        {
            for (int i = characterSet.Start; i <= characterSet.End; i++)
            {
                char character = (char)i;
                GenerateCombinationsRecursive(combinations, prefix + character, length - 1);
            }
        }
    }

}


