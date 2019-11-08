﻿using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Stringier.Patterns.Nodes;

namespace Stringier.Patterns {
	public partial class Pattern {
		#region Literals

		public static implicit operator Pattern(Char Char) => new Pattern(new CharLiteral(Char));

		public static implicit operator Pattern(String Pattern) => new Pattern(new StringLiteral(Pattern));

		#endregion

		//#region Alternator

		///// <summary>
		///// Declares <paramref name="Right"/> to be an alternate of <paramref name="Left"/>
		///// </summary>
		///// <param name="Left">The <see cref="Pattern"/> to check first</param>
		///// <param name="Right">The <see cref="Pattern"/> to check if <paramref name="Left"/> does not match</param>
		///// <returns></returns>
		//public static Pattern operator |(Pattern Left, Pattern Right) => new Pattern(Left.Head.Alternate(Right.Head));

		///// <summary>
		///// Declares <paramref name="Right"/> to be an alternate of <paramref name="Left"/>
		///// </summary>
		///// <param name="Left">The <see cref="Pattern"/> to check first</param>
		///// <param name="Right">The <see cref="Char"/> to check if <paramref name="Left"/> does not match</param>
		///// <returns></returns>
		//public static Pattern operator |(Pattern Left, Char Right) => new Pattern(Left.Head.Alternate(new CharLiteral(Right)));

		///// <summary>
		///// Declares <paramref name="Right"/> to be an alternate of <paramref name="Left"/>
		///// </summary>
		///// <param name="Left">The <see cref="Char"/> to check first</param>
		///// <param name="Right">The <see cref="Pattern"/> to check if <paramref name="Left"/> does not match</param>
		///// <returns></returns>
		//public static Pattern operator |(Char Left, Pattern Right) => new Pattern(new CharLiteral(Left).Alternate(Right.Head));

		///// <summary>
		///// Declares <paramref name="Right"/> to be an alternate of <paramref name="Left"/>
		///// </summary>
		///// <param name="Left">The <see cref="Pattern"/> to check first</param>
		///// <param name="Right">The <see cref="String"/> to check if <paramref name="Left"/> does not match</param>
		///// <returns></returns>
		//public static Pattern operator |(Pattern Left, String Right) {
		//	if (Right is null) {
		//		throw new ArgumentNullException(nameof(Right));
		//	}
		//	return new Pattern(Left.Head.Alternate(new StringLiteral(Right)));
		//}

		///// <summary>
		///// Declares <paramref name="Right"/> to be an alternate of <paramref name="Left"/>
		///// </summary>
		///// <param name="Left">The <see cref="String"/> to check first</param>
		///// <param name="Right">The <see cref="Pattern"/> to check if <paramref name="Left"/> does not match</param>
		///// <returns></returns>
		//public static Pattern operator |(String Left, Pattern Right) {
		//	if (Left is null) {
		//		throw new ArgumentNullException(nameof(Left));
		//	}
		//	return new Pattern(new StringLiteral(Left).Alternate(Right.Head));
		//}

		//#endregion

		//#region Capturer

		//public static implicit operator Pattern(Capture Capture) => new Pattern(new CaptureLiteral(Capture));

		///// <summary>
		///// Declares this <see cref="Pattern"/> should be captured into <paramref name="Capture"/> for later reference
		///// </summary>
		///// <param name="Capture">A <see cref="Patterns.Capture"/> object to store into.</param>
		///// <returns></returns>
		//public Pattern Capture(out Capture Capture) => new Pattern(Head.Capture(out Capture));

		//#endregion

		//#region Checker

		///// <summary>
		///// Use the <paramref name="Check"/> to represent a single character.
		///// </summary>
		///// <param name="Name">Name to display this pattern as.</param>
		///// <param name="Check">A <see cref="Func{T, TResult}"/> to validate the character.</param>
		///// <returns></returns>
		//public static Pattern Check(String Name, Func<Char, Boolean> Check) {
		//	if (Name is null) {
		//		throw new ArgumentNullException(nameof(Name));
		//	}
		//	return new Pattern(new CharChecker(Name, Check));
		//}

		///// <summary>
		///// Use the specified <paramref name="HeadCheck"/>, <paramref name="BodyCheck"/>, and <paramref name="TailCheck"/> to represent a variable length string. The head and tail are only present once, with the body being repeatable.
		///// </summary>
		///// <param name="Name">Name to display this pattern as.</param>
		///// <param name="HeadCheck">A <see cref="Func{T, TResult}"/> to validate the head.</param>
		///// <param name="BodyCheck">A <see cref="Func{T, TResult}"/> to validate the body, which may repeat.</param>
		///// <param name="TailCheck">A <see cref="Func{T, TResult}"/> to validate the tail.</param>
		///// <returns></returns>
		//public static Pattern Check(String Name, Func<Char, Boolean> HeadCheck, Func<Char, Boolean> BodyCheck, Func<Char, Boolean> TailCheck) {
		//	if (Name is null) {
		//		throw new ArgumentNullException(nameof(Name));
		//	}
		//	return new Pattern(new StringChecker(Name, HeadCheck, BodyCheck, TailCheck));
		//}

		///// <summary>
		///// Use the specified <paramref name="HeadCheck"/>, <paramref name="BodyCheck"/>, and <paramref name="TailCheck"/> to represent a variable length string, along with whether each check is required for a valid string. The head and tail are only present once, with the body being repeatable.
		///// </summary>
		///// <param name="Name">Name to display this pattern as.</param>
		///// <param name="HeadCheck">A <see cref="Func{T, TResult}"/> to validate the head.</param>
		///// <param name="HeadRequired">Whether the <paramref name="HeadCheck"/> is required.</param>
		///// <param name="BodyCheck">A <see cref="Func{T, TResult}"/> to validate the body, which may repeat.</param>
		///// <param name="BodyRequired">Whether the <paramref name="BodyCheck"/> is required.</param>
		///// <param name="TailCheck">A <see cref="Func{T, TResult}"/> to validate the tail.</param>
		///// <param name="TailRequired">Whether the <paramref name="TailRequired"/> is required.</param>
		///// <returns></returns>
		//public static Pattern Check(String Name, Func<Char, Boolean> HeadCheck, Boolean HeadRequired, Func<Char, Boolean> BodyCheck, Boolean BodyRequired, Func<Char, Boolean> TailCheck, Boolean TailRequired) {
		//	if (Name is null) {
		//		throw new ArgumentNullException(nameof(Name));
		//	}
		//	return new Pattern(new StringChecker(Name, HeadCheck, HeadRequired, BodyCheck, BodyRequired, TailCheck, TailRequired));
		//}

		///// <summary>
		///// Use the specified <paramref name="HeadCheck"/>, <paramref name="BodyCheck"/>, and <paramref name="TailCheck"/> to represent the valid form of a word, along with the <paramref name="Bias"/>.
		///// </summary>
		///// <param name="Name">Name to display this pattern as.</param>
		///// <param name="Bias">Endian bias of the word. Head bias requires the head if only one letter is present. Tail bias requires the tail if only one letter is present.</param>
		///// <param name="HeadCheck">A <see cref="Func{T, TResult}"/> to validate the head.</param>
		///// <param name="BodyCheck">A <see cref="Func{T, TResult}"/> to validate the body, which may repeat.</param>
		///// <param name="TailCheck">A <see cref="Func{T, TResult}"/> to validate the tail.</param>
		///// <returns></returns>
		//public static Pattern Check(String Name, Bias Bias, Func<Char, Boolean> HeadCheck, Func<Char, Boolean> BodyCheck, Func<Char, Boolean> TailCheck) {
		//	if (Name is null) {
		//		throw new ArgumentNullException(nameof(Name));
		//	}
		//	return new Pattern(new WordChecker(Name, Bias, HeadCheck, BodyCheck, TailCheck));
		//}

		//#endregion

		#region Concatenator

		/// <summary>
		/// Concatenates the patterns so that <paramref name="left"/> comes before <paramref name="right"/>
		/// </summary>
		/// <param name="left">The preceeding <see cref="Pattern"/></param>
		/// <param name="right">The succeeding <see cref="Pattern"/></param>
		/// <returns>A new <see cref="Pattern"/> concatenating <paramref name="left"/> and <paramref name="right"/></returns>
		public static Pattern operator &(Pattern left, Pattern right) => new Pattern(left.Head.Concatenate(right.Head));

		/// <summary>
		/// Concatenates the patterns so that <paramref name="left"/> comes before <paramref name="right"/>
		/// </summary>
		/// <param name="left">The preceeding <see cref="Pattern"/></param>
		/// <param name="right">The succeeding <see cref="Char"/></param>
		/// <returns>A new <see cref="Pattern"/> concatenating <paramref name="left"/> and <paramref name="right"/></returns>
		public static Pattern operator &(Pattern left, Char right) => new Pattern(left.Head.Concatenate(new CharLiteral(right)));

		/// <summary>
		/// Concatenates the patterns so that <paramref name="left"/> comes before <paramref name="right"/>
		/// </summary>
		/// <param name="left">The preceeding <see cref="Char"/></param>
		/// <param name="right">The succeeding <see cref="Pattern"/></param>
		/// <returns>A new <see cref="Pattern"/> concatenating <paramref name="left"/> and <paramref name="right"/></returns>
		public static Pattern operator &(Char left, Pattern right) => new Pattern(new CharLiteral(left).Concatenate(right.Head));

		/// <summary>
		/// Concatenates the patterns so that <paramref name="left"/> comes before <paramref name="right"/>
		/// </summary>
		/// <param name="left">The preceeding <see cref="Pattern"/></param>
		/// <param name="right">The succeeding <see cref="String"/></param>
		/// <returns>A new <see cref="Pattern"/> concatenating <paramref name="left"/> and <paramref name="right"/></returns>
		public static Pattern operator &(Pattern left, String right) {
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
		public static Pattern operator &(String left, Pattern right) {
			if (left is null) {
				throw new ArgumentNullException(nameof(left));
			}
			return new Pattern(new StringLiteral(left).Concatenate(right.Head));
		}

		#endregion

		//#region Jumper

		//public Pattern Target(out Target Target) {
		//	Target = new Target(this);
		//	return this;
		//}

		//#endregion

		//#region Negator

		///// <summary>
		///// Marks the <paramref name="Pattern"/> as negated
		///// </summary>
		///// <param name="Pattern"></param>
		///// <returns></returns>
		//public static Pattern operator !(Pattern Pattern) => new Pattern(Pattern.Head.Negate());

		//#endregion

		//#region Optor

		///// <summary>
		///// Marks the <paramref name="Pattern"/> as optional
		///// </summary>
		///// <param name="Pattern"></param>
		///// <returns></returns>
		//public static Pattern operator -(Pattern Pattern) => new Pattern(Pattern.Head.Optional());

		//#endregion

		//#region Ranger

		///// <summary>
		///// Create a pattern representing the range <paramref name="From"/> until <paramref name="To"/>.
		///// </summary>
		///// <param name="From">Begining <see cref="Pattern"/>.</param>
		///// <param name="To">Ending <see cref="Pattern"/>.</param>
		//public static Pattern Range(Pattern From, Pattern To) => new Pattern(new Ranger(From.Head, To.Head));

		///// <summary>
		///// Create a pattern representing the range <paramref name="From"/> until <paramref name="To"/>, allowing an <paramref name="Escape"/>.
		///// </summary>
		///// <param name="From">Begining <see cref="Pattern"/>.</param>
		///// <param name="To">Ending <see cref="Pattern"/>.</param>
		///// <param name="Escape">Escape <see cref="Pattern"/>./</param>
		//public static Pattern Range(Pattern From, Pattern To, Pattern Escape) => new Pattern(new EscapedRanger(From.Head, To.Head, Escape.Head));

		///// <summary>
		///// Create a pattern representing the range <paramref name="From"/> until <paramref name="To"/>, that allows nesting of this pattern inside of itself.
		///// </summary>
		///// <remarks>
		///// The easiest way to explain this is that is shows up a lot in programming, with things like if-then-else statements which can contain other if-then-else statements.
		///// </remarks>
		///// <param name="From">Begining <see cref="Pattern"/>.</param>
		///// <param name="To">Ending <see cref="Pattern"/>.</param>
		//public static Pattern NestedRange(Pattern From, Pattern To) => new Pattern(new NestedRanger(From.Head, To.Head));

		//#endregion

		//#region RegexAdapter

		//public static implicit operator Pattern(Regex Regex) => new Pattern(new RegexAdapter(Regex));

		//#endregion

		//#region Repeater

		///// <summary>
		///// Marks the <paramref name="Pattern"/> as repeating <paramref name="Count"/> times
		///// </summary>
		///// <param name="Pattern"></param>
		///// <param name="Count"></param>
		///// <returns></returns>
		//public static Pattern operator *(Pattern Pattern, Int32 Count) => new Pattern(Pattern.Head.Repeat(Count));

		//#endregion

		//#region Spanner

		///// <summary>
		///// Marks the <paramref name="Pattern"/> as spanning
		///// </summary>
		///// <param name="Pattern"></param>
		///// <returns></returns>
		//public static Pattern operator +(Pattern Pattern) => new Pattern(Pattern.Head.Span());

		//#endregion
	}
}