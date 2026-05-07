# Application Sitemap — VendingMachineApp

This document describes the routing structure of the VendingMachineApp ASP.NET Core MVC application.  
It lists all available URLs, their controllers, action methods, and corresponding views.

---

## DashboardController

- GET /DashBoard/Index  
  → Controller: DashboardController  
  → Action: Index  
  → View: Views/DashBoard/Index.cshtml

---

## ProductController (Custom Routing)

> This controller uses custom attribute routing instead of default MVC routes.

- GET /products/all  
  → Controller: ProductController  
  → Action: Index  
  → View: Views/Product/Index.cshtml

- GET /products/info/{id}  
  → Controller: ProductController  
  → Action: Details  
  → View: Views/Product/Details.cshtml

- GET /products/new  
  → Controller: ProductController  
  → Action: Create  
  → View: Views/Product/Create.cshtml

- POST /products/new  
  → Controller: ProductController  
  → Action: Create (POST)  
  → View: Views/Product/Create.cshtml (on validation failure)

- GET /products/update/{id}  
  → Controller: ProductController  
  → Action: Edit  
  → View: Views/Product/Edit.cshtml

- POST /products/update/{id}  
  → Controller: ProductController  
  → Action: Edit (POST)  
  → View: Views/Product/Edit.cshtml (on validation failure)

- GET /products/remove/{id}  
  → Controller: ProductController  
  → Action: Delete  
  → View: Views/Product/Delete.cshtml

- POST /products/remove/{id}  
  → Controller: ProductController  
  → Action: DeleteConfirmed  
  → Redirect: /products/all

---

## SupplierController

- GET /Supplier/Index  
  → Controller: SupplierController  
  → Action: Index  
  → View: Views/Supplier/Index.cshtml

- GET /Supplier/Details/{id}  
  → Controller: SupplierController  
  → Action: Details  
  → View: Views/Supplier/Details.cshtml

- GET /Supplier/Create  
  → Controller: SupplierController  
  → Action: Create  
  → View: Views/Supplier/Create.cshtml

- POST /Supplier/Create  
  → Controller: SupplierController  
  → Action: Create (POST)  
  → View: Views/Supplier/Create.cshtml

- GET /Supplier/Edit/{id}  
  → Controller: SupplierController  
  → Action: Edit  
  → View: Views/Supplier/Edit.cshtml

- POST /Supplier/Edit/{id}  
  → Controller: SupplierController  
  → Action: Edit (POST)  
  → View: Views/Supplier/Edit.cshtml

- GET /Supplier/Delete/{id}  
  → Controller: SupplierController  
  → Action: Delete  
  → View: Views/Supplier/Delete.cshtml

- POST /Supplier/Delete/{id}  
  → Controller: SupplierController  
  → Action: DeleteConfirmed  
  → Redirect: /Supplier/Index

---

## Notes

- ProductController uses custom attribute routing (`/products/...`)
- SupplierController and other controllers use default MVC routing
- View files are standard Razor views located in `Views/{Controller}/`
