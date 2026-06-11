using System.Net;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Xunit;
using VendingMachineApp.Data;
using VendingMachineApp.Data.Entities;
using VendingMachineApp.DTOs;

namespace VendingMachineApp.Tests
{
    public class SupplierApiTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory _factory;

        public SupplierApiTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetById_ShouldReturnSupplier_WhenExists()
        {
            // ARRANGE
            var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var supplier = new Supplier
            {
                Name = "Test Supplier",
                Email = "test@test.com",
                PhoneNumber = "123",
                Address = "Zagreb",
                ContactPerson = "Marko",
                RegistrationDate = DateTime.UtcNow
            };

            db.Suppliers.Add(supplier);
            await db.SaveChangesAsync(); // ove 2 linije koda ubacuju testnog dobavljača u bazu

            // ACT
            var response = await _client.GetAsync($"/api/supplierapi/{supplier.SupplierId}");

            // 🔥 DEBUG OVDJE
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);

            // ASSERT
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var dto = await response.Content.ReadFromJsonAsync<SupplierDTO>();
            Assert.NotNull(dto);
            Assert.Equal("Test Supplier", dto!.Name);
        }

        // GET ALL
        [Fact]
        public async Task GetAll_ShouldReturnSuppliers()
        {
            // ARRANGE
            var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            db.Suppliers.Add(new Supplier
            {
                Name = "Supplier 1",
                Email = "s1@test.com",
                PhoneNumber = "111",
                Address = "Zagreb",
                ContactPerson = "Marko",
                RegistrationDate = DateTime.UtcNow
            });

            db.Suppliers.Add(new Supplier
            {
                Name = "Supplier 2",
                Email = "s2@test.com",
                PhoneNumber = "222",
                Address = "Split",
                ContactPerson = "Ivan",
                RegistrationDate = DateTime.UtcNow
            });

            await db.SaveChangesAsync();

            // ACT
            var response = await _client.GetAsync("/api/supplierapi");

            // ASSERT
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var suppliers =
                await response.Content.ReadFromJsonAsync<List<SupplierDTO>>();

            Assert.NotNull(suppliers);
            Assert.True(suppliers.Count >= 2);
        }

        // GET BY ID - NOT FOUND
        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenSupplierDoesNotExist()
        {
            // ACT
            var response = await _client.GetAsync("/api/supplierapi/99999");

            // ASSERT
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        // CREATE - SUCCESS
        [Fact]
        public async Task Create_ShouldReturnCreated_WhenValidSupplier()
        {
            // ARRANGE
            var supplier = new Supplier
            {
                Name = "New Supplier",
                Email = "new@test.com",
                PhoneNumber = "123",
                Address = "Zagreb",
                ContactPerson = "Ana",
                RegistrationDate = DateTime.UtcNow
            };

            // ACT
            var response = await _client.PostAsJsonAsync("/api/supplierapi", supplier);

            // ASSERT
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var dto = await response.Content.ReadFromJsonAsync<SupplierDTO>();
            Assert.NotNull(dto);
            Assert.Equal("New Supplier", dto!.Name);
        }

        // CREATE - INVALID MODEL
        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenModelInvalid()
        {
            // ARRANGE (prazan objekt = invalid)
            var supplier = new Supplier();

            // ACT
            var response = await _client.PostAsJsonAsync("/api/supplierapi", supplier);

            // ASSERT
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        // UPDATE - SUCCESS
        [Fact]
        public async Task Update_ShouldModifySupplier_WhenExists()
        {
            // ARRANGE
            var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var supplier = new Supplier
            {
                Name = "Old Name",
                Email = "old@test.com",
                PhoneNumber = "111",
                Address = "Zagreb",
                ContactPerson = "Marko",
                RegistrationDate = DateTime.UtcNow
            };

            db.Suppliers.Add(supplier);
            await db.SaveChangesAsync();

            supplier.Name = "Updated Name";

            // ACT
            var response = await _client.PutAsJsonAsync(
                $"/api/supplierapi/{supplier.SupplierId}",
                supplier);

            // ASSERT
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var dto = await response.Content.ReadFromJsonAsync<SupplierDTO>();
            Assert.Equal("Updated Name", dto!.Name);
        }


        // UPDATE - NOT FOUND
        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenSupplierDoesNotExist()
        {
            var supplier = new Supplier
            {
                SupplierId = 99999,
                Name = "Does not exist"
            };

            var response = await _client.PutAsJsonAsync("/api/supplierapi/99999", supplier);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        
        //DELETE - SUCCESS
        [Fact]
        public async Task Delete_ShouldRemoveSupplier_WhenExists()
        {
            // ARRANGE
            var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var supplier = new Supplier
            {
                Name = "To Delete",
                Email = "del@test.com",
                PhoneNumber = "123",
                Address = "Zagreb",
                ContactPerson = "Marko",
                RegistrationDate = DateTime.UtcNow
            };

            db.Suppliers.Add(supplier);
            await db.SaveChangesAsync();

            // ACT
            var response = await _client.DeleteAsync($"/api/supplierapi/{supplier.SupplierId}");

            // ASSERT
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        // DELETE - NOT FOUND
        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenSupplierDoesNotExist()
        {
            var response = await _client.DeleteAsync("/api/supplierapi/99999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        // AUTHORIZATION TEST
        [Fact]
        public async Task Create_ShouldBeAllowedOnlyForAuthorizedUser()
        {
            // ARRANGE
                // Ovdje stvaramo poseban 'unauthorizedClient' koji nema lažnog korisnika
            var unauthorizedClient = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Nadjačavamo autentifikaciju praznom shemom kako bi klijent slao anonimne zahtjeve
                    services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = "AnonymousTest";
                        options.DefaultChallengeScheme = "AnonymousTest";
                    }).AddCookie("AnonymousTest");
                });
            }).CreateClient();

            var supplier = new Supplier
            {
                Name = "Auth Test",
                Email = "auth@test.com"
            };

            // ACT
            var response = await unauthorizedClient.PostAsJsonAsync("/api/supplierapi", supplier);

            // ASSERT
            Assert.NotEqual(HttpStatusCode.Created, response.StatusCode);
        }
    }
}