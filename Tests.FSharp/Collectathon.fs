﻿namespace Collectathon

open Collectathon.Arrays
open System
open Xunit

module FSharp =
    [<Fact>]
    let ``FixedArray`` () =
        let array = fix[|1;2;3;4;5|]
        Assert.IsType<FixedArray<int>>(array)

    [<Fact>]
    let ``BoundedArray`` () =
        let array = bnd[|1;2;3;4;5|]
        Assert.IsType<BoundedArray<int>>(array)

    [<Fact>]
    let ``DynamicArray`` () =
        let array = dyn[|1;2;3;4;5|]
        Assert.IsType<DynamicArray<int>>(array)