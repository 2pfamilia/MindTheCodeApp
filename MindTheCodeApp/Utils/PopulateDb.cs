using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AppCore.Models.AuthModels;
using AppCore.Models.BookModels;
using AppCore.Models.OrderModels;

namespace MindTheCodeApp.Utils
{
    public class PopulateDb : BackgroundService
    {
        private readonly ILogger<PopulateDb> _logger;
        private readonly ApplicationDbContext _dbcontext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PopulateDb(ILogger<PopulateDb> logger, IServiceProvider serviceScopeFactory,
            IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _dbcontext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
            _webHostEnvironment = webHostEnvironment;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(1000);

            _logger.LogWarning("Database population started.");

            await StartPopulation(stoppingToken);

            _logger.LogWarning("Database population finished.");
        }

        private async Task StartPopulation(CancellationToken stoppingToken)
        {
            // Populate UserRoleEntity
            if (await _dbcontext.UserRoleEntity.AnyAsync())
                _logger.LogWarning("UserRole already populated.");
            else
            {
                _logger.LogWarning("UserRole population started.");
                await PopulateUserRole(stoppingToken);
                _logger.LogWarning("UserRole population finished.");
            }

            // Populate AddressInformationEntity
            if (await _dbcontext.AddressInformationEntity.AnyAsync())
                _logger.LogWarning("AddressInformationEntity already populated.");
            else
            {
                _logger.LogWarning("AddressInformationEntity population started.");
                await PopulateAddressInformationEntity(stoppingToken);
                _logger.LogWarning("AddressInformationEntity population finished.");
            }

            // Populate Userentity
            if (await _dbcontext.UserEntity.AnyAsync())
                _logger.LogWarning("User already populated.");
            else
            {
                _logger.LogWarning("User population started.");
                await PopulateUser(stoppingToken);
                _logger.LogWarning("User population finished.");
            }

            // Populate BookAuthorEntity
            if (await _dbcontext.BookAuthorEntity.AnyAsync())
                _logger.LogWarning("BookAuthor already populated.");
            else
            {
                _logger.LogWarning("BookAuthor population started.");
                await PopulateBookAuthor(stoppingToken);
                _logger.LogWarning("BookAuthor population finished.");
            }

            // Populate BookCategoryEntity
            if (await _dbcontext.BookCategoryEntity.AnyAsync())
                _logger.LogWarning("BookCategory already populated.");
            else
            {
                _logger.LogWarning("BookCategory population started.");
                await PopulateBookCategory(stoppingToken);
                _logger.LogWarning("BookCategory population finished.");
            }

            // Populate BookPhotoEntity
            if (await _dbcontext.BookPhotoEntity.AnyAsync())
                _logger.LogWarning("BookPhoto already populated.");
            else
            {
                _logger.LogWarning("BookPhoto population started.");
                await PopulateBookPhoto(stoppingToken);
                _logger.LogWarning("BookPhoto population finished.");
            }

            // Populate BookEntity
            if (await _dbcontext.BookEntity.AnyAsync())
                _logger.LogWarning("Book already populated.");
            else
            {
                _logger.LogWarning("Book population started.");
                await PopulateBook(stoppingToken);
                _logger.LogWarning("Book population finished.");
            }

            // Populate OrderEntity
            if (await _dbcontext.OrderEntity.AnyAsync())
                _logger.LogWarning("Order already populated.");
            else
            {
                _logger.LogWarning("Order population started.");
                await PopulateOrder(stoppingToken);
                _logger.LogWarning("Order population finished.");
            }

            // Populate BOrderDetailsEntity
            if (await _dbcontext.OrderDetailsEntity.AnyAsync())
                _logger.LogWarning("OrderdDetails already populated.");
            else
            {
                _logger.LogWarning("OrderdDetails population started.");
                await PopulateOrderDetails(stoppingToken);
                _logger.LogWarning("OrderdDetails population finished.");
            }
        }

        private async Task PopulateUserRole(CancellationToken stoppingToken)
        {
            var data = new List<UserRole>
            {
                new()
                {
                    Code = "admin", Title = "Administrator",
                    Description = "The Administrator role has the most privileges in the application",
                },
                new()
                {
                    Code = "reuser", Title = "Registered User",
                    Description = "The Registered User role has basic privileges in the application",
                },
            };

            await _dbcontext.AddRangeAsync(data);
            await _dbcontext.SaveChangesAsync();
        }

        private async Task PopulateAddressInformationEntity(CancellationToken stoppingToken)
        {
            var data = new List<AddressInformation>
            {
                new() { StreetAddress = "2623 Lyngvejen", City = "Hammel", PostalCode = "83453", Country = "Denmark", },
                new()
                {
                    StreetAddress = "1268 Manukau Road", City = "Tauranga", PostalCode = "72552",
                    Country = "New Zealand",
                },
                new()
                {
                    StreetAddress = "5718 Montée Saint-Barthélémy", City = "Sauge", PostalCode = "3137",
                    Country = "Switzerland",
                },
                new()
                {
                    StreetAddress = "8177 Jamaica", City = "Nuth", PostalCode = "0228 AT", Country = "Netherlands",
                },
                new()
                {
                    StreetAddress = "2111 Rua Santa Luzia ", City = "Volta Redonda", PostalCode = "55254",
                    Country = "Brazil",
                },
                new()
                {
                    StreetAddress = "5834 Calle de Tetuán", City = "Castellón de la Plana", PostalCode = "57845",
                    Country = "Spain",
                },
                new()
                {
                    StreetAddress = "1902 Ingemannsvej", City = "Askeby", PostalCode = "97457", Country = "Denmark",
                },
                new()
                {
                    StreetAddress = "6576 Wellington St", City = "Georgetown", PostalCode = "H7N 9F8",
                    Country = "Canada",
                },
                new()
                {
                    StreetAddress = "2855 Burgemeester Galleestraat", City = "Ophemert", PostalCode = "9642 EZ",
                    Country = "Netherlands",
                },
                new()
                {
                    StreetAddress = "6482 Septembervej", City = "Haslev", PostalCode = "86361", Country = "Denmark",
                },
                new() { StreetAddress = "4376 Grange Road", City = "Bray", PostalCode = "50273", Country = "Ireland", },
                new()
                {
                    StreetAddress = "2799 Rue de la Charité", City = "Poitiers", PostalCode = "96492",
                    Country = "France",
                },
                new()
                {
                    StreetAddress = "3622 Kristiansands gate", City = "Stryn", PostalCode = "3713", Country = "Norway",
                },
                new()
                {
                    StreetAddress = "3546 Vestermarken", City = "Nykøbing F", PostalCode = "84333", Country = "Denmark",
                },
            };

            await _dbcontext.AddRangeAsync(data);
            await _dbcontext.SaveChangesAsync();
        }

        private async Task PopulateUser(CancellationToken stoppingToken)
        {
            var data = new List<User>
            {
                new()
                {
                    FirstName = "Admin", LastName = "Admin", Email = "administrator@admin.org",
                    Birthdate = DateTime.Now, /*Username = "admin",*/ Password = "admin",
                    Phone = "2102713100",
                    Role = _dbcontext.UserRoleEntity.Single(role => role.Code.Equals("admin")),
                    AddressInformation = _dbcontext.AddressInformationEntity.Single(o => o.AddressInformationId == 1),
                },
                new()
                {
                    FirstName = "Summer", LastName = "Gates", Email = "summer@gates.org", Birthdate = DateTime.Now,
                    /*Username = "Summergates",*/ Password = "123456",
                    Phone = "2102713100",
                    Role = _dbcontext.UserRoleEntity.Single(role => role.Code.Equals("reuser")),
                    AddressInformation = _dbcontext.AddressInformationEntity.Single(o => o.AddressInformationId == 1),
                },
                new()
                {
                    FirstName = "Kael", LastName = "Kelly", Email = "kael@kelly.org", Birthdate = DateTime.Now,
                    /*Username = "Kaelkelly",*/ Password = "123456",
                    Phone = "2102713100",
                    Role = _dbcontext.UserRoleEntity.Single(role => role.Code.Equals("reuser")),
                    AddressInformation = _dbcontext.AddressInformationEntity.Single(o => o.AddressInformationId == 1),
                },
                new()
                {
                    FirstName = "Wilson", LastName = "Trevino", Email = "wilson@trevino.org", Birthdate = DateTime.Now,
                    /*Username = "Wilsontrevino",*/ Password = "123456",
                    Phone = "2102713100",
                    Role = _dbcontext.UserRoleEntity.Single(role => role.Code.Equals("reuser")),
                    AddressInformation = _dbcontext.AddressInformationEntity.Single(o => o.AddressInformationId == 1),
                },
            };

            await _dbcontext.AddRangeAsync(data);
            await _dbcontext.SaveChangesAsync();
        }

        private async Task PopulateBookAuthor(CancellationToken stoppingToken)
        {
            var data = new List<BookAuthor>
            {
                new()
                {
                    Name = "Emma Green",
                    Description =
                        "Emma Green is a bestselling romance author known for her steamy and emotional love stories. Her books often feature strong female leads and explore themes of empowerment and personal growth.",
                },
                new()
                {
                    Name = "Max Cooper",
                    Description =
                        "Max Cooper is a prolific science fiction author whose work spans a range of sub-genres, from hard sci-fi to space opera. His stories often grapple with complex scientific concepts and the ethical dilemmas that arise from new technologies.",
                },
                new()
                {
                    Name = "Harper Jameson",
                    Description =
                        "Harper Jameson is a mystery author whose books are known for their gripping plot twists and intricate puzzles. Her stories often feature strong female leads who use their intelligence and intuition to solve crimes.",
                },
                new()
                {
                    Name = "Alex Rodriguez",
                    Description =
                        "Alex Rodriguez is a thriller author who writes fast-paced and suspenseful stories that keep readers on the edge of their seats. His books often explore themes of power and corruption, and the dangers that arise when people are pushed to their limits.",
                },
                new()
                {
                    Name = "Sarah Patel",
                    Description =
                        "Sarah Patel is a fantasy author whose work is inspired by myth and legend from around the world. Her stories often feature richly imagined worlds populated by a wide variety of magical creatures and characters.",
                },
                new()
                {
                    Name = "Daniel Hill",
                    Description =
                        "Daniel Hill is a horror author known for his chilling and unsettling stories that delve into the darkest corners of the human psyche. His books often explore themes of fear, isolation, and the supernatural.",
                },
                new()
                {
                    Name = "Grace Kim",
                    Description =
                        "Grace Kim is a contemporary fiction author whose work explores the complexities of modern life and relationships. Her stories often feature diverse and relatable characters grappling with the challenges of love, family, and career.",
                },
                new()
                {
                    Name = "Jack Davis",
                    Description =
                        "Jack Davis is a historical fiction author whose books transport readers to a wide variety of time periods and places, from ancient Rome to the American Civil War. His stories often feature richly drawn characters and meticulously researched historical detail.",
                },
                new()
                {
                    Name = "Lily Chen",
                    Description =
                        "Lily Chen is a young adult author whose books explore the challenges of growing up and finding one's place in the world. Her stories often feature diverse and relatable characters grappling with issues like identity, self-confidence, and social justice.",
                },
                new()
                {
                    Name = "Jason Lee",
                    Description =
                        "Jason Lee is a non-fiction author whose work spans a wide variety of topics, from personal finance to self-improvement to travel. His books are known for their accessible and practical advice, as well as their engaging and conversational tone.",
                },
            };

            await _dbcontext.AddRangeAsync(data);
            await _dbcontext.SaveChangesAsync();
        }

        private async Task PopulateBookCategory(CancellationToken stoppingToken)
        {
            var data = new List<BookCategory>
            {
                new()
                {
                    Code = "adv",
                    Title = "Adventure",
                    Description =
                        "Adventure novels typically feature a hero or heroine who embarks on a journey or quest, facing obstacles and challenges along the way. These books can be set in exotic locations or in familiar settings, and often involve elements of danger, exploration, and discovery.\r\n\r\n"
                },
                new()
                {
                    Code = "bio",
                    Title = "Biography",
                    Description =
                        "Biographies are non-fiction books that explore the lives of real people, often historical figures or celebrities. These books can be written in a straightforward narrative style or can take a more creative approach, using elements of storytelling to bring the subject to life.\r\n\r\n"
                },
                new()
                {
                    Code = "clsc",
                    Title = "Classic Literature",
                    Description =
                        "Classic literature includes books that have stood the test of time and are widely regarded as masterpieces of the written word. These books can be from any genre, but are typically considered to be of exceptional quality, with enduring themes and timeless appeal.\r\n\r\n"
                },
                new()
                {
                    Code = "crm",
                    Title = "Crime",
                    Description =
                        "Crime novels are typically focused on the investigation of a crime, often a murder, and the efforts of law enforcement or private investigators to solve the case. These books can be gritty and realistic or more light-hearted and humorous, depending on the author and the tone they choose to take.\r\n\r\n"
                },
                new()
                {
                    Code = "fan",
                    Title = "Fantasy",
                    Description =
                        "Fantasy novels are set in imaginary worlds, often featuring magic, mythical creatures, and epic battles between good and evil. These books can be aimed at children, young adults, or adults, and can range from light and whimsical to dark and gritty.\r\n\r\n"
                },
                new()
                {
                    Code = "his",
                    Title = "Historical Fiction",
                    Description =
                        "Historical fiction is a genre that blends real historical events and people with fictional elements to create a compelling story. These books can be set in any time period or location, and often provide insight into what life was like in the past.\r\n\r\n"
                },
                new()
                {
                    Code = "hor",
                    Title = "Horror",
                    Description =
                        "Horror novels are designed to scare and unsettle the reader, often featuring supernatural elements such as ghosts, vampires, or zombies. These books can be graphic and violent, or rely more on psychological suspense to create a sense of unease.\r\n\r\n"
                },
                new()
                {
                    Code = "sci",
                    Title = "Science Fiction",
                    Description =
                        "Science fiction novels explore the possibilities of scientific and technological advances, often set in the future or in space. These books can range from hard science fiction, with a focus on scientific accuracy, to space opera, with epic battles and adventure.\r\n\r\n"
                },
                new()
                {
                    Code = "eco",
                    Title = "Economics",
                    Description =
                        "Economics books explore the principles of how societies allocate resources and make decisions, often focusing on topics like markets, trade, and finance. These books can be written for academic audiences or for a more general readership, and can provide insight into how the world works.\r\n\r\n"
                },
                new()
                {
                    Code = "env",
                    Title = "Environmental",
                    Description =
                        "Environmental books examine the relationship between humans and the natural world, often focusing on issues like climate change, conservation, and sustainability. These books can be written from a scientific or policy perspective, or can take a more personal approach to exploring the natural world.\r\n\r\n"
                },
                new()
                {
                    Code = "hum",
                    Title = "Humor",
                    Description =
                        "Humor books are designed to make readers laugh, often featuring satire, parody, or absurd situations. These books can be written in any genre, and can be aimed at children, young adults, or adults.\r\n\r\n"
                },
                new()
                {
                    Code = "mstr",
                    Title = "Mystery",
                    Description =
                        "Mystery novels feature a puzzle or crime that the main character must solve, often with twists and turns that keep the reader guessing until the end. These books can be set in any time period or location, and can range from cozy mysteries to gritty noir.\r\n\r\n"
                },
                new()
                {
                    Code = "ptr",
                    Title = "Poetry",
                    Description =
                        "Poetry books feature lyrical and evocative language, often exploring themes like love, loss, nature, and the human condition. These books can be written in any style or form, from sonnets to free verse, and can be aimed at any audience.\r\n\r\n"
                },
                new()
                {
                    Code = "spirit",
                    Title = "Spirituality",
                    Description =
                        "Spirituality books explore the inner life of the individual, often focusing on themes like mindfulness, meditation, and personal growth. These books can be written from a religious or secular perspective, and can provide guidance and inspiration to readers seeking to connect with their spiritual side.\r\n\r\n"
                },
                new()
                {
                    Code = "thrllr",
                    Title = "Thriller",
                    Description =
                        "Thriller novels are designed to keep readers on the edge of their seats, often featuring high stakes, danger, and suspense. These books can be set in any time period or location, and can range from political thrillers to psychological suspense.\r\n\r\n"
                },
                new()
                {
                    Code = "travel",
                    Title = "Travel",
                    Description =
                        "Travel books take readers on a journey to different parts of the world, often exploring the culture, history, and geography of a particular place. These books can be written from a personal or journalistic perspective, and can provide readers with a sense of adventure and exploration.\r\n\r\n"
                },
                new()
                {
                    Code = "ya",
                    Title = "Young Adult",
                    Description =
                        "Young adult books are aimed at readers between the ages of 12 and 18, often exploring themes like coming of age, identity, and relationships. These books can be written in any genre, from fantasy to contemporary, and can provide readers with a sense of connection and understanding as they navigate the challenges of adolescence.\r\n\r\n"
                },
            };

            await _dbcontext.AddRangeAsync(data);
            await _dbcontext.SaveChangesAsync();
        }

        private async Task PopulateBookPhoto(CancellationToken stoppingToken)
        {
            // var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "img/books", "placeholder.png");
            // string fileBytes = File.ReadAllText(imagePath);
            var filePath = "img/books/placeholder.png";

            var data = new List<BookPhoto>
            {
                new() { Title = "Placeholder", Description = "Placeholder", FilePath = filePath },
            };

            await _dbcontext.AddRangeAsync(data);
            await _dbcontext.SaveChangesAsync();
        }

        private async Task PopulateBook(CancellationToken stoppingToken)
        {
            var random = new Random();
            var categories = _dbcontext.BookCategoryEntity.ToList();
            var authors = _dbcontext.BookAuthorEntity.ToList();

            var data = new List<Book>
            {
                new()
                {
                    Title = "The Adventures of Tom Sawyer",
                    Description =
                        "A classic novel that follows the mischievous adventures of a young boy in a small town on the Mississippi River.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 5,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 9.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "Pride and Prejudice",
                    Description =
                        "A beloved classic by Jane Austen that follows the romantic pursuits of Elizabeth Bennet and her sisters in 19th-century England.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 3,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 7.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Girl with the Dragon Tattoo",
                    Description =
                        "A gripping thriller by Stieg Larsson that follows an investigative journalist and a tattooed hacker as they uncover dark secrets in the Swedish underworld.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 2,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 12.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Hitchhiker's Guide to the Galaxy",
                    Description =
                        "A comedic science fiction novel by Douglas Adams that follows the misadventures of an unwitting human and his alien friend as they travel through space.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 6,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 8.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Great Gatsby",
                    Description =
                        "A classic novel by F. Scott Fitzgerald that depicts the decadence and excess of the Jazz Age through the eyes of its enigmatic protagonist, Jay Gatsby.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 4,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 10.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Martian",
                    Description =
                        "A science fiction novel by Andy Weir that follows an astronaut's efforts to survive on Mars after being left behind by his crew.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 7,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 14.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "To Kill a Mockingbird",
                    Description =
                        "A Pulitzer Prize-winning novel by Harper Lee that explores racial injustice and the loss of innocence in a small town in the American South.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 3,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 12.50m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Picture of Dorian Gray",
                    Description =
                        "A philosophical novel by Oscar Wilde that tells the story of a young man who sells his soul for eternal youth and beauty, leading to his eventual downfall.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 2,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 10.75m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Catcher in the Rye",
                    Description =
                        "A coming-of-age novel by J.D. Salinger that follows the adventures of a teenage boy in New York City as he struggles with feelings of alienation and disillusionment.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 4,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 11.25m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Ethereal Realm of Unseen Shadows",
                    Description =
                        "A metaphysical journey into the unknown, exploring the limits of human consciousness and the nature of reality.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 3,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 12.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Luminous Depths of the Mind",
                    Description =
                        "A visionary work of fiction that delves into the uncharted depths of the human psyche, illuminating the darkest corners of the soul.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 1,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 9.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Enigmatic Symphony of the Cosmos",
                    Description =
                        "An epic journey through the vast expanse of the universe, exploring the mysteries of creation and the secrets of existence.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 5,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 15.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Arcane Matrix of Infinite Possibilities",
                    Description =
                        "A mind-bending exploration of the limitless potential of the human imagination, unlocking the secrets of the universe and the power of the mind.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 2,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 11.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Ephemeral Dreamscape of Reality",
                    Description =
                        "A surreal and ethereal journey through the mysteries of the subconscious mind, revealing the hidden truths of existence and the nature of reality.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 4,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 13.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Wine Bible: A Comprehensive Guide to Wine",
                    Description =
                        "A detailed guide to all aspects of wine, including its history, production, tasting, and pairing with food.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 3,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 25.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The World Atlas of Wine",
                    Description =
                        "An illustrated guide to the world's wine regions and the wines they produce, including detailed maps, tasting notes, and vintage charts.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 2,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 35.50m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "Wine Folly: The Essential Guide to Wine",
                    Description =
                        "An accessible and informative introduction to the world of wine, with detailed tasting notes, food pairings, and tips for selecting and storing wine.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 4,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 18.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Oxford Companion to Wine",
                    Description =
                        "An authoritative reference work on all aspects of wine, including its history, production, culture, and consumption.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 1,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 50.00m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "Wine Trails: 52 Perfect Weekends in Wine Country",
                    Description =
                        "A guide to the world's best wine regions and the wineries, restaurants, and other attractions to visit in each one.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 2,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 29.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Art of Wine Tasting",
                    Description =
                        "A practical guide to tasting and appreciating wine, with tips on how to develop your palate, evaluate wine quality, and identify different wine styles.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 3,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 14.95m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "Wine Science: Principles and Applications",
                    Description =
                        "A comprehensive overview of the science behind wine production and the factors that influence wine quality, including grape growing, fermentation, and aging.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 1,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 42.50m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Stronger Self: How to Build Your Physical and Mental Fitness",
                    Description =
                        "A guide to building your physical and mental fitness, including exercises and practices for both body and mind.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 3,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 12.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "Fit for Life: How to Live a Healthy and Active Lifestyle",
                    Description =
                        "A comprehensive guide to living a healthy and active lifestyle, with tips on exercise, nutrition, and mental wellness.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 5,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 15.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Fitness Mindset: How to Stay Motivated and Reach Your Goals",
                    Description =
                        "A practical guide to developing a positive mindset for fitness, with tips on setting and achieving your goals.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 2,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 9.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "Fitness Fusion: Combining Exercise and Yoga for a Holistic Workout",
                    Description =
                        "A guide to combining traditional exercise with yoga for a holistic workout that benefits both body and mind.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 4,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 11.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Fitness Recipe Book: Healthy and Delicious Meals for Active Living",
                    Description =
                        "A collection of healthy and delicious recipes designed to fuel an active lifestyle and support your fitness goals.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 2,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 13.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Ultimate Workout Guide: Strength, Cardio, and Flexibility for Optimal Fitness",
                    Description =
                        "A comprehensive guide to all aspects of fitness, including strength training, cardio, and flexibility exercises.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 5,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 17.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Fitness Fix: Simple and Effective Strategies for Getting in Shape",
                    Description =
                        "A no-nonsense guide to getting in shape, with simple and effective strategies for improving your fitness.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 3,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 10.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The History of Video Games",
                    Description =
                        "An in-depth exploration of the evolution of video games, from the early days of Pong and Space Invaders to the modern era of virtual reality and esports.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 3,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 24.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "Game Design Fundamentals",
                    Description =
                        "A comprehensive guide to the principles of game design, including topics such as player psychology, game mechanics, and level design.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 5,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 17.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Art of Video Game Storytelling",
                    Description =
                        "A critical examination of the narrative techniques used in video games, with case studies of iconic titles like The Last of Us, Bioshock, and Red Dead Redemption.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 2,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 12.50m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "Level Up Your Gaming Skills",
                    Description =
                        "A practical guide to improving your gameplay, covering topics such as strategy, tactics, and communication.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 4,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 14.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Business of Video Games",
                    Description =
                        "An insider's look at the video game industry, exploring topics such as game development, marketing, and distribution.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 3,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 21.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "Virtual Worlds and Real Consequences",
                    Description =
                        "An examination of the social, cultural, and ethical implications of online gaming, including topics such as addiction, toxicity, and online harassment.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 2,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 19.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Psychology of Video Game Addiction",
                    Description =
                        "A scientific analysis of the phenomenon of video game addiction, exploring its causes, symptoms, and treatment options.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 1,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 9.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Art of Video Game Design",
                    Description =
                        "A guide to the creative and artistic aspects of game development, with insights from industry professionals.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 5,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 16.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "Teamwork: The Power of Collaboration",
                    Description =
                        "A practical guide to building high-performing teams and achieving business success through effective collaboration.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 3,
                    Author = authors.OrderBy(α => random.Next()).First(),
                    Price = 12.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Five Dysfunctions of a Team",
                    Description =
                        "A leadership fable about a CEO who learns to build a cohesive and effective team by addressing the five dysfunctions that undermine teamwork.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 2,
                    Author = authors.OrderBy(α => random.Next()).First(),
                    Price = 15.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "Crucial Conversations: Tools for Talking When Stakes Are High",
                    Description =
                        "A guide to having productive and respectful conversations in high-stakes situations, such as those that can arise in a team setting.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 1,
                    Author = authors.OrderBy(α => random.Next()).First(),
                    Price = 19.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Culture Code: The Secrets of Highly Successful Groups",
                    Description =
                        "A study of the behaviors and practices that make certain groups and teams successful, and how leaders can foster those practices in their own organizations.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 4,
                    Author = authors.OrderBy(α => random.Next()).First(),
                    Price = 14.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "Team of Teams: New Rules of Engagement for a Complex World",
                    Description =
                        "A book about how a military task force transformed into a more flexible and effective team by embracing a more decentralized and networked approach.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 3,
                    Author = authors.OrderBy(α => random.Next()).First(),
                    Price = 17.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Ideal Team Player: How to Recognize and Cultivate The Three Essential Virtues",
                    Description =
                        "A guide to identifying and nurturing the qualities of being humble, hungry, and smart in team members, and how those qualities can contribute to success.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 2,
                    Author = authors.OrderBy(α => random.Next()).First(),
                    Price = 13.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "Team Building: Proven Strategies for Improving Team Performance",
                    Description =
                        "A comprehensive guide to team building, with practical advice on how to build a strong and effective team, overcome challenges, and achieve goals.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 2,
                    Author = authors.OrderBy(α => random.Next()).First(),
                    Price = 11.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "Clean Code",
                    Description =
                        "A practical guide to writing better code and creating software that is easier to maintain.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 3,
                    Author = authors.OrderBy(α => random.Next()).First(),
                    Price = 20.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "Code Complete",
                    Description =
                        "A comprehensive guide to software development, covering topics such as design, testing, and maintenance.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 5,
                    Author = authors.OrderBy(α => random.Next()).First(),
                    Price = 25.50m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Pragmatic Programmer",
                    Description =
                        "A guide to practical software development techniques and best practices, with a focus on creating maintainable and efficient code.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 2,
                    Author = authors.OrderBy(α => random.Next()).First(),
                    Price = 18.75m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "Head First Design Patterns",
                    Description =
                        "A beginner-friendly guide to object-oriented design patterns and their application in software development.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 4,
                    Author = authors.OrderBy(α => random.Next()).First(),
                    Price = 21.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "Refactoring: Improving the Design of Existing Code",
                    Description =
                        "A guide to improving the quality of existing code through refactoring, a process of restructuring code without changing its behavior.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 3,
                    Author = authors.OrderBy(α => random.Next()).First(),
                    Price = 23.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "Code: The Hidden Language of Computer Hardware and Software",
                    Description =
                        "A book that explains the basics of computer hardware and software, and how they work together to execute code.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 1,
                    Author = authors.OrderBy(α => random.Next()).First(),
                    Price = 16.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "Cracking the Coding Interview",
                    Description =
                        "A guide to preparing for coding interviews, covering common interview questions and providing strategies for solving them.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 5,
                    Author = authors.OrderBy(α => random.Next()).First(),
                    Price = 28.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Art of Computer Programming",
                    Description =
                        "A comprehensive guide to computer programming, covering topics such as algorithms, data structures, and computer architecture.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 2,
                    Author = authors.OrderBy(α => random.Next()).First(),
                    Price = 45.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title =
                        "The World Atlas of Coffee: From Beans to Brewing - Coffees Explored, Explained and Enjoyed",
                    Description =
                        "A comprehensive guide to coffee from around the world, covering everything from the different types of beans to the best brewing techniques.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 3,
                    Author = authors.OrderBy(α => random.Next()).First(),
                    Price = 15.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "Uncommon Grounds: The History of Coffee and How It Transformed Our World",
                    Description =
                        "An in-depth look at the history of coffee, including its origins, its spread around the world, and its impact on society and culture.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 2,
                    Author = authors.OrderBy(α => random.Next()).First(),
                    Price = 12.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "God in a Cup: The Obsessive Quest for the Perfect Coffee",
                    Description =
                        "A fascinating exploration of the world of specialty coffee, including the growers, roasters, and baristas who are obsessed with creating the perfect cup.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 4,
                    Author = authors.OrderBy(α => random.Next()).First(),
                    Price = 9.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Coffee Roaster's Companion",
                    Description =
                        "An essential guide for anyone who wants to learn the art and science of roasting coffee, including tips on sourcing beans, selecting equipment, and mastering different roasting techniques.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 2,
                    Author = authors.OrderBy(α => random.Next()).First(),
                    Price = 14.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Joy of Coffee: The Essential Guide to Buying, Brewing, and Enjoying",
                    Description =
                        "A beginner-friendly guide to coffee, covering everything from the basics of brewing to the more advanced techniques used by professional baristas.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 3,
                    Author = authors.OrderBy(α => random.Next()).First(),
                    Price = 11.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "Brew: Better Coffee At Home",
                    Description =
                        "A practical guide to brewing great coffee at home, with tips on selecting the right equipment, choosing the best beans, and mastering different brewing methods.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 5,
                    Author = authors.OrderBy(α => random.Next()).First(),
                    Price = 8.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title =
                        "The Professional Barista's Handbook: An Expert Guide to Preparing Espresso, Coffee, and Tea",
                    Description =
                        "An essential resource for anyone who wants to become a professional barista, covering everything from the basics of espresso preparation to latte art and customer service.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 2,
                    Author = authors.OrderBy(α => random.Next()).First(),
                    Price = 16.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Internet of Money",
                    Description =
                        "A book by Andreas M. Antonopoulos that explores the future of money in a digital age.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 5,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 13.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title =
                        "The Innovators: How a Group of Hackers, Geniuses, and Geeks Created the Digital Revolution",
                    Description =
                        "A book by Walter Isaacson that tells the story of the pioneers of the digital age, including Ada Lovelace, Alan Turing, and Steve Jobs.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 2,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 16.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Shallows: What the Internet Is Doing to Our Brains",
                    Description =
                        "A book by Nicholas Carr that explores how the internet is changing the way we think and learn.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 3,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 12.50m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "Code: The Hidden Language of Computer Hardware and Software",
                    Description =
                        "A book by Charles Petzold that explores the history and inner workings of computer programming.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 1,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 21.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Master Switch: The Rise and Fall of Information Empires",
                    Description =
                        "A book by Tim Wu that explores the history of communication technologies, from radio to the internet, and how they have been controlled by monopolies and governments.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 4,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 18.95m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "Coders at Work: Reflections on the Craft of Programming",
                    Description =
                        "A book by Peter Seibel that features interviews with 15 of the most influential computer programmers of our time, including Donald Knuth and Ken Thompson.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 2,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 14.99m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
                new()
                {
                    Title = "The Internet is Not the Answer",
                    Description =
                        "A book by Andrew Keen that argues that the internet has failed to live up to its promise of democratizing information and promoting creativity.",
                    Category = categories.OrderBy(c => random.Next()).First(),
                    Count = 3,
                    Author = authors.OrderBy(a => random.Next()).First(),
                    Price = 15.25m,
                    Photo = _dbcontext.BookPhotoEntity.First()
                },
            };

            await _dbcontext.AddRangeAsync(data);
            await _dbcontext.SaveChangesAsync();
        }

        private async Task PopulateOrder(CancellationToken stoppingToken)
        {
            var data = new List<Order>
            {
                new()
                {
                    User = _dbcontext.UserEntity.Single(u => u.UserId == 2),
                    AddressInformation = _dbcontext.UserEntity.Include(a => a.AddressInformation)
                        .Single(u => u.UserId == 2).AddressInformation,
                    Cost = 66.95m
                },
                new()
                {
                    User = _dbcontext.UserEntity.Single(u => u.UserId == 4),
                    AddressInformation = _dbcontext.UserEntity.Include(a => a.AddressInformation)
                        .Single(u => u.UserId == 4).AddressInformation,
                    Cost = 10.99m
                },
                new()
                {
                    User = _dbcontext.UserEntity.Single(u => u.UserId == 4),
                    AddressInformation = _dbcontext.UserEntity.Include(a => a.AddressInformation)
                        .Single(u => u.UserId == 4).AddressInformation,
                    Cost = 24.98m, Active = false, Fulfilled = true
                },
            };

            await _dbcontext.AddRangeAsync(data);
            await _dbcontext.SaveChangesAsync();
        }

        private async Task PopulateOrderDetails(CancellationToken stoppingToken)
        {
            var data = new List<OrderDetails>
            {
                new()
                {
                    Order = _dbcontext.OrderEntity.Single(o => o.OrderId == 1),
                    Book = _dbcontext.BookEntity.Single(b => b.BookId == 1),
                    Unitcost = (decimal)_dbcontext.BookEntity.Single(b => b.BookId == 1).Price,
                    TotalCost = (decimal)_dbcontext.BookEntity.Single(b => b.BookId == 1).Price, Count = 1
                },
                new()
                {
                    Order = _dbcontext.OrderEntity.Single(o => o.OrderId == 1),
                    Book = _dbcontext.BookEntity.Single(b => b.BookId == 4),
                    Unitcost = (decimal)_dbcontext.BookEntity.Single(b => b.BookId == 4).Price,
                    TotalCost = (decimal)_dbcontext.BookEntity.Single(b => b.BookId == 4).Price * 4, Count = 4
                },
                new()
                {
                    Order = _dbcontext.OrderEntity.Single(o => o.OrderId == 2),
                    Book = _dbcontext.BookEntity.Single(b => b.BookId == 13),
                    Unitcost = (decimal)_dbcontext.BookEntity.Single(b => b.BookId == 13).Price,
                    TotalCost = (decimal)_dbcontext.BookEntity.Single(b => b.BookId == 13).Price, Count = 1
                },
                new()
                {
                    Order = _dbcontext.OrderEntity.Single(o => o.OrderId == 3),
                    Book = _dbcontext.BookEntity.Single(b => b.BookId == 11),
                    Unitcost = (decimal)_dbcontext.BookEntity.Single(b => b.BookId == 11).Price,
                    TotalCost = (decimal)_dbcontext.BookEntity.Single(b => b.BookId == 11).Price, Count = 1
                },
                new()
                {
                    Order = _dbcontext.OrderEntity.Single(o => o.OrderId == 3),
                    Book = _dbcontext.BookEntity.Single(b => b.BookId == 5),
                    Unitcost = (decimal)_dbcontext.BookEntity.Single(b => b.BookId == 5).Price,
                    TotalCost = (decimal)_dbcontext.BookEntity.Single(b => b.BookId == 5).Price, Count = 1
                },

                new()
                {
                    Order = _dbcontext.OrderEntity.Single(o => o.OrderId == 1),
                    Book = _dbcontext.BookEntity.Single(b => b.BookId == 1),
                    Unitcost = _dbcontext.BookEntity.Single(b => b.BookId == 1).Price,
                    TotalCost = _dbcontext.BookEntity.Single(b => b.BookId == 1).Price, Count = 1
                },
                new()
                {
                    Order = _dbcontext.OrderEntity.Single(o => o.OrderId == 1),
                    Book = _dbcontext.BookEntity.Single(b => b.BookId == 4),
                    Unitcost = _dbcontext.BookEntity.Single(b => b.BookId == 4).Price,
                    TotalCost = _dbcontext.BookEntity.Single(b => b.BookId == 4).Price * 4, Count = 4
                },
                new()
                {
                    Order = _dbcontext.OrderEntity.Single(o => o.OrderId == 2),
                    Book = _dbcontext.BookEntity.Single(b => b.BookId == 14),
                    Unitcost = _dbcontext.BookEntity.Single(b => b.BookId == 14).Price,
                    TotalCost = _dbcontext.BookEntity.Single(b => b.BookId == 14).Price, Count = 1
                },
                new()
                {
                    Order = _dbcontext.OrderEntity.Single(o => o.OrderId == 3),
                    Book = _dbcontext.BookEntity.Single(b => b.BookId == 24),
                    Unitcost = _dbcontext.BookEntity.Single(b => b.BookId == 24).Price,
                    TotalCost = _dbcontext.BookEntity.Single(b => b.BookId == 24).Price, Count = 1
                },
                new()
                {
                    Order = _dbcontext.OrderEntity.Single(o => o.OrderId == 3),
                    Book = _dbcontext.BookEntity.Single(b => b.BookId == 4),
                    Unitcost = _dbcontext.BookEntity.Single(b => b.BookId == 4).Price,
                    TotalCost = _dbcontext.BookEntity.Single(b => b.BookId == 4).Price, Count = 1
                },
            };

            await _dbcontext.AddRangeAsync(data);
            await _dbcontext.SaveChangesAsync();
        }
    }
}