﻿using System.Diagnostics.CodeAnalysis;

namespace System.Text.Patterns {
	[SuppressMessage("Microsoft.Analyzers", "CA1720", Justification = "This is an extension library, of course I'm naming the calling type the type itself")]
	public static class StringExtensions {
		/// <summary>
		/// Attempt to consume the <paramref name="Pattern"/> from the <paramref name="Candidate"/>
		/// </summary>
		/// <param name="Pattern">The <see cref="String"/> to match</param>
		/// <param name="Candidate">The <see cref="String"/> to consume</param>
		/// <returns>A <see cref="Result"/> containing whether a match occured and the remaining string</returns>
		public static Result Consume(this String Pattern, String Candidate) => Consume(Pattern, Candidate, out _);

		/// <summary>
		/// Attempt to consume the <paramref name="Pattern"/> from the <paramref name="Candidate"/>
		/// </summary>
		/// <param name="Pattern">The <see cref="String"/> to match</param>
		/// <param name="Candidate">The <see cref="String"/> to consume</param>
		/// <param name="Consumed">The <see cref="String"/> that was consumed, empty if not matched</param>
		/// <returns>A <see cref="Result"/> containing whether a match occured and the remaining string</returns>
		public static Result Consume(this String Pattern, String Candidate, out String Capture) {
			if (Pattern.Length > Candidate.Length) {
				Capture = "";
				return new Result(false, Candidate);
			}
			if (String.Equals(Pattern, Candidate.Substring(0, Pattern.Length), StringComparison.InvariantCulture)) {
				Capture = Candidate.Substring(0, Pattern.Length);
				return new Result(true, Candidate.Remove(0, Pattern.Length));
			} else {
				Capture = "";
				return new Result(false, Candidate);
			}
		}
	}
}