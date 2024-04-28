using InteraCoop.Shared.Entities;

namespace InteraCoop.Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckClientsAsync();
        }

        private async Task CheckClientsAsync()
        {
            if (!_context.Clients.Any())
            {
                _context.Clients.Add(new Client { City = 1, Name = "Claudia", Document = 123456, DocumentType = "CC", Telephone = 3005378, Address = "Cll 80 #110-14", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate= new DateTime(2024,04,27), AuditUser = "System" });
                _context.Clients.Add(new Client { City = 2, Name = "Enrique", Document = 98765, DocumentType = "CC", Telephone = 2145379, Address = "Cll 13 #101-03", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });
                _context.Clients.Add(new Client { City = 3, Name = "Manuel", Document = 13579, DocumentType = "CC", Telephone = 5329634, Address = "Cll 24 #50-32", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });
                _context.Clients.Add(new Client { City = 4, Name = "Maria", Document = 24680, DocumentType = "CC", Telephone = 2459807, Address = "Cll 50 #110-21", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });

                _context.Clients.Add(new Client { City = 1, Name = "Gloria", Document = 123856, DocumentType = "CC", Telephone = 3015378, Address = "Cll 90 #120-14", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });
                _context.Clients.Add(new Client { City = 2, Name = "Andrea", Document = 98065, DocumentType = "CC", Telephone = 2155379, Address = "Cll 23 #111-03", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });
                _context.Clients.Add(new Client { City = 3, Name = "Andres", Document = 17579, DocumentType = "CC", Telephone = 5339634, Address = "Cll 34 #60-32", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });
                _context.Clients.Add(new Client { City = 4, Name = "Julia", Document = 24630, DocumentType = "CC", Telephone = 2559807, Address = "Cll 60 #210-21", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });

                _context.Clients.Add(new Client { City = 1, Name = "Manuela", Document = 923456, DocumentType = "CC", Telephone = 3205378, Address = "Cll 70 #50-14", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });
                _context.Clients.Add(new Client { City = 2, Name = "Isabela", Document = 18765, DocumentType = "CC", Telephone = 2445379, Address = "Cll 03 #91-03", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });
                _context.Clients.Add(new Client { City = 3, Name = "Lorena", Document = 13379, DocumentType = "CC", Telephone = 5529634, Address = "Cll 14 #40-32", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser = "System" });
                _context.Clients.Add(new Client { City = 4, Name = "Blanca", Document = 24480, DocumentType = "CC", Telephone = 2659807, Address = "Cll 40 #130-21", RegistryDate = new DateTime(2024, 04, 27), AuditUpdate = new DateTime(2024, 04, 27), AuditUser ="System" });

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
    }
}