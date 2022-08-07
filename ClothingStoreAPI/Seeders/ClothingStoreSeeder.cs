using ClothingStoreAPI.Entities;
using ClothingStoreAPI.Entities.DbContextConfigure;

namespace ClothingStoreAPI.Seeders
{
    public class ClothingStoreSeeder
    {
        private readonly ClothingStoreDbContext dbContext;

        public ClothingStoreSeeder(ClothingStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Seed()
        {
            if (dbContext.Database.CanConnect())
            {
                if (!dbContext.ClothingStores.Any())
                {
                    dbContext.AddRange(GetStores());
                    dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<ClothingStore> GetStores()
        {
            var clothingStores = new List<ClothingStore>()
            {
                new ClothingStore()
                {
                    Name = "Puma",
                    Description = "Clothing store",
                    ContactEmail = "pumaShop@gmail.com",
                    ContactNumber = "223445667",
                    Headquaters = "Germany",

                    Address = new Address()
                    {
                        Country = "Poland",
                        City = "Poznań",
                        Street = "Maciejewicza 4"
                    },

                    Owner = new Owner()
                    {
                        FullName = "Jakub Kamiński",
                        ContactEmail = "jakubKaminski@gmail.com",
                        ContactNumber = "333222111"
                    },

                    StoreReviews = new List<StoreReview>()
                    {
                        new StoreReview()
                        {
                            Rating = 4
                        },
                        new StoreReview()
                        {
                            Rating = 5
                        }
                    },

                    Products = new List<Product>()
                    {
                        new Product()
                        {
                            Name = "Puma Shoes",
                            Type = "Shoes",
                            Brand = "Puma",
                            Price = 299.99M,
                            Size = "45",
                            Gender = "Men",
                            Quantity = 10,

                            ProductReviews = new List<ProductReview>()
                            {
                                new ProductReview()
                                {
                                    Rating = 3
                                }
                            }
                        },
                        new Product()
                        {
                            Name = "Sport T-Shirt",
                            Type = "T-Shirt",
                            Brand = "Fila",
                            Price = 69.99M,
                            Size = "L",
                            Gender = "Men",
                            Quantity = 8,

                            ProductReviews = new List<ProductReview>()
                            {
                                new ProductReview()
                                {
                                    Rating = 4
                                }
                            }
                        }
                    }
                },

                new ClothingStore()
                {
                    Name = "H&M",
                    Description = "Clothing store",
                    ContactEmail = "H&MStore@gmail.com",
                    ContactNumber = "111222999",
                    Headquaters = "Sweden",

                    Address = new Address()
                    {
                        Country = "Poland",
                        City = "Gdańsk",
                        Street = "Nieskończona 8"
                    },

                    Owner = new Owner()
                    {
                        FullName = "Karol Fila",
                        ContactEmail = "karolFila@gmail.com",
                        ContactNumber = "445656781"
                    },

                    StoreReviews = new List<StoreReview>()
                    {
                        new StoreReview()
                        {
                            Rating = 4
                        },
                        new StoreReview()
                        {
                            Rating = 3
                        },
                        new StoreReview()
                        {
                            Rating = 2
                        }
                    },

                    Products = new List<Product>()
                    {
                        new Product()
                        {
                            Name = "Sweatpants",
                            Type = "Trousers",
                            Description = "Slim",
                            Brand = "H&M",
                            Price = 199.99M,
                            Size = "M",
                            Gender = "Women",
                            Quantity = 10,

                            ProductReviews = new List<ProductReview>()
                            {
                                new ProductReview()
                                {
                                    Rating = 5
                                }
                            }
                        }
                    }
                },
            };

            return clothingStores;
        }
    }
}
