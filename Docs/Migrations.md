# Entity Framework Core Migrations

## Dodavanje nove migracije

```bash
dotnet ef migrations add InitialCreate

```

## Ažuriranje baze

```bash
dotnet ef database update

```

## Ažuriranje baze na određenu migraciju

```bash
dotnet ef database update SeedProducts

```

## Brisanje zadnje migracije

```bash
dotnet ef migrations remove

```

## Pregled svih migracija

```bash
dotnet ef migrations list

```
