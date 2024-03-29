using Application.Contracts;
using Application.Implementations;
using Domain;
using Domain.Contracts;
using Microsoft.Extensions.DependencyInjection;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var serviceCollection = new ServiceCollection();

// Registering services
// Repositories
serviceCollection.AddSingleton<IRepository<User>, RepositoryUsersPersistent>();
serviceCollection.AddSingleton<IRepository<Hotel>, RepositoryDishesPersistent>();

// Services
serviceCollection.AddTransient<IUserService, UserService>();
serviceCollection.AddTransient<IBookingService, BookingService>();
serviceCollection.AddTransient<IHotelService, HotelService>();

serviceCollection.AddTransient<IMenuPrinter, MenuPrinter>();

var serviceProvider = serviceCollection.BuildServiceProvider();

// Resolve an instance of IConsoleLogger
var menuPrinter = serviceProvider.GetService<IMenuPrinter>();
menuPrinter?.Run();

