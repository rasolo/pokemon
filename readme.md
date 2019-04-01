## API

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

### Add Pokemon

Todo

### TODO:

- [ ] Update to Core 3.
- [ ] - [ ] Correctly load the collections of Pokemon with Entity Framework (Evolutions and Moves).
- [ ] Refractor.