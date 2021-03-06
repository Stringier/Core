# Extending the Testing Framework

[`Philosoft.Testing`](https://www.nuget.org/packages/Philosoft.Testing) was designed from the ground up to be extensible. In fact, the majority of its own design is entirely through the extensibility mechanism. So how does one do so?

Extension methods. That's it. There's no clever things you need to learn like with [NUnit](https://nunit.org/), or mechanism that requires different syntax like [MSTest](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest), or no possibility at all like with [xUnit](https://xunit.net/). This should be as easy, obvious, and straightforward as possible.

There's two points of extension that we'll cover. Some example extensions follow after.

## Assertions

These are the methods called off of the **Asserter** object. They should always return the caller, so that assertions can optionally be chained. You write these like any assertion method, if you've ever written them for other frameworks. Check if a failure happened, and throw an exception if one did.

I can't reasonably provide a universal formatter for any possible situation, so by convention, format the exception message like so: `{Why the exception occurred}\nActual: {actual}\nExpected: {expected}`. But not everything needs the actual and expected values printed. And some things may need even more information printed. Use your head, but keep with the idea for the line terminators. Not everyone prefers using the text explorer as a central panel, with some opting to dock it on the side. Formatting the output like this allows for quick and easy reading regardless of the way the explorer is docked, or any other tooling presenting the issue.

The caller can be a little tricky but isn't too bad, because of how there's multiple **Asserter** types. The standard **Asserter** types are listed in that section below.

## Asserter

This is what's generated by `Assert.That(actual)` calls. There isn't any single type, although `Assert<T>` covers most use cases. You almost never need to extend this. The idea is for overloads of `That()` to handle creating the correct **Asserter** type.

### Asserter<T>

This is the most common **Asserter** and, in fact, works in the overwhelming majority of situations. It holds the instance, nothing more, nothing less.

### ArrayAsserter<T>

Using `Asserter<T>` for [`T[]`](https://docs.microsoft.com/en-us/dotnet/api/system.array), [`Memory<T>`](https://docs.microsoft.com/en-us/dotnet/api/system.memory-1), [`ReadOnlyMemory<T>`](https://docs.microsoft.com/en-us/dotnet/api/system.readonlymemory-1), [`Span<T>`](https://docs.microsoft.com/en-us/dotnet/api/system.span-1?), and [`ReadOnlySpan<T>`](https://docs.microsoft.com/en-us/dotnet/api/system.readonlyspan-1) gets a little tricky. Instead of expecting people to figure all this out, it was easier to just provide a new asserter to handle those specifically.

## Example Assertion

We'll use two examples from the standard assertions that are implemented as an extension. It's simple and easy to follow along with.

First, let's look at handling `Assert<T>`:

~~~~csharp
public static Assert<T> Null<T>(this Assert<T> assert) where T : class {
	if (assert.Actual is not null) {
		throw new AssertException($"The instance was not null.\nActual: {assert.Actual}");
	}
	return assert;
}

public static Assert<Nullable<T>> Null<T>(this Assert<Nullable<T>> assert) where T : struct {
	if (assert.Actual is not null) {
		throw new AssertException($"The instance was not null.\nActual: {assert.Actual}");
	}
	return assert;
}
~~~~

Why two of otherwise the same assertion? Well, that's partially the power of this approach. In the first case, we're covering any reference type, which, of course that can potentially be `null`. The second covers any cases of `Nullable<T>` or `T?`. Together, these mean any non-nullable struct won't even have these checks presented, because the result is obvious. It also serves as an example of how `Assert<T>` can be partially constrained, like with `Assert<Nullable<T>>`.

Second, let's look at handling `ArrayAssert<T>`:

~~~~csharp
public static ArrayAssert<T> Length<T>(this ArrayAssert<T> assert, Int32 expected) {
	if (assert.Actual.Length != expected) {
		throw new AssertException($"The length was not what was expected.\nActual: {assert.Actual.Length}\nExpected: {expected}");
	}
	return assert;
}
~~~~

This will allow for _any_ array-like to have its length checked in a clearer way than using `actual.Length` at the callsite, and works better with property testing.
