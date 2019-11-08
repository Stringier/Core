﻿using System;
using Stringier.Patterns.Nodes;

namespace Stringier.Patterns.Bindings {
	/// <summary>
	/// Holds useful definitions for creating bindings to <see cref="Pattern"/>.
	/// </summary>
	/// <remarks>
	/// The entire point of this is to make it easy to declare bindings to this library from another language which does not map directly, such as F#.
	/// </remarks>
	public static class PatternBindings {
		/// <summary>
		/// Create a <see cref="Pattern"/> from the given <paramref name="char"/>.
		/// </summary>
		/// <param name="char">The <see cref="Char"/> literal.</param>
		/// <returns>A <see cref="Pattern"/> representing literally the <paramref name="char"/>.</returns>
		public static Pattern Literal(Char @char) => new Pattern(new CharLiteral(@char));

		/// <summary>
		/// Create a <see cref="Pattern"/> from the given <paramref name="string"/>.
		/// </summary>
		/// <param name="string">The <see cref="String"/> literal.</param>
		/// <returns>A <see cref="Pattern"/> representing literally the <paramref name="string"/>.</returns>
		public static Pattern Literal(String @string) {
			if (@string is null) {
				throw new ArgumentNullException(nameof(@string));
			}
			return new Pattern(new StringLiteral(@string));
		}

		//public static Pattern Alternator(Pattern Left, Pattern Right) => new Pattern(Left.Head.Alternate(Right.Head));

		//public static Pattern Alternator(Pattern Left, String Right) {
		//	if (Right is null) {
		//		throw new ArgumentNullException(nameof(Right));
		//	}
		//	return new Pattern(Left.Head.Alternate(new StringLiteral(Right)));
		//}

		//public static Pattern Alternator(String Left, Pattern Right) {
		//	if (Left is null) {
		//		throw new ArgumentNullException(nameof(Left));
		//	}
		//	return new Pattern(new StringLiteral(Left).Alternate(Right.Head));
		//}

		//public static Pattern Alternator(Pattern Left, Char Right) => new Pattern(Left.Head.Alternate(new CharLiteral(Right)));

		//public static Pattern Alternator(Char Left, Pattern Right) => new Pattern(new CharLiteral(Left).Alternate(Right.Head));

		//public static Pattern Alternator(String Left, String Right) {
		//	if (Left is null) {
		//		throw new ArgumentNullException(nameof(Left));
		//	} else if (Right is null) {
		//		throw new ArgumentNullException(nameof(Right));
		//	}
		//	return new Pattern(new StringLiteral(Left).Alternate(new StringLiteral(Right)));
		//}

		//public static Pattern Alternator(String Left, Char Right) {
		//	if (Left is null) {
		//		throw new ArgumentNullException(nameof(Left));
		//	}
		//	return new Pattern(new StringLiteral(Left).Alternate(new CharLiteral(Right)));
		//}


		//public static Pattern Alternator(Char Left, String Right) {
		//	if (Right is null) {
		//		throw new ArgumentNullException(nameof(Right));
		//	}
		//	return new Pattern(new CharLiteral(Left).Alternate(new StringLiteral(Right)));
		//}

		//public static Pattern Alternator(Char Left, Char Right) => new Pattern(new CharLiteral(Left).Alternate(new CharLiteral(Right)));

		//public static Pattern Capturer(Pattern Pattern, out Capture Capture) => Pattern.Capture(out Capture);

		//public static Pattern Checker(String Name, Func<Char, Boolean> Check) => new Pattern(new CharChecker(Name, Check));

		/// <summary>
		/// Concatenates the patterns so that <paramref name="left"/> comes before <paramref name="right"/>
		/// </summary>
		/// <param name="left">The preceeding <see cref="Pattern"/></param>
		/// <param name="right">The succeeding <see cref="Pattern"/></param>
		/// <returns>A new <see cref="Pattern"/> concatenating <paramref name="left"/> and <paramref name="right"/></returns>
		public static Pattern Concatenator(Pattern left, Pattern right) => new Pattern(left.Head.Concatenate(right.Head));

		/// <summary>
		/// Concatenates the patterns so that <paramref name="left"/> comes before <paramref name="right"/>
		/// </summary>
		/// <param name="left">The preceeding <see cref="Pattern"/></param>
		/// <param name="right">The succeeding <see cref="String"/></param>
		/// <returns>A new <see cref="Pattern"/> concatenating <paramref name="left"/> and <paramref name="right"/></returns>
		public static Pattern Concatenator(Pattern left, String right) {
			if (right is null) {
				throw new ArgumentNullException(nameof(right));
			}
			return new Pattern(left.Head.Concatenate(new StringLiteral(right)));
		}

		/// <summary>
		/// Concatenates the patterns so that <paramref name="left"/> comes before <paramref name="right"/>
		/// </summary>
		/// <param name="left">The preceeding <see cref="String"/></param>
		/// <param name="right">The succeeding <see cref="Pattern"/></param>
		/// <returns>A new <see cref="Pattern"/> concatenating <paramref name="left"/> and <paramref name="right"/></returns>
		public static Pattern Concatenator(String left, Pattern right) {
			if (left is null) {
				throw new ArgumentNullException(nameof(left));
			}
			return new Pattern(new StringLiteral(left).Concatenate(right.Head));
		}

		/// <summary>
		/// Concatenates the patterns so that <paramref name="left"/> comes before <paramref name="right"/>
		/// </summary>
		/// <param name="left">The preceeding <see cref="Pattern"/></param>
		/// <param name="right">The succeeding <see cref="Char"/></param>
		/// <returns>A new <see cref="Pattern"/> concatenating <paramref name="left"/> and <paramref name="right"/></returns>
		public static Pattern Concatenator(Pattern left, Char right) => new Pattern(left.Head.Concatenate(new CharLiteral(right)));

		/// <summary>
		/// Concatenates the patterns so that <paramref name="left"/> comes before <paramref name="right"/>
		/// </summary>
		/// <param name="left">The preceeding <see cref="Char"/></param>
		/// <param name="right">The succeeding <see cref="Pattern"/></param>
		/// <returns>A new <see cref="Pattern"/> concatenating <paramref name="left"/> and <paramref name="right"/></returns>
		public static Pattern Concatenator(Char left, Pattern right) => new Pattern(new CharLiteral(left).Concatenate(right.Head));

		/// <summary>
		/// Concatenates the patterns so that <paramref name="left"/> comes before <paramref name="right"/>
		/// </summary>
		/// <param name="left">The preceeding <see cref="String"/></param>
		/// <param name="right">The succeeding <see cref="String"/></param>
		/// <returns>A new <see cref="Pattern"/> concatenating <paramref name="left"/> and <paramref name="right"/></returns>
		public static Pattern Concatenator(String left, String right) {
			if (left is null) {
				throw new ArgumentNullException(nameof(left));
			} else if (right is null) {
				throw new ArgumentNullException(nameof(right));
			}
			return new Pattern(new StringLiteral(left).Concatenate(new StringLiteral(right)));
		}

		/// <summary>
		/// Concatenates the patterns so that <paramref name="left"/> comes before <paramref name="right"/>
		/// </summary>
		/// <param name="left">The preceeding <see cref="String"/></param>
		/// <param name="right">The succeeding <see cref="Char"/></param>
		/// <returns>A new <see cref="Pattern"/> concatenating <paramref name="left"/> and <paramref name="right"/></returns>
		public static Pattern Concatenator(String left, Char right) {
			if (left is null) {
				throw new ArgumentNullException(nameof(left));
			}
			return new Pattern(new StringLiteral(left).Concatenate(new CharLiteral(right)));
		}

		/// <summary>
		/// Concatenates the patterns so that <paramref name="left"/> comes before <paramref name="right"/>
		/// </summary>
		/// <param name="left">The preceeding <see cref="Char"/></param>
		/// <param name="right">The succeeding <see cref="String"/></param>
		/// <returns>A new <see cref="Pattern"/> concatenating <paramref name="left"/> and <paramref name="right"/></returns>
		public static Pattern Concatenator(Char left, String right) {
			if (right is null) {
				throw new ArgumentNullException(nameof(right));
			}
			return new Pattern(new CharLiteral(left).Concatenate(new StringLiteral(right)));
		}

		/// <summary>
		/// Concatenates the patterns so that <paramref name="left"/> comes before <paramref name="right"/>
		/// </summary>
		/// <param name="left">The preceeding <see cref="Char"/></param>
		/// <param name="right">The succeeding <see cref="Char"/></param>
		/// <returns>A new <see cref="Pattern"/> concatenating <paramref name="left"/> and <paramref name="right"/></returns>
		public static Pattern Concatenator(Char left, Char right) => new Pattern(new CharLiteral(left).Concatenate(new CharLiteral(right)));

		//public static Pattern Negator(Pattern Pattern) => new Pattern(Pattern.Head.Negate());

		//public static Pattern Negator(String Pattern) {
		//	if (Pattern is null) {
		//		throw new ArgumentNullException(nameof(Pattern));
		//	}
		//	return new Pattern(new StringLiteral(Pattern).Negate());
		//}

		//public static Pattern Negator(Char Pattern) => new Pattern(new CharLiteral(Pattern).Negate());

		//public static Pattern Optor(Pattern Pattern) => new Pattern(Pattern.Head.Optional());

		//public static Pattern Optor(String Pattern) {
		//	if (Pattern is null) {
		//		throw new ArgumentNullException(nameof(Pattern));
		//	}
		//	return new Pattern(new StringLiteral(Pattern).Optional());
		//}

		//public static Pattern Optor(Char Pattern) => new Pattern(new CharLiteral(Pattern).Optional());

		//public static Pattern Ranger(Pattern From, Pattern To) => new Pattern(new Ranger(From.Head, To.Head));

		//public static Pattern Ranger(Pattern From, String To) {
		//	if (To is null) {
		//		throw new ArgumentNullException(nameof(To));
		//	}
		//	return new Pattern(new Ranger(From.Head, new StringLiteral(To)));
		//}

		//public static Pattern Ranger(String From, Pattern To) {
		//	if (From is null) {
		//		throw new ArgumentNullException(nameof(From));
		//	}
		//	return new Pattern(new Ranger(new StringLiteral(From), To.Head));
		//}

		//public static Pattern Ranger(Pattern From, Char To) => new Pattern(new Ranger(From.Head, new CharLiteral(To)));

		//public static Pattern Ranger(Char From, Pattern To) => new Pattern(new Ranger(new CharLiteral(From), To.Head));

		//public static Pattern Ranger(String From, String To) {
		//	if (From is null || To is null) {
		//		throw new ArgumentNullException(From is null ? nameof(From) : nameof(To));
		//	}
		//	return new Pattern(new Ranger(new StringLiteral(From), new StringLiteral(To)));
		//}

		//public static Pattern Ranger(String From, Char To) {
		//	if (From is null) {
		//		throw new ArgumentNullException(nameof(From));
		//	}
		//	return new Pattern(new Ranger(new StringLiteral(From), new CharLiteral(To)));
		//}

		//public static Pattern Ranger(Char From, String To) {
		//	if (To is null) {
		//		throw new ArgumentNullException(nameof(To));
		//	}
		//	return new Pattern(new Ranger(new CharLiteral(From), new StringLiteral(To)));
		//}

		//public static Pattern Ranger(Char From, Char To) => new Pattern(new Ranger(new CharLiteral(From), new CharLiteral(To)));

		//public static Pattern Ranger(Pattern From, Pattern To, Pattern Escape) => new Pattern(new EscapedRanger(From.Head, To.Head, Escape.Head));

		//public static Pattern Ranger(Pattern From, Pattern To, String Escape) {
		//	if (Escape is null) {
		//		throw new ArgumentNullException(nameof(Escape));
		//	}
		//	return new Pattern(new EscapedRanger(From.Head, To.Head, new StringLiteral(Escape)));
		//}

		//public static Pattern Ranger(Pattern From, Pattern To, Char Escape) => new Pattern(new EscapedRanger(From.Head, To.Head, new CharLiteral(Escape)));

		//public static Pattern Ranger(Pattern From, String To, Pattern Escape) {
		//	if (To is null) {
		//		throw new ArgumentNullException(nameof(To));
		//	}
		//	return new Pattern(new EscapedRanger(From.Head, new StringLiteral(To), Escape.Head));
		//}

		//public static Pattern Ranger(Pattern From, String To, String Escape) {
		//	if (To is null || Escape is null) {
		//		throw new ArgumentNullException(To is null ? nameof(To) : nameof(Escape));
		//	}
		//	return new Pattern(new EscapedRanger(From.Head, new StringLiteral(To), new StringLiteral(Escape)));
		//}

		//public static Pattern Ranger(Pattern From, String To, Char Escape) {
		//	if (To is null) {
		//		throw new ArgumentNullException(nameof(To));
		//	}
		//	return new Pattern(new EscapedRanger(From.Head, new StringLiteral(To), new CharLiteral(Escape)));
		//}

		//public static Pattern Ranger(String From, Pattern To, Pattern Escape) {
		//	if (From is null) {
		//		throw new ArgumentNullException(nameof(From));
		//	}
		//	return new Pattern(new EscapedRanger(new StringLiteral(From), To.Head, Escape.Head));
		//}

		//public static Pattern Ranger(String From, Pattern To, String Escape) {
		//	if (From is null || Escape is null) {
		//		throw new ArgumentNullException(From is null ? nameof(From) : nameof(Escape));
		//	}
		//	return new Pattern(new EscapedRanger(new StringLiteral(From), To.Head, new StringLiteral(Escape)));
		//}

		//public static Pattern Ranger(String From, Pattern To, Char Escape) {
		//	if (From is null) {
		//		throw new ArgumentNullException(nameof(From));
		//	}
		//	return new Pattern(new EscapedRanger(new StringLiteral(From), To.Head, new CharLiteral(Escape)));
		//}

		//public static Pattern Ranger(Pattern From, Char To, Pattern Escape) => new Pattern(new EscapedRanger(From.Head, new CharLiteral(To), Escape.Head));

		//public static Pattern Ranger(Pattern From, Char To, String Escape) {
		//	if (Escape is null) {
		//		throw new ArgumentNullException(nameof(Escape));
		//	}
		//	return new Pattern(new EscapedRanger(From.Head, new CharLiteral(To), new StringLiteral(Escape)));
		//}

		//public static Pattern Ranger(Pattern From, Char To, Char Escape) => new Pattern(new EscapedRanger(From.Head, new CharLiteral(To), new CharLiteral(Escape)));

		//public static Pattern Ranger(Char From, Pattern To, Pattern Escape) => new Pattern(new EscapedRanger(new CharLiteral(From), To.Head, Escape.Head));

		//public static Pattern Ranger(Char From, Pattern To, String Escape) {
		//	if (Escape is null) {
		//		throw new ArgumentNullException(nameof(Escape));
		//	}
		//	return new Pattern(new EscapedRanger(new CharLiteral(From), To.Head, new StringLiteral(Escape)));
		//}

		//public static Pattern Ranger(Char From, Pattern To, Char Escape) => new Pattern(new EscapedRanger(new CharLiteral(From), To.Head, new CharLiteral(Escape)));

		//public static Pattern Ranger(String From, String To, Pattern Escape) {
		//	if (From is null || To is null) {
		//		throw new ArgumentNullException(From is null ? nameof(From) : nameof(To));
		//	}
		//	return new Pattern(new EscapedRanger(new StringLiteral(From), new StringLiteral(To), Escape.Head));
		//}

		//public static Pattern Ranger(String From, String To, String Escape) {
		//	if (From is null || To is null || Escape is null) {
		//		if (From is null) {
		//			throw new ArgumentNullException(nameof(From));
		//		} else {
		//			throw new ArgumentNullException(To is null ? nameof(To) : nameof(Escape));
		//		}
		//	}
		//	return new Pattern(new EscapedRanger(new StringLiteral(From), new StringLiteral(To), new StringLiteral(Escape)));
		//}

		//public static Pattern Ranger(String From, String To, Char Escape) {
		//	if (From is null || To is null) {
		//		throw new ArgumentNullException(From is null ? nameof(From) : nameof(To));
		//	}
		//	return new Pattern(new EscapedRanger(new StringLiteral(From), new StringLiteral(To), new CharLiteral(Escape)));
		//}

		//public static Pattern Ranger(String From, Char To, Pattern Escape) {
		//	if (From is null) {
		//		throw new ArgumentNullException(nameof(From));
		//	}
		//	return new Pattern(new EscapedRanger(new StringLiteral(From), new CharLiteral(To), Escape.Head));
		//}

		//public static Pattern Ranger(String From, Char To, String Escape) {
		//	if (From is null || Escape is null) {
		//		throw new ArgumentNullException(From is null ? nameof(From) : nameof(Escape));
		//	}
		//	return new Pattern(new EscapedRanger(new StringLiteral(From), new CharLiteral(To), new StringLiteral(Escape)));
		//}

		//public static Pattern Ranger(String From, Char To, Char Escape) {
		//	if (From is null) {
		//		throw new ArgumentNullException(nameof(From));
		//	}
		//	return new Pattern(new EscapedRanger(new StringLiteral(From), new CharLiteral(To), new CharLiteral(Escape)));
		//}

		//public static Pattern Ranger(Char From, String To, Pattern Escape) {
		//	if (To is null) {
		//		throw new ArgumentNullException(nameof(To));
		//	}
		//	return new Pattern(new EscapedRanger(new CharLiteral(From), new StringLiteral(To), Escape.Head));
		//}

		//public static Pattern Ranger(Char From, String To, String Escape) {
		//	if (To is null || Escape is null) {
		//		throw new ArgumentNullException(To is null ? nameof(To) : nameof(Escape));
		//	}
		//	return new Pattern(new EscapedRanger(new CharLiteral(From), new StringLiteral(To), new StringLiteral(Escape)));
		//}

		//public static Pattern Ranger(Char From, String To, Char Escape) {
		//	if (To is null) {
		//		throw new ArgumentNullException(nameof(To));
		//	}
		//	return new Pattern(new EscapedRanger(new CharLiteral(From), new StringLiteral(To), new CharLiteral(Escape)));
		//}

		//public static Pattern Ranger(Char From, Char To, Pattern Escape) => new Pattern(new EscapedRanger(new CharLiteral(From), new CharLiteral(To), Escape.Head));

		//public static Pattern Ranger(Char From, Char To, String Escape) {
		//	if (Escape is null) {
		//		throw new ArgumentNullException(nameof(Escape));
		//	}
		//	return new Pattern(new EscapedRanger(new CharLiteral(From), new CharLiteral(To), new StringLiteral(Escape)));
		//}

		//public static Pattern Ranger(Char From, Char To, Char Escape) => new Pattern(new EscapedRanger(new CharLiteral(From), new CharLiteral(To), new CharLiteral(Escape)));

		//public static Pattern NestedRanger(Pattern From, Pattern To) => new Pattern(new NestedRanger(From.Head, To.Head));

		//public static Pattern NestedRanger(Pattern From, String To) {
		//	if (To is null) {
		//		throw new ArgumentNullException(nameof(To));
		//	}
		//	return new Pattern(new NestedRanger(From.Head, new StringLiteral(To)));
		//}

		//public static Pattern NestedRanger(String From, Pattern To) {
		//	if (From is null) {
		//		throw new ArgumentNullException(nameof(From));
		//	}
		//	return new Pattern(new NestedRanger(new StringLiteral(From), To.Head));
		//}

		//public static Pattern NestedRanger(Pattern From, Char To) => new Pattern(new NestedRanger(From.Head, new CharLiteral(To)));

		//public static Pattern NestedRanger(Char From, Pattern To) => new Pattern(new NestedRanger(new CharLiteral(From), To.Head));

		//public static Pattern NestedRanger(String From, String To) {
		//	if (From is null || To is null) {
		//		throw new ArgumentNullException(From is null ? nameof(From) : nameof(To));
		//	}
		//	return new Pattern(new NestedRanger(new StringLiteral(From), new StringLiteral(To)));
		//}

		//public static Pattern NestedRanger(String From, Char To) {
		//	if (From is null) {
		//		throw new ArgumentNullException(nameof(From));
		//	}
		//	return new Pattern(new NestedRanger(new StringLiteral(From), new CharLiteral(To)));
		//}

		//public static Pattern NestedRanger(Char From, String To) {
		//	if (To is null) {
		//		throw new ArgumentNullException(nameof(To));
		//	}
		//	return new Pattern(new NestedRanger(new CharLiteral(From), new StringLiteral(To)));
		//}

		//public static Pattern NestedRanger(Char From, Char To) => new Pattern(new NestedRanger(new CharLiteral(From), new CharLiteral(To)));

		//public static Pattern Repeater(Pattern Pattern, Int32 Count) => new Pattern(Pattern.Head.Repeat(Count));

		//public static Pattern Repeater(String Pattern, Int32 Count) {
		//	if (Pattern is null) {
		//		throw new ArgumentNullException(nameof(Pattern));
		//	}
		//	return new Pattern(new StringLiteral(Pattern).Repeat(Count));
		//}

		//public static Pattern Repeater(Char Pattern, Int32 Count) => new Pattern(new CharLiteral(Pattern).Repeat(Count));

		//public static Pattern Spanner(Pattern Pattern) => new Pattern(Pattern.Head.Span());

		//public static Pattern Spanner(String Pattern) {
		//	if (Pattern is null) {
		//		throw new ArgumentNullException(nameof(Pattern));
		//	}
		//	return new Pattern(new StringLiteral(Pattern).Span());
		//}

		//public static Pattern Spanner(Char Pattern) => new Pattern(new CharLiteral(Pattern).Span());
	}
}