﻿using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using static Stringier.Metrics;

namespace Benchmarks.Algorithms {
	[SimpleJob(RuntimeMoniker.NetCoreApp30)]
	[SimpleJob(RuntimeMoniker.CoreRt30)]
	[SimpleJob(RuntimeMoniker.Mono)]
	[MemoryDiagnoser]
	public class FixedEditDistanceBenchmarks {
		[Params("ram")]
		public String Source { get; set; }

		[Params("ram", "rom", "rob", "bob")]
		public String Other { get; set; }

		[Benchmark]
		public Int32 Hamming() => HammingDistance(Source, Other);

		[Benchmark]
		public Int32 Levenshtein() => LevenshteinDistance(Source, Other);
	}
}
