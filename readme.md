# Pokemon API #

## About ##
An Api made in .NET core 3 related to Pokemon.

### List Pokemon

Retrieve a list of pokemon.

#### `GET` /api/v1.0/pokemon/list

| Parameter    | Value                        | Required | Example   |
| ------------ | ---------------------------- | -------- | --------- |
| `Sort`       | `{propertyName} {direction}` |          | name desc |
| `PageSize`   | `{number}`                   |          | 10        |
| `PageNumber` | `{number}`                   |          | 1         |

### Get Pokemon

Retrieve a single pokemon.

#### `GET` `/api/v1.0/pokemon/bulbasaur`

| Parameter | Value    | Required | Example           |
| --------- | -------- | -------- | ----------------- |
| `Name`      | `{string}` | ✅        | Bulbasaur |

## Requirements
Dotnet core 3 SDK and the runtime: https://dotnet.microsoft.com/download/dotnet-core/3.0
Visual Studio 2019: https://visualstudio.microsoft.com/downloads/
Use previews of the .NET Core SDK
In Visual Studio 2019
1. Tools
2. Projects and Solutions
3. .NET Core
4. Check "Use previews of the .NET Core SDK"

### TODO:

- [x] Update to Core 3.
- [x] Correctly load the collections of Pokemon with Entity Framework (Evolutions and Moves).
- [ ] Refractor.
- [ ] Add Pokemon endpoint
- [ ] Delete Pokemon endpoint