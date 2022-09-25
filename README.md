# Work in Progress

## Design

![Design](docs/design.jfif)

## Running

Don't forget to change the MongoDb connection string in appsettings.json

```
dotnet run
```

## Testing

```
dotnet test
```

# About

This API responsability is to deliver data that is stored inside a mongodb database.

All the requests are stored in a redis database with a lifespan of 24 hours. This is important because looking up in the database is costly.

I decided to use 24 hours because TvMazeAPI show request has a 24 hours caching, as well.

## Kanban

### Todo

### In Progress

### Done ✓

- [x] k8s
- [x] github actions
- [x] Redis caching
- [x] ShowController
