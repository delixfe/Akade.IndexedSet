﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Akade.IndexedSet.Tests.Samples.TypeaheadSample;

[TestClass]
public class TypeaheadSample
{
    private readonly IndexedSet<Type> _types;

    public TypeaheadSample()
    {
        _types = typeof(string).Assembly.GetTypes()
                                        .ToIndexedSet()
                                        .WithPrefixIndex(x => x.Name.ToLowerInvariant().AsMemory())
                                        .Build();
    }

    [TestMethod]
    public void Case_insensitve_lookahead_in_all_types_within_system_runtime()
    {
        // Travers the prefix trie to efficiently find all matches
        Type[] types = _types.StartsWith(x => x.Name.ToLowerInvariant().AsMemory(), "int".AsMemory()).ToArray();

        Assert.IsTrue(types.Any());
        Assert.IsTrue(types.All(t => t.Name.StartsWith("int", StringComparison.InvariantCultureIgnoreCase)));
    }
}
