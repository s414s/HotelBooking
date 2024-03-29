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

// Presentation
serviceCollection.AddTransient<IMenuPrinter, MenuPrinter>();

var serviceProvider = serviceCollection.BuildServiceProvider();

// Resolve an instance of IConsoleLogger
var menuPrinter = serviceProvider.GetService<IMenuPrinter>();

menuPrinter?.Run();
