# Semantic Database Model — VendingMachineApp

## Introduction

This document describes the semantic database model used in the VendingMachineApp ASP.NET Core MVC project.  
The model is implemented using Entity Framework Core and represents the core business entities and their relationships within a vending machine management system.

---

## 1. Product

**Description:** Represents an item available for sale in a vending machine.

**Properties:**

- Id (int)
- Name (string)
- Description (string)
- Price (decimal)
- Quantity (int)
- SupplierId (int)

---

## 2. Supplier

**Description:** Represents a company or individual supplying products to vending machines.

**Properties:**

- Id (int)
- Name (string)
- ContactInfo (string)

---

## 3. Order

**Description:** Represents a purchase order for products from a supplier.

**Properties:**

- Id (int)
- OrderDate (DateTime)
- SupplierId (int)
- Status (string)

---

## 4. OrderItem

**Description:** Represents a specific product and quantity within an order.

**Properties:**

- Id (int)
- OrderId (int)
- ProductId (int)
- Quantity (int)
- UnitPrice (decimal)

---

## 5. VendingMachine

**Description:** Represents a physical vending machine managed by the system.

**Properties:**

- Id (int)
- Location (string)
- Status (string)

---

## 6. Technician

**Description:** Represents a technician responsible for maintaining vending machines.

**Properties:**

- Id (int)
- Name (string)
- ContactInfo (string)

---

## 7. MaintenanceLog

**Description:** Represents a record of maintenance performed on a vending machine.

**Properties:**

- Id (int)
- VendingMachineId (int)
- TechnicianId (int)
- Date (DateTime)
- Description (string)

---

## 8. Transaction

**Description:** Represents a purchase transaction made by a customer at a vending machine.

**Properties:**

- Id (int)
- VendingMachineId (int)
- ProductId (int)
- Date (DateTime)
- Quantity (int)
- TotalAmount (decimal)
- Status (string)

---

## Relationships

- Product → Supplier (many-to-one)
- Order → Supplier (many-to-one)
- Order → OrderItem (one-to-many)
- OrderItem → Product (many-to-one)
- VendingMachine → MaintenanceLog (one-to-many)
- Technician → MaintenanceLog (one-to-many)
- VendingMachine → Transaction (one-to-many)
- Transaction → Product (many-to-one)
