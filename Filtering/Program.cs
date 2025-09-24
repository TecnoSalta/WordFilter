﻿using Counter;

Console.WriteLine("Hello, World!");

var matrix = new[]
{
    "abcdc",
    "fgwio",
    "chill",
    "pqnsd",
    "uvdxy"
};

var wordFinder = new WordFinder(matrix);

Console.WriteLine("Matrix read successfully and preprocessed.");

// The next step would be to call wordFinder.Find(wordStream)
// and print the results.
