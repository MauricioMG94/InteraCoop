using InteraCoop.Backend.UnitsOfWork.Interfaces;
using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Enums;


namespace InteraCoop.Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUsersUnitOfWork _usersUnitOfWork;

        public SeedDb(DataContext context, IUsersUnitOfWork usersUnitOfWork)
        {
            _context = context;
            _usersUnitOfWork = usersUnitOfWork;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            await CheckCountriesAsync();
            await CheckClientsAsync();
            await CheckInteractionsAsync();
            await CheckOpportunitiesAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010","Harold","Aguirre", "harold@yopmail.com","3008930134","Calle Luna Calle sol", UserType.Admin);
           
        }

        private async Task<User> CheckUserAsync(string document, string firstName, string LastName, string email, string phone, string address, UserType userType)
        {
          var user = await _usersUnitOfWork.GetUserAsync(email);
            if (user == null)
            {
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
                };
                await _usersUnitOfWork.AddUserAsync(user, "123456");
                await _usersUnitOfWork.AddUserToRolesAsync(user, userType.ToString());
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
                _context.Clients.Add(new Client { CityId = 1, Name = "Claudia", Document = 123496, DocumentType = DocumentType.CC, Telephone = 3005378, Address = "Cll 80 #110-14", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });
                _context.Clients.Add(new Client { CityId =  2, Name = "Enrique", Document = 98765, DocumentType = DocumentType.CC, Telephone = 2145379, Address = "Cll 13 #101-03", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });
                 _context.Clients.Add(new Client { CityId = 3, Name = "Manuel", Document = 13579, DocumentType = DocumentType.CC, Telephone = 5329634, Address = "Cll 24 #50-32", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });
                 _context.Clients.Add(new Client { CityId =4, Name = "Maria", Document = 24680, DocumentType = DocumentType.CC, Telephone = 2459807, Address = "Cll 50 #110-21", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });

                 _context.Clients.Add(new Client { CityId =5 , Name = "Gloria", Document = 123856, DocumentType = DocumentType.CC, Telephone = 3015378, Address = "Cll 90 #120-14", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });
                 _context.Clients.Add(new Client { CityId =6, Name = "Andrea", Document = 98065, DocumentType = DocumentType.CC, Telephone = 2155379, Address = "Cll 23 #111-03", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });
                 _context.Clients.Add(new Client { CityId =7 , Name = "Andres", Document = 17579, DocumentType = DocumentType.CC, Telephone = 5339634, Address = "Cll 34 #60-32", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });
                 _context.Clients.Add(new Client { CityId =8, Name = "Julia", Document = 24630, DocumentType = DocumentType.CC, Telephone = 2559807, Address = "Cll 60 #210-21", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });

                 _context.Clients.Add(new Client { CityId =9, Name = "Manuela", Document = 923456, DocumentType = DocumentType.CC, Telephone = 3205378, Address = "Cll 70 #50-14", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });
                 _context.Clients.Add(new Client { CityId =10, Name = "Isabela", Document = 18765, DocumentType = DocumentType.CC, Telephone = 2445379, Address = "Cll 03 #91-03", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });
                 _context.Clients.Add(new Client { CityId = 11 , Name = "Lorena", Document = 13379, DocumentType = DocumentType.CC, Telephone = 5529634, Address = "Cll 14 #40-32", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });
                 _context.Clients.Add(new Client { CityId = 12 , Name = "Blanca", Document = 24480, DocumentType = DocumentType.CC, Telephone = 2659807, Address = "Cll 40 #130-21", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });

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
            new Product { ProductType = "Tarjeta de crédito", Name = "Tarjeta Visa", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 },
            new Product { ProductType = "Tarjeta de crédito", Name = "Tarjeta Mastercard", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 }
        };
                var products2 = new List<Product>
        {
            new Product { ProductType = "Crédito hipotecario", Name = "Crédito Hipotecario Banco X", Quota = 0, Term = "20 años", Value = 5000000, Rate = 5.5 },
            new Product { ProductType = "Crédito hipotecario", Name = "Crédito Hipotecario Banco Y", Quota = 0, Term = "25 años", Value = 6000000, Rate = 6.0 }
        };
                var products3 = new List<Product>
        {
            new Product { ProductType = "Leasing habitacional", Name = "Leasing Habitacional Banco Z", Quota = 0, Term = "15 años", Value = 7000000, Rate = 6.5 },
            new Product { ProductType = "Leasing habitacional", Name = "Leasing Habitacional Banco W", Quota = 0, Term = "10 años", Value = 8000000, Rate = 7.0 }
        };
                var products4 = new List<Product>
        {
            new Product { ProductType = "Cuenta de ahorros", Name = "Cuenta de Ahorros Banco A", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 },
            new Product { ProductType = "Fondo de inversión", Name = "Fondo de Inversión Banco B", Quota = 0, Term = "5 años", Value = 10000000, Rate = 8.0 }
        };
                var products5 = new List<Product>
        {
            new Product { ProductType = "Seguro de vida", Name = "Seguro de Vida ABC", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 },
            new Product { ProductType = "Seguro vehicular", Name = "Seguro Vehicular DEF", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 }
        };

                var products6 = new List<Product>
        {
            new Product { ProductType = "Depósito a plazo", Name = "Depósito a Plazo Banco C", Quota = 0, Term = "1 año", Value = 5000000, Rate = 4.5 },
            new Product { ProductType = "Fondo mutuo", Name = "Fondo Mutuo Banco D", Quota = 0, Term = "3 años", Value = 15000000, Rate = 7.0 }
        };
                var product7 = new Product { ProductType = "Cuenta de ahorros", Name = "Cuenta de Ahorros Banco A", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 };
                var product8 = new Product { ProductType = "Seguro de vida", Name = "Seguro de Vida ABC", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 };
                var product9 = new Product { ProductType = "Depósito a plazo", Name = "Depósito a Plazo Banco C", Quota = 0, Term = "1 año", Value = 5000000, Rate = 4.5 };
                var product10 = new Product { ProductType = "Fondo mutuo", Name = "Fondo Mutuo Banco D", Quota = 0, Term = "3 años", Value = 15000000, Rate = 7.0 };
                AddCampaigns("CAM001", "Campaña de Tarjetas de Crédito", "Activa", "Promoción", "¡Solicita tu tarjeta de crédito con beneficios exclusivos!", DateTime.Now, DateTime.Now.AddDays(30), products1);
                AddCampaigns("CAM002", "Campaña de Créditos Hipotecarios", "Activa", "Promoción", "¡Adquiere tu casa propia con nuestras opciones de crédito hipotecario!", DateTime.Now.AddDays(35), DateTime.Now.AddDays(65), products2);
                AddCampaigns("CAM003", "Campaña de Leasing Habitacional", "Activa", "Promoción", "¡Haz realidad el sueño de tu hogar con nuestro leasing habitacional!", DateTime.Now.AddDays(70), DateTime.Now.AddDays(100), products3);
                AddCampaigns("CAM007", "Campaña de Productos de Ahorro e Inversión", "Activa", "Promoción", "¡Descubre nuestras opciones para hacer crecer tu dinero!", DateTime.Now.AddDays(105), DateTime.Now.AddDays(135), products4);
                AddCampaigns("CAM008", "Campaña de Seguros", "Activa", "Promoción", "¡Protege lo que más importa con nuestros seguros!", DateTime.Now.AddDays(140), DateTime.Now.AddDays(170), products5);
                AddCampaigns("CAM009", "Campaña de Inversiones", "Activa", "Promoción", "¡Haz crecer tu dinero con nuestras opciones de inversión!", DateTime.Now.AddDays(175), DateTime.Now.AddDays(205), products6);
                AddCampaigns("CAM004", "Campaña de Cuenta de Ahorros", "Activa", "Promoción", "¡Abre una cuenta de ahorros y empieza a ahorrar hoy mismo!", DateTime.Now, DateTime.Now.AddDays(30), new List<Product> { product7 });
                AddCampaigns("CAM005", "Campaña de Seguro de Vida", "Activa", "Promoción", "¡Protege a tus seres queridos con nuestro seguro de vida!", DateTime.Now.AddDays(35), DateTime.Now.AddDays(65), new List<Product> { product8 });
                AddCampaigns("CAM006", "Campaña de Depósito a Plazo", "Activa", "Promoción", "¡Haz crecer tu dinero con nuestro depósito a plazo!", DateTime.Now.AddDays(70), DateTime.Now.AddDays(100), new List<Product> { product9 });
                AddCampaigns("CAM007", "Campaña de Fondo Mutuo", "Activa", "Promoción", "¡Invierte en nuestro fondo mutuo y alcanza tus metas financieras!", DateTime.Now.AddDays(105), DateTime.Now.AddDays(135), new List<Product> { product10 });
                await _context.SaveChangesAsync();
            }
        }

        private void AddCampaigns(string campaignId, string name, string status, string campaignType, string description, DateTime startDate, DateTime endDate, List<Product> products)
        {
            Campaign campaign = new Campaign
            {
                CampaignId = campaignId,
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
                _context.Products.Add(new Product { ProductType = "Tarjeta de crédito", Name = "Tarjeta Visa", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Tarjeta de crédito", Name = "Tarjeta Mastercard", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Crédito hipotecario", Name = "Crédito Hipotecario Banco X", Quota = 0, Term = "20 años", Value = 5000000, Rate = 5.5 });
                _context.Products.Add(new Product { ProductType = "Leasing habitacional", Name = "Leasing Habitacional Banco Y", Quota = 0, Term = "10 años", Value = 7000000, Rate = 6.0 });
                _context.Products.Add(new Product { ProductType = "Cuenta de ahorros", Name = "Cuenta de Ahorros Banco Z", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Fondo de inversión", Name = "Fondo de Inversión XYZ", Quota = 0, Term = "5 años", Value = 10000000, Rate = 8.0 });
                _context.Products.Add(new Product { ProductType = "Seguro de vida", Name = "Seguro de Vida ABC", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Seguro vehicular", Name = "Seguro Vehicular DEF", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Fondo de pensiones", Name = "Fondo de Pensiones GHI", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Tarjeta de débito", Name = "Tarjeta Débito Banco W", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Crédito de consumo", Name = "Crédito de Consumo Banco V", Quota = 0, Term = "5 años", Value = 2000000, Rate = 9.0 });
                _context.Products.Add(new Product { ProductType = "Cuenta corriente", Name = "Cuenta Corriente Banco U", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Depósito a plazo", Name = "Depósito a Plazo Banco T", Quota = 0, Term = "1 año", Value = 5000000, Rate = 4.5 });
                _context.Products.Add(new Product { ProductType = "Seguro médico", Name = "Seguro Médico Empresa S", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Fondo mutuo", Name = "Fondo Mutuo Empresa R", Quota = 0, Term = "3 años", Value = 15000000, Rate = 7.0 });
                _context.Products.Add(new Product { ProductType = "Tarjeta prepagada", Name = "Tarjeta Prepagada Banco Q", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Crédito automotriz", Name = "Crédito Automotriz Banco P", Quota = 0, Term = "5 años", Value = 4000000, Rate = 6.5 });
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
                var products1 = new List<Product>
                {
                    new Product { ProductType = "Tarjeta de crédito", Name = "Tarjeta Visa", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 },
                    new Product { ProductType = "Tarjeta de crédito", Name = "Tarjeta Mastercard", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 }
                };

                var campaign1 = new List<Campaign>
                {
                     new Campaign { CampaignId= "CAM001", CampaignName = "Campaña de Tarjetas de Crédito", Status = "Activa", CampaignType = "Promoción", Description = "¡Solicita tu tarjeta de crédito con beneficios exclusivos!", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(30), ProductsList = products1 }
                };

                _context.Opportunities.Add(new Opportunity { Status = "Abierta", OpportunityObservations = "Contactar el 30 de mayo.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 05, 25), CampaingsList = campaign1 });
                _context.Opportunities.Add(new Opportunity { Status = "Pendiente", OpportunityObservations = "Visitar en oficina el 20 de mayo.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 05, 26), CampaingsList = campaign1 });
                _context.Opportunities.Add(new Opportunity { Status = "Abierta", OpportunityObservations = "Cliente interesado en crédito hipotecario.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 05, 27), CampaingsList = campaign1 });
                _context.Opportunities.Add(new Opportunity { Status = "Cerrada", OpportunityObservations = "Cliente aceptó.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 05, 28), CampaingsList = campaign1 });
                _context.Opportunities.Add(new Opportunity { Status = "Desestimada", OpportunityObservations = "Cliente no deacuerdo con oferta.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 05, 29), CampaingsList = campaign1 });
                _context.Opportunities.Add(new Opportunity { Status = "Abierta", OpportunityObservations = "Volver a llamar mañana.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 05, 30), CampaingsList = campaign1 });
                _context.Opportunities.Add(new Opportunity { Status = "Abierta", OpportunityObservations = "Confirmar intereses para cliente.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 05, 31), CampaingsList = campaign1 });
                _context.Opportunities.Add(new Opportunity { Status = "Cerrada", OpportunityObservations = "Oportunidad vencida.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 01), CampaingsList = campaign1 });
                _context.Opportunities.Add(new Opportunity { Status = "Concretada", OpportunityObservations = "Pendiente de firma de pagares.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 02), CampaingsList = campaign1 });
                _context.Opportunities.Add(new Opportunity { Status = "Concretada", OpportunityObservations = "Agendar firma de documentos.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 03), CampaingsList = campaign1 });
                _context.Opportunities.Add(new Opportunity { Status = "Abierta", OpportunityObservations = "Contactar el 01 de junio", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 04) });
                _context.Opportunities.Add(new Opportunity { Status = "Abierta", OpportunityObservations = "Ver posibiidad de tarjeta de crédito.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 05), CampaingsList = campaign1 });
                _context.Opportunities.Add(new Opportunity { Status = "Cerrada", OpportunityObservations = "No interesado.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 06), CampaingsList = campaign1 });
                _context.Opportunities.Add(new Opportunity { Status = "Abierta", OpportunityObservations = "Ninguna.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 07), CampaingsList = campaign1 });
                _context.Opportunities.Add(new Opportunity { Status = "Desestimada", OpportunityObservations = "Cliente se cambió de país.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 08), CampaingsList = campaign1 });
                await _context.SaveChangesAsync();
            }

        }

        private async Task CheckInteractionsAsync()
        {
                    

            if (!_context.Interactions.Any())
            {

                var clients1 = new List<Client>
                {
                 new Client { CityId = 2, Name = "Marian", Document = 123456, DocumentType = DocumentType.CC, Telephone = 3005378, Address = "Cll 80 #110-14", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" }
                };

                _context.Interactions.Add(new Interaction { InteractionType = "Normal", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Home", ObservationsInteraction = "Funcion", Office = "Home", AuditDate = new DateTime(2024, 06, 08), AuditUser = "Harol", ClientsList = clients1 });

                _context.Interactions.Add(new Interaction { InteractionType = "Normal", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Oficina", ObservationsInteraction = "Funcion", Office = "Home", AuditDate = new DateTime(2024, 06, 08), AuditUser = "Ivan", ClientsList = clients1 });

                _context.Interactions.Add(new Interaction { InteractionType = "Normal", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Modulo 3", ObservationsInteraction = "Funcion", Office = "Home", AuditDate = new DateTime(2024, 06, 08), AuditUser = "Ivan", ClientsList = clients1 });

                _context.Interactions.Add(new Interaction { InteractionType = "Normal", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Home", ObservationsInteraction = "Funcion", Office = "Home", AuditDate = new DateTime(2024, 06, 08), AuditUser = "Cristian", ClientsList = clients1 });

                _context.Interactions.Add(new Interaction { InteractionType = "Normal", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Modulo 1", ObservationsInteraction = "Funcion", Office = "Home", AuditDate = new DateTime(2024, 06, 08), AuditUser = "Ivan", ClientsList = clients1 });

                _context.Interactions.Add(new Interaction { InteractionType = "Normal", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Home", ObservationsInteraction = "Funcion", Office = "Home", AuditDate = new DateTime(2024, 06, 08), AuditUser = "Harol", ClientsList = clients1 });

                _context.Interactions.Add(new Interaction { InteractionType = "Normal", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Modulo 2", ObservationsInteraction = "Funcion", Office = "Home", AuditDate = new DateTime(2024, 06, 08), AuditUser = "Ivan", ClientsList = clients1 });

                _context.Interactions.Add(new Interaction { InteractionType = "Normal", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Modulo 4", ObservationsInteraction = "Funcion", Office = "Home", AuditDate = new DateTime(2024, 06, 08), AuditUser = "Cristian", ClientsList = clients1 });

                _context.Interactions.Add(new Interaction { InteractionType = "Normal", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Modulo 3", ObservationsInteraction = "Funcion", Office = "Home", AuditDate = new DateTime(2024, 06, 08), AuditUser = "Harol", ClientsList = clients1 });

                _context.Interactions.Add(new Interaction { InteractionType = "Normal", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Home", ObservationsInteraction = "Funcion", Office = "Home", AuditDate = new DateTime(2024, 06, 08), AuditUser = "Ivan", ClientsList = clients1 });

                _context.Interactions.Add(new Interaction { InteractionType = "Normal", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Oficina", ObservationsInteraction = "Funcion", Office = "Home", AuditDate = new DateTime(2024, 06, 08), AuditUser = "Cristian", ClientsList = clients1 });

                _context.Interactions.Add(new Interaction { InteractionType = "Normal", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Modulo 1", ObservationsInteraction = "Funcion", Office = "Home", AuditDate = new DateTime(2024, 06, 08), AuditUser = "Harol", ClientsList = clients1 });

                _context.Interactions.Add(new Interaction { InteractionType = "Normal", InteractionCreationDate = DateTime.Now, StartDate = new DateTime(2024, 06, 08), EndDate = new DateTime(2024, 06, 08), Address = "Home 3", ObservationsInteraction = "Funcion", Office = "Home", AuditDate = new DateTime(2024, 06, 08), AuditUser = "Ivan", ClientsList = clients1 });
                await _context.SaveChangesAsync();
            }
        }

    }
}