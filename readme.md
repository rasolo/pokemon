# Pokemon API #

## About ##
An Api made in .NET core 3 related to Pokemon.
___

### List Pokemon

Retrieve a list of pokemon.

Parameters are sent as query parameters.

#### `GET` /api/v1.0/pokemon/list

| Parameter    | Value                        | Required | Example   |
| ------------ | ---------------------------- | -------- | --------- |
| `Sort`       | `{propertyName} {direction}` |          | name desc |
| `PageSize`   | `{number}`                   |          | 10        |
| `PageNumber` | `{number}`                   |          | 1         |

___
### Get Pokemon

Retrieve a single pokemon.

#### `GET` `/api/v1.0/pokemon/{pokemonname}`

| Parameter | Value    | Required | Example           |
| --------- | -------- | -------- | ----------------- |
| `Name`      | `{string}` | ✅        | Bulbasaur |
___

### Add Pokemon

Add a single pokemon.

Parameters are sent as JSON from body.

#### `POST` `api/v1.0/pokemon/add`

| Parameter | Value    | Required | Example           |
| --------- | -------- | -------- | ----------------- |
| `index`      | `{string}` |         | 719 |
| `name`      | `{string}` | ✅        | Bulbasaur |
| `imageUrl`      | `{string}` | ✅        | http://serebii.net/xy/pokemon/001.png|
| `types`      | `{array}` |         | grass,poison |
| `evolutions`      | `{array}`of`{object`} |         | pokemon:2, event: level-16|
| `moves`      | `{array}`of`{object`} |         | ▼ |
  ```javascript
"moves": [
    {
      "level": "37",
      "name": "Seed Bomb",
      "type": "grass",
      "category": "physical",
      "attack": "80",
      "accuracy": "100",
      "pp": "15",
      "effect_percent": "--",
      "description": "The user slams a barrage of hard-shelled seeds down on the target from above."
    }
  ]
  ```

___
### Delete Pokemon

Delete a single pokemon.

#### `DELETE` `/api/v1.0/pokemon/{pokemonname}`

| Parameter | Value    | Required | Example           |
| --------- | -------- | -------- | ----------------- |
| `Name`      | `{string}` | ✅        | Bulbasaur |
___


## Requirements
Dotnet core 3 SDK and the runtime: https://dotnet.microsoft.com/download/dotnet-core/3.0
Visual Studio 2019: https://visualstudio.microsoft.com/downloads/
Use previews of the .NET Core SDK
In Visual Studio 2019
1. Tools
2. Projects and Solutions
3. .NET Core
4. Check "Use previews of the .NET Core SDK"

___

### TODO:

- [x] Update to Core 3.
- [x] Correctly load the collections of Pokemon with Entity Framework (Evolutions and Moves).
- [ ] Refractor.
- [x] Add Pokémon endpoint
- [x] Delete Pokémon endpoint
- [x] Logging
- [x] Custom exception handling (return generic api response)