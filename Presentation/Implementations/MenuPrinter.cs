using Application.Contracts;
using Application.DTOs;
using Presentation.Contracts;

namespace Presentation.Implementations;

public class MenuPrinter : IMenuPrinter
{
    private readonly IUserService _userServices;
    private readonly IHotelService _hotelService;
    private readonly IBookingService _bookingService;
    private readonly IRoomService _roomService;
    private readonly List<Functionality> _functions;
    private UserDTO? _activeUser;
    private bool _exit;

    public MenuPrinter(
        IUserService userServices,
        IRoomService roomService,
        IHotelService hotelService,
        IBookingService bookingService)
    {
        _userServices = userServices;
        _hotelService = hotelService;
        _bookingService = bookingService;
        _roomService = roomService;

        _functions = [
            new () { Key = 1, Description = "Sign In", Function = AuthenticateUser },
            new () { Key = 2, Description = "Add new booking", Function = PrintAddNewBooking },
            new () { Key = 3, Description = "Delete a booking", Function = PrintDeleteBooking },
            new () { Key = 4, Description = "Print all bookings by hotel", Function = PrintFilteredBookingsByHotels },
            new () { Key = 5, Description = "Sign Out", Function = Logout },
            new () { Key = 6, Description = "Exit", Function = Exit }
        ];
    }

    public void Run()
    {
        do
        {
            if (_activeUser is null)
            {
                PrintSignInScreen();
                continue;
            }
            PrintMainScreen(_activeUser.Role);
        }
        while (!_exit);
    }

    private void PrintSignInScreen()
    {
        List<int> options = _functions.Where(x => x.Key == 1 || x.Key == 6).Select(x => x.Key).ToList();
        AskForOption(options);
    }

    private void PrintMainScreen(string role)
    {
        List<int> options = _functions.Where(x => x.Key != 1).Select(x => x.Key).ToList();
        AskForOption(options);
    }

    private void AskForOption(List<int> functions)
    {
        Console.Clear();
        Console.WriteLine("OPTIONS");
        foreach (var key in functions)
        {
            var functionality = _functions.FirstOrDefault(x => x.Key == key);

            if (functionality is not null)
                Console.WriteLine($"{key}.- {functionality.Description}");
            else
                Console.WriteLine($"Key {key} not found");

        }
        int chosenOption = ValueSeeker.AskForInteger("Choose an option", functions);

        try
        {
            ExecuteFunction(chosenOption);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Order Could not be Executed");
            Console.WriteLine(ex is ApplicationException ? ex : "System error");
        }
    }

    private void AuthenticateUser()
    {
        Console.Clear();
        Console.WriteLine("Authenticate your user");
        while (true)
        {
            string inputUsername = ValueSeeker.AskForString("Introduce your username");
            string inputPassword = ValueSeeker.AskForString("Introduce your password");
            var activeUser = _userServices.SignIn(inputUsername, inputPassword);
            if (activeUser is not null)
            {
                _activeUser = activeUser;
                return;
            }
            Console.WriteLine("Username or password incorrect, try again");
        }
    }

    private void ExecuteFunction(int key) => _functions.FirstOrDefault(x => x.Key == key)?.Execute();

    private void PrintAddNewBooking()
    {
        Console.Clear();
        var allHotels = _hotelService.GetAll().ToArray();
        ItemsLogger<HotelDTO>.PrintItems(allHotels);
        var selectedHotelIndex = ValueSeeker.AskForInteger("Select the hotel for this booking:", allHotels.Select((x, i) => i + 1).ToList() ?? []);

        var startBookingDate = ValueSeeker.AskForDate("Select the start of the booking:");
        var endBookingDate = ValueSeeker.AskForDate("Select the end of the booking:");

        var availableRooms = _roomService.GetAvailableRoomsOnDate(startBookingDate, endBookingDate, allHotels[selectedHotelIndex-1].Id).ToArray();
        Console.WriteLine("These are the available rooms for those dates");
        ItemsLogger<RoomDTO>.PrintItems(availableRooms);

        var selectedRoomIndex = ValueSeeker.AskForInteger("Select a room for your booking:", availableRooms.Select((x, i) => i + 1).ToList() ?? []);

        var clientName = ValueSeeker.AskForString("Insert client/s name/s separated by a comma:");

        _bookingService.BookRoom(availableRooms[selectedRoomIndex-1].Id, clientName.Split(","), startBookingDate, endBookingDate);
        Console.WriteLine("Booking Created Successfully");
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    private void PrintDeleteBooking()
    {
        Console.Clear();
        var allBookings = _bookingService.GetAll().ToArray();
        ItemsLogger<BookingDTO>.PrintItems(allBookings);
        var selectedBookingIndex = ValueSeeker.AskForInteger("Select the booking you want to delete:", allBookings.Select((x, i) => i + 1).ToList() ?? []);

        _bookingService.DeleteBooking(allBookings[selectedBookingIndex-1].Id);
        Console.WriteLine("Order Deleted Successfully");
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    private void PrintFilteredBookingsByHotels()
    {
        Console.Clear();
        var hotelName = ValueSeeker.AskForString("Write the hotel room you want to filter your bookings by");
        var bookings = _bookingService.GetFilteredBookings(hotelName);
        if (bookings.Any())
        {
            Console.WriteLine($"Here are the bookings in {hotelName}:");
            ItemsLogger<BookingDTO>.PrintItems(bookings);
        }
        else
        {
            Console.WriteLine($"No bookings found in {hotelName} hotel");
        }
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    private void Logout()
    {
        Console.Clear();
        _activeUser = null;
        Console.WriteLine("Logged out");
    }

    private void Exit()
    {
        _exit = true;
        Console.WriteLine("Good Bye");
    }
}
