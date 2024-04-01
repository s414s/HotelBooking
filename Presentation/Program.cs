using Application.Contracts;
using Application.Implementations;
using DataAccess.Implementations;
using DataAccess.Implementations.Base;
using Domain;
using Domain.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Contracts;
using Presentation.Implementations;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Welcome to the Hotel app!");

var serviceCollection = new ServiceCollection();

// ==== Registering services ====
// Repositories
serviceCollection.AddSingleton<IRepository<User>, RepositoryUsersPersistent>();
//serviceCollection.AddSingleton<IRepository<Hotel>, RepositoryPersistent<Hotel>>();
serviceCollection.AddSingleton<IRepository<Hotel>, RepositoryHotelsPersistent>();

// Services
serviceCollection.AddTransient<IUserService, UserService>();
serviceCollection.AddTransient<IBookingService, BookingService>();
serviceCollection.AddTransient<IHotelService, HotelService>();
serviceCollection.AddTransient<IRoomService, RoomService>();

// Presentation
serviceCollection.AddTransient<IMenuPrinter, MenuPrinter>();

var serviceProvider = serviceCollection.BuildServiceProvider();

// ==== Seeding repos ====
var usersStorage = serviceProvider.GetService<IRepository<User>>();
var hotelsStorage = serviceProvider.GetService<IRepository<Hotel>>();

if(usersStorage?.GetAll().Count() == 0)
{
    usersStorage.Add(new User("alberto", "salas", "root"));
    usersStorage.Add(new User("ana", "sanz", "root"));
    usersStorage.SaveChanges();
}

if(hotelsStorage?.GetAll().Count() == 0)
{
    hotelsStorage.Add(new Hotel { Id = new Guid(), Name = "boston", Address = "boston address", Rooms =
        [
            new Room(1, RoomTypes.Single),
            new Room(1, RoomTypes.Double),
            new Room(1, RoomTypes.Single),
            new Room(1, RoomTypes.Double),
        ] }
    );

    hotelsStorage.Add(new Hotel { Id = new Guid(), Name = "melia", Address = "melia address", Rooms =
        [
            new Room(1, RoomTypes.Single),
            new Room(1, RoomTypes.Double),
            new Room(1, RoomTypes.Single),
            new Room(1, RoomTypes.Double),
        ] }
    );

    hotelsStorage.SaveChanges();
}

// Resolve an instance of IConsoleLogger
var menuPrinter = serviceProvider.GetService<IMenuPrinter>();

menuPrinter?.Run();
