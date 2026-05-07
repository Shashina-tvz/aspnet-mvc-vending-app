# MVC Create & Edit Form Skill — VendingMachineApp

## Purpose

This skill is used to generate consistent Create and Edit forms across all entities in the VendingMachineApp (Product, Supplier, Order, Technician, MaintenanceLog).

It ensures uniform structure, validation, and repository usage.

---

## When to use this skill

Use this skill when:

- Creating a new entity form (Create)
- Editing an existing entity (Edit)
- Extending CRUD functionality to a new controller
- Ensuring consistent UI and validation across forms

---

## Standard MVC Pattern

Each entity follows this structure:

### 1. GET Create

- Returns empty form view

### 2. POST Create

- Validates ModelState
- Calls repository AddAsync
- Redirects to Index

### 3. GET Edit

- Loads entity by Id
- Returns view with model

### 4. POST Edit

- Validates Id match + ModelState
- Calls repository UpdateAsync
- Redirects to Index

---

## Code Template

### Create (GET)

```csharp
public IActionResult Create()
{
    return View();
}
```

### Create (POST)

```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(Entity model)
{
    if (ModelState.IsValid)
    {
        await _repo.AddAsync(model);
        return RedirectToAction(nameof(Index));
    }
    return View(model);
}
```

### Edit (GET)

```csharp
public async Task<IActionResult> Edit(int id)
{
    var entity = await _repo.GetByIdAsync(id);
    if (entity == null) return NotFound();
    return View(entity);
}
```

### Edit (POST)

```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, Entity model)
{
    if (id != model.Id) return NotFound();

    if (ModelState.IsValid)
    {
        await _repo.UpdateAsync(model);
        return RedirectToAction(nameof(Index));
    }

    return View(model);
}
```
