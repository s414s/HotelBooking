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
    private readonly List<int> _tables;
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

        _tables = new() { 1, 2, 3, 4, 5 };
        _functions = new(){
            new () { Key = 1, Description = "Sign In", Function = AuthenticateUser },
            new () { Key = 2, Description = "Add new booking", Function = PrintAddNewBooking },
            new () { Key = 3, Description = "Delete a booking", Function = PrintDeleteBooking },
            new () { Key = 4, Description = "Print all bookings in a month", Function = PrintGetAllBookingsInMonth },
            new () { Key = 5, Description = "Sign Out", Function = Logout },
            new () { Key = 6, Description = "Exit", Function = Exit }
        };
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
        List<int> options = new() { 1, 8 };
        AskForOption(options);
    }

    private void PrintMainScreen(string role)
    {
        List<int> options = role == "admin"
            ? new() { 2, 3, 4, 5, 6, 7, 8 }
            : new() { 2, 3, 4, 5, 6, 7, 8 };
        AskForOption(options);
    }

    private void AskForOption(List<int> functions)
    {
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
        ExecuteFunction(chosenOption);
    }

    private void AuthenticateUser()
    {
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
                // break;
            }
            Console.WriteLine("Username or password incorrect, try again");
        }
    }

    private void ExecuteFunction(int key) => _functions.FirstOrDefault(x => x.Key == key)?.Execute();

    private void PrintAddNewBooking()
    {
        var allHotels = _hotelService.GetAll().ToArray();
        ItemsLogger<HotelDTO>.PrintItems(allHotels);
        var selectedHotelIndex = ValueSeeker.AskForInteger("Select the hotel for this booking:", allHotels.Select((x, i) => i + 1).ToList() ?? []);

        var startBookingDate = ValueSeeker.AskForDate("Select the start of the booking:");
        var endBookingDate = ValueSeeker.AskForDate("Select the end of the booking:");

        var availableRooms = _roomService.GetAvailableRoomsOnDate(startBookingDate, endBookingDate, allHotels[selectedHotelIndex-1].Id).ToArray();
        Console.WriteLine("These are the available rooms for those dates");
        ItemsLogger<RoomDTO>.PrintItems(availableRooms);

        var selectedRoomIndex = ValueSeeker.AskForInteger("Select a room for your booking:", availableRooms.Select((x, i) => i + 1).ToList() ?? []);

        var clientName = ValueSeeker.AskForString("Insert client name/s separated by a comma:");

        try
        {
            _bookingService.BookRoom(availableRooms[selectedRoomIndex-1].Id, clientName.Split(","), startBookingDate, endBookingDate);
            Console.WriteLine("Booking Created Successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Booking Could not be Created");
            Console.WriteLine(ex is ApplicationException ? ex : "System error");
        }
    }

    private void PrintDeleteBooking()
    {
        var allBookings = _bookingService.GetAll().ToArray();
        ItemsLogger<BookingDTO>.PrintItems(allBookings);
        var selectedBookingIndex = ValueSeeker.AskForInteger("Select the booking you want to delete:", allBookings.Select((x, i) => i + 1).ToList() ?? []);

        try
        {
            _bookingService.DeleteBooking(allBookings[selectedBookingIndex-1].Id);
            Console.WriteLine("Order Deleted Successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Order Could not be Deleted");
            Console.WriteLine(ex is ApplicationException ? ex : "System error");
        }
    }

    private void PrintGetAllBookingsInMonth()
    {
        var selectedDate = ValueSeeker.AskForDate("Select any day of the month you want to retrieve:");

        var bookings = _bookingService.GetBookingsOfMonth(selectedDate.Year, selectedDate.Month, null);
        Console.WriteLine("Here are the bookings");
        ItemsLogger<BookingDTO>.PrintItems(bookings);
    }

    private void Logout()
    {
        _activeUser = null;
        Console.WriteLine("Logged out");
    }

    private void Exit()
    {
        _exit = true;
        Console.WriteLine("Good Bye");
    }
}
