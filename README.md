# Bitfield

Bitfield is a [Kari](https://github.com/AntonC9018/Kari) plugin which allows generating bitfield types from a specification.

In the simple example below, a `MyBitfield` struct is generated, with helper setters and getters for each of the properties. It will take the smallest space possible, while providing a convenient API at the same time.

```csharp
using BF = Kari.Plugins.Bitfield;

public enum FlagsEnum { A = 1, B = 2, C = 4, D = 8 }

// Generate a struct that takes up just 4 bytes!
[BF.Specification("MyBitfield"/*, DynamicallySized = false, Struct = true*/)]
interface IMyBitfield
{
    // Occupies 2 bits in memory.
    [BF.Bits(2)]       int Stuff2 { get; set; }
    // Occupies 4 bits in memory.
    [BF.Bits(4)]       int Stuff4 { get; set; }
    // Occupies 1 bit, the range of values is constraint to [10, 11].
    [BF.Range(10, 11)] int Ranged10_11 { get; set; }
    // Takes up 1 bit.
    [BF.Bit]           bool IsSet { get; set; }
    // Will take up 4 bits, one for each of the enum members.
    [BF.Flags]         FlagsEnum FlagsEnum { get; set; }
}

```

Unimplemented features:

* Allow values that would take more than 4 bytes (e.g. custom structs);
* Better strategy for distributing the bits over the bytes, perhaps make use of some graph search algorithm?
* Allow overflow error checking (Debug.Assert / exceptions / provide checker methods);
* Allow dynamic bitfields (useful when e.g. scalable flags are involved that get added at runtime and may go beyond 4 bytes);
* Pretty prints?
* Protobuf-like storage efficient serialization?


## Building

Firstly, you need the source code of Kari somewhere on you computer.

> Also install it as a tool for convenience.

Now look at the file `config.example.props`.
You will need to duplicate it under the name `config.props` and change the path to plugin props to point to where Kari is. 
The one provided as an example, assumes Kari is next to the folder that contains this repo.

> I will remove this step in the future, when I figure out how to distribute msbuild property files.

Now you can compile the project in `./Bitfield` in any way you like (presumably, with `dotnet build --configuration Release`).

For an example kari config, see `./Bitfield.Tests/kari.json`.


## Testing

Run `dotnet test`.