using InteraCoop.Backend.Helpers;
using InteraCoop.Backend.UnitsOfWork.Interfaces;
using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;


namespace InteraCoop.Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUsersUnitOfWork _usersUnitOfWork;
        private readonly IFileStorage _fileStorage;

        public SeedDb(DataContext context, IUsersUnitOfWork usersUnitOfWork, IFileStorage fileStorage)
        {
            _context = context;
            _usersUnitOfWork = usersUnitOfWork;
            _fileStorage = fileStorage;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            //await CheckCountriesFullAsync();
            await CheckCountriesAsync();
            await CheckClientsAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Cristian", "Jaimes", "cristianjaimes@yopmail.com", "3008930134", "Calle Luna Calle sol", "User-1.png", UserType.Admin);
            await CheckUserAsync("1011", "Andres", "Castillo", "andrescastillol@yopmail.com", "3108940135", "Avenida Siempre Viva", "User-2.png", UserType.Admin);
            await CheckUserAsync("1012", "Harold", "Aguirre", "haroldaguirre@yopmail.com", "3208950136", "Carrera Estrella", "User-3.png", UserType.Analist);
            await CheckUserAsync("1013", "Mauricio", "Martinez", "mauriciomartinez@yopmail.com", "3308960137", "Calle Primavera", "User-4.png", UserType.Analist);
            await CheckUserAsync("1014", "Jorge", "Hernandez", "jorgeh@yopmail.com", "3408970138", "Calle Lluvia", "User-5.png", UserType.Employee);
            await CheckUserAsync("1015", "Marta", "Sanchez", "martas@yopmail.com", "3508980139", "Avenida Sol", "User-6.png", UserType.Employee);
            await CheckUserAsync("1016", "Luis", "Perez", "luiperez@yopmail.com", "3608990140", "Calle Viento", "User-7.png", UserType.Employee);
            await CheckUserAsync("1017", "Paula", "Rodriguez", "paular@yopmail.com", "3709000141", "Calle Nieve", "User-8.png", UserType.Employee);
            await CheckUserAsync("1018", "Carlos", "Gonzalez", "carlosg@yopmail.com", "3809010142", "Calle Estrella", "User-9.png", UserType.Employee);
            await CheckUserAsync("1019", "Ana", "Ramirez", "anar@yopmail.com", "3909020143", "Calle Luna", "User-10.png", UserType.Employee);
            await CheckUserAsync("1020", "Jose", "Torres", "joset@yopmail.com", "4009030144", "Avenida Sol", "User-11.png", UserType.Employee);
            await CheckUserAsync("1021", "Lucia", "Moreno", "luciam@yopmail.com", "4109040145", "Calle Viento", "User-12.png", UserType.Employee);
           
            await CheckInteractionsAsync();
            await CheckOpportunitiesAsync();
        }

        private async Task CheckCountriesFullAsync()
        {
            if (!_context.Countries.Any())
            {
                var countriesStatesCitiesSQLScript = File.ReadAllText("Data\\CountriesStatesCities.sql");
                await _context.Database.ExecuteSqlRawAsync(countriesStatesCitiesSQLScript);
            }
        }

        private async Task<User> CheckUserAsync(string document, string firstName, string LastName, string email, string phone, string address, string image, UserType userType)
        {
          var user = await _usersUnitOfWork.GetUserAsync(email);
            if (user == null)
            {
                var city = await _context.Cities.FirstOrDefaultAsync(x => x.Name == "Medellín");
                city ??= await _context.Cities.FirstOrDefaultAsync();

                var filePath = $"{Environment.CurrentDirectory}\\Images\\users\\{image}";
                var fileBytes = File.ReadAllBytes(filePath);
                var imagePath = await _fileStorage.SaveFileAsync(fileBytes, "jpg", "users");

                user = new User
                {
                    FirstName = firstName,
                    LastName = LastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = _context.Cities.FirstOrDefault(),
                    UserType = userType,
                    Photo = imagePath,
                };
                await _usersUnitOfWork.AddUserAsync(user, "123456");
                await _usersUnitOfWork.AddUserToRolesAsync(user, userType.ToString());

                var token = await _usersUnitOfWork.GenerateEmailConfirmationTokenAsync(user);
                await _usersUnitOfWork.ConfirmEmailAsync(user, token);

            }
            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _usersUnitOfWork.CheckRoleAsync(UserType.Admin.ToString());
            await _usersUnitOfWork.CheckRoleAsync(UserType.Analist.ToString());
            await _usersUnitOfWork.CheckRoleAsync(UserType.Employee.ToString());
        }

        private async Task CheckClientsAsync()
        {
            if (!_context.Clients.Any())
            {
                _context.Clients.Add(new Client { CityId = 1, Name = "Claudia", Document = 123496, DocumentType = DocumentType.CC.ToString(), Telephone = 3005378, Address = "Cll 80 #110-14", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });
                _context.Clients.Add(new Client { CityId =  2, Name = "Enrique", Document = 98765, DocumentType = DocumentType.CC.ToString(), Telephone = 2145379, Address = "Cll 13 #101-03", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });
                 _context.Clients.Add(new Client { CityId = 3, Name = "Manuel", Document = 13579, DocumentType = DocumentType.CC.ToString(), Telephone = 5329634, Address = "Cll 24 #50-32", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });
                 _context.Clients.Add(new Client { CityId =4, Name = "Maria", Document = 24680, DocumentType = DocumentType.CC.ToString(), Telephone = 2459807, Address = "Cll 50 #110-21", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });

                 _context.Clients.Add(new Client { CityId =5 , Name = "Gloria", Document = 123856, DocumentType = DocumentType.CC.ToString(), Telephone = 3015378, Address = "Cll 90 #120-14", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });
                 _context.Clients.Add(new Client { CityId =6, Name = "Andrea", Document = 98065, DocumentType = DocumentType.CE.ToString(), Telephone = 2155379, Address = "Cll 23 #111-03", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });
                 _context.Clients.Add(new Client { CityId =7 , Name = "Andres", Document = 17579, DocumentType = DocumentType.CC.ToString(), Telephone = 5339634, Address = "Cll 34 #60-32", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });
                 _context.Clients.Add(new Client { CityId =8, Name = "Julia", Document = 24630, DocumentType = DocumentType.RUT.ToString(), Telephone = 2559807, Address = "Cll 60 #210-21", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });

                 _context.Clients.Add(new Client { CityId =9, Name = "Manuela", Document = 923456, DocumentType = DocumentType.CC.ToString(), Telephone = 3205378, Address = "Cll 70 #50-14", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });
                 _context.Clients.Add(new Client { CityId =10, Name = "Isabela", Document = 18765, DocumentType = DocumentType.TI.ToString(), Telephone = 2445379, Address = "Cll 03 #91-03", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });
                 _context.Clients.Add(new Client { CityId = 11 , Name = "Lorena", Document = 13379, DocumentType = DocumentType.CC.ToString(), Telephone = 5529634, Address = "Cll 14 #40-32", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });
                 _context.Clients.Add(new Client { CityId = 12 , Name = "Blanca", Document = 24480, DocumentType = DocumentType.RUT.ToString(), Telephone = 2659807, Address = "Cll 40 #130-21", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });

                await CheckCampaignProducts();
                await CheckProductsAsync();
                await CheckOpportunitiesAsync();
                await CheckInteractionsAsync();            }
        }

        private async Task CheckCampaignProducts()
        {
            if (!_context.Campaigns.Any())
            {
                var products1 = new List<Product>
        {
            new Product { ProductType = "Activo", Name = "Tarjeta Visa", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 },
            new Product { ProductType = "Activo", Name = "Tarjeta Mastercard", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 }
        };
                var products2 = new List<Product>
        {
            new Product { ProductType = "Pasivo", Name = "Crédito Hipotecario Banco X", Quota = 0, Term = "20 años", Value = 5000000, Rate = 5.5 },
            new Product { ProductType = "Pasivo", Name = "Crédito Hipotecario Banco Y", Quota = 0, Term = "25 años", Value = 6000000, Rate = 6.0 }
        };
                var products3 = new List<Product>
        {
            new Product { ProductType = "Pasivo", Name = "Leasing Habitacional Banco Z", Quota = 0, Term = "15 años", Value = 7000000, Rate = 6.5 },
            new Product { ProductType = "Pasivo", Name = "Leasing Habitacional Banco W", Quota = 0, Term = "10 años", Value = 8000000, Rate = 7.0 }
        };
                var products4 = new List<Product>
        {
            new Product { ProductType = "Transaccional", Name = "Cuenta de Ahorros Banco A", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 },
            new Product { ProductType = "Transaccional", Name = "Fondo de Inversión Banco B", Quota = 0, Term = "5 años", Value = 10000000, Rate = 8.0 }
        };
                var products5 = new List<Product>
        {
            new Product { ProductType = "Activo", Name = "Seguro de Vida ABC", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 },
            new Product { ProductType = "Activo", Name = "Seguro Vehicular DEF", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 }
        };

                var products6 = new List<Product>
        {
            new Product { ProductType = "Transaccional", Name = "Depósito a Plazo Banco C", Quota = 0, Term = "1 año", Value = 5000000, Rate = 4.5 },
            new Product { ProductType = "Transaccional", Name = "Fondo Mutuo Banco D", Quota = 0, Term = "3 años", Value = 15000000, Rate = 7.0 }
        };
                var product7 = new Product { ProductType = "Activo", Name = "Cuenta de Ahorros Banco A", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 };
                var product8 = new Product { ProductType = "Pasivo", Name = "Seguro de Vida ABC", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 };
                var product9 = new Product { ProductType = "Transaccional", Name = "Depósito a Plazo Banco C", Quota = 0, Term = "1 año", Value = 5000000, Rate = 4.5 };
                var product10 = new Product { ProductType = "Transaccional", Name = "Fondo Mutuo Banco D", Quota = 0, Term = "3 años", Value = 15000000, Rate = 7.0 };
                AddCampaigns("CAM001", "Campaña de Tarjetas de Crédito", "Sin asignar", "Fidelización", "¡Solicita tu tarjeta de crédito con beneficios exclusivos!", DateTime.Now, DateTime.Now.AddDays(30), products1);
                AddCampaigns("CAM002", "Campaña de Créditos Hipotecarios", "Sin asignar", "Fidelización", "¡Adquiere tu casa propia con nuestras opciones de crédito hipotecario!", DateTime.Now.AddDays(35), DateTime.Now.AddDays(65), products2);
                AddCampaigns("CAM003", "Campaña de Leasing Habitacional", "Sin asignar", "Fidelización", "¡Haz realidad el sueño de tu hogar con nuestro leasing habitacional!", DateTime.Now.AddDays(70), DateTime.Now.AddDays(100), products3);
                AddCampaigns("CAM007", "Campaña de Productos de Ahorro e Inversión", "Asignada", "Fidelización", "¡Descubre nuestras opciones para hacer crecer tu dinero!", DateTime.Now.AddDays(105), DateTime.Now.AddDays(135), products4);
                AddCampaigns("CAM008", "Campaña de Seguros", "Asignada", "Captación de clientes", "¡Protege lo que más importa con nuestros seguros!", DateTime.Now.AddDays(140), DateTime.Now.AddDays(170), products5);
                AddCampaigns("CAM009", "Campaña de Inversiones", "Asignada", "Captación de clientes", "¡Haz crecer tu dinero con nuestras opciones de inversión!", DateTime.Now.AddDays(175), DateTime.Now.AddDays(205), products6);
                AddCampaigns("CAM004", "Campaña de Cuenta de Ahorros", "Asignada", "Captación de clientes", "¡Abre una cuenta de ahorros y empieza a ahorrar hoy mismo!", DateTime.Now, DateTime.Now.AddDays(30), products1 );
                AddCampaigns("CAM005", "Campaña de Seguro de Vida", "Vencida", "Captación de clientes", "¡Protege a tus seres queridos con nuestro seguro de vida!", DateTime.Now.AddDays(35), DateTime.Now.AddDays(65), products3);
                AddCampaigns("CAM006", "Campaña de Depósito a Plazo", "Vencida", "Productos financieros", "¡Haz crecer tu dinero con nuestro depósito a plazo!", DateTime.Now.AddDays(70), DateTime.Now.AddDays(100), products4 );
                AddCampaigns("CAM007", "Campaña de Fondo Mutuo", "Vencida", "Productos financieros", "¡Invierte en nuestro fondo mutuo y alcanza tus metas financieras!", DateTime.Now.AddDays(105), DateTime.Now.AddDays(135), products6 );
                await _context.SaveChangesAsync();
            }
        }

        private void AddCampaigns(string CampaignIdentifier, string name, string status, string campaignType, string description, DateTime startDate, DateTime endDate, List<Product> products)
        {
            Campaign campaign = new Campaign
            {
                CampaignIdentifier = CampaignIdentifier,
                CampaignName = name,
                Status = status,
                CampaignType = campaignType,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                ProductsList = new List<Product>()
            };
            foreach (var product in products)
            {
                campaign.ProductsList.Add(product);
            }
            _context.Campaigns.Add(campaign);
        }

        private async Task CheckProductsAsync()
        {
            if (!_context.Products.Any())
            {
                _context.Products.Add(new Product { ProductType = "Activo", Name = "Tarjeta Visa", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Activo", Name = "Tarjeta Mastercard", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Pasivo", Name = "Crédito Hipotecario Banco X", Quota = 0, Term = "20 años", Value = 5000000, Rate = 5.5 });
                _context.Products.Add(new Product { ProductType = "Pasivo", Name = "Leasing Habitacional Banco Y", Quota = 0, Term = "10 años", Value = 7000000, Rate = 6.0 });
                _context.Products.Add(new Product { ProductType = "Activo", Name = "Cuenta de Ahorros Banco Z", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Transaccional", Name = "Fondo de Inversión XYZ", Quota = 0, Term = "5 años", Value = 10000000, Rate = 8.0 });
                _context.Products.Add(new Product { ProductType = "Transaccional", Name = "Seguro de Vida ABC", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Transaccional", Name = "Seguro Vehicular DEF", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Transaccional", Name = "Fondo de Pensiones GHI", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Pasivo", Name = "Tarjeta Débito Banco W", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Pasivo", Name = "Crédito de Consumo Banco V", Quota = 0, Term = "5 años", Value = 2000000, Rate = 9.0 });
                _context.Products.Add(new Product { ProductType = "Activo", Name = "Cuenta Corriente Banco U", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Transaccional", Name = "Depósito a Plazo Banco T", Quota = 0, Term = "1 año", Value = 5000000, Rate = 4.5 });
                _context.Products.Add(new Product { ProductType = "Activo", Name = "Seguro Médico Empresa S", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Transaccional", Name = "Fondo Mutuo Empresa R", Quota = 0, Term = "3 años", Value = 15000000, Rate = 7.0 });
                _context.Products.Add(new Product { ProductType = "Activo", Name = "Tarjeta Prepagada Banco Q", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Pasivo", Name = "Crédito Automotriz Banco P", Quota = 0, Term = "5 años", Value = 4000000, Rate = 6.5 });
                _context.Products.Add(new Product { ProductType = "Pasivo", Name = "Crédito Educativo Banco O", Quota = 0, Term = "10 años", Value = 3000000, Rate = 5.0 });
                _context.Products.Add(new Product { ProductType = "Transaccional", Name = "Seguro de Hogar Empresa N", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Transaccional", Name = "Plan de Ahorro Banco M", Quota = 0, Term = "5 años", Value = 2000000, Rate = 3.0 });
                _context.Products.Add(new Product { ProductType = "Pasivo", Name = "Microcrédito Banco L", Quota = 0, Term = "3 años", Value = 1000000, Rate = 12.0 });
                _context.Products.Add(new Product { ProductType = "Pasivo", Name = "Crédito Agrícola Banco K", Quota = 0, Term = "7 años", Value = 8000000, Rate = 4.0 });
                _context.Products.Add(new Product { ProductType = "Transaccional", Name = "Seguro de Viaje Empresa J", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Transaccional", Name = "Fondo de Emergencia Banco I", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Transaccional", Name = "Bono de Inversión Empresa H", Quota = 0, Term = "10 años", Value = 5000000, Rate = 5.5 });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _ = _context.Countries.Add(new Country
                {
                    Name = "Colombia",
                    States =
                    [
                        new State()
                        {
                            Name = "Antioquia",
                            Cities = [
                                new() { Name = "Medellín" },
                                new() { Name = "Itagüí" },
                                new() { Name = "Envigado" },
                                new() { Name = "Bello" },
                                new() { Name = "Rionegro" },
                                new() { Name = "Marinilla" },
                            ]
                        },
                        new State()
                        {
                            Name = "Bogotá",
                            Cities = [
                                new() { Name = "Usaquen" },
                                new() { Name = "Champinero" },
                                new() { Name = "Santa fe" },
                                new() { Name = "Useme" },
                                new() { Name = "Bosa" },
                            ]
                        },
                    ]
                });

                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos",
                    States =
                    [
                        new State()
                        {
                            Name = "Florida",
                            Cities = [
                                new() { Name = "Orlando" },
                                new() { Name = "Miami" },
                                new() { Name = "Tampa" },
                                new() { Name = "Fort Lauderdale" },
                                new() { Name = "Key West" },
                            ]
                        },
                        new State()
                        {
                            Name = "Texas",
                            Cities = [
                                new() { Name = "Houston" },
                                new() { Name = "San Antonio" },
                                new() { Name = "Dallas" },
                                new() { Name = "Austin" },
                                new() { Name = "El Paso" },
                            ]
                        },
                    ]
                });

                _context.Countries.Add(new Country
                {
                    Name = "Mexico",
                    States =
                    [
                        new State()
                        {
                            Name = "Chiapas",
                            Cities = [
                                new() { Name = "Berriozábal" },
                                new() { Name = "Chiapa de Corzo" },
                                new() { Name = "Ocozocoautla de Espinosa" },
                                new() { Name = "Osumacinta" },
                                new() { Name = "San Fernando" },
                            ]
                        },
                    ]
                });

                _context.Countries.Add(new Country
                {
                    Name = "Argentina",
                    States =
                    [
                        new State()
                        {
                            Name = "Buenos Aires",
                            Cities = [
                                new() { Name = "Avellaneda" },
                                new() { Name = "La Plata" },
                                new() { Name = "Victoria" },
                                new() { Name = "San Isidro" },
                            ]
                        },
                    ]
                });

                _context.Countries.Add(new Country
                {
                    Name = "Venezuela",
                    States =
                    [
                        new State()
                        {
                            Name = "Carabobo",
                            Cities = [
                                new() { Name = "Valencia" },
                                new() { Name = "Naguanagua" },
                                new() { Name = "Puerto Cabello" },
                                new() { Name = "Tocuyito" },
                                new() { Name = "Guacara" },
                            ]
                        },
                        new State()
                        {
                            Name = "Mérida",
                            Cities = [
                                new() { Name = "El Vigía" },
                                new() { Name = "Tovar" },
                                new() { Name = "Ejido" },
                                new() { Name = "Lagunillas" },
                            ]
                        },
                    ]
                });

                _context.Countries.Add(new Country
                {
                    Name = "Panama",
                    States =
                    [
                        new State()
                        {
                            Name = "Colón",
                            Cities = [
                                new() { Name = "Chagres" },
                                new() { Name = "Donoso" },
                                new() { Name = "Portobelo" },
                                new() { Name = "Santa Isabel " },
                            ]
                        },
                        new State()
                        {
                            Name = "Chiriquí",
                            Cities = [
                                new() { Name = "Alanje" },
                                new() { Name = "Barú" },
                                new() { Name = "Boquerón" },
                                new() { Name = "Boquete" },
                                new() { Name = "Bugaba" },
                            ]
                        },
                    ]
                });

                _context.Countries.Add(new Country
                {
                    Name = "Germany",
                    States =
                    [
                        new State()
        {
            Name = "Bavaria",
            Cities = [
                new() { Name = "Munich" },
                new() { Name = "Nuremberg" },
                new() { Name = "Augsburg" },
                new() { Name = "Regensburg" },
                new() { Name = "Ingolstadt" },
            ]
        },
        new State()
        {
            Name = "North Rhine-Westphalia",
            Cities = [
                new() { Name = "Cologne" },
                new() { Name = "Düsseldorf" },
                new() { Name = "Dortmund" },
                new() { Name = "Essen" },
                new() { Name = "Duisburg" },
            ]
        },
    ]
                });

                _context.Countries.Add(new Country
                {
                    Name = "France",
                    States =
                    [
                        new State()
        {
            Name = "Île-de-France",
            Cities = [
                new() { Name = "Paris" },
                new() { Name = "Versailles" },
                new() { Name = "Boulogne-Billancourt" },
                new() { Name = "Saint-Denis" },
                new() { Name = "Argenteuil" },
            ]
        },
        new State()
        {
            Name = "Provence-Alpes-Côte d'Azur",
            Cities = [
                new() { Name = "Marseille" },
                new() { Name = "Nice" },
                new() { Name = "Toulon" },
                new() { Name = "Aix-en-Provence" },
                new() { Name = "Avignon" },
            ]
        },
    ]
                });

                _context.Countries.Add(new Country
                {
                    Name = "Russia",
                    States =
                    [
                        new State()
        {
            Name = "Moscow Oblast",
            Cities = [
                new() { Name = "Moscow" },
                new() { Name = "Zelenograd" },
                new() { Name = "Krasnogorsk" },
                new() { Name = "Lyubertsy" },
                new() { Name = "Korolyov" },
            ]
        },
        new State()
        {
            Name = "Saint Petersburg",
            Cities = [
                new() { Name = "Saint Petersburg" },
                new() { Name = "Kolpino" },
                new() { Name = "Kronshtadt" },
                new() { Name = "Pushkin" },
                new() { Name = "Peterhof" },
            ]
        },
    ]
                });

                _context.Countries.Add(new Country
                {
                    Name = "South Africa",
                    States =
                    [
                        new State()
        {
            Name = "Gauteng",
            Cities = [
                new() { Name = "Johannesburg" },
                new() { Name = "Pretoria" },
                new() { Name = "Soweto" },
                new() { Name = "Benoni" },
                new() { Name = "Randburg" },
            ]
        },
        new State()
        {
            Name = "Western Cape",
            Cities = [
                new() { Name = "Cape Town" },
                new() { Name = "Bellville" },
                new() { Name = "Stellenbosch" },
                new() { Name = "George" },
                new() { Name = "Paarl" },
            ]
        },
    ]
                });

                _context.Countries.Add(new Country
                {
                    Name = "Japan",
                    States =
                    [
                        new State()
        {
            Name = "Tokyo",
            Cities = [
                new() { Name = "Tokyo" },
                new() { Name = "Yokohama" },
                new() { Name = "Osaka" },
                new() { Name = "Nerima" },
                new() { Name = "Chiba" },
            ]
        },
        new State()
        {
            Name = "Osaka",
            Cities = [
                new() { Name = "Osaka" },
                new() { Name = "Sakai" },
                new() { Name = "Higashiosaka" },
                new() { Name = "Suita" },
                new() { Name = "Matsubara" },
            ]
        },
    ]
                });

                _context.Countries.Add(new Country
                {
                    Name = "Spain",
                    States =
                    [
                        new State()
        {
            Name = "Community of Madrid",
            Cities = [
                new() { Name = "Madrid" },
                new() { Name = "Móstoles" },
                new() { Name = "Alcalá de Henares" },
                new() { Name = "Fuenlabrada" },
                new() { Name = "Leganés" },
            ]
        },
        new State()
        {
            Name = "Catalonia",
            Cities = [
                new() { Name = "Barcelona" },
                new() { Name = "L'Hospitalet de Llobregat" },
                new() { Name = "Badalona" },
                new() { Name = "Terrassa" },
                new() { Name = "Sabadell" },
            ]
        },
    ]
                });
            }

		await _context.SaveChangesAsync();
		}

        private async Task CheckOpportunitiesAsync()
        {
            if (!_context.Opportunities.Any())
            {
                _context.Opportunities.Add(new Opportunity { Status = "En trámite", OpportunityObservations = "Revisar historial crediticio del cliente.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 09), CampaignId = 1, InteractionId =1 });
                _context.Opportunities.Add(new Opportunity { Status = "Formalizada", OpportunityObservations = "Cliente firmó contrato para préstamo personal.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 10), CampaignId = 2, InteractionId = 2 });
                _context.Opportunities.Add(new Opportunity { Status = "En trámite", OpportunityObservations = "Enviar documentos adicionales solicitados por el cliente.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 11), CampaignId = 3, InteractionId = 3 });
                _context.Opportunities.Add(new Opportunity { Status = "Formalizada", OpportunityObservations = "Cliente aceptó las condiciones del préstamo automotriz.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 12), CampaignId = 4, InteractionId = 4 });
                _context.Opportunities.Add(new Opportunity { Status = "En trámite", OpportunityObservations = "Llamar al cliente para confirmar detalles del seguro.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 13), CampaignId = 5, InteractionId = 5 });
                _context.Opportunities.Add(new Opportunity { Status = "Formalizada", OpportunityObservations = "Cliente aprobó el financiamiento para su negocio.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 14), CampaignId = 1, InteractionId = 6 });
                _context.Opportunities.Add(new Opportunity { Status = "En trámite", OpportunityObservations = "Contactar al cliente el 30 de mayo en horas de la mañana.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 05, 25), CampaignId = 2, InteractionId = 7 });
                _context.Opportunities.Add(new Opportunity { Status = "En trámite", OpportunityObservations = "Visitar al cliente en la oficina del cc plaza mayor el 20 de mayo 10:00 am.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 05, 26), CampaignId = 3, InteractionId = 8 });
                _context.Opportunities.Add(new Opportunity { Status = "En trámite", OpportunityObservations = "Cliente interesado en crédito hipotecario a 15 años.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 05, 27), CampaignId = 4, InteractionId = 9 });
                _context.Opportunities.Add(new Opportunity { Status = "Formalizada", OpportunityObservations = "Cliente aceptó producto, tarjeta de crédito.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 05, 28), CampaignId = 5, InteractionId = 10 });
                _context.Opportunities.Add(new Opportunity { Status = "Desestimada", OpportunityObservations = "Cliente no estuvo deacuerdo con oferta.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 05, 29), CampaignId = 1, InteractionId = 11 });
                _context.Opportunities.Add(new Opportunity { Status = "En trámite", OpportunityObservations = "Volver a llamar mañana despues de 9:00 am.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 05, 30), CampaignId = 2, InteractionId = 12 });
                _context.Opportunities.Add(new Opportunity { Status = "En trámite", OpportunityObservations = "Confirmar rebaja de intereses para cliente.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 05, 31), CampaignId = 3, InteractionId = 13 });
                _context.Opportunities.Add(new Opportunity { Status = "Desestimada", OpportunityObservations = "Oportunidad vencida por no contestación del cliente.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 01), CampaignId = 4, InteractionId = 14 });
                _context.Opportunities.Add(new Opportunity { Status = "En trámite", OpportunityObservations = "Pendiente de firma de pagares, agendar reunión con cliente el 20 de mayo.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 02), CampaignId = 5, InteractionId = 15 });
                _context.Opportunities.Add(new Opportunity { Status = "En trámite", OpportunityObservations = "Agendar firma de documentos con cliente, para créidto hipotecario.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 03), CampaignId = 1, InteractionId = 16 });
                _context.Opportunities.Add(new Opportunity { Status = "En trámite", OpportunityObservations = "Contactar al cliente el 01 de junio en horas de la tarde.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 04), CampaignId = 2, InteractionId = 17 });
                _context.Opportunities.Add(new Opportunity { Status = "En trámite", OpportunityObservations = "Ver posibiidad de aumentar el cupo de la tarjeta de crédito ofrecida.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 05), CampaignId = 3, InteractionId = 18 });
                _context.Opportunities.Add(new Opportunity { Status = "Desestimada", OpportunityObservations = "Cliente no interesado en los productos de la empresa.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 06), CampaignId = 4, InteractionId = 19 });
                _context.Opportunities.Add(new Opportunity { Status = "Desestimada", OpportunityObservations = "Ninguna, cliente no volvió a contestar.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 07), CampaignId = 5, InteractionId = 20 });
                await _context.SaveChangesAsync();
            }

        }

        private async Task CheckInteractionsAsync()
        {


            if (!_context.Interactions.Any())
            {
                _context.Interactions.Add(new Interaction { InteractionType = "Visita a cliente", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Home", ObservationsInteraction = "Cliente mostró interés en la tarjeta de crédito Visa. Se discutieron los beneficios y el proceso de aplicación.", Office = "Home", AuditDate = new DateTime(2024, 06, 08), UserDocument= "1014", ClientId = 1 });
                _context.Interactions.Add(new Interaction { InteractionType = "Visita a cliente", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Oficina", ObservationsInteraction = "Cliente interesado en el crédito hipotecario del Banco X. Se revisaron los términos y la tasa de interés.\"", Office = "Home", AuditDate = new DateTime(2024, 06, 08), UserDocument = "1015", ClientId = 2 });
                _context.Interactions.Add(new Interaction { InteractionType = "Visita a cliente", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Modulo 3", ObservationsInteraction = "Se discutió el seguro vehicular con el cliente. Cliente pidió más información sobre la cobertura.", Office = "Home", AuditDate = new DateTime(2024, 06, 08), UserDocument = "1014", ClientId = 3 });
                _context.Interactions.Add(new Interaction { InteractionType = "Visita a cliente", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Home", ObservationsInteraction = "Se ofreció el crédito de consumo del Banco V. Cliente pidió tiempo para revisar los términos.", Office = "Home", AuditDate = new DateTime(2024, 06, 08), UserDocument = "1015", ClientId = 4 });
                _context.Interactions.Add(new Interaction { InteractionType = "Visita a cliente", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Modulo 1", ObservationsInteraction = "Cliente mostró interés en la cuenta de ahorros del Banco Z. Se discutieron las ventajas y el proceso de apertura.", Office = "Home", AuditDate = new DateTime(2024, 06, 08), UserDocument = "1016", ClientId = 5 });
                _context.Interactions.Add(new Interaction { InteractionType = "Visita en oficina", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Home", ObservationsInteraction = "Se discutieron los beneficios del fondo de pensiones GHI. Cliente interesado en más detalles.", Office = "Home", AuditDate = new DateTime(2024, 06, 08), UserDocument = "1017", ClientId = 6 });
                _context.Interactions.Add(new Interaction { InteractionType = "Visita en oficina", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Modulo 2", ObservationsInteraction = "Se ofreció el crédito de consumo del Banco V. Cliente pidió tiempo para revisar los términos.", Office = "Home", AuditDate = new DateTime(2024, 06, 08), UserDocument = "1016", ClientId = 7 });
                _context.Interactions.Add(new Interaction { InteractionType = "Visita en oficina", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Modulo 4", ObservationsInteraction = "Cliente interesado en la tarjeta de débito del Banco W. Se explicó el proceso de emisión y las ventajas.\"", Office = "Home", AuditDate = new DateTime(2024, 06, 08), UserDocument = "1017", ClientId = 8 });
                _context.Interactions.Add(new Interaction { InteractionType = "Visita en oficina", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Modulo 3", ObservationsInteraction = "Cliente consultó sobre el seguro de vida ABC. Se proporcionaron detalles sobre las pólizas disponibles.", Office = "Home", AuditDate = new DateTime(2024, 06, 08), UserDocument = "1018", ClientId = 9 });
                _context.Interactions.Add(new Interaction { InteractionType = "Visita en oficina", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Home", ObservationsInteraction = "Cliente consultó sobre el seguro de vida ABC. Se proporcionaron detalles sobre las pólizas disponibles.", Office = "Home", AuditDate = new DateTime(2024, 06, 08), UserDocument = "1019", ClientId = 10 });
                _context.Interactions.Add(new Interaction { InteractionType = "Llamada entrante", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Oficina", ObservationsInteraction = "Se discutieron los beneficios del fondo de pensiones GHI. Cliente interesado en más detalles.", Office = "Home", AuditDate = new DateTime(2024, 06, 08), UserDocument = "1018", ClientId = 11 });
                _context.Interactions.Add(new Interaction { InteractionType = "Llamada entrante", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Modulo 1", ObservationsInteraction = "Cliente interesado en el fondo de inversión XYZ. Se revisaron los términos y la tasa de retorno esperada.", Office = "Home", AuditDate = new DateTime(2024, 06, 08), UserDocument = "1019", ClientId = 12 });
                _context.Interactions.Add(new Interaction { InteractionType = "Llamada entrante", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Oficina", ObservationsInteraction = "Se discutió el seguro vehicular con el cliente. Cliente pidió más información sobre la cobertura.", Office = "Home", AuditDate = new DateTime(2024, 06, 08), UserDocument = "1020", ClientId = 1 });
                _context.Interactions.Add(new Interaction { InteractionType = "Llamada entrante", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Modulo 1", ObservationsInteraction = "Se discutieron los beneficios del fondo de pensiones GHI. Cliente interesado en más detalles.", Office = "Home", AuditDate = new DateTime(2024, 06, 08), UserDocument = "1021", ClientId = 2 });
                _context.Interactions.Add(new Interaction { InteractionType = "Llamada entrante", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Home 3", ObservationsInteraction = "Cliente interesado en la tarjeta de débito del Banco W. Se explicó el proceso de emisión y las ventajas.\"", Office = "Home", AuditDate = new DateTime(2024, 06, 08), UserDocument = "1020", ClientId = 3 });
                _context.Interactions.Add(new Interaction { InteractionType = "Visita a cliente", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Home", ObservationsInteraction = "Cliente mostró interés en la cuenta de ahorros del Banco Z. Se discutieron las ventajas y el proceso de apertura.", Office = "Home", AuditDate = new DateTime(2024, 06, 08), UserDocument = "1021", ClientId = 4 });
                _context.Interactions.Add(new Interaction { InteractionType = "Visita a cliente", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Oficina", ObservationsInteraction = "Cliente mostró interés en la tarjeta de crédito Visa. Se discutieron los beneficios y el proceso de aplicación.", Office = "Home", AuditDate = new DateTime(2024, 06, 08), UserDocument = "1014", ClientId = 5 });
                _context.Interactions.Add(new Interaction { InteractionType = "Visita a cliente", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Modulo 1", ObservationsInteraction = "Cliente interesado en el crédito hipotecario del Banco X. Se revisaron los términos y la tasa de interés.\"", Office = "Home", AuditDate = new DateTime(2024, 06, 08), UserDocument = "1016", ClientId = 6 });
                _context.Interactions.Add(new Interaction { InteractionType = "Visita a cliente", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Oficina", ObservationsInteraction = "Cliente consultó sobre el seguro de vida ABC. Se proporcionaron detalles sobre las pólizas disponibles.", Office = "Home", AuditDate = new DateTime(2024, 06, 08), UserDocument = "1018", ClientId = 7 });
                _context.Interactions.Add(new Interaction { InteractionType = "Visita a cliente", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Modulo 1", ObservationsInteraction = "Cliente interesado en el fondo de inversión XYZ. Se revisaron los términos y la tasa de retorno esperada.", Office = "Home", AuditDate = new DateTime(2024, 06, 08), UserDocument = "1020", ClientId = 8 });
                await _context.SaveChangesAsync();
            }
        }

    }
}