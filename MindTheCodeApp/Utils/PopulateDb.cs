using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AppCore.Models.AuthModels;
using AppCore.Models.BookModels;
using AppCore.Models.OrderModels;

namespace AppCore.Utils
{
    public class PopulateDb : BackgroundService
    {
        private readonly ILogger<PopulateDb> _logger;
        private readonly ApplicationDbContext _dbcontext;

        public PopulateDb(ILogger<PopulateDb> logger, IServiceProvider serviceScopeFactory)
        {
            _logger = logger;
            _dbcontext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
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
            /*
                TODO: Add population methods for models: 
                - BookPhoto
             */
            if (await _dbcontext.UserRoleEntity.AnyAsync())
                _logger.LogWarning("UserRole already populated.");
            else
            {
                _logger.LogWarning("UserRole population started.");
                await PopulateUserRole(stoppingToken);
                _logger.LogWarning("UserRole population finished.");
            }

            if (await _dbcontext.AddressInformationEntity.AnyAsync())
                _logger.LogWarning("AddressInformationEntity already populated.");
            else
            {
                _logger.LogWarning("AddressInformationEntity population started.");
                await PopulateAddressInformationEntity(stoppingToken);
                _logger.LogWarning("AddressInformationEntity population finished.");
            }

            if (await _dbcontext.UserEntity.AnyAsync())
                _logger.LogWarning("User already populated.");
            else
            {
                _logger.LogWarning("User population started.");
                await PopulateUser(stoppingToken);
                _logger.LogWarning("User population finished.");
            }

            if (await _dbcontext.BookAuthorEntity.AnyAsync())
                _logger.LogWarning("BookAuthor already populated.");
            else
            {
                _logger.LogWarning("BookAuthor population started.");
                await PopulateBookAuthor(stoppingToken);
                _logger.LogWarning("BookAuthor population finished.");
            }

            if (await _dbcontext.BookCategoryEntity.AnyAsync())
                _logger.LogWarning("BookCategory already populated.");
            else
            {
                _logger.LogWarning("BookCategory population started.");
                await PopulateBookCategory(stoppingToken);
                _logger.LogWarning("BookCategory population finished.");
            }

            if (await _dbcontext.BookEntity.AnyAsync())
                _logger.LogWarning("Book already populated.");
            else
            {
                _logger.LogWarning("Book population started.");
                await PopulateBook(stoppingToken);
                _logger.LogWarning("Book population finished.");
            }

            if (await _dbcontext.OrderEntity.AnyAsync())
                _logger.LogWarning("Order already populated.");
            else
            {
                _logger.LogWarning("Order population started.");
                await PopulateOrder(stoppingToken);
                _logger.LogWarning("Order population finished.");
            }

            if (await _dbcontext.OrderDetailsEntity.AnyAsync())
                _logger.LogWarning("OrderdDetails already populated.");
            else
            {
                _logger.LogWarning("OrderdDetails population started.");
                await PopulateOrderdDetails(stoppingToken);
                _logger.LogWarning("OrderdDetails population finished.");
            }
        }

        private async Task PopulateUserRole(CancellationToken stoppingToken)
        {
            var data = new List<UserRole> {
                new() { Code = "admin", Title = "Administrator", Description = "The Administrator role has the most privileges in the application", },
                new() { Code = "reuser", Title = "Registered User", Description = "The Registered User role has basic privileges in the application", },
            };

            await _dbcontext.AddRangeAsync(data);
            await _dbcontext.SaveChangesAsync();
        }

        private async Task PopulateAddressInformationEntity(CancellationToken stoppingToken)
        {
            var data = new List<AddressInformation>
            {
                new() { StreetAddress = "2623 Lyngvejen", City = "Hammel", PostalCode = "83453", Country = "Denmark", },
                new() { StreetAddress = "1268 Manukau Road", City = "Tauranga", PostalCode = "72552", Country = "New Zealand", },
                new() { StreetAddress = "5718 Montée Saint-Barthélémy", City = "Sauge", PostalCode = "3137", Country = "Switzerland", },
                new() { StreetAddress = "8177 Jamaica", City = "Nuth", PostalCode = "0228 AT", Country = "Netherlands", },
                new() { StreetAddress = "2111 Rua Santa Luzia ", City = "Volta Redonda", PostalCode = "55254", Country = "Brazil", },
                new() { StreetAddress = "5834 Calle de Tetuán", City = "Castellón de la Plana", PostalCode = "57845", Country = "Spain", },
                new() { StreetAddress = "1902 Ingemannsvej", City = "Askeby", PostalCode = "97457", Country = "Denmark", },
                new() { StreetAddress = "6576 Wellington St", City = "Georgetown", PostalCode = "H7N 9F8", Country = "Canada", },
                new() { StreetAddress = "2855 Burgemeester Galleestraat", City = "Ophemert", PostalCode = "9642 EZ", Country = "Netherlands", },
                new() { StreetAddress = "6482 Septembervej", City = "Haslev", PostalCode = "86361", Country = "Denmark", },
                new() { StreetAddress = "4376 Grange Road", City = "Bray", PostalCode = "50273", Country = "Ireland", },
                new() { StreetAddress = "2799 Rue de la Charité", City = "Poitiers", PostalCode = "96492", Country = "France", },
                new() { StreetAddress = "3622 Kristiansands gate", City = "Stryn", PostalCode = "3713", Country = "Norway", },
                new() { StreetAddress = "3546 Vestermarken", City = "Nykøbing F", PostalCode = "84333", Country = "Denmark", },
            };

            await _dbcontext.AddRangeAsync(data);
            await _dbcontext.SaveChangesAsync();
        }

        private async Task PopulateUser(CancellationToken stoppingToken)
        {
            var data = new List<User>
            {
                new() { FirstName = "Admin", LastName = "Admin", Email = "administrator@admin.org", Birthdate = DateTime.Now, Username = "admin", Password = "admin", Role = _dbcontext.UserRoleEntity.Single(role => role.Code.Equals("admin")), AddressInformation = _dbcontext.AddressInformationEntity.Single(o => o.AddressInformationId == 1), },
                new() { FirstName = "Summer", LastName = "Gates", Email = "summer@gates.org", Birthdate = DateTime.Now, Username = "Summergates", Password = "123456", Role = _dbcontext.UserRoleEntity.Single(role => role.Code.Equals("reuser")), AddressInformation = _dbcontext.AddressInformationEntity.Single(o => o.AddressInformationId == 1), },
                new() { FirstName = "Kael", LastName = "Kelly", Email = "kael@kelly.org", Birthdate = DateTime.Now, Username = "Kaelkelly", Password = "123456", Role = _dbcontext.UserRoleEntity.Single(role => role.Code.Equals("reuser")), AddressInformation = _dbcontext.AddressInformationEntity.Single(o => o.AddressInformationId == 1), },
                new() { FirstName = "Wilson", LastName = "Trevino", Email = "wilson@trevino.org", Birthdate = DateTime.Now, Username = "Wilsontrevino", Password = "123456", Role = _dbcontext.UserRoleEntity.Single(role => role.Code.Equals("reuser")), AddressInformation = _dbcontext.AddressInformationEntity.Single(o => o.AddressInformationId == 1), },
            };

            await _dbcontext.AddRangeAsync(data);
            await _dbcontext.SaveChangesAsync();
        }

        private async Task PopulateBookAuthor(CancellationToken stoppingToken)
        {
            var data = new List<BookAuthor>
            {
                new() { Name = "Emma Green", Description = "Emma Green is a bestselling romance author known for her steamy and emotional love stories. Her books often feature strong female leads and explore themes of empowerment and personal growth.", },
                new() { Name = "Max Cooper", Description = "Max Cooper is a prolific science fiction author whose work spans a range of sub-genres, from hard sci-fi to space opera. His stories often grapple with complex scientific concepts and the ethical dilemmas that arise from new technologies.", },
                new() { Name = "Harper Jameson", Description = "Harper Jameson is a mystery author whose books are known for their gripping plot twists and intricate puzzles. Her stories often feature strong female leads who use their intelligence and intuition to solve crimes.", },
                new() { Name = "Alex Rodriguez", Description = "Alex Rodriguez is a thriller author who writes fast-paced and suspenseful stories that keep readers on the edge of their seats. His books often explore themes of power and corruption, and the dangers that arise when people are pushed to their limits.", },
                new() { Name = "Sarah Patel", Description = "Sarah Patel is a fantasy author whose work is inspired by myth and legend from around the world. Her stories often feature richly imagined worlds populated by a wide variety of magical creatures and characters.", },
                new() { Name = "Daniel Hill", Description = "Daniel Hill is a horror author known for his chilling and unsettling stories that delve into the darkest corners of the human psyche. His books often explore themes of fear, isolation, and the supernatural.", },
                new() { Name = "Grace Kim", Description = "Grace Kim is a contemporary fiction author whose work explores the complexities of modern life and relationships. Her stories often feature diverse and relatable characters grappling with the challenges of love, family, and career.", },
                new() { Name = "Jack Davis", Description = "Jack Davis is a historical fiction author whose books transport readers to a wide variety of time periods and places, from ancient Rome to the American Civil War. His stories often feature richly drawn characters and meticulously researched historical detail.", },
                new() { Name = "Lily Chen", Description = "Lily Chen is a young adult author whose books explore the challenges of growing up and finding one's place in the world. Her stories often feature diverse and relatable characters grappling with issues like identity, self-confidence, and social justice.", },
                new() { Name = "Jason Lee", Description = "Jason Lee is a non-fiction author whose work spans a wide variety of topics, from personal finance to self-improvement to travel. His books are known for their accessible and practical advice, as well as their engaging and conversational tone.", },
            };

            await _dbcontext.AddRangeAsync(data);
            await _dbcontext.SaveChangesAsync();
        }
        private async Task PopulateBookCategory(CancellationToken stoppingToken)
        {
            var data = new List<BookCategory>
            {
                new() { Code = "crm", Title = "Crime", Description = "Crime novels are typically focused on the investigation of a crime, often a murder, and the efforts of law enforcement or private investigators to solve the case. These books can be gritty and realistic or more light-hearted and humorous, depending on the author and the tone they choose to take.\r\n\r\n" },
                new() { Code = "fnt", Title = "Fantasy", Description = "Fantasy novels are set in imaginary worlds that often feature magic, mythical creatures, and supernatural powers. These stories can take place in a variety of settings, from medieval castles to futuristic cities, and often include epic battles between good and evil.\r\n\r\n" },
                new() { Code = "msr", Title = "Mystery", Description = "Mystery novels focus on solving a puzzle or crime, often with a detective or amateur sleuth as the main character. These stories can be set in a variety of settings, from small towns to big cities, and can feature a wide range of crimes and motives.\r\n\r\n" },
                new() { Code = "rnc", Title = "Romance", Description = "Romance novels focus on the development of a romantic relationship between two people, often with obstacles and challenges to overcome. These stories can be set in a variety of time periods and settings, from modern-day cities to historical romances in far-off lands.\r\n\r\n" },
                new() { Code = "scf", Title = "Sci-fi", Description = "Science fiction novels are often set in the future or in alternative realities and can explore a range of scientific concepts and technologies. These books can range from hard sci-fi, which is focused on scientific accuracy and realism, to soft sci-fi, which is more focused on the social and cultural implications of new technologies.\r\n\r\n\r\n\r\n" },
            };

            await _dbcontext.AddRangeAsync(data);
            await _dbcontext.SaveChangesAsync();
        }

        private async Task PopulateBook(CancellationToken stoppingToken)
        {
            var data = new List<Book>
            {
                new() { Title = "The Name of the Wind", Description = "An epic fantasy novel about a legendary wizard who recounts his life story, from humble beginnings to epic battles against dark forces.", Category = _dbcontext.BookCategoryEntity.Single(b => b.Title == "Fantasy"), Count = 18, Author = _dbcontext.BookAuthorEntity.Single(ba => ba.Name == "Max Cooper"), Price = 14.99m },
                new() { Title = "The Da Vinci Code", Description = "A thrilling mystery novel that follows a symbologist and a cryptologist as they race to uncover a secret that could shake the foundations of Christianity.", Category = _dbcontext.BookCategoryEntity.Single(b => b.Title == "Mystery"), Count = 10, Author = _dbcontext.BookAuthorEntity.Single(ba => ba.Name == "Daniel Hill"), Price = 14.99m },
                new() { Title = "A Song of Ice and Fire", Description = "A gripping fantasy series that takes place in a vast, medieval-inspired world filled with political intrigue, war, and magic.", Category = _dbcontext.BookCategoryEntity.Single(b => b.Title == "Fantasy"), Count = 8, Author = _dbcontext.BookAuthorEntity.Single(ba => ba.Name == "Grace Kim"), Price = 19.99m },
                new() { Title = "Gone Girl", Description = "A psychological thriller that follows a husband and wife as their marriage unravels and a mystery unfolds.", Category = _dbcontext.BookCategoryEntity.Single(b => b.Title == "Mystery"), Count = 5, Author = _dbcontext.BookAuthorEntity.Single(ba => ba.Name == "Lily Chen"), Price = 12.99m },
                new() { Title = "The Time Traveler's Wife", Description = "A moving love story about a man with a rare genetic disorder that causes him to time travel unpredictably, and the woman who loves him through it all.", Category = _dbcontext.BookCategoryEntity.Single(b => b.Title == "Romance"), Count = 15, Author = _dbcontext.BookAuthorEntity.Single(ba => ba.Name == "Sarah Patel"), Price = 10.99m },
                new() { Title = "Dune", Description = "A classic science fiction novel set in a distant future where noble families fight for control of a desert planet called Arrakis, which holds the key to the galaxy's most valuable resource.", Category = _dbcontext.BookCategoryEntity.Single(b => b.Title == "Sci-Fi"), Count = 12, Author = _dbcontext.BookAuthorEntity.Single(ba => ba.Name == "Jason Lee"), Price = 11.99m },
                new() { Title = "The Girl with the Dragon Tattoo", Description = "A dark and suspenseful crime novel that follows an investigative journalist and a young computer hacker as they work together to solve a decades-old mystery.", Category = _dbcontext.BookCategoryEntity.Single(b => b.Title == "Crime"), Count = 7, Author = _dbcontext.BookAuthorEntity.Single(ba => ba.Name == "Jack Davis"), Price = 13.99m },
                new() { Title = "The Fellowship of the Ring", Description = "The first book in J.R.R. Tolkien's beloved fantasy series, which follows a hobbit named Frodo as he sets out on a perilous journey to destroy a powerful ring that could bring about the end of the world.", Category = _dbcontext.BookCategoryEntity.Single(b => b.Title == "Fantasy"), Count = 11, Author = _dbcontext.BookAuthorEntity.Single(ba => ba.Name == "Alex Rodriguez"), Price = 16.99m },
                new() { Title = "The Hound of the Baskervilles", Description = "A classic mystery novel featuring the famous detective Sherlock Holmes, who is called upon to solve the case of a cursed family and a terrifying hound that stalks their estate.", Category = _dbcontext.BookCategoryEntity.Single(b => b.Title == "Mystery"), Count = 9, Author = _dbcontext.BookAuthorEntity.Single(ba => ba.Name == "Daniel Hill"), Price = 9.99m },
                new() { Title = "Pride and Prejudice", Description = "A timeless romance novel that follows the trials and tribulations of the Bennet family, particularly the headstrong Elizabeth and the proud Mr. Darcy.", Category = _dbcontext.BookCategoryEntity.Single(b => b.Title == "Romance"), Count = 14, Author = _dbcontext.BookAuthorEntity.Single(ba => ba.Name == "Daniel Hill"), Price = 8.99m },
                new() { Title = "Ender's Game", Description = "A gripping science fiction novel that follows a young boy named Ender as he is trained to become a military commander in a war against an alien race.", Category = _dbcontext.BookCategoryEntity.Single(b => b.Title == "Sci-Fi"), Count = 6, Author = _dbcontext.BookAuthorEntity.Single(ba => ba.Name == "Daniel Hill"), Price = 11.99m },
                new() { Title = "The Silent Patient", Description = "A psychological thriller that follows a therapist's efforts to uncover the truth behind a patient's mysterious silence, which began after she was accused of murdering her husband.", Category = _dbcontext.BookCategoryEntity.Single(b => b.Title == "Mystery"), Count = 8, Author = _dbcontext.BookAuthorEntity.Single(ba => ba.Name == "Harper Jameson"), Price = 12.99m },
                new() { Title = "The Hunger Games", Description = "A dystopian science fiction novel set in a future where teenagers are forced to compete in a brutal televised battle to the death.", Category = _dbcontext.BookCategoryEntity.Single(b => b.Title == "Sci-Fi"), Count = 10, Author = _dbcontext.BookAuthorEntity.Single(ba => ba.Name == "Emma Green"), Price = 10.99m },
            };

            await _dbcontext.AddRangeAsync(data);
            await _dbcontext.SaveChangesAsync();
        }

        private async Task PopulateOrder(CancellationToken stoppingToken)
        {
            var data = new List<Order>
            {
                new() { User = _dbcontext.UserEntity.Single(u => u.UserId == 2), AddressInformation = _dbcontext.UserEntity.Include(a => a.AddressInformation).Single(u => u.UserId == 2).AddressInformation, Cost = 66.95m},
                new() { User = _dbcontext.UserEntity.Single(u => u.UserId == 4), AddressInformation = _dbcontext.UserEntity.Include(a => a.AddressInformation).Single(u => u.UserId == 4).AddressInformation, Cost = 10.99m},
                new() { User = _dbcontext.UserEntity.Single(u => u.UserId == 4), AddressInformation = _dbcontext.UserEntity.Include(a => a.AddressInformation).Single(u => u.UserId == 4).AddressInformation, Cost = 24.98m, Active = false, Fulfilled = true},
            };

            await _dbcontext.AddRangeAsync(data);
            await _dbcontext.SaveChangesAsync();
        }

        private async Task PopulateOrderdDetails(CancellationToken stoppingToken)
        {
            var data = new List<OrderDetails>
            {
                new() { Order = _dbcontext.OrderEntity.Single(o => o.OrderId == 1), Book = _dbcontext.BookEntity.Single(b => b.BookId == 1),  Unitcost = _dbcontext.BookEntity.Single(b => b.BookId == 1).Price, TotalCost = _dbcontext.BookEntity.Single(b => b.BookId == 1).Price, Count = 1},
                new() { Order = _dbcontext.OrderEntity.Single(o => o.OrderId == 1), Book = _dbcontext.BookEntity.Single(b => b.BookId == 4),  Unitcost = _dbcontext.BookEntity.Single(b => b.BookId == 4).Price, TotalCost = _dbcontext.BookEntity.Single(b => b.BookId == 4).Price * 4, Count = 4},
                new() { Order = _dbcontext.OrderEntity.Single(o => o.OrderId == 2), Book = _dbcontext.BookEntity.Single(b => b.BookId == 13),  Unitcost = _dbcontext.BookEntity.Single(b => b.BookId == 13).Price, TotalCost = _dbcontext.BookEntity.Single(b => b.BookId == 13).Price, Count = 1},
                new() { Order = _dbcontext.OrderEntity.Single(o => o.OrderId == 3), Book = _dbcontext.BookEntity.Single(b => b.BookId == 11),  Unitcost = _dbcontext.BookEntity.Single(b => b.BookId == 11).Price, TotalCost = _dbcontext.BookEntity.Single(b => b.BookId == 11).Price, Count = 1},
                new() { Order = _dbcontext.OrderEntity.Single(o => o.OrderId == 3), Book = _dbcontext.BookEntity.Single(b => b.BookId == 4),  Unitcost = _dbcontext.BookEntity.Single(b => b.BookId == 4).Price, TotalCost = _dbcontext.BookEntity.Single(b => b.BookId == 4).Price, Count = 1},
            };

            await _dbcontext.AddRangeAsync(data);
            await _dbcontext.SaveChangesAsync();
        }

    }
}
